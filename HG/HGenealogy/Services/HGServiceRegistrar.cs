using System.Data.Entity;
using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.Framework.Mvc;
using HGenealogy.Data;
using HGenealogy.Controllers;
using HGenealogy.Infrastructure.Installation;
using Nop.Data;
using Nop.Core.Data;
using HGenealogy.Domain;
using Nop.Core.Configuration;
using System;

namespace HGenealogy.Services
{
    public class HGServiceRegistrar
    {
        public static void RegisterService(ContainerBuilder builder)
        {
            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<HGPedigreeMetaService>().As<IHGPedigreeMetaService>().InstancePerLifetimeScope();
            builder.RegisterType<HGTestService>().As<IHGTestService>().InstancePerLifetimeScope();
            builder.RegisterType<HGFamilyMemberService>().As<IHGFamilyMemberService>().InstancePerLifetimeScope();

        }
    }
}
