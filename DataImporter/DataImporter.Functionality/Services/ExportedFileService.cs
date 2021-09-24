using DataImporter.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public class ExportedFileService : IExportedFileService
    {
        private IFunctionalityUnitOfWork _membershipUnitOfWork;
        public ExportedFileService(IFunctionalityUnitOfWork membershipUnitOfWork)
        {
            _membershipUnitOfWork = membershipUnitOfWork;
        }
    }
}
