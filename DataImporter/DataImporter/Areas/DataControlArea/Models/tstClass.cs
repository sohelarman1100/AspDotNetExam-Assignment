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
    public class tstClass
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public List<List<string>> ExcelData = new List<List<string>>();    // list[i][j]
        string FilePath = "/tempfiles/";
        public void ShowFileData()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DirectoryInfo d = new DirectoryInfo(FilePath);
            //FileInfo existingFile = new FileInfo(FilePath);
            FileInfo[] existingFile = d.GetFiles("*.xlsx");
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
                    for (int row = 1; row <= rowCount; row++)
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

    }
}
