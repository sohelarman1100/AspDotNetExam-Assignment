using Attendence.Data;
using Attendence.Functionality.Repositories;
using Attendence.Functionality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IAttendanceRepository Attendances { get; }
    }
}
