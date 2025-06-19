using IronCore.BusinessLogic.BL;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Cart;
using IronCore.Domain.Entities.Order;
using IronCore.Domain.Entities.Product;
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
    public class AccountController : BaseController
    {
        private readonly IUser _user;
        private readonly IOrder _order;
        private readonly ICart _cart;
        private readonly IProduct _product;
        public AccountController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
            _order = bl.GetOrderBL();
            _cart = bl.GetCartBL();
            _product = bl.GetProductBL();
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
                UserId = o.UserId,
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
                Products = cartDTO?.Products?.Select(p => new ProductViewModel
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
        public ActionResult AddToCart(int id)
        {
            var product = _product.GetProductById(id); 
            if (product == null)
            {
                return HttpNotFound();
            }

            _cart.AddToCart(product);
            return RedirectToAction("ShoppingCart");
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder()
        {
            int userId = (int)(Session["UserId"] ?? 0);
            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var user = _user.GetById(userId);
            if (user == null)
                return RedirectToAction("Login", "Login");
            System.Diagnostics.Debug.WriteLine($"[PlaceOrder] UserId in session: {userId}");
            var cart = _cart.GetCurrentCart();
            if (cart == null || cart.Products == null || !cart.Products.Any())
            {
                TempData["OrderError"] = "Корзина пуста.";
                return RedirectToAction("ShoppingCart");
            }

            var newOrder = new OrderViewModel
            {
                Id = cart.Id,
                UserId = user.Id,
                Created = DateTime.Now,
                State = IronCore.Domain.Enums.Order.UState.Waiting,
                Total = cart.Price,
                Products = cart.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageUrl = p.ImageUrl,
                    IsVisibleInCatalog = false
                }).ToList()
            };

            var orderDto = new OrderDTO
            {
                Id = newOrder.Id,
                UserId = newOrder.UserId,
                Created = newOrder.Created,
                State = newOrder.State,
                Total = newOrder.Total,
                Products = newOrder.Products.Select(p => new ProductDbModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageUrl = p.ImageUrl,
                    IsVisibleInCatalog = p.IsVisibleInCatalog
                }).ToList()
            };

            bool success = _order.CreateOrder(orderDto);

            if (success)
            {
                _cart.ClearCart();
                TempData["OrderSuccess"] = "Заказ успешно оформлен!";
                return RedirectToAction("Orders");
            }
            else
            {
                TempData["OrderError"] = "Не удалось оформить заказ. Попробуйте позже.";
                return RedirectToAction("ShoppingCart");
            }
        }


        private void SetCartInfo()
        {
            var cart = _cart.GetCurrentCart();
            ViewBag.CartCount = cart?.Products.Count ?? 0;
            ViewBag.CartTotal = cart?.Price ?? 0m;
        }
    }
}