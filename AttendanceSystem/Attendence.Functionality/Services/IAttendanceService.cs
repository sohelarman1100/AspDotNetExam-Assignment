using Attendence.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Services
{
    public interface IAttendanceService
    {
        void CreateAttendance(AttendanceBO attendanceBO);
        (IList<AttendanceBO> records, int total, int totalDisplay) GetAllAttendance(int pageIndex, int pageSize, 
            string searchText, string sortText);
        AttendanceBO GetAttendanceForEdit(int id);
        void UpdateAttendance(AttendanceBO attendanceinfo);
        void DeleteAttendance(int id);
    }
}
