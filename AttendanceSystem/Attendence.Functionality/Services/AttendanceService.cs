using Attendence.Functionality.BusinessObjects;
using Attendence.Functionality.Entities;
using Attendence.Functionality.Exceptions;
using Attendence.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Services
{
    public class AttendanceService : IAttendanceService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        public AttendanceService(IFunctionalityUnitOfWork functionalityUnitOfWork)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
        }

        public void CreateAttendance(AttendanceBO attendanceBO)
        {
            if (attendanceBO == null)
                throw new InvalidParameterException("attendance info was not provided");

            var attendanceEntity = new Attendance
            {
                StudentId = attendanceBO.StudentId,
                Date = attendanceBO.Date
            };

            _functionalityUnitOfWork.Attendances.Add(attendanceEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<AttendanceBO> records, int total, int totalDisplay) GetAllAttendance(int pageIndex,
            int pageSize, string searchText, string sortText)
        {
            int srctxt = string.IsNullOrWhiteSpace(searchText) ? 0 : Convert.ToInt32(searchText[0]);
            var TypeConvertedSearchText = (string.IsNullOrWhiteSpace(searchText) == false && srctxt >= 49 &&
                                           srctxt <= 57) ? int.Parse(searchText) : 0;

            var attendanceData = _functionalityUnitOfWork.Attendances.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.StudentId == TypeConvertedSearchText, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from attendance in attendanceData.data
                              select new AttendanceBO
                              {
                                  Id = attendance.Id,
                                  StudentId = attendance.StudentId,
                                  Date = attendance.Date
                              }).ToList();

            return (resultData, attendanceData.total, attendanceData.totalDisplay);
        }

        public AttendanceBO GetAttendanceForEdit(int id)
        {
            var entityAttendance = _functionalityUnitOfWork.Attendances.GetById(id);
            var BusObjSAttendance = new AttendanceBO
            {
                Id = entityAttendance.Id,
                StudentId = entityAttendance.StudentId,
                Date = entityAttendance.Date
            };

            return BusObjSAttendance;
        }

        public void UpdateAttendance(AttendanceBO attendanceinfo)
        {
            if (attendanceinfo == null)
                throw new InvalidOperationException("attendance is missing");

            var entityAttendance = _functionalityUnitOfWork.Attendances.GetById(attendanceinfo.Id);

            if (entityAttendance != null)
            {
                entityAttendance.StudentId = attendanceinfo.StudentId;
                entityAttendance.Date = attendanceinfo.Date;

                _functionalityUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Attendance");
        }

        public void DeleteAttendance(int id)
        {
            _functionalityUnitOfWork.Attendances.Remove(id);

            _functionalityUnitOfWork.Save();
        }
    }
}
