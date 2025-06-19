using System.Web.Mvc;
using IronCore.Filters;
using IronCore.Models;
using IronCore.BusinessLogic;
using IronCore.BusinessLogic.BL;
using System.Linq;
using System.IO;
using System.Web;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Product;
using IronCore.Domain.Enums.Order;
using System;
using IronCore.Domain.Entities.Coach;
using IronCore.Domain.Entities.Program;

namespace IronCore.Controllers
{
    [AdminOnly]
    public class AdminController : BaseController
    {
        private readonly IUser _user;
        private readonly IOrder _order;
        private readonly IProduct _product;
        private readonly IContact _contact;
        private readonly ICoach _coach;
        private readonly IProgram _program;
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
            _order = bl.GetOrderBL();
            _product = bl.GetProductBL();
            _contact = bl.GetContactBL();
            _coach = bl.GetCoachBL();
            _program = bl.GetProgramBL();
        }

        [AdminOnly]
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";

            var userCount = _user.GetAllUsers().Count;
            var productCount = _product.GetAll().Count();
            var orderCount = _order.GetAllOrders().Count;
            var contactCount = _contact.GetAllContacts().Count;

            var recentOrders = _order.GetAllOrders()
                .OrderByDescending(o => o.Created)
                .Take(5)
                .ToList();
            var userDict = _user.GetAllUsers().ToDictionary(u => u.Id, u => $"{u.FirstName} {u.LastName}");

            var recentOrderVMs = recentOrders.Select(order =>
            {
                var buyerName = "Гость";
                if (userDict.ContainsKey(order.UserId))
                {
                    buyerName = userDict[order.UserId];
                }

                return new RecentOrderViewModel
                {
                    Id = order.Id,
                    Buyer = buyerName,
                    Created = order.Created,
                    Total = order.Total,
                    State = order.State
                };
            }).ToList();

            ViewBag.UserCount = userCount;
            ViewBag.ProductCount = productCount;
            ViewBag.OrderCount = orderCount;
            ViewBag.contactCount = contactCount;
            ViewBag.RecentOrders = recentOrderVMs;

