using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShareMarket.BusinessLogic.Message;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            IocConfig.RegisterDependencies();
            // Set Mapping using automapper.
            AutoMapperConfig.Mapping();
            // Log4net configuration.
            LogConfig.RegisterLog4NetConfig();
            // Database configuration.
            DatabaseConfig.ConfigureDataBase();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initializes the appsettings.
            GlobalUtil.SetAppSettingValue();

            //Task initialize
            //Schedule task
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();

        }
    }
}