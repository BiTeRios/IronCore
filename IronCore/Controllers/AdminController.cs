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

namespace IronCore.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly IUser _user;
        private readonly IOrder _order;
        private readonly IProduct _product;
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
            _order = bl.GetOrderBL();
            _product = bl.GetProductBL();
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

        [AdminOnly]
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            return View();                   
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
                Id = o.Id,
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
                Id = o.Id,
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
                Id = o.Id,
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
            _order.DeleteOrder(id);
            return RedirectToAction("Orders");
        }

        // Список товаров
        [AdminOnly]
        public ActionResult Products()
        {
            ViewBag.ActivePage = "Products";
            var products = _product.GetAll()
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
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Quantity = model.Quantity
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
    }
}