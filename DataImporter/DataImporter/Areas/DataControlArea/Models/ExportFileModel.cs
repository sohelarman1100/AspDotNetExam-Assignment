using Autofac;
using DataImporter.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExportFileModel
    {
        private readonly IExportedFileService _exportedFileService;

        public ExportFileModel()
        {
            _exportedFileService = Startup.AutofacContainer.Resolve<IExportedFileService>();
        }
        public ExportFileModel(IExportedFileService exportedFileService)
        {
            _exportedFileService = exportedFileService;
        }
        internal void ExportFile(int id)
        {
            throw new NotImplementedException();
        }
    }
}
