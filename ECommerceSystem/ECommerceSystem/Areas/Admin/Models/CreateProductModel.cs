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
    public class CreateProductModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }

        [Required, Range(500, 50000)]
        public Double Price { get; set; }


        private IProductService _productService;

        public CreateProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }
        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }
        internal void CreateProduct()
        {
            var product = new ProductBO
            {
                Name = Name,
                Price = Price
            };

            _productService.CreateProduct(product);
        }
    }
}
