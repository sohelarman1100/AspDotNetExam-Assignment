using Autofac;
using ECommerce.Functionality.BusinessObjects;
using ECommerce.Functionality.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class EditProductModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, MaxLength(200, ErrorMessage = "name should provide")]
        public string Name { get; set; }

        [Required, Range(1, 500000, ErrorMessage = "product price is required")]
        public double? Price { get; set; }

        private readonly IProductService _productService;

        public EditProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }

        internal void LoadProductDataForEdit(int id)
        {
            var data = _productService.GetProductForEdit(id);
            Id = data?.Id;
            Name = data?.Name;
            Price = data?.Price;
        }

        internal void Update()
        {
            var productinfo = new ProductBO
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                Price = Price.HasValue ? Price.Value : 0
            };
            _productService.UpdateProduct(productinfo);
        }
    }
}
