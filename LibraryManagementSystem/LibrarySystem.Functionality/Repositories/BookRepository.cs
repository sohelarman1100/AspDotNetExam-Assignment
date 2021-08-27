using LibrarySystem.Data;
using LibrarySystem.Functionality.Contexts;
using LibrarySystem.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Repositories
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        public BookRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
