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
    public class CreateAuthorModel
    {
        [Required, MaxLength(200, ErrorMessage = "name should be less than 200 charcaters")]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public CreateAuthorModel()
        {
            _authorService = Startup.AutofacContainer.Resolve<IAuthorService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateAuthorModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        internal void CreateAuthor()
        {
            var author = _mapper.Map<AuthorBO>(this);

            _authorService.CreateAuthor(author);
        }
    }
}
