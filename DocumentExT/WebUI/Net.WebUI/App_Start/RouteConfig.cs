using Net.Common.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Net.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new CatalogUrlProvider());

            //routes.MapRoute(
            //    name: "Catalog",
            //    url: "design/{CatalogNo}",
            //    defaults: new { controller = "Catalog", action = "ShowDesigns", no = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{no}",
                defaults: new { controller = "Home", action = "Index", no = UrlParameter.Optional }
            );
        }
    }
}