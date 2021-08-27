using AutoMapper;
using LibraryManagementSystem.Areas.Admin.Models;
using LibrarySystem.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Profiles
{
    public class WebProfile :Profile
    {
        public WebProfile()
        {
            CreateMap<CreateBookModel, BookBO>().ReverseMap();
            CreateMap<EditBookModel, BookBO>().ReverseMap();
            CreateMap<CreateAuthorModel, AuthorBO>().ReverseMap();
            //CreateMap<EditAuthorModel, AuthorBO>().ReverseMap();
        }
    }
}
