using Autofac;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter
{
    public class webModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GooglereCaptchaService>().AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
