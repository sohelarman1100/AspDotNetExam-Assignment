using ECommerce.Functionality.BusinessObjects;
using ECommerce.Functionality.Entities;
using ECommerce.Functionality.Exceptions;
using ECommerce.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Functionality.Services
{
    public class ProductService : IProductService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public ProductService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateProduct(ProductBO product)
        {
            if (product == null)
                throw new InvalidParameterException("product info was not provided");

            var productEntity = new Product
            {
                Name = product.Name,
                Price = product.Price
            };

            _functionalityUnitOfWork.Products.Add(productEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<ProductBO> records, int total, int totalDisplay) GetAllProducts(int pageIndex,
            int pageSize, string searchText, string sortText)
        {
            var productData = _functionalityUnitOfWork.Products.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.Name.Contains(searchText), sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from product in productData.data
                              select new ProductBO
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  Price = product.Price
                              }).ToList();

            return (resultData, productData.total, productData.totalDisplay);
        }

        public ProductBO GetProductForEdit(int id)
        {
            var entityProduct = _functionalityUnitOfWork.Products.GetById(id);
            var BusObjProduct = new ProductBO
            {
                Id = entityProduct.Id,
                Name = entityProduct.Name,
                Price = entityProduct.Price
            };

            return BusObjProduct;
        }

        public void UpdateProduct(ProductBO productinfo)
        {
            if (productinfo == null)
                throw new InvalidOperationException("Product is missing");

            var entityProduct = _functionalityUnitOfWork.Products.GetById(productinfo.Id);

            if (entityProduct != null)
            {
                entityProduct.Name = productinfo.Name;
                entityProduct.Price = productinfo.Price;

                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Product");
        }

        public void DeleteProduct(int id)
        {
            _functionalityUnitOfWork.Products.Remove(id);

            _functionalityUnitOfWork.Save();
        }
    }
}
