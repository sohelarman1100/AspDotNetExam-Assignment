using Autofac;
using DataImporter.Common.DateTimeUtilities;
using DataImporter.Common.Methods;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExcelManageModel
    {
        public int GroupId { get; set; }
        public int count = 0;
        private IWebHostEnvironment _hostEnvironment;
        private IImportedFileService _importedFileService;
        private IDateTimeUtility _dateTimeUtility;
        private ILifetimeScope _scope;
        private ICopyExcelDataToList _copyExcelDataToList;

        public List<List<string>> ExcelData = new List<List<string>>();
        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _importedFileService = _scope.Resolve<IImportedFileService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _copyExcelDataToList = _scope.Resolve<ICopyExcelDataToList>();
        }

        public ExcelManageModel()
        {

        }

        public ExcelManageModel(IImportedFileService importedFileService, IWebHostEnvironment hostEnvironment,
            IDateTimeUtility dateTimeUtility, ICopyExcelDataToList copyExcelDataToList)
        {
            _hostEnvironment = hostEnvironment;
            _importedFileService = importedFileService;
            _dateTimeUtility = dateTimeUtility;
            _copyExcelDataToList = copyExcelDataToList;
        }

        //following method is for reading data from excel file
        public void ShowFileData(string filePath)   
        {
            FileInfo[] existingFile = _copyExcelDataToList.GetFiles(filePath);

            ExcelData = _copyExcelDataToList.CopyFileDataToList(existingFile);
           
        }

        internal void FileSaveToConfirmFolderAndDeleteFromTemporary(string tempFilePath, string confirmFilePath)
        {
            FileInfo[] existingFile = _copyExcelDataToList.GetFiles(tempFilePath);
            //string wwwRootPath = _hostEnvironment.WebRootPath;

            foreach (FileInfo file in existingFile)
            {
                string name = file.Name;
                var data = name.Split('_');
                var fileName = data[0];
                var userId = Guid.Parse(data[1]);
                var GrpName = data[2];
                var GrpId = data[3].Split('.');
                var groupId = int.Parse(GrpId[0]);
                //var user = Guid.Parse(userId);
                var importFileBO = _importedFileService.isFileExistOrNot(userId, groupId, fileName);

                if(importFileBO != null && importFileBO.Status == "Successfully Uploaded")
                {
                    count = 1;

                    var tmpfile = Path.Combine(tempFilePath, name);
                    if (System.IO.File.Exists(tmpfile))
                        System.IO.File.Delete(tmpfile);
                }
                else
                {
                    count = 0;

                    if(importFileBO != null && importFileBO.Status == "Pending...")
                    {
                        var confirmfile = Path.Combine(confirmFilePath, name);
                        if (System.IO.File.Exists(confirmfile))
                            System.IO.File.Delete(confirmfile);

                        _importedFileService.DeleteImportedFile(importFileBO.Id);
                    }
                    //importing data to ImportedFiles table start
                    var fileBO = new ImportedFileBO
                    {
                        FileName = fileName,
                        UserId = userId,
                        GroupId = int.Parse(GrpId[0]),
                        GroupName = GrpName,
                        Status = "Pending...",
                        ImportDate = _dateTimeUtility.Now
                    };
                    _importedFileService.CreateImportedFile(fileBO);
                    //importing data to ImportedFiles table end

                    //start Moving a file from one path to another path.....same system e copy o kora jay
                    string srcFile = file.Directory + "\\" + file.Name;
                    string destFile = Path.Combine(confirmFilePath, file.Name);
                    File.Move(srcFile, destFile);
                    //end Moving a file from one path to another path
                }


            }
        }

        internal void DeleteExcel(string filePath)
        {
            FileInfo[] existingFile = _copyExcelDataToList.GetFiles(filePath);

            foreach (FileInfo file in existingFile)
            {
                string srcFile = file.Directory + "\\" + file.Name;
                if (System.IO.File.Exists(srcFile))
                    System.IO.File.Delete(srcFile);
            }
        }

        //public FileInfo[] GetFiles()
        //{
        //    string FilePath = _hostEnvironment.WebRootPath + "/tempfiles/";
           
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  //EPPlus library(nuget package) ti use korle ei license ti
        //                                                                 //add kora lage.
        //    DirectoryInfo d = new DirectoryInfo(FilePath);
        //    FileInfo[] existingFile = d.GetFiles("*.xlsx");
        //    return existingFile;
        //}
    }
}
