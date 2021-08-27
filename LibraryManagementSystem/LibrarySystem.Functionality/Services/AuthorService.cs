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
    public class AuthorService : IAuthorService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public AuthorService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public void CreateAuthor(AuthorBO author)
        {
            if (author == null)
                throw new InvalidParameterException("book info was not provided");

            var authorEntity = _mapper.Map<Author>(author);

            _functionalityUnitOfWork.Authors.Add(authorEntity);

            _functionalityUnitOfWork.Save();
        }
    }
}
