using StockExchange.Data;
using StockExchange.Functionality.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public ICompanyRepository Companies { get; }
        public IStockPriceRepository StockPrices { get; }
    }
}
