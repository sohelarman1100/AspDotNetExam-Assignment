using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Models
{
    public interface ICreateCompanyModel
    {
        public string TradeCode { get; set; }
        public void CreateCompany();
        public int NumOfCompany();
    }
}
