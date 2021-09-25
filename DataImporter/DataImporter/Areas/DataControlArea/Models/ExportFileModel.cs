using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExportFileModel
    {
        private IAllDataService _allDataService;
        private IWebHostEnvironment _hostEnvironment;
        private EmailSettings _emailSettings;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _allDataService = _scope.Resolve<IAllDataService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
        }

        public ExportFileModel()
        {

        }
        public ExportFileModel(IAllDataService allDataService, IWebHostEnvironment hostEnvironment)
        {
            _allDataService = allDataService;
            _hostEnvironment = hostEnvironment;
        }
        internal void ExportFile(int id)
        {
            List<AllDataBO> allRecords = _allDataService.ExportFile(id);

            string wwwRootPath = _hostEnvironment.WebRootPath;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                int cnt = 1;
                for (int i = 0; i < allRecords.Count; i++)
                {
                    string data = allRecords[0].KeyForColumnName;
                    var columnName = data.Split('>');

                    if (i == 0)
                    {
                        for (int j = 0; j < columnName.Length; j++)
                            worksheet.Cells[1, j + 1].Value = columnName[j];
                    }

                    data = allRecords[i].ValueForColumnValue;
                    var columnValue = data.Split('>');
                    cnt++;
                    for (int j = 0; j < columnValue.Length; j++)
                    {
                        //cnt++;
                        worksheet.Cells[cnt, j + 1].Value = columnValue[j];
                    }
                }

                string fileName = id.ToString() + ".xlsx";
                string filePath = Path.Combine(wwwRootPath, "exportedFiles", fileName);
                FileInfo fileSaveAs = new FileInfo(filePath);
                package.SaveAs(fileSaveAs);
            }
        }
    }
}
