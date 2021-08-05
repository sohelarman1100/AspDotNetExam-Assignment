using AttendanceSystem.Models;
using Attendence.Functionality.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models
{
    public class GetStudentModel
    {
        private readonly IStudentService _studentService;

        public GetStudentModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }
        public GetStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        internal object GetAllStudents(DataTablesAjaxRequestModel tableModel)
        {
            var data = _studentService.GetAllStudents(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name", "RollNumber" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.Name,    
                             record.RollNumber.ToString(),
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
