using Microsoft.EntityFrameworkCore;
using StockExchange.Data;
using StockExchange.Functionality.Contexts;
using StockExchange.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, int>, IStockPriceRepository
    {
        public StockPriceRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
