using IronCore.BusinessLogic.BL;
using IronCore.Domain.Entities.Product;
using IronCore.Filters;
using IronCore.Models;
using System.Linq;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductBL _bl = new ProductBL();


        public ActionResult Index()
        {
            var vm = _bl.GetAll()                   
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


        [AdminMod]
        public ActionResult Create() => View();

        [HttpPost, AdminMod, ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.CreateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [AdminMod]
        public ActionResult Edit(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, AdminMod, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.UpdateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [AdminMod]
        public ActionResult Delete(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, ActionName("Delete"),
         AdminMod, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bl.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }


        private static ProductViewModel ToVm(ProductDbModel p) => p == null ? null :
            new ProductViewModel
            {
                ProductID = p.Id,
                ProductName = p.ProductName,   // alias к Title (см. partial ProductDbModel)
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            };

        private static ProductDbModel ToEntity(ProductViewModel vm) => new ProductDbModel
        {
            Id = vm.ProductID,
            ProductName = vm.ProductName,
            Description = vm.Description,
            ImageUrl = vm.ImageUrl,
            Price = vm.Price,
            Quantity = vm.Quantity
        };
    }
}
