using StockExchange.Data;
using StockExchange.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Functionality.Repositories
{
    public interface ICompanyRepository : IRepository<Company, int>
    {
    }
}
