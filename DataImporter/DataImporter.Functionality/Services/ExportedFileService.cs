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
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public ExportedFileService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public int SearchFile(int id)
        {
            int cnt = _functionalityUnitOfWork.ExFiles.GetCount(x=> x.importedFileId == id);
            return cnt;
        }

        public void StoreExportedFileInfo(object exportFileBO)
        {
            throw new NotImplementedException();
        }
    }
}
