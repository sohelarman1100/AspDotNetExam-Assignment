using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Methods
{
    public class CopyExcelDataToList : ICopyExcelDataToList
    {
        public List<List<string>> CopyFileDataToList(FileInfo[] existingFile)
        {
            List<List<string>> ExcelData = new List<List<string>>();

            foreach (FileInfo file in existingFile)
            {
                string s = file.Directory + "\\" + file.Name;
                FileInfo exFile = new FileInfo(s);

                using (ExcelPackage package = new ExcelPackage(exFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    for (int row = 1; row <= Math.Min(rowCount, 10); row++)
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
            return ExcelData;
        }


        public FileInfo[] GetFiles(string filePath)
        {
            string FilePath = filePath;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  //EPPlus library(nuget package) ti use korle ei license ti
                                                                         //add kora lage.
            DirectoryInfo d = new DirectoryInfo(FilePath);
            FileInfo[] existingFile = d.GetFiles("*.xlsx");
            return existingFile;
        }
    }
}
