using IronCore.BusinessLogic.BL;
using IronCore.Domain.Entities.Product;
using System.Linq;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CartController : Controller
    {
        private CartBL Cart
        {
            get
            {
                if (Session["cart"] is CartBL c) return c;
                c = new CartBL();
                Session["cart"] = c;
                return c;
            }
        }

        private readonly ProductBL _products = new ProductBL();

        public ActionResult Index() => View(Cart);


        [HttpPost]
        public ActionResult Add(int id, int qty = 1)
        {
            ProductDbModel p = _products.GetProductById(id);
            if (p == null) return HttpNotFound();

            Cart.AddToCart(p);                 
            return RedirectToAction("Index");
        }

        [HttpPost] public ActionResult Plus(int id) { Cart.IncrementQuantity(id); return RedirectToAction("Index"); }
        [HttpPost] public ActionResult Minus(int id) { Cart.DecrementQuantity(id); return RedirectToAction("Index"); }
        [HttpPost] public ActionResult Remove(int id) { Cart.RemoveFromCart(id); return RedirectToAction("Index"); }
        [HttpPost] public ActionResult Clear() { Cart.ClearCart(); return RedirectToAction("Index"); }
        private readonly OrderBL _orders = new OrderBL();


        [HttpPost]
        public ActionResult Checkout()
        {
            var orderId = _orders.CreateOrder(Cart.GetCurrentCart());
            Cart.ClearCart();                                

            TempData["OrderSuccess"] = $"Заказ №{orderId} оформлен!";
            return RedirectToAction("Index", "Cart");
        }

    }
}
