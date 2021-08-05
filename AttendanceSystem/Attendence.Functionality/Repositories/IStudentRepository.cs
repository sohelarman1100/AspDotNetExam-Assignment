using Attendence.Data;
using Attendence.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Repositories
{
    public interface IStudentRepository : IRepository<Student, int>
    {
    }
}
