using Autofac;
using Inventory.Functionality.Contexts;
using Inventory.Functionality.Repositories;
using Inventory.Functionality.Services;
using Inventory.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Functionality
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

            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<StockRepository>().As<IStockRepository>().InstancePerLifetimeScope();

            builder.RegisterType<FunctionalityUnitOfWork>().As<IFunctionalityUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockService>().As<IStockService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
