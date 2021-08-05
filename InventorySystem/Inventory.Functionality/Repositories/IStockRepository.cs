using Inventory.Data;
using Inventory.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.Repositories
{
    public interface IStockRepository : IRepository<Stock, int>
    {
    }
}
