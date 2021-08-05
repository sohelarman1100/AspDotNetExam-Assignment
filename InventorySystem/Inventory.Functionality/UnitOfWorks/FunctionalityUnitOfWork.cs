using Inventory.Data;
using Inventory.Functionality.Contexts;
using Inventory.Functionality.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork, IFunctionalityUnitOfWork
    {
        public IProductRepository Products { get; private set; }
        public IStockRepository Stocks { get; private set; }
        public FunctionalityUnitOfWork(IFunctionalityContext context, IProductRepository products,
            IStockRepository stocks) : base((DbContext)context)
        {
            Products = products;
            Stocks = stocks;
        }
    }
}
