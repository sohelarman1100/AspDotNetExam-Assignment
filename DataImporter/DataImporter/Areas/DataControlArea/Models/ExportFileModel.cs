using Autofac;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExportFileModel
    {
        private readonly IAllDataService _allDataService;

        public ExportFileModel()
        {
            _allDataService = Startup.AutofacContainer.Resolve<IAllDataService>();
        }
        public ExportFileModel(IAllDataService allDataService)
        {
            _allDataService = allDataService;
        }
        internal void ExportFile(int id)
        {
            List<AllDataBO> allRecords = _allDataService.ExportFile(id);
            for(int i=0; i<allRecords.Count; i++)
            {

            }
        }
    }
}
