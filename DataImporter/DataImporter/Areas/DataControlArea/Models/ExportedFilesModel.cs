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
        private IExportedFileService _exportedFileService;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _exportedFileService = _scope.Resolve<IExportedFileService>();
        }

        public ExportedFilesModel()
        {

        }

        public ExportedFilesModel(IExportedFileService exportedFileService)
        {
            _exportedFileService = exportedFileService;
        }
    }
}
