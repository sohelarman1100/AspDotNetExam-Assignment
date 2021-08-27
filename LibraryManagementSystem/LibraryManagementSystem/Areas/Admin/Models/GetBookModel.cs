using Autofac;
using LibraryManagementSystem.Models;
using LibrarySystem.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Areas.Admin.Models
{
    public class GetBookModel
    {
        private readonly IBookService _bookService;

        public GetBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
        }
        public GetBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        internal object GetAllBooks(DataTablesAjaxRequestModel tableModel)
        {
            var data = _bookService.GetAllBooks(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Title", "BarCode", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.Title,
                             record.Barcode,
                             record.Price.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
