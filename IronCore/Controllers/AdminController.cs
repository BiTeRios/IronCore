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
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
            _order = bl.GetOrderBL();
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

        public ActionResult Products()
        {
            ViewBag.ActivePage = "Products";
            return View();                   
        }
    }
}