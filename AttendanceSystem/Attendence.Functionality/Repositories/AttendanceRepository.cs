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
        protected DbContext _dbContext;
        protected DbSet<Attendance> _dbSet1;
        protected DbSet<Student> _dbSet2;
        public AttendanceRepository(IFunctionalityContext context, IFunctionalityContext context1) : base((DbContext) context)
        {
            _dbContext = (DbContext)context1;
            _dbSet1 = _dbContext.Set<Attendance>();
            _dbSet2 = _dbContext.Set<Student>();


        }
    }
}
