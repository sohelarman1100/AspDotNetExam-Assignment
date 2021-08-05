using Autofac;
using Inventory.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class DeleteStockModel
    {
        private readonly IStockService _stockService;

        public DeleteStockModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }

        public DeleteStockModel(IStockService stockService)
        {
            _stockService = stockService;
        }
        internal void DeleteStock(int id)
        {
            _stockService.DeleteStock(id);
        }
    }
}
