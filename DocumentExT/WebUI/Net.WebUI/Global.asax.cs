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

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Response.Clear();

        //    HttpException httpException = exception as HttpException;

        //    if (httpException != null)
        //    {
        //        string action;

        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                // page not found
        //                action = "Error404";
        //                break;
        //            case 500:
        //                // server error
        //                action = "Error500";
        //                break;
        //            default:
        //                action = "General";
        //                break;
        //        }

        //        // clear error on server
        //        Server.ClearError();

        //        Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
        //    }
        //}

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //foreach (var item in this.Context.Items.Values)
            //{
            //    var disposableObj = item as IDisposable;
            //    if (disposableObj != null) disposableObj.Dispose();
            //}
        }

        protected void RemoveWebFormEngines()
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