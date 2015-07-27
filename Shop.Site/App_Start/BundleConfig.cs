using System.Web.Optimization;

namespace Shop.Site.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/lib/*.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/controllers")
                .Include("~/Scripts/*Controller.js"));

            bundles.Add(new ScriptBundle("~/bundles/init")
                .Include("~/Scripts/init.js"));
        }
    }
}