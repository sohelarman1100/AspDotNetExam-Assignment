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
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }
        public IActionResult CreateStudent()
        {
            var model = new CreateStudentModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateStudent(CreateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsCreateStudent = false;
                try
                {
                    model.CreateStudent();
                    IsCreateStudent = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Student");
                    _logger.LogError(ex, "Create Student Failed");
                }
                if (IsCreateStudent)
                    return RedirectToAction(nameof(CreateStudent));
            }
            return View(model);
        }

        public IActionResult GetStudentDataView()
        {
            var model = new GetStudentModel();
            return View(model);
        }

        public JsonResult GetStudentData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new GetStudentModel();
            var data = model.GetAllStudents(dataTablesModel);
            return Json(data);
        }

        public IActionResult AllStudentsWithEditAndDeleteButton()
        {
            var model = new GetStudentModel();
            return View(model);
        }

        public IActionResult EditStudent(int id)
        {
            var model = new EditStudentModel();
            model.LoadStudentDataForEdit(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditStudent(EditStudentModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
                return RedirectToAction(nameof(AllStudentsWithEditAndDeleteButton));
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteStudent(int id)
        {
            var model = new DeleteStudentModel();

            model.DeleteStudent(id);

            return RedirectToAction(nameof(AllStudentsWithEditAndDeleteButton));
        }

    }
}
