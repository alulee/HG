using FluentValidation.Mvc;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Data.Mapping;
using Nop.Services.Logging;
using Nop.Services.Tasks;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Routes;
using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HGenealogy
{
    public class MvcApplication : System.Web.HttpApplication
    {
 
        protected void Application_Start()
        {
            /****** Refenced From Nop.Web.Application_Start() 開始******/
            //initialize engine context
            EngineContext.Initialize(false);

            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (databaseInstalled)
            {
                //remove all view engines
                ViewEngines.Engines.Clear();
                //except the themeable razor view engine we use
                ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }
            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new NopMetadataProvider();

            /****** Refenced From Nop.Web.Application_Start() 結束******/

            AreaRegistration.RegisterAllAreas();

            // RegisterRoutes(RouteTable.Routes);  
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Ref from Nop
            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new NopValidatorFactory()));

            //start scheduled tasks
            //if (databaseInstalled)
            //{
            //    TaskManager.Instance.Initialize();
            //    TaskManager.Instance.Start();
            //}
 
            //log application start
            if (databaseInstalled)
            {
                try
                {
                    //log
                    var logger = EngineContext.Current.Resolve<ILogger>();
                    logger.Information("Application started", null, null);
                }
                catch (Exception)
                {
                    //don't throw new exception if occurs
                }
            }

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
 
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            //keep alive page requested (we ignore it to prevent creating a guest customer records)
            string keepAliveUrl = string.Format("{0}keepalive/index", webHelper.GetStoreLocation());
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;

            //ensure database is installed
            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                string installUrl = string.Format("{0}install", webHelper.GetStoreLocation());
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Response.Redirect(installUrl);
                }
            }

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            ////miniprofiler
            //if (EngineContext.Current.Resolve<StoreInformationSettings>().DisplayMiniProfilerInPublicStore)
            //{
            //    MiniProfiler.Start();
            //    //store a value indicating whether profiler was started
            //    HttpContext.Current.Items["nop.MiniProfilerStarted"] = true;
            //}

            // test
        }

    }
}
