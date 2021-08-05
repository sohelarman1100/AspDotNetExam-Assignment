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
    public class EditStockModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, Range(1, 500000)]
        public int? ProductId { get; set; }

        [Required, Range(0, 500)]
        public int? Quantity { get; set; }

        private readonly IStockService _stockService;

        public EditStockModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }

        public EditStockModel(IStockService stockService)
        {
            _stockService = stockService;
        }

        internal void LoadStockDataForEdit(int id)
        {
            var data = _stockService.GetStockForEdit(id);
            Id = data?.Id;
            ProductId = data?.ProductId;
            Quantity = data?.Quantity;
        }

        internal void Update()
        {
            var stockinfo = new StockBO
            {
                Id = Id.HasValue ? Id.Value : 0,
                ProductId = ProductId.HasValue ? ProductId.Value : 0,
                Quantity = Quantity.HasValue ? Quantity.Value : 0
            };
            _stockService.UpdateStock(stockinfo);
        }
    }
}
