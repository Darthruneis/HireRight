using System.Web.Optimization;

namespace HireRight
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/modernizr-*",
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/Bootstrap/bootstrap.min.js",
                "~/Scripts/respond.min.js",
                "~/Scripts/jquery.validate.min.js*",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/Navigation/_navigation.js",
                "~/Scripts/Global/Collapsibles.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/styles.min.css"
                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}