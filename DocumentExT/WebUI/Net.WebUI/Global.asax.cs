using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Net.WebUI
{
    // 참고: IIS6 또는 IIS7 클래식 모드를 사용하도록 설정하는 지침을 보려면 
    // http://go.microsoft.com/?LinkId=9394801을 방문하십시오.

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RemoveWebFormEngines();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string l4net = Server.MapPath("~/Web.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));

            MvcHandler.DisableMvcResponseHeader = true;
        }

        void RemoveWebFormEngines()
        {
            var viewEngines = ViewEngines.Engines;
            var webFormEngines = viewEngines.OfType<WebFormViewEngine>().FirstOrDefault();
            if (webFormEngines != null)
            {
                viewEngines.Remove(webFormEngines);
            }
        }
    }
}