using Attendence.Data;
using Attendence.Functionality.Context;
using Attendence.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.Repositories
{
    public class AttendanceRepository : Repository<Attendance, int>, IAttendanceRepository
    {
        public AttendanceRepository(IFunctionalityContext context) : base((DbContext) context)
        {

        }
    }
}
