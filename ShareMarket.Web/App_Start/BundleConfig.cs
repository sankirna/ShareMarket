using System.Web.Optimization;

namespace ShareMarket.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Temp Asp.net Default template

      

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            #endregion

            #region "Scrips"

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

         

            #region "Kendo UI Bundle(s)"

            //bundles.Add(new ScriptBundle("~/bundle/kendo-knockout").Include(
            //                             "~/Scripts/knockout-kendo*"));

            //bundles.Add(new ScriptBundle("~/bundle/kendotabstrip").Include(
            //    "~/Scripts/kendoui/kendo.web*",
            //    "~/Scripts/kendoui/kendo.tabstrip*"));

            //bundles.Add(new ScriptBundle("~/bundle/grid").Include(
            //    "~/Scripts/kendoui/kendo.web*",
            //    "~/Scripts/kendoui/kendo.grid*"));

            //bundles.Add(new ScriptBundle("~/bundle/kendodropdownlist").Include(
            //    "~/Scripts/kendoui/kendo.web*",
            //    "~/Scripts/kendoui/kendo.dropdownlist*"));

            #endregion "Kendo UI Bundle(s)"


         

            #endregion "Scrips"

            #region "Style"

            

            //######################## Start LOGIN ASSETS #######################
            bundles.Add(new StyleBundle("~/Content/assets/css/Login").Include(
             "~/Areas/Admin/Content/assets/css/pages/login.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/assets/js/Login").Include(
                 "~/Areas/Admin/Content/assets/plugins/jquery-validation/dist/jquery.validate.js",
             "~/Areas/Admin/Content/assets/plugins/jquery-validation/dist/jquery.validate.min.js",
             "~/Areas/Admin/Content/assets/scripts/login.js"
             ));

            //######################## END LOGIN ASSETS #########################



            #region admin Assest
            
            #endregion

            bundles.Add(new StyleBundle("~/Content/assets/css").Include(
                  "~/Areas/Admin/Content/assets/plugins/bootstrap/css/bootstrap.css",
                  "~/Areas/Admin/Content/assets/plugins/bootstrap/css/bootstrap.min.css",
                  "~/Areas/Admin/Content/assets/plugins/bootstrap/css/bootstrap-responsive.css",
"~/Areas/Admin/Content/assets/plugins/bootstrap/css/bootstrap-responsive.min.css",
"~/Areas/Admin/Content/assets/plugins/font-awesome/css/font-awesome.css",
"~/Areas/Admin/Content/assets/plugins/font-awesome/css/font-awesome.min.css",
"~/Areas/Admin/Content/assets/css/style-metro.css",
"~/Areas/Admin/Content/assets/css/style.css",
"~/Areas/Admin/Content/assets/css/style-responsive.css",
"~/Areas/Admin/Content/assets/css/themes/default.css",
"~/Areas/Admin/Content/assets/plugins/uniform/css/uniform.default.css"
                 ));

            bundles.Add(new ScriptBundle("~/Content/assets/js").Include(
"~/Areas/Admin/Content/assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.js",
"~/Areas/Admin/Content/assets/plugins/bootstrap/js/bootstrap.js",
"~/Areas/Admin/Content/assets/plugins/bootstrap/js/bootstrap.min.js",
"~/Areas/Admin/Content/assets/plugins/breakpoints/breakpoints.js",
"~/Areas/Admin/Content/assets/plugins/jquery-slimscroll/jquery.slimscroll.js",
"~/Areas/Admin/Content/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
"~/Areas/Admin/Content/assets/plugins/jquery.blockui.js",
"~/Areas/Admin/Content/assets/plugins/jquery.cookie.js",
"~/Areas/Admin/Content/assets/plugins/uniform/jquery.uniform.js",
"~/Areas/Admin/Content/assets/plugins/uniform/jquery.uniform.min.js",
"~/Areas/Admin/Content/assets/scripts/app.js"
         ));


            #region Jquery Data Table

            bundles.Add(new StyleBundle("~/Content/assets/DataTablecss").Include(
                  "~/Areas/Admin/Content/assets/plugins/data-tables/DT_bootstrap.css"
                    ));

            bundles.Add(new ScriptBundle("~/Content/assets/DataTable").Include(
"~/Areas/Admin/Content/assets/plugins/data-tables/jquery.dataTables.js",
"~/Areas/Admin/Content/assets/plugins/data-tables/DT_bootstrap.js",
"~/Areas/Admin/Content/assets/scripts/table-managed.js"
 ));
            #endregion

            #region Ck editor

               bundles.Add(new ScriptBundle("~/Content/assets/ckeditorjs").Include(
"~/Areas/Admin/Content/assets/plugins/ckeditor/ckeditor.js"
 ));
            #endregion

            #endregion "Style"



            BundleTable.EnableOptimizations = false;


        }
    }
}