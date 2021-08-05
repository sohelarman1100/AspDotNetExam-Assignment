using Attendence.Functionality.BusinessObjects;
using Attendence.Functionality.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class CreateStudentModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }

        [Required, Range(1, 50)]
        public int RollNumber { get; set; }


        private IStudentService _studentService;

        public CreateStudentModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }
        public CreateStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        internal void CreateStudent()
        {
            var student = new StudentBO
            {
                Name = Name,
                RollNumber = RollNumber
            };

            _studentService.CreateCustomer(student);
        }
    }
}
