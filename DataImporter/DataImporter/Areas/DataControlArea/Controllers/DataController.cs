using Autofac;
using DataImporter.Areas.DataControlArea.Models;
using DataImporter.Membership.Entities;
using DataImporter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Controllers
{
    [Area("DataControlArea"), Authorize(Policy = "AccessPermission")]
    public class DataController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DataController> _logger;
        private readonly ILifetimeScope _scope;

        public DataController(ILifetimeScope scope, ILogger<DataController> logger, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult CreateGroups()
        {
            var model = _scope.Resolve<CreateGroupModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateGroups(CreateGroupModel model)
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            model.ResolveDependency(_scope);
            model.UserId =Guid.Parse(ViewBag.userid);
            if (ModelState.IsValid)
            {
                bool IsCreateGroup = false;
                try
                {
                    
                    model.CreateGroup();
                    IsCreateGroup = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create group");
                    _logger.LogError(ex, "Create Group Failed");
                }
                if (IsCreateGroup)
                    return RedirectToAction(nameof(CreateGroups));
            }

            return View(model);
        }
        public IActionResult Groups()  //group name,edit,dlt,import file
        {
            var model = _scope.Resolve<GetGroupModel>();

            return View();
        }
        public JsonResult GetGroupData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<GetGroupModel>();

            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            model.UserId = Guid.Parse(ViewBag.userid);

            var data = model.GetAllGroups(dataTablesModel);
            return Json(data);
        }
        public IActionResult EditGroup(int id)
        {
            var model = _scope.Resolve<EditGroupModel>();
            model.EditGroup(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditGroup(EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                model.UpdateGroup();
            }

            return RedirectToAction(nameof(Groups));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteGroup(int id)
        {
            var model = _scope.Resolve<EditGroupModel>();

            model.DeleteGroup(id);

            return RedirectToAction(nameof(Groups));
        }

        public IActionResult GetGroupsFileData()
        {
            //var model = new GetGroupInfoModel();
            ////var model = _scope.Resolve<EditGroupModel>();
            //var lst = model.GetGroups().ToList();
            //SelectList Lst = new SelectList(lst, "Id", "GroupName");
            //ViewBag.Mylst = lst;
            return View();
        }

        public IActionResult UploadContacts(int id)
        {
            //var model = new FileUploadModel();
            var model = _scope.Resolve<FileUploadModel>();
            model.GroupId = id;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UploadContacts(FileUploadModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsFileUploaded = false;
                try
                {
                    model.ResolveDependency(_scope);
                    ViewBag.userId = _userManager.GetUserId(HttpContext.User);
                    model.UploadFile(ViewBag.userId);
                    IsFileUploaded = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Member");
                    _logger.LogError(ex, "Create Member Failed");
                }
                if (IsFileUploaded)
                    return RedirectToAction(nameof(ExcelUploadConfirmation));
            }

            return View(model);
        }

        public IActionResult ExcelUploadConfirmation()
        {
            //var model = new ExcelManageModel();
            var model = _scope.Resolve<ExcelManageModel>();
            model.ShowFileData();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ExcelUploadConfirmation(ExcelManageModel model)
        {
            //ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            model.ResolveDependency(_scope);
            model.FileSaveToConfirmFolderAndDeleteFromTemporary();
            if (model.count == 1)
                return RedirectToAction(nameof(FileExistingErrorMessage));
            else
                return RedirectToAction(nameof(GetImportedFiles));
        }

        public IActionResult FileExistingErrorMessage()
        {
            return View();
        }

        public IActionResult ExcelDelete()
        {
            //var model = new ExcelManageModel();
            var model = _scope.Resolve<ExcelManageModel>();
            model.DeleteExcel();
            return RedirectToAction(nameof(Groups));
        }
        public IActionResult GetImportedFiles()    //imports
        {
            //var model = new GetImportedFilesModel();
            var model = _scope.Resolve<GetImportedFilesModel>();
            return View(model);
        }

        public JsonResult GetFileData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            //var model = new GetImportedFilesModel();
            var model = _scope.Resolve<GetImportedFilesModel>();
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            model.UserId = Guid.Parse(ViewBag.userid);

            var data = model.GetAllFiles(dataTablesModel);
            return Json(data);
        }

        public IActionResult ExportFile(int id)
        {
            //var model = new ExportFileModel();
            var model = _scope.Resolve<ExportFileModel>();
            model.ExportFile(id);
            return RedirectToAction(nameof(FileExportsuccessMessage));
        }

        public IActionResult FileExportsuccessMessage()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteImportedFile(int id)
        {
            //var model = new GetImportedFilesModel();
            var model = _scope.Resolve<GetImportedFilesModel>();

            model.DeleteImportedFile(id);

            return RedirectToAction(nameof(GetImportedFiles));
        }

        public IActionResult GetExportedFiles()    //exports
        {
            return View();
        }
    }
}
