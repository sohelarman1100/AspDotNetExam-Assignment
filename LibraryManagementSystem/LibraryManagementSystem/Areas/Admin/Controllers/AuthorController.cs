using LibraryManagementSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateAuthor()
        {
            var model = new CreateAuthorModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateAuthor(CreateAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsAuthorCreated = false;
                try
                {
                    model.CreateAuthor();
                    IsAuthorCreated = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Author");
                    _logger.LogError(ex, "Create Author Failed");
                }

                if (IsAuthorCreated)
                    return RedirectToAction(nameof(CreateAuthor));
            }
            return View(model);
        }
    }
}
