using Autofac;
using Inventory.Functionality.BusinessObjects;
using Inventory.Functionality.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class CreateStockModel
    {
        [Required, Range(1, 500000)]
        public int ProductId { get; set; }

        [Required, Range(0, 500)]
        public int Quantity { get; set; }


        private IStockService _stockService;

        public CreateStockModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }
        public CreateStockModel(IStockService stockService)
        {
            _stockService = stockService;
        }
        internal void CreateStock()
        {
            var stock = new StockBO
            {
                ProductId = ProductId,
                Quantity = Quantity
            };

            _stockService.CreateStock(stock);
        }
    }
}
