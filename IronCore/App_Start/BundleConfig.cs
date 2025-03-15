using System.Web;
using System.Web.Optimization;

namespace IronCore
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/style.css",
                "~/Content/responsive.css",
                "~/Content/Site.css",
                "~/Content/style.css.map"));

            bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
        }
    }
}
