using System.Web;
using System.Web.Optimization;

namespace Net.WebUI
{
    public class BundleConfig
    {
        // Bundling에 대한 자세한 정보는 http://go.microsoft.com/fwlink/?LinkId=254725를 방문하십시오.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/common/common.js",
                        "~/Scripts/common/ajaxmodel.js",
                        "~/Scripts/common/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/threejs_loader").Include(
                        "~/Scripts/threejs.v71/STLLoader.js",
                        "~/Scripts/threejs.v71/OBJLoader.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/thingiview").Include(
                        "~/Scripts/thingiview/thingiview.js",
                        "~/Scripts/thingiview/normalcontrols.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/thingiview.v2").Include(
                        "~/Scripts/thingiview.v2/thingiview.js",
                        "~/Scripts/thingiview.v2/normalcontrols.js",
                        "~/Scripts/thingiview.v2/detector.js"
                        ));

            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션할 준비가 되면 http://modernizr.com의 빌드 도구를 사용하여 필요한 테스트만 선택하십시오.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/common_css").Include(
                        "~/Content/css/layout.css",
                        "~/Content/css/default.css",
                        "~/Content/css/font.css",
                        "~/Content/css/common.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}