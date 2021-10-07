using Autofac;
using DataImporter.Functionality.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class GetContactsModel
    {
        public Guid UserId { get; set; }
        private IAllDataService _allDataService;
        private ILifetimeScope _scope;
        public List<List<string>> ExcelData = new List<List<string>>();
        public List<string> colName = new List<string>();
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        [Required]
        public string GroupName { get; set; }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _allDataService = _scope.Resolve<IAllDataService>();
        }

        public GetContactsModel()
        {

        }
        public GetContactsModel(IAllDataService allDataService)
        {
            _allDataService = allDataService;
        }

        internal void GetAllContacts(string userId)
        {
            var AllDataList = _allDataService.GetAllData(Guid.Parse(userId), GroupName, dateFrom, dateTo);
            
            for(int i=0; i<AllDataList.Count; i++)
            {
                if(i==0)
                {
                    var data = AllDataList[i].KeyForColumnName;
                    var header = data.Split('>');
                    for (int j = 0; j < header.Length; j++)
                        colName.Add(header[j]);
                }

                var value = AllDataList[i].ValueForColumnValue;
                var colValue = value.Split('>');
                List<string> colVAlRecver = new List<string>();

                for (int j = 0; j < colValue.Length; j++)
                    colVAlRecver.Add(colValue[j]);
                
                ExcelData.Add(colVAlRecver);
            }
        }
        
    }
}
