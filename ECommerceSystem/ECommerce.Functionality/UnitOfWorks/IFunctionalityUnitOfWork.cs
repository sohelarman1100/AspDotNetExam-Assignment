using ECommerce.Data;
using ECommerce.Functionality.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
    }
}
