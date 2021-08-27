using Autofac;
using AutoMapper;
using LibrarySystem.Functionality.BusinessObjects;
using LibrarySystem.Functionality.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Areas.Admin.Models
{
    public class EditBookModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, MaxLength(200, ErrorMessage = "title should provide")]
        public string Title { get; set; }

        [Required]
        public string BarCode { get; set; }

        [Required, Range(100, 500000, ErrorMessage = "book price is required")]
        public double? Price { get; set; }

        private readonly IBookService _bookService;
        public readonly IMapper _mapper;

        public EditBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper= Startup.AutofacContainer.Resolve<IMapper>();
        }

        public EditBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        internal void LoadBookDataForEdit(int id)
        {
            var book = _bookService.GetBook(id);
            _mapper.Map(book, this);
        }

        internal void Update()
        {
            var book = _mapper.Map<BookBO>(this);
            _bookService.UpdateBook(book);
        }
    }
}
