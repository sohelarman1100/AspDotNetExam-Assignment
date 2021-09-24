using Autofac;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class FileUploadModel
    {
        public int GroupId { get; set; }
        
        [Required(ErrorMessage = "please select a file")]
        public IFormFile UploadedFile { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IGroupService _groupService;
        public FileUploadModel()
        {
            _hostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public FileUploadModel(ILogger<FileUploadModel> logger, IWebHostEnvironment hostEnvironment , IGroupService groupService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
        }

        public void UploadFile(string userId)
        {
           
            string grpName = _groupService.GetGroupById(GroupId); 

            //saving image to wwwroot/tempfiles
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(UploadedFile.FileName);
            string extension = Path.GetExtension(UploadedFile.FileName);
            fileName = fileName + "_" + userId + "_" + grpName + "_" + GroupId + extension;
            //PhotoFileName = fileName;
            string path = Path.Combine(wwwRootPath + "/tempfiles/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                UploadedFile.CopyTo(fileStream);
            }
        }
    }
}
