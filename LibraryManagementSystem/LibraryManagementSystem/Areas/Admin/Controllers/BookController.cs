using LibraryManagementSystem.Areas.Admin.Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateBook()
        {
            var model = new CreateBookModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateBook(CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsBookCreated = false;
                try
                {
                    model.CreateBook();
                    IsBookCreated = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Book");
                    _logger.LogError(ex, "Create book Failed");
                }

                if (IsBookCreated)
                    return RedirectToAction(nameof(CreateBook));
            }
            return View(model);
        }

        public IActionResult GetAllBookData()
        {
            var model = new GetBookModel();
            return View(model);
        }

        public JsonResult GetBookData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetBookModel();
            var data = model.GetAllBooks(dataTablesModel);
            return Json(data);
        }

        public IActionResult EditBook(int id)
        {
            var model = new EditBookModel();
            model.LoadBookDataForEdit(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditBook(EditBookModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return RedirectToAction(nameof(GetAllBookData));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteBook(int id)
        {
            var model = new DeleteBookModel();

            model.DeleteBook(id);

            return RedirectToAction(nameof(GetAllBookData));
        }
    }
}
