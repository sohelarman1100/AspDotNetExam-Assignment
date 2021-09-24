using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Entities;
using DataImporter.Functionality.Exceptions;
using DataImporter.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public class ImportedFileService : IImportedFileService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public ImportedFileService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public void CreateImportedFile(ImportedFileBO importFileBO)
        {
            if (importFileBO == null)
                throw new InvalidParameterException("file info was not provided");

            var iportedFileEntity = _mapper.Map<ImportedFiles>(importFileBO);
            _functionalityUnitOfWork.ImFiles.Add(iportedFileEntity);

            _functionalityUnitOfWork.Save();
        }

        public int GetFile(ImportedFileBO importFileBO)
        {
            var fileId = _functionalityUnitOfWork.ImFiles.Get(x => x.FileName == importFileBO.FileName &&
                          x.UserId==importFileBO.UserId && x.GroupId==importFileBO.GroupId);

            return fileId[0].Id;
        }

        public void UpdateStatus(int fileId)
        {
            var fileEntity = _functionalityUnitOfWork.ImFiles.GetById(fileId);
            if (fileEntity != null)
            { 
                fileEntity.Status = "Successfully Uploaded";
                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find file");
        }

        public (IList<ImportedFileBO> records, int total, int totalDisplay) GetAllFiles(int pageIndex,
            int pageSize, string searchText, string sortText, Guid userId)
        {
            var fileData = _functionalityUnitOfWork.ImFiles.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? x => x.UserId == userId : x => x.GroupName.Contains(searchText)
                && x.UserId == userId, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from file in fileData.data
                              select _mapper.Map<ImportedFileBO>(file)).ToList();

            return (resultData, fileData.total, fileData.totalDisplay);
        }

        public void DeleteImportedFile(int id)
        {
            _functionalityUnitOfWork.ImFiles.Remove(id);

            _functionalityUnitOfWork.Save();
        }
    }
}
