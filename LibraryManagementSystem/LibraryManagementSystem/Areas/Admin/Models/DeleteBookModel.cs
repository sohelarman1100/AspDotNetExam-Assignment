using Autofac;
using LibrarySystem.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Areas.Admin.Models
{
    public class DeleteBookModel
    {
        private readonly IBookService _bookService;

        public DeleteBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
        }

        public DeleteBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        internal void DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
        }
    }
}
