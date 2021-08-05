using Inventory.Data;
using Inventory.Functionality.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IStockRepository Stocks { get; }
    }
}
