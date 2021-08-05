using AttendanceSystem.Models;
using Attendence.Functionality.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class GetAttendanceModel
    {
        private readonly IAttendanceService _attendanceService;

        public GetAttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }
        public GetAttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        internal object GetAllAttendance(DataTablesAjaxRequestModel tableModel)
        {
            var data = _attendanceService.GetAllAttendance(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "StudentId", "Date" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.StudentId.ToString(),
                             record.Date.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
