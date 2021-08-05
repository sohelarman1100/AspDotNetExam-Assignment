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
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateStock()
        {
            var model = new CreateStockModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateStock(CreateStockModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsStockCreated = false;
                try
                {
                    model.CreateStock();
                    IsStockCreated = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create stock");
                    _logger.LogError(ex, "Create Stock Failed");
                }

                if(IsStockCreated)
                    return RedirectToAction(nameof(CreateStock));
            }

            return View(model);
        }

        public IActionResult GetStockDataView()
        {
            var model = new GetStockModel();
            return View(model);
        }

        public JsonResult GetStockData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetStockModel();
            var data = model.GetAllStocks(dataTablesModel);
            return Json(data);
        }

        public IActionResult AllStocksWithEditAndDeleteButton()
        {
            var model = new GetStockModel();
            return View(model);
        }

        public IActionResult EditStock(int id)
        {
            var model = new EditStockModel();
            model.LoadStockDataForEdit(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditStock(EditStockModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
                return RedirectToAction(nameof(AllStocksWithEditAndDeleteButton));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteStock(int id)
        {
            var model = new DeleteStockModel();

            model.DeleteStock(id);

            return RedirectToAction(nameof(AllStocksWithEditAndDeleteButton));
        }
    }
}
