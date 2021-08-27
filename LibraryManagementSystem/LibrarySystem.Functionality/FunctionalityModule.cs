using Autofac;
using LibrarySystem.Functionality.Contexts;
using LibrarySystem.Functionality.Repositories;
using LibrarySystem.Functionality.Services;
using LibrarySystem.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality
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

            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityUnitOfWork>().As<IFunctionalityUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();

            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
