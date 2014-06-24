using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.BusinessLogic.Repository;
using ShareMarket.DataAccess;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Utilities;
using ShareMarket.Web.Areas.Admin.Controllers;
using ShareMarket.Web.Helpers;

namespace ShareMarket.Web
{
    /// <summary>
    /// Inject Dependencies which are registered here with Container Builder.
    /// </summary>
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            // Register all controllers.
            containerBuilder.RegisterControllers(typeof(HomeController).Assembly);

            // Register HttpContextBase.
            containerBuilder.RegisterType(typeof(HttpContextBase));

            // Register Libs.
            containerBuilder.RegisterAssemblyTypes(typeof(HomeLib).Assembly).Where(t => t.Name.EndsWith("Lib")).InstancePerLifetimeScope();

            // Register other dependencies.
            containerBuilder.RegisterType<ShareMarketDbContext>().As<DbContext>().InstancePerDependency();
            containerBuilder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>)).InstancePerDependency();
            containerBuilder.RegisterType<WebSecurityWrapper>().As<IWebSecurity>();
            containerBuilder.RegisterType<RoleWrapper>().As<IRole>();
            containerBuilder.RegisterType<RolePrincipalWrapper>().As<IRolePrincipal>();
            //containerBuilder.RegisterType<AppSettingsHelper>().As<IAppSettingsHelper>();

            IContainer container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalUtil.Container = container;
        }
    }
}