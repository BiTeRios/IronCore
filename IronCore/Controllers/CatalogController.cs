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
    public class CatalogController : Controller
    {
        private readonly IProduct _product;
        public CatalogController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBL();
        }

        public ActionResult Nutritions()
        {
            ViewBag.ActivePage = "Nutritions";

            var products = _product.GetAll();

            var model = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity
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
            var products = _product.GetAll()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList();
            return View(products);
        }

        public ActionResult ProgramDetail(int id)
        {
            var product = _product.GetProductById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }
    }
}