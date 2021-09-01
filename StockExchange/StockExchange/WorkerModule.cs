using Autofac;
using Microsoft.Extensions.Configuration;
using StockExchange.Controls;
using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public WorkerModule(string connectionStringName, string migrationAssemblyName,
            IConfiguration configuration)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateStockPriceModel>().As<ICreateStockPriceModel>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockServiceControl>().As<IStockServiceControl>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateCompanyModel>().As<ICreateCompanyModel>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CompanyServiceControl>().As<ICompanyServiceControl>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
