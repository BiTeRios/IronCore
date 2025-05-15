using IronCore.BusinessLogic.BL;
using IronCore.Domain.Entities.Product;
using IronCore.Models;
using System.Linq;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductBL _bl = new ProductBL();

        /* ---------- каталог для всех ---------- */

        public ActionResult Index()
        {
            var vm = _bl.GetAll()                   // ← было GetAllProducts()
                         .Select(ToVm)
                         .ToList();
            return View(vm);
        }

        public ActionResult Details(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        /* ---------- CRUD только для админов ---------- */

        [Authorize(Roles = "Admin")]
        public ActionResult Create() => View();

        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.CreateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.UpdateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, ActionName("Delete"),
         Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bl.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        /* ---------- helpers ---------- */

        private static ProductViewModel ToVm(ProductDbModel p) => p == null ? null :
            new ProductViewModel
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,   // alias к Title (см. partial ProductDbModel)
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            };

        private static ProductDbModel ToEntity(ProductViewModel vm) => new ProductDbModel
        {
            ProductID = vm.ProductID,
            ProductName = vm.ProductName,
            Description = vm.Description,
            ImageUrl = vm.ImageUrl,
            Price = vm.Price,
            Quantity = vm.Quantity
        };
    }
}
