using LibrarySystem.Data;
using LibrarySystem.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Repositories
{
    public interface IAuthorRepository : IRepository<Author, int>
    {
    }
}
