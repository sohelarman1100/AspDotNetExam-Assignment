using AutoMapper;
using LibrarySystem.Functionality.BusinessObjects;
using LibrarySystem.Functionality.Entities;
using LibrarySystem.Functionality.Exceptions;
using LibrarySystem.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Services
{
    public class BookService : IBookService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public BookService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public void CreateBook(BookBO book)
        {
            if (book == null)
                throw new InvalidParameterException("book info was not provided");

            var bookEntity = _mapper.Map<Book>(book);

            _functionalityUnitOfWork.Books.Add(bookEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<BookBO> records, int total, int totalDisplay) GetAllBooks(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var bookData = _functionalityUnitOfWork.Books.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.Title.Contains(searchText), sortText, string.Empty, pageIndex, pageSize);
            var resultData = (from book in bookData.data
                              select _mapper.Map<BookBO>(book)).ToList();

            return (resultData, bookData.total, bookData.totalDisplay);
        }

        public BookBO GetBook(int id)
        {
            var book = _functionalityUnitOfWork.Books.GetById(id);

            if (book == null) return null;

            return _mapper.Map<BookBO>(book);
        }

        public void UpdateBook(BookBO book)
        {
            if (book == null)
                throw new InvalidOperationException("book is missing");

            var bookEntity = _functionalityUnitOfWork.Books.GetById(book.Id);

            if (bookEntity != null)
            {
                _mapper.Map(book, bookEntity);
                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find course");
        }

        public void DeleteBook(int id)
        {
            _functionalityUnitOfWork.Books.Remove(id);
            _functionalityUnitOfWork.Save();
        }
    }
}
