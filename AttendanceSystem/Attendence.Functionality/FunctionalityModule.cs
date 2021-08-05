using Attendence.Functionality.Context;
using Attendence.Functionality.Repositories;
using Attendence.Functionality.Services;
using Attendence.Functionality.UnitOfWorks;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence.Functionality
{
    public class FunctionalityModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FunctionalityModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FunctionalityContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityContext>().As<IFunctionalityContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AttendanceRepository>().As<IAttendanceRepository>().InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityUnitOfWork>().As<IFunctionalityUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceService>().As<IAttendanceService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
