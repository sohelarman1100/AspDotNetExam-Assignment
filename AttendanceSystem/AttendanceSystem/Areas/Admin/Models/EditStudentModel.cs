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
    public class EditStudentModel
    {
        [Required, Range(1,500000)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "name should provide"), MaxLength(200)]
        public string Name { get; set; }

        [Required, Range(1, 50, ErrorMessage ="student roll should between 1 to 50")]
        public int? RollNumber { get; set; }

        private readonly IStudentService _studentService;

        public EditStudentModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }

        public EditStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        internal void LoadStudentDataForEdit(int id)
        {
            var data = _studentService.GetStudentForEdit(id);
            Id = data?.Id;
            Name = data?.Name;
            RollNumber = data?.RollNumber;
        }

        internal void Update()
        {
            var studentinfo = new StudentBO
            {
                Id = Id.HasValue ? Id.Value : 0,   
                Name = Name,
                RollNumber = RollNumber.HasValue ? RollNumber.Value : 0
            };
            _studentService.UpdateStudent(studentinfo);
        }
    }
}
