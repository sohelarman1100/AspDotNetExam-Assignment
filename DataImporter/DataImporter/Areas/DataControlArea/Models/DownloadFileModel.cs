using Autofac;
using DataImporter.Common.Methods;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class DownloadFileModel
    {
        private IWebHostEnvironment _hostEnvironment;
        private IFileDownload _fileDownload;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _fileDownload = _scope.Resolve<IFileDownload>();
        }

        public DownloadFileModel()
        {
            
        }

        public DownloadFileModel(IWebHostEnvironment hostEnvironment, IFileDownload fileDownload)
        {
            _hostEnvironment = hostEnvironment;
            _fileDownload = fileDownload;
        }

        internal MemoryStream DownloadFile(int id)
        {
            var filePath = _hostEnvironment.WebRootPath + "/exportedFiles/" + id + ".xlsx";
            var memory = _fileDownload.DownloadFile(filePath);
            return memory;
        }
    }
}
