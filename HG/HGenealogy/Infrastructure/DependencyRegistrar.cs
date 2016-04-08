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
using HGenealogy.Services;
using Nop.Data;
using Nop.Core.Data;
using HGenealogy.Domain;
using Nop.Core.Configuration;
using System;

namespace HGenealogy.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            ////we cache presentation models between requests
            //builder.RegisterType<BlogController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<CatalogController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<CountryController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<CommonController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<NewsController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<PollController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<ProductController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<ShoppingCartController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<TopicController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            //builder.RegisterType<WidgetController>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));

            //data layer
            var hgDataSettingsManager = new HGDataSettingsManager();
            var hgDataProviderSettings = hgDataSettingsManager.LoadSettings();
            builder.Register(c => hgDataSettingsManager.LoadSettings()).As<HGDataSettings>();
            builder.Register(x => new HGEfDataProviderManager(x.Resolve<HGDataSettings>())).As<HGEfDataProviderManager>().InstancePerDependency();

            if (hgDataProviderSettings != null && hgDataProviderSettings.IsValid())
            {
                var hgEfDataProviderManager = new HGEfDataProviderManager(hgDataSettingsManager.LoadSettings());
                var hgDataProvider = hgEfDataProviderManager.LoadDataProvider();
                hgDataProvider.InitConnectionFactory();

                builder.Register<HGenealogyObjectContext>(c => new HGenealogyObjectContext(hgDataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<HGenealogyObjectContext>(c => new HGenealogyObjectContext(hgDataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }

            Database.SetInitializer<HGenealogyObjectContext>(null);

            //data context
            RegisterHGDataContext<HGenealogyObjectContext>(builder, "nop_object_context_hgenealogy");

            // 注入 HGenealogy Domain Repository
            HGRepositoryRegistrar.RegisterRepository(builder);

            // 注入 HGenealogy 相關 Service
            HGServiceRegistrar.RegisterService(builder);
        }

        public int Order
        {
            get { return 2; }
        }

        /// <summary>
        /// Register HGenealogy DataContext
        /// </summary>
        /// <typeparam name="T">Class implementing IDbContext</typeparam>
        /// <param name="builder">Builder</param>
        /// <param name="contextName">Context name</param>
        public static void RegisterHGDataContext<T>(ContainerBuilder builder, string contextName)
             where T : IDbContext
        {
            //data layer
            var dataSettingsManager = new HGDataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register(c => (IDbContext)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
            else
            {
                //register named context
                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<HGDataSettings>().DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<HGDataSettings>().DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
        }
    }
}
