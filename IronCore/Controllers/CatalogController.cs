using IronCore.BusinessLogic.BL;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronCore.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IProduct _product;
        private readonly IProgram _program;
        public CatalogController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBL();
            _program = bl.GetProgramBL();
        }

        public ActionResult Nutritions()
        {
            ViewBag.ActivePage = "Nutritions";

            var products = _product.GetAll();

            var model = products
                .Where(p => p.IsVisibleInCatalog)
                .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                IsVisibleInCatalog = p.IsVisibleInCatalog
            }).ToList();

            return View(model);
        }
        public ActionResult NutritionsDetail(int id)
        {
            var product = _product.GetProductById(id);
            if (product == null)
                return HttpNotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            };
            return View(model);
        }
        public ActionResult Programs()
        {
            ViewBag.ActivePage = "Programs";

            var programs = _program.GetAllPrograms();

            var model = programs.Select(p => new ProgramViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Duration = p.Duration,
                Trainer = p.Trainer,
                SuitableFor = p.SuitableFor
            }).ToList();

            return View(model);
        }

        public ActionResult ProgramDetail(int id)
        {
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

            return View(model);
        }
    }
}