            return View();
        }

        [AdminOnly]
        public ActionResult Contacts()
        {
            ViewBag.ActivePage = "Contacts";
            var contacts = _contact.GetAllContacts()
                .Select(u => new ContactViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Message = u.Message
                }).ToList();

            return View(contacts);
        }

        [AdminOnly]
        public ActionResult DeleteContact(int id)
        {
            ViewBag.ActivePage = "Contacts";
            var contact = _contact.GetContactById(id);
            if (contact == null) return HttpNotFound();

            var model = new ContactViewModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Message = contact.Message
            };
            return View(model);
        }

        [HttpPost, ActionName("DeleteContact"), ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteContactConfirmed(int id)
        {
            _contact.DeleteContact(id);
            return RedirectToAction("Contacts");
        }

        [AdminOnly]
        public ActionResult Users()
        {
            ViewBag.ActivePage = "Users";
            var users = _user.GetAllUsers()
                .Select(u => new EditUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber
                }).ToList();

            return View(users);
        }

        // GET: /Admin/EditUser/5
        [AdminOnly]
        public ActionResult EditUser(int id)
        {
            ViewBag.ActivePage = "Users";
            var user = _user.GetById(id);
            if (user == null)
                return HttpNotFound();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        // POST: /Admin/EditUser/5
        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult EditUser(EditUserViewModel model)
        {
            ViewBag.ActivePage = "Users";
            if (!ModelState.IsValid)
                return View(model);

            var userDTO = _user.GetById(model.Id);
            if (userDTO == null) return HttpNotFound();

            userDTO.UserName = model.UserName;
            userDTO.Email = model.Email;
            userDTO.FirstName = model.FirstName;
            userDTO.LastName = model.LastName;
            userDTO.PhoneNumber = model.PhoneNumber;
            _user.Update(userDTO);

            return RedirectToAction("Users");
        }

        // GET: /Admin/DeleteUser/5
        [AdminOnly]
        public ActionResult DeleteUser(int id)
        {
            ViewBag.ActivePage = "Users";
            var user = _user.GetById(id);
            if (user == null) return HttpNotFound();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        // POST: /Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser"), ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteUserConfirmed(int id)
        {
            _user.Delete(id);
            return RedirectToAction("Users");
        }

        // Список заказов
        [AdminOnly]
        public ActionResult Orders()
        {
            ViewBag.ActivePage = "Orders";
            var orders = _order.GetAllOrders()
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    Created = o.Created,
                    State = o.State,
                    Total = o.Total,
                    Products = o.Products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Price = p.Price,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Quantity = p.Quantity
                    }).ToList()
                }).ToList();

            return View(orders);
        }

        // Детали заказа
        [AdminOnly]
        public ActionResult OrderDetails(int id)
        {
            ViewBag.ActivePage = "Orders";
            var o = _order.GetOrderById(id);
            if (o == null) return HttpNotFound();
            var order = new OrderViewModel
            {
                UserId = o.UserId,
                Created = o.Created,
                State = o.State,
                Total = o.Total,
                Products = o.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity
                }).ToList()
            };
            return View(order);
        }

        // Изменение статуса заказа
        [AdminOnly]
        public ActionResult ChangeOrderState(int id)
        {
            ViewBag.ActivePage = "Orders";
            var o = _order.GetOrderById(id);
            if (o == null) return HttpNotFound();
            var model = new OrderViewModel
            {
                UserId = o.UserId ,
                Created = o.Created,
                State = o.State,
                Total = o.Total,
                Products = o.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity
                }).ToList()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult ChangeOrderState(int id, UState newState)
        {
            _order.ChangeOrderState(id, newState);
            return RedirectToAction("Orders");
        }

        [AdminOnly]
        public ActionResult DeleteOrder(int id)
        {
            ViewBag.ActivePage = "Orders";
            var o = _order.GetOrderById(id);
            if (o == null) return HttpNotFound();
            var order = new OrderViewModel
            {
                UserId = o.UserId,
                Created = o.Created,
                State = o.State,
                Total = o.Total,
                Products = o.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity
                }).ToList()
            };
            return View(order);
        }

        [HttpPost, ActionName("DeleteOrder"), ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteOrderConfirmed(int id)
        {
            var order = _order.GetOrderById(id);
            if (order == null) return HttpNotFound();

            // Удаление связанных продуктов
            foreach (var product in order.Products.ToList())
            {
                _product.DeleteProduct(product.Id);
            }

            // Удаление самого заказа
            _order.DeleteOrder(id);

            return RedirectToAction("Orders");
        }

        // Список товаров
        [AdminOnly]
        public ActionResult Products()
        {
            ViewBag.ActivePage = "Products";
            var products = _product.GetAll()
                .Where(p => p.IsVisibleInCatalog)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity
                }).ToList();

            return View(products);
        }

        // Детали товара
        [AdminOnly]
        public ActionResult ProductDetails(int id)
        {
            ViewBag.ActivePage = "Products";
            var p = _product.GetProductById(id);
            if (p == null) return HttpNotFound();

            var model = new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity
            };
            return View(model);
        }

        // Добавление товара - GET
        [AdminOnly]
        [HttpGet]
        public ActionResult CreateProduct()
        {
            ViewBag.ActivePage = "Products";
            return View(new ProductViewModel());
        }

        // Добавление товара - POST
        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult CreateProduct(ProductViewModel model, HttpPostedFileBase photo)
        {
            ViewBag.ActivePage = "Products";
            if (!ModelState.IsValid)
                return View(model);

            if (photo != null && photo.ContentLength > 0)
            {
                var folder = "~/Images/";
                var serverPath = Server.MapPath(folder);
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(serverPath);

                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(serverPath, fileName);
                photo.SaveAs(path);
                model.ImageUrl = VirtualPathUtility.Combine(folder, fileName);
            }

            var dto = new ProductDTO
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Quantity = model.Quantity,
                IsVisibleInCatalog = true
            };
            _product.CreateProduct(dto);
            return RedirectToAction("Products");
        }

        // Редактирование товара - GET
        [AdminOnly]
        public ActionResult EditProduct(int id)
        {
            ViewBag.ActivePage = "Products";
            var p = _product.GetProductById(id);
            if (p == null) return HttpNotFound();

            var model = new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity
            };
            return View(model);
        }

        // Редактирование товара - POST
        [HttpPost, ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult EditProduct(ProductViewModel model, HttpPostedFileBase photo)
        {
            ViewBag.ActivePage = "Products";
            if (!ModelState.IsValid)
                return View(model);

            if (photo != null && photo.ContentLength > 0)
            {
                var folder = "~/Images/Products";
                var serverPath = Server.MapPath(folder);
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(serverPath);

                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(serverPath, fileName);
                photo.SaveAs(path);
                model.ImageUrl = VirtualPathUtility.Combine(folder, fileName);
            }

            var dto = new ProductDTO
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Quantity = model.Quantity
            };
            _product.UpdateProduct(dto);
            return RedirectToAction("Products");
        }

        // Удаление товара - GET
        [AdminOnly]
        public ActionResult DeleteProduct(int id)
        {
            ViewBag.ActivePage = "Products";
            var p = _product.GetProductById(id);
            if (p == null) return HttpNotFound();

            var model = new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity
            };
            return View(model);
        }

        // Удаление товара - POST
        [HttpPost, ActionName("DeleteProduct"), ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteProductConfirmed(int id)
        {
            _product.DeleteProduct(id);
            return RedirectToAction("Products");
        }

        [AdminOnly]
        public ActionResult Coaches()
        {
            ViewBag.ActivePage = "Coaches";
            var coaches = _coach.GetAllCoaches();
            var viewModels = coaches.Select(MapToViewModel).ToList();
            return View(viewModels);
        }

        // GET
        [AdminOnly]
        public ActionResult CreateCoach()
        {
            return View("EditCreateCoach", new CoachViewModel());
        }

        // POST
        [AdminOnly]
        [HttpPost]
        public ActionResult CreateCoach(CoachViewModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
                return View("EditCreateCoach", model);
            if (photo != null && photo.ContentLength > 0)
            {
                var folder = "~/Images/";
                var serverPath = Server.MapPath(folder);
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(serverPath);

                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(serverPath, fileName);
                photo.SaveAs(path);
                model.ImagePath = VirtualPathUtility.Combine(folder, fileName);
            }

            _coach.CreateCoach(MapToDTO(model));
            return RedirectToAction("Index");
        }

        // GET
        [AdminOnly]
        public ActionResult EditCoach(int id)
        {
            var coach = _coach.GetCoachById(id);
            if (coach == null) return HttpNotFound();
            return View("EditCreateCoach", MapToViewModel(coach));
        }

        // POST
        [AdminOnly]
        [HttpPost]
        public ActionResult EditCoach(CoachViewModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
                return View("EditCreateCoach", model);

            if (photo != null && photo.ContentLength > 0)
            {
                var folder = "~/Images/";
                var serverPath = Server.MapPath(folder);
                if (!Directory.Exists(serverPath))
                    Directory.CreateDirectory(serverPath);

                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(serverPath, fileName);
                photo.SaveAs(path);
                model.ImagePath = VirtualPathUtility.Combine(folder, fileName);
            }

            _coach.UpdateCoach(MapToDTO(model));
            return RedirectToAction("Index");
        }

        // GET: Coach/Delete/5
        [AdminOnly]
        public ActionResult DeleteCoach(int id)
        {
            var coach = _coach.GetCoachById(id);
            if (coach == null) return HttpNotFound();
            return View(MapToViewModel(coach));
        }

        // POST: Coach/Delete/5
        [HttpPost, ActionName("Delete")]
        [AdminOnly]
        public ActionResult DeleteCoachConfirmed(int id)
        {
            _coach.DeleteCoach(id);
            return RedirectToAction("Index");
        }

        [AdminOnly]
        public ActionResult ProgramsList()
        {
            ViewBag.ActivePage = "Programs";

            var programs = _program.GetAllPrograms()
                .Select(p => new ProgramViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Duration = p.Duration,
                    Trainer = p.Trainer,
                    SuitableFor = p.SuitableFor
                }).ToList();

            return View(programs);
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult CreateProgram()
        {
            ViewBag.ActivePage = "Programs";
            return View("EditCreateProgram", new ProgramViewModel());
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult EditProgram(int id)
        {
            ViewBag.ActivePage = "Programs";
            var program = _program.GetProgramById(id);
            if (program == null) return HttpNotFound();

            var model = new ProgramViewModel
            {
                Id = program.Id,
                Title = program.Title,
                Description = program.Description,
                Duration = program.Duration,
                Trainer = program.Trainer,
                SuitableFor = program.SuitableFor
            };

            return View("EditCreateProgram", model);
        }

        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCreateProgram(ProgramViewModel model)
        {
            ViewBag.ActivePage = "Programs";
            if (!ModelState.IsValid)
                return View(model);

            var dto = new ProgramDTO
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Trainer = model.Trainer,
                SuitableFor = model.SuitableFor
            };

            if (model.Id == 0)
            {
                _program.CreateProgram(dto);
            }
            else
            {
                _program.UpdateProgram(dto);
            }

            return RedirectToAction("ProgramsList");
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult DeleteProgram(int id)
        {
            ViewBag.ActivePage = "Programs";

            var program = _program.GetProgramById(id);
            if (program == null) return HttpNotFound();

            var model = new ProgramViewModel
            {
                Id = program.Id,
                Title = program.Title,
                Description = program.Description
            };

            return View(model);
        }

        [HttpPost, ActionName("DeleteProgram")]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteProgramConfirmed(int id)
        {
            _program.DeleteProgram(id);
            return RedirectToAction("ProgramsList");
        }

        private CoachViewModel MapToViewModel(CoachDTO dto)
        {
            return new CoachViewModel
            {
                Id = dto.Id,
                ImagePath = dto.ImagePath,
                FullName = dto.FullName,
                ExperienceTime = dto.ExperienceTime,
                Qualification = dto.Qualification,
                Specialization = dto.Specialization,
                Bio = dto.Bio,
                Testimonials = dto.Testimonials,
                TelegramUrl = dto.TelegramUrl,
                SteamUrl = dto.SteamUrl,
                InstagramUrl = dto.InstagramUrl
            };
        }

        private CoachDTO MapToDTO(CoachViewModel model)
        {
            return new CoachDTO
            {
                Id = model.Id,
                ImagePath = model.ImagePath,
                FullName = model.FullName,
                ExperienceTime = model.ExperienceTime,
                Qualification = model.Qualification,
                Specialization = model.Specialization,
                Bio = model.Bio,
                Testimonials = model.Testimonials,
                TelegramUrl = model.TelegramUrl,
                SteamUrl = model.SteamUrl,
                InstagramUrl = model.InstagramUrl
            };
        }
    }
}