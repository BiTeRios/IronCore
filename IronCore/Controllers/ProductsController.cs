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


        [AdminOnly]
        public ActionResult Create() => View();

        [HttpPost, AdminOnly, ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.CreateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [AdminOnly]
        public ActionResult Edit(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, AdminOnly, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _bl.UpdateProduct(ToEntity(vm));
            return RedirectToAction(nameof(Index));
        }

        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var entity = _bl.GetProductById(id);
            if (entity == null) return HttpNotFound();

            return View(ToVm(entity));
        }

        [HttpPost, ActionName("Delete"),
         AdminOnly, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bl.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }


        private static ProductViewModel ToVm(ProductDbModel p) => p == null ? null :
            new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,   // alias к Title (см. partial ProductDbModel)
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            };

        private static ProductDbModel ToEntity(ProductViewModel vm) => new ProductDbModel
        {
            Id = vm.Id,
            Title = vm.Title,
            Description = vm.Description,
            ImageUrl = vm.ImageUrl,
            Price = vm.Price,
            Quantity = vm.Quantity
        };
    }
}
