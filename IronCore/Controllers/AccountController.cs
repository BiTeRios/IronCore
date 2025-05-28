using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Filters;
using IronCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IronCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _user;
        private readonly IOrder _order;
        private readonly ICart _cart;
        public AccountController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
            _order = bl.GetOrderBL();
            _cart = bl.GetCartBL();
        }
        [Authorize]
        public ActionResult User()
        {
            ViewBag.ActivePage = "User";
            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var user = _user.GetById(userId);
            if (user == null)
                return RedirectToAction("Login", "Login");

          
            var orders = _order.GetAllOrders();
            int ordersCount = orders.Count(o => o.UserId == userId); 

            var model = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RegistrationDate = user.RegistrationDate,
                OrdersCount = ordersCount
            };

            return View(model);
        }
        [Authorize]
        public ActionResult EditProfile()
        {
            ViewBag.ActivePage = "User";
            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var user = _user.GetById(userId);
            if (user == null)
                return RedirectToAction("Login", "Login");

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditProfile(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0 || model.Id != userId)
                return RedirectToAction("Login", "Login");

            var user = _user.GetById(userId);
            if (user == null)
                return RedirectToAction("Login", "Login");
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            _user.Update(user);

            TempData["ProfileUpdated"] = "Профиль успешно обновлён!";
            return RedirectToAction("User");
        }
        [Authorize]
        public ActionResult Orders()
        {
            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var orders = _order.GetAllOrders()
                .Where(o => o.UserId == userId) 
                .OrderByDescending(o => o.Created)
                .ToList();

            var model = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                Created = o.Created,
                State = o.State,
                Total = o.Total,
                Products = o.Products?.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity
                }).ToList()
            }).ToList();

            return View(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ChangePass()
        {
            if (TempData["PassResult"] != null)
                ViewBag.PassResult = TempData["PassResult"];
            return View(new ChangePasswordViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePass(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0)
                return RedirectToAction("Login", "Login");

            string message;
            bool result = _user.ChangePassword(userId, model.OldPassword, model.NewPassword, out message);
            
            if (!result)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }
            TempData["PassResult"] = message;
            return RedirectToAction("ChangePass");
        }
        [Authorize]
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            SetCartInfo();
            int userId = (int)(Session["UserId"] ?? 0);
            CartDTO cartDTO = _cart.GetCurrentCart(); 

            var model = new CartViewModel
            {
                Id = cartDTO?.Id ?? 0,
                Price = cartDTO?.Price ?? 0,
                Discount = cartDTO?.Discount ?? 0,
                Products = cartDTO?.ProductsInCart?.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageUrl = p.ImageUrl
                }).ToList() ?? new List<ProductViewModel>()
            };

            return View(model);
        }

        [Authorize]
        public ActionResult RemoveFromCart(int id)
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("ShoppingCart");
        }

        [Authorize]
        public ActionResult ClearCart()
        {
            _cart.ClearCart();
            return RedirectToAction("ShoppingCart");
        }

        [Authorize]
        public ActionResult IncrementQuantity(int id)
        {
            _cart.IncrementQuantity(id);
            return RedirectToAction("ShoppingCart");
        }

        [Authorize]
        public ActionResult DecrementQuantity(int id)
        {
            _cart.DecrementQuantity(id);
            return RedirectToAction("ShoppingCart");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        private void SetCartInfo()
        {
            var cart = _cart.GetCurrentCart();
            ViewBag.CartCount = cart?.ProductsInCart.Count ?? 0;
            ViewBag.CartTotal = cart?.Price ?? 0m;
        }
    }
}