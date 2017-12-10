using System.Web;
using System.Web.Optimization;

namespace Prova.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
            "~/Scripts/knockout-{version}.js",
            "~/Scripts/knockout.mapping-latest.js",
            "~/Scripts/src/ko.initial.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/src/loginViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/registrar").Include(
                "~/src/registrarViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/alterarSenha").Include(
                "~/src/alterarSenhaViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/alterarDados").Include(
                "~/src/alterarDadosViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/esqueciSenha").Include(
                "~/src/esqueciSenhaViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/resetarSenha").Include(
                "~/src/resetarSenhaViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/contato").Include(            
            "~/Scripts/knockout.validation.js",
            "~/Scripts/inputmask/inputmask.js",
            "~/Scripts/inputmask/jquery.inputmask.js",
            "~/src/contatosViewModel.js"));
        }
    }
}
