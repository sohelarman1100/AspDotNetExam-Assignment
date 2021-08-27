using LibrarySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Entities
{
    public class Author : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Book> WrittenBooks { get; set; }
        public List<BookAuthor> BooksTitle { get; set; }
    }
}
