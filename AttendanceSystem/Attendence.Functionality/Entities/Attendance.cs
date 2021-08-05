using Attendence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Entities
{
    public class Attendance : IEntity<int>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public Student Student { get; set; }
    }
}
