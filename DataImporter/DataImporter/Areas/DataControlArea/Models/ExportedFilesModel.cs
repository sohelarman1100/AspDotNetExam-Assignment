using Autofac;
using DataImporter.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExportedFilesModel
    {
        private readonly IExportedFileService _exportedFileService;

        public ExportedFilesModel()
        {
            _exportedFileService = Startup.AutofacContainer.Resolve<IExportedFileService>();
        }
        public ExportedFilesModel(IExportedFileService exportedFileService)
        {
            _exportedFileService = exportedFileService;
        }
    }
}
