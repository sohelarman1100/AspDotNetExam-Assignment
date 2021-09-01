using StockExchange.Functionality.BusinessObjects;
using StockExchange.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Models
{
    public class CreateStockPriceModel : ICreateStockPriceModel
    {
        public string TradeCode { get; set; }
        public int CompanyId { get; set; }
        public string LastTradingPrice { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string ClosePrice { get; set; }
        public string YesterdayClosePrice { get; set; }
        public string Change { get; set; }
        public string Trade { get; set; }
        public string Value { get; set; }
        public string Volume { get; set; }

        private IStockPriceService _stockPriceService;
        public CreateStockPriceModel(IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
        }
        public void CreateStockPrice()
        {
            int Id = _stockPriceService.GetCompanyId(TradeCode);
            var stock = new StockPricesBO
            {
                CompanyId = Id,
                LastTradingPrice = LastTradingPrice,
                High = High,
                Low = Low,
                ClosePrice = ClosePrice,
                YesterdayClosePrice = YesterdayClosePrice,
                Change = Change,
                Trade = Trade,
                Value = Value,
                Volume = Volume
            };

            //Console.WriteLine("Hello from createStockPriceModel");
            _stockPriceService.CreateStockPrice(stock);
        }
    }
}
