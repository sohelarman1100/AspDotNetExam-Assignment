using LibrarySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Entities
{
    public class Book : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookAuthor> AuthorsName { get; set; }
    }
}
