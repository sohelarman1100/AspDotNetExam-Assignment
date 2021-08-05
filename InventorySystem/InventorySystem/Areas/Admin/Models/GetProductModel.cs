using AttendanceSystem.Models;
using Autofac;
using Inventory.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class GetProductModel
    {
        private readonly IProductService _productService;

        public GetProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }
        public GetProductModel(IProductService productService)
        {
            _productService = productService;
        }
        internal object GetAllProducts(DataTablesAjaxRequestModel tableModel)
        {
            var data = _productService.GetAllProducts(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.Name,
                             record.Price.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
