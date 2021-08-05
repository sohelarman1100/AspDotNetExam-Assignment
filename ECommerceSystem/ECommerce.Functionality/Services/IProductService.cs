using ECommerce.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Functionality.Services
{
    public interface IProductService
    {
        void CreateProduct(ProductBO product);
        (IList<ProductBO> records, int total, int totalDisplay) GetAllProducts(int pageIndex,
            int pageSize, string searchText, string sortText);
        ProductBO GetProductForEdit(int id);
        void UpdateProduct(ProductBO productinfo);
        void DeleteProduct(int id);
    }
}
