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
    public class EditAttendanceModel
    {
        [Required, Range(1, 500000)]
        public int? Id { get; set; }

        [Required, Range(1, 500000)]
        public int? StudentId { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        private readonly IAttendanceService _attendanceService;

        public EditAttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }

        public EditAttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        internal void LoadAttendanceDataForEdit(int id)
        {
            var data = _attendanceService.GetAttendanceForEdit(id);
            Id = data?.Id;
            StudentId = data?.StudentId;
            Date = data?.Date;
        }

        internal void Update()
        {
            var attendanceinfo = new AttendanceBO
            {
                Id = Id.HasValue ? Id.Value : 0,
                StudentId = StudentId.HasValue? StudentId.Value : 0,
                Date = Date.HasValue ? Date.Value : DateTime.MinValue
            };
            _attendanceService.UpdateAttendance(attendanceinfo);
        }
    }
}
