using Inventory.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality.Services
{
    public interface IStockService
    {
        void CreateStock(StockBO stock);
        (IList<StockBO> records, int total, int totalDisplay) GetAllStocks(int pageIndex,
            int pageSize, string searchText, string sortText);
        StockBO GetStockForEdit(int id);
        void UpdateStock(StockBO stockinfo);
        void DeleteStock(int id);
    }
}
