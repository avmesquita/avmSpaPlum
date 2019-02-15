using System.Web;
using System.Web.Optimization;

namespace SpaPlum.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // ------------ SCRIPTS

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"
            //));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.12.4.min.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jquery1124").Include(
                        "~/Scripts/jquery-1.12.4.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery171").Include(
                        "~/Scripts/jquery-1.7.1.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery182").Include(
                        "~/Scripts/jquery-1.8.2.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery224").Include(
                        "~/Scripts/jquery-2.2.4.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery311").Include(
                        "~/Scripts/jquery-3.1.1.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.aditional-methods.min.js",
                        "~/Scripts/globalize.js",
                        "~/Scripts/jquery.validade.globalize.js",
                        "~/Scripts/jquery.validade.unobtrusive.min.js",
                        "~/Scripts/globalize/currency.js",
                        "~/Scripts/globalize/date.js",
                        "~/Scripts/globalize/message.js",
                        "~/Scripts/globalize/number.js",
                        "~/Scripts/globalize/relative-time.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/jquerymaskedinput").Include(
                        "~/Scripts/jquery.maskedinput.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //    "~/Scripts/jquery-ui-1.12.1.min.js"
            //));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-modal.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/datetimepicker-setup.js",
                      "~/Scripts/respond.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/daypilot").Include(
                      "~/Scripts/DayPilot/daypilot-all.min.js",
                      "~/Scripts/DayPilot/common.js",
                      "~/Scripts/DayPilot/menu.js",
                      "~/Scripts/DayPilot/month.js",
                      "~/Scripts/DayPilot/calendar.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                      "~/Scripts/fullcalendar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
                      "~/Scripts/globalize.js",
                      "~/Scripts/globalize/currency.js",
                      "~/Scripts/globalize/date.js",
                      "~/Scripts/globalize/message.js",
                      "~/Scripts/globalize/number.js",
                      "~/Scripts/globalize/relative-time.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/cldr").Include(
                      "~/Scripts/cldrjs/dist/cldr/event.js",
                      "~/Scripts/cldrjs/dist/cldr//supplemental.js",
                      "~/Scripts/cldrjs/dist/unresolved.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery.browser.min.js",
                      "~/Scripts/jquery-ui-1.8.24.min.js"
            ));

            //
            //----- WORDPRESS

            bundles.Add(new ScriptBundle("~/bundles/wordpress").Include(
                      "~/Scripts/spaplum-create-wordpress.js"
            ));

            //
            //----- WIZARD
            bundles.Add(new ScriptBundle("~/bundles/wizard").Include(
                      "~/Scripts/spaplum-wizard.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/wizardpasso1").Include(
                      "~/Scripts/spaplum-wizard-passo1.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/wizardpasso2").Include(
                      "~/Scripts/spaplum-wizard-passo2.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/wizardpasso3").Include(
                      "~/Scripts/spaplum-wizard-passo3.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/wizardpasso4").Include(
                      "~/Scripts/spaplum-wizard-passo4.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/wizardpasso5").Include(
                      "~/Scripts/spaplum-wizard-passo5.js"
            ));

            // DATE TIME PICKER
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                      "~/Scripts/DateTimePicker/build/jquery.datetimepicker.full.min.js"
            ));
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                      "~/Scripts/DateTimePicker/jquery.datetimepicker.css"
            ));


            //
            //----- ESTILO
            //

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/bootstrap.css",
               "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssCompromise").Include(
               "~/Content/bootstrap.css",
               "~/Content/siteCompromise.css"));


            bundles.Add(new StyleBundle("~/Content/cssui").Include(
                      "~/Content/jquery-ui-themes/jquery.ui.min.css"
                      , "~/Content/jquery-ui-themes/jquery.ui.structure.min.css"
                      , "~/Content/jquery-ui-themes/jquery.ui.theme.min.css"
                      , "~/Content/jquery-ui-themes/themes/black-tie/jquery.ui.css"
            //,"~/Content/Site.css"
            ));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                        "~/Content/jquery-ui-themes/base/jquery-ui.min.css",
                        "~/Content/jquery-ui-themes/jquery-ui-structure.min.css",
                        "~/Content/jquery-ui-themes/jquery-ui-theme.min.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.core.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.resizable.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.selectable.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.accordion.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.autocomplete.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.button.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.dialog.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.slider.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.tabs.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.datepicker.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.progressbar.css",
                        "~/Content/jquery-ui-themes/base/jquery.ui.theme.css"));


            bundles.Add(new StyleBundle("~/Content/jquery").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui-structure.css",
                      "~/Content/jquery-ui-theme.css"
            //"~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/daypilottheme").Include(
                      "~/Content/daypilot-themes/calendar_white.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/fullcalendar").Include(
                      "~/Content/full-calendar/fullcalendar.css"
            ));

            bundles.Add(new StyleBundle("~/Content/fullcalendarui").Include(
                        "~/Content/fullcalendar-themes/base/jquery.ui.core.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.resizable.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.selectable.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.accordion.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.autocomplete.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.button.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.dialog.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.slider.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.tabs.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.datepicker.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.progressbar.css",
                        "~/Content/fullcalendar-themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/jquery-1.7.1.min.js",
                        "~/Scripts/jquery.easyui.min.js",
                        "~/Scripts/datagrid-filter.js",
                        "~/Scripts/easyui-lang-pt_BR.js"));

            bundles.Add(new StyleBundle("~/Content/easyui").Include(
                        "~/Content/easyui/themes/default/easyui.css"));

            bundles.Add(new ScriptBundle("~/bundles/chat").Include(
                        "~/Scripts/chat.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/datatables.min.js"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                        "~/Scripts/DataTables/datatables.min.css"));


            bundles.Add(new StyleBundle("~/Content/duallistbox").Include(
                "~/Content/bootstrap-duallistbox-4/dist/bootstrap-duallistbox.min.css"));


            System.Web.Optimization.BundleTable.EnableOptimizations = false;

        }
    }
}
