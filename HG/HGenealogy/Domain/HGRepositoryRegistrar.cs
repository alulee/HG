using System;
using System.Data.Entity;
using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.Framework.Mvc;
using Nop.Core.Configuration;
using Nop.Data;
using Nop.Core.Data;
using HGenealogy.Data;
using HGenealogy.Controllers;
using HGenealogy.Infrastructure.Installation;
using HGenealogy.Domain.HGFamilyMembers;
using Nop.Core.Domain.Media;


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

            builder.RegisterType<EfRepository<FamilyMember>>()
                .As<IRepository<FamilyMember>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<FamilyMemberPicture>>()
                .As<IRepository<FamilyMemberPicture>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<FamilyMemberRelation>>()
                .As<IRepository<FamilyMemberRelation>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_hgenealogy"))
                .InstancePerLifetimeScope();
        }
    }
}
