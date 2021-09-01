using StockExchange.Functionality.BusinessObjects;
using StockExchange.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Models
{
    public class CreateCompanyModel : ICreateCompanyModel
    {
        public string TradeCode { get; set; }
        private ICompanyService _companyService;
        public CreateCompanyModel(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public void CreateCompany()
        {
            var company = new CompanyBO
            {
                TradeCode = TradeCode
            };

            _companyService.CreateCompany(company);
        }
    }
}
