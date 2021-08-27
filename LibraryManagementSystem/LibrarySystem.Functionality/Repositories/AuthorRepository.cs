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
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(IFunctionalityContext context)
            : base((DbContext)context)
        {
        }
    }
}
