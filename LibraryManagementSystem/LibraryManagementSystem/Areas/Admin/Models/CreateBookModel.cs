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
    public class CreateBookModel
    {
        [Required, MaxLength(200, ErrorMessage = "Title should be less than 200 charcaters")]
        public string Title { get; set; }

        [Required]
        public string BarCode { get; set; }

        [Required]
        public double Price { get; set; }

        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public CreateBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        internal void CreateBook()
        {
            var book = _mapper.Map<BookBO>(this);
            
            _bookService.CreateBook(book);
        }
    }
}
