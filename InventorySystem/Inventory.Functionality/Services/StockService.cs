using Inventory.Functionality.BusinessObjects;
using Inventory.Functionality.Entities;
using Inventory.Functionality.Exceptions;
using Inventory.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.Services
{
    public class StockService : IStockService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public StockService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateStock(StockBO stock)
        {
            if (stock == null)
                throw new InvalidParameterException("stock info was not provided");

            var stockEntity = new Stock
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity
            };

            _functionalityUnitOfWork.Stocks.Add(stockEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<StockBO> records, int total, int totalDisplay) GetAllStocks(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            int srctxt = string.IsNullOrWhiteSpace(searchText) ? 0 : Convert.ToInt32(searchText[0]);
            var TypeConvertedSearchText = (string.IsNullOrWhiteSpace(searchText) == false && srctxt >= 49 &&
                                           srctxt <= 57) ? int.Parse(searchText) : 0;

            var stockData = _functionalityUnitOfWork.Stocks.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.ProductId == TypeConvertedSearchText, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from stock in stockData.data
                              select new StockBO
                              {
                                  Id = stock.Id,
                                  ProductId = stock.ProductId,
                                  Quantity = stock.Quantity
                              }).ToList();

            return (resultData, stockData.total, stockData.totalDisplay);
        }

        public StockBO GetStockForEdit(int id)
        {
            var entityStock = _functionalityUnitOfWork.Stocks.GetById(id);
            var BusObjStock = new StockBO
            {
                Id = entityStock.Id,
                ProductId = entityStock.ProductId,
                Quantity = entityStock.Quantity
            };

            return BusObjStock;
        }

        public void UpdateStock(StockBO stockinfo)
        {
            if (stockinfo == null)
                throw new InvalidOperationException("Stock is missing");

            var entityStock = _functionalityUnitOfWork.Stocks.GetById(stockinfo.Id);

            if (entityStock != null)
            {
                entityStock.ProductId = stockinfo.ProductId;
                entityStock.Quantity = stockinfo.Quantity;

                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Stock");
        }

        public void DeleteStock(int id)
        {
            _functionalityUnitOfWork.Stocks.Remove(id);

            _functionalityUnitOfWork.Save();
        }
    }
}
