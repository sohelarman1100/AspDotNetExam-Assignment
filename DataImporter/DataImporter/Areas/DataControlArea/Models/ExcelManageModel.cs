using Autofac;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        public List<List<string>> ExcelData = new List<List<string>>();
        public ExcelManageModel()
        {
            _hostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
        }
        public ExcelManageModel(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        //following method is for reading data from excel file
        public void ShowFileData()   
        {
            FileInfo[] existingFile = GetFiles();
            foreach(FileInfo file in existingFile)
            {
                string s = file.Directory + "\\" + file.Name;
                FileInfo exFile = new FileInfo(s);

                using (ExcelPackage package = new ExcelPackage(exFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    for (int row = 1; row <= Math.Min(rowCount , 10); row++)
                    {
                        List<string> lst = new List<string>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            lst.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                        }
                        ExcelData.Add(lst);
                    }
                }
            }
        }

        internal void FileSaveToConfirmFolderAndDeleteFromTemporary()
        {
            FileInfo[] existingFile = GetFiles();
            string wwwRootPath = _hostEnvironment.WebRootPath;

            foreach (FileInfo file in existingFile)
            {
                //start Moving a file from one path to another path.....same system e copy o kora jay
                string srcFile = file.Directory + "\\" + file.Name;
                string destFile = Path.Combine(wwwRootPath + "/confirmfiles/", file.Name);
                File.Move(srcFile, destFile);
                //end Moving a file from one path to another path
            }
        }

        internal void DeleteExcel()
        {
            FileInfo[] existingFile = GetFiles();

            foreach (FileInfo file in existingFile)
            {
                string srcFile = file.Directory + "\\" + file.Name;
                if (System.IO.File.Exists(srcFile))
                    System.IO.File.Delete(srcFile);
            }
        }

        public FileInfo[] GetFiles()
        {
            string FilePath = _hostEnvironment.WebRootPath + "/tempfiles/";
           
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  //EPPlus library(nuget package) ti use korle ei license ti
                                                                         //add kora lage.
            DirectoryInfo d = new DirectoryInfo(FilePath);
            FileInfo[] existingFile = d.GetFiles("*.xlsx");
            return existingFile;
        }
    }
}
