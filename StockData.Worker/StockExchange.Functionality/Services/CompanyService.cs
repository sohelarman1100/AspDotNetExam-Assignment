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
    public class CompanyService : ICompanyService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public CompanyService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateCompany(CompanyBO company)
        {
            var companyEntity = new Company
            {
                TradeCode = company.TradeCode
            };

            _functionalityUnitOfWork.Companies.Add(companyEntity);

            _functionalityUnitOfWork.Save();
        }

        public int NumOfCompany()
        {
            return _functionalityUnitOfWork.Companies.GetAll().Count;
        }
    }
}
