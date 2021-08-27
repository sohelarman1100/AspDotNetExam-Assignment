using AutoMapper;
using LibrarySystem.Functionality.BusinessObjects;
using LibrarySystem.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Profiles
{
    class FunctionalityProfile : Profile
    {
        public FunctionalityProfile()
        {
            CreateMap<Book, BookBO>().ReverseMap();
            CreateMap<Author, AuthorBO>().ReverseMap();
        }
    }
}
