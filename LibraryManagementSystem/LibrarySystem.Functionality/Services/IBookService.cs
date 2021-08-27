using LibrarySystem.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Services
{
    public interface IBookService
    {
        void CreateBook(BookBO book);
        (IList<BookBO> records, int total, int totalDisplay) GetAllBooks(int pageIndex,
            int pageSize, string searchText, string sortText);
        BookBO GetBook(int id);
        void UpdateBook(BookBO book);
        void DeleteBook(int id);
    }
}
