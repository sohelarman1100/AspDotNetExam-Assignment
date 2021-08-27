using LibrarySystem.Data;
using LibrarySystem.Functionality.Contexts;
using LibrarySystem.Functionality.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork , IFunctionalityUnitOfWork
    {
        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public FunctionalityUnitOfWork(IFunctionalityContext context,
            IBookRepository books, IAuthorRepository authors) : base((DbContext)context)
        {
            Books = books;
            Authors = authors;
        }
    }
}
