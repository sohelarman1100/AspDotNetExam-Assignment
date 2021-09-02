using Microsoft.EntityFrameworkCore;
using StockExchange.Data;
using StockExchange.Functionality.Contexts;
using StockExchange.Functionality.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork, IFunctionalityUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrices { get; private set; }
        public FunctionalityUnitOfWork(IFunctionalityContext context,
            ICompanyRepository companies, IStockPriceRepository stockPrices) : base((DbContext)context)
        {
            Companies = companies;
            StockPrices = stockPrices;
        }
    }
}
