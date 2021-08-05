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
    public class CreateAttendanceModel
    {
        [Required, Range(1, 50)]
        public int StudentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        private IAttendanceService _attendanceService;
        public CreateAttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public CreateAttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        internal void CreateAttendance()
        {
            var attendanceBO = new AttendanceBO
            {
                StudentId=StudentId,
                Date=Date
            };

            _attendanceService.CreateAttendance(attendanceBO);
        }
    }
}
