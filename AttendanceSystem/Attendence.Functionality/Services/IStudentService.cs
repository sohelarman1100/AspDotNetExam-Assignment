using Attendence.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Services
{
    public interface IStudentService
    {
        void CreateCustomer(StudentBO student);
        (IList<StudentBO> records, int total, int totalDisplay) GetAllStudents(int pageIndex, int pageSize,
            string searchText, string sortText);
        StudentBO GetStudentForEdit(int id);
        void UpdateStudent(StudentBO studentinfo);
        void DeleteStudent(int id);
    }
}
