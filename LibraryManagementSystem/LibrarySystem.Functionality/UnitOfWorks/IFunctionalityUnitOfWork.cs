using LibrarySystem.Data;
using LibrarySystem.Functionality.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
    }
}
