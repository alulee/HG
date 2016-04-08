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

namespace HGenealogy.Domain
{
    public class HGRepositoryRegistrar
    {
        public static void RegisterRepository(ContainerBuilder builder)
        {
            //override required repository with our custom context
            builder.RegisterType<EfRepository<HGTest>>()
                .As<IRepository<HGTest>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<HGPedigreeMeta>>()
                .As<IRepository<HGPedigreeMeta>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<HGFamilyMember>>()
                .As<IRepository<HGFamilyMember>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<HGFamilyMemberPicture>>()
                .As<IRepository<HGFamilyMemberPicture>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();
        }
    }
}
