using System.Web.Mvc;

namespace Net.WebUI.Helper
{
    public static class UrlHelperExtensions
    {
        private static readonly string version = typeof(UrlHelperExtensions).Assembly.GetName().Version.ToString();

        public static string VersionedContent(this UrlHelper urlHelper, string resUrl)
        {
            return urlHelper.Content(resUrl) + "?v=" + version;
        }
    }
}