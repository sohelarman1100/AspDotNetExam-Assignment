using StockExchange.Functionality.BusinessObjects;
using StockExchange.Functionality.Entities;
using StockExchange.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.Services
{
    public class StockPriceService : IStockPriceService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public StockPriceService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateStockPrice(StockPricesBO stockPrice)
        {
            var stockPriceEntity = new StockPrice
            {
                CompanyId = stockPrice.CompanyId,
                LastTradingPrice = stockPrice.LastTradingPrice,
                High = stockPrice.High,
                Low = stockPrice.Low,
                ClosePrice = stockPrice.ClosePrice,
                YesterdayClosePrice = stockPrice.YesterdayClosePrice,
                Change = stockPrice.Change,
                Trade = stockPrice.Trade,
                Value = stockPrice.Value,
                Volume = stockPrice.Volume
            };

            //Console.WriteLine("Hello from StockService");
            _functionalityUnitOfWork.StockPrices.Add(stockPriceEntity);

            _functionalityUnitOfWork.Save();
        }

        public int GetCompanyId(string tradeCode)
        {
            IList<Company> company = _functionalityUnitOfWork.Companies.Get(x => x.TradeCode == tradeCode);
            return company[0].Id;
        }
    }
}
