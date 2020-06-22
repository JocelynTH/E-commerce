using System.Web;
using System.Web.Optimization;

namespace ArbolesPlantas
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            /*
                        bundles.Add(new StyleBundle("~/Content/css").Include(
                                  "~/Content/bootstrap.css",
                                  "~/Content/site.css"));*/

            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/css/open-iconic-bootstrap.min.css",
              "~/Content/css/animate.css",
              "~/Content/css/owl.carousel.min.css",
              "~/Content/css/owl.theme.default.min.css",
              "~/Content/css/magnific-popup.css",
              "~/Content/css/aos.css",
              "~/Content/css/ionicons.min.css",
              "~/Content/css/bootstrap-datepicker.css",
              "~/Content/css/jquery.timepicker.css",
              "~/Content/css/flaticon.css",
              "~/Content/css/icomoon.css",
              "~/Content/css/style.css"
          ));
        }
    }
}
