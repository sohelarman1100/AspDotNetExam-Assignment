using Inventory.Data;
using Inventory.Functionality.Contexts;
using Inventory.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.Repositories
{
    public class StockRepository : Repository<Stock, int>, IStockRepository
    {
        public StockRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
