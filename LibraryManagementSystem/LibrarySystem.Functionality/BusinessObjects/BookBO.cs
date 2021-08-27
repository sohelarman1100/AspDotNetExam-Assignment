using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.BusinessObjects
{
    public class BookBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
    }
}
