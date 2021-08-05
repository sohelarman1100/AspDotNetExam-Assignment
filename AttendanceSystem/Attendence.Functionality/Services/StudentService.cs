using Attendence.Functionality.BusinessObjects;
using Attendence.Functionality.Entities;
using Attendence.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendence.Functionality.Exceptions;

namespace Attendence.Functionality.Services
{
    public class StudentService : IStudentService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public StudentService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateCustomer(StudentBO student)
        {
            if (student == null)
                throw new InvalidParameterException("student info was not provided");

            var studentEntity = new Student
            {
                Name=student.Name,
                RollNumber=student.RollNumber
            };

            _functionalityUnitOfWork.Students.Add(studentEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<StudentBO> records, int total, int totalDisplay) GetAllStudents(int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            int srctxt = string.IsNullOrWhiteSpace(searchText) ? 0 : Convert.ToInt32(searchText[0]);
            var TypeConvertedSearchText = (string.IsNullOrWhiteSpace(searchText) == false && srctxt >= 49 &&
                                           srctxt <= 57) ? int.Parse(searchText) : 0;

            var studentData = _functionalityUnitOfWork.Students.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? 
                null : x => x.RollNumber == TypeConvertedSearchText,sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from student in studentData.data
                              select new StudentBO
                              {
                                  Id=student.Id,
                                  Name = student.Name,
                                  RollNumber= student.RollNumber
                              }).ToList();

            return (resultData, studentData.total, studentData.totalDisplay);
        }

        public StudentBO GetStudentForEdit(int id)
        {
            var entityStudent = _functionalityUnitOfWork.Students.GetById(id);
            var BusObjStudent = new StudentBO
            {
                Id = entityStudent.Id,
                Name = entityStudent.Name,
                RollNumber = entityStudent.RollNumber
            };

            return BusObjStudent;
        }

        public void UpdateStudent(StudentBO studentinfo)
        {
            if (studentinfo == null)
                throw new InvalidOperationException("Student is missing");

            var entityStudent = _functionalityUnitOfWork.Students.GetById(studentinfo.Id);

            if (entityStudent != null)
            {
                entityStudent.Name = studentinfo.Name;
                entityStudent.RollNumber = studentinfo.RollNumber;

                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Student");

        }

        public void DeleteStudent(int id)
        {
            _functionalityUnitOfWork.Students.Remove(id);

            _functionalityUnitOfWork.Save();
        }

    }
}
