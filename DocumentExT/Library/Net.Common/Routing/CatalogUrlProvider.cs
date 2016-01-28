using Net.Common.Model;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Net.Common.Routing
{
    public class CatalogUrlProvider : RouteBase
    {
        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            var virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo;//获取相对路径

            virtualPath = virtualPath.Split('/').Last();

            if (!virtualPath.StartsWith("ca-"))
                return null;

            var catalogname = virtualPath.Split('-').Last();

            var catalog = CatalogManager.AllCategories.Find(c => c.CatalogName.Equals(catalogname, StringComparison.OrdinalIgnoreCase));

            if (catalog == null)
                return null;

            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "Catalog");
            data.Values.Add("action", "ShowDesigns");
            data.Values.Add("no", catalog.CatalogNo);

            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //判断请求是否来源于CatalogController.ShowDesigns(string no),不是则返回null,让匹配继续
            var catalogNo = values["no"] as string;

            if (catalogNo == null)//路由信息中缺少参数no，不是我们要处理的请求，返回null
                return null;

            //请求不是CatalogController发起的，不是我们要处理的请求，返回null
            if (!values.ContainsKey("controller") || !values["controller"].ToString().Equals("category", StringComparison.OrdinalIgnoreCase))
                return null;
            //请求不是CatalogController.ShowDesigns(string no)发起的，不是我们要处理的请求，返回null
            if (!values.ContainsKey("action") || !values["action"].ToString().Equals("showdesigns", StringComparison.OrdinalIgnoreCase))
                return null;

            //至此，我们可以确定请求是CatalogController.ShowDesigns(string no)发起的，生成相应的URL并返回
            var catalog = CatalogManager.AllCategories.Find(c => c.CatalogNo == catalogNo);

            if (catalog == null)
                throw new ArgumentNullException("design");//找不到分类抛出异常

            var path = "ca-" + catalog.CatalogName.Trim();//生成URL

            return new VirtualPathData(this, path.ToLowerInvariant());
        }
    }
}
