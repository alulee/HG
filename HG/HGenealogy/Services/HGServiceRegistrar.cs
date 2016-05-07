using System.Data.Entity;
using Autofac;
using Autofac.Core;
using System;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.Framework.Mvc;
using Nop.Data;
using Nop.Core.Data;
using Nop.Services.Common;
using HGenealogy.Domain;
using Nop.Core.Configuration;
using HGenealogy.Data;
using HGenealogy.Controllers;
using HGenealogy.Infrastructure.Installation;
using HGenealogy.Services.HGFamilyMembers;
using HGenealogy.Services.Common;

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
            builder.RegisterType<FamilyMemberService>().As<IFamilyMemberService>().InstancePerLifetimeScope();
            builder.RegisterType<HGPedigreeInfoService>().As<IHGPedigreeInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<HGAddressService>().As<IHGAddressService>().InstancePerLifetimeScope();

        }
    }
}
