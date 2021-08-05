using Attendence.Data;
using Attendence.Functionality.Context;
using Attendence.Functionality.Repositories;
using Attendence.Functionality.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork, IFunctionalityUnitOfWork
    {
        public IStudentRepository Students { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public FunctionalityUnitOfWork(IFunctionalityContext context, IStudentRepository students,
            IAttendanceRepository attendances) : base((DbContext)context)
        {
            Students = students;
            Attendances = attendances;
        }
    }
}
