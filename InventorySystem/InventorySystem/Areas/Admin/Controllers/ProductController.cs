using AttendanceSystem.Models;
using InventorySystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateProduct()
        {
            var model = new CreateProductModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateProduct(CreateProductModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsProductCreated = false;
                try
                {
                    model.CreateProduct();
                    IsProductCreated = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Product");
                    _logger.LogError(ex, "Create Product Failed");
                }
                if(IsProductCreated)
                    return RedirectToAction(nameof(CreateProduct));
            }
            return View(model);
        }

        public IActionResult GetProductDataView()
        {
            var model = new GetProductModel();
            return View(model);
        }

        public JsonResult GetProductData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetProductModel();
            var data = model.GetAllProducts(dataTablesModel);
            return Json(data);
        }

        public IActionResult AllProductsWithEditAndDeleteButton()
        {
            var model = new GetProductModel();
            return View(model);
        }

        public IActionResult EditProduct(int id)
        {
            var model = new EditProductModel();
            model.LoadProductDataForEdit(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditProduct(EditProductModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
                return RedirectToAction(nameof(AllProductsWithEditAndDeleteButton));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            var model = new DeleteProductModel();

            model.DeleteProduct(id);

            return RedirectToAction(nameof(AllProductsWithEditAndDeleteButton));
        }
    }
}
