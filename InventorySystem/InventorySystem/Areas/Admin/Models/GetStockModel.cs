using AttendanceSystem.Models;
using Autofac;
using Inventory.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class GetStockModel
    {
        private readonly IStockService _stockService;

        public GetStockModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }
        public GetStockModel(IStockService stockService)
        {
            _stockService = stockService;
        }
        internal object GetAllStocks(DataTablesAjaxRequestModel tableModel)
        {
            var data = _stockService.GetAllStocks(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "ProductId", "Quantity" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.ProductId.ToString(),
                             record.Quantity.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
