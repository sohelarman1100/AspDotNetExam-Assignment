using AttendanceSystem.Areas.Admin.Models;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttendanceController : Controller
    {
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(ILogger<AttendanceController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateAttendance()
        {
            var model = new CreateAttendanceModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateAttendance(CreateAttendanceModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsCreateAttendence = false;
                try
                {
                    model.CreateAttendance();
                    IsCreateAttendence = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Attendance");
                    _logger.LogError(ex, "Create Attendance Failed");
                }
                if (IsCreateAttendence)
                    return RedirectToAction(nameof(CreateAttendance));
            }

            return View(model);
        }

        public IActionResult GetAttendanceDataView()
        {
            var model = new GetAttendanceModel();
            return View(model);
        }

        public JsonResult GetAttendanceData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetAttendanceModel();
            var data = model.GetAllAttendance(dataTablesModel);
            return Json(data);
        }

        public IActionResult AllAttendanceWithEditAndDeleteButton()
        {
            var model = new GetAttendanceModel();
            return View(model);
        }

        public IActionResult EditAttendance(int id)
        {
            var model = new EditAttendanceModel();
            model.LoadAttendanceDataForEdit(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditAttendance(EditAttendanceModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
                return RedirectToAction(nameof(AllAttendanceWithEditAndDeleteButton));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteAttendance(int id)
        {
            var model = new DeleteAttendanceModel();

            model.DeleteAttendance(id);

            return RedirectToAction(nameof(AllAttendanceWithEditAndDeleteButton));
        }
    }
}
