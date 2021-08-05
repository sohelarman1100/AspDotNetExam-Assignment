using ECommerce.Data;
using ECommerce.Functionality.Context;
using ECommerce.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Functionality.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
