using LibrarySystem.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Services
{
    public interface IAuthorService
    {
        void CreateAuthor(AuthorBO author);
    }
}
