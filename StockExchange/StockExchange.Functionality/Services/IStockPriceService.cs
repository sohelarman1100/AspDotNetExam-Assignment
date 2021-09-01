using StockExchange.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.Services
{
    public interface IStockPriceService
    {
        void CreateStockPrice(StockPricesBO stockPrice);
        int GetCompanyId(string tradeCode);
    }
}
