using Autofac;
using Inventory.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class DeleteProductModel
    {
        private readonly IProductService _productService;

        public DeleteProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }
        internal void DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
