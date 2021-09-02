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
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
