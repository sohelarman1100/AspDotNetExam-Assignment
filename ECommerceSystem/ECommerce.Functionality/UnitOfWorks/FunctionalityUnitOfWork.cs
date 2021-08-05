using ECommerce.Data;
using ECommerce.Functionality.Context;
using ECommerce.Functionality.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork, IFunctionalityUnitOfWork
    {
        public IProductRepository Products { get; private set; }
        public FunctionalityUnitOfWork(IFunctionalityContext context, 
            IProductRepository products) : base((DbContext)context)
        {
            Products = products;
        }
    }
}
