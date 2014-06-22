using System.IO;
using System.Web.Hosting;

namespace ShareMarket.Web
{
    public static class LogConfig
    {
        public static void RegisterLog4NetConfig()
        {
            // log4net file info.
            var log4NetFileInfo =
                new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Conf/log4net.config"));

            // log4net initializing configuration.
            log4net.Config.XmlConfigurator.Configure(log4NetFileInfo); 
        }
    }
}