using System.Web.Optimization;

namespace SysIgreja
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Clear();
            bundles.ResetAll();

            BundleTable.EnableOptimizations = false;

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/evento.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/all.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            // jsCrop 
            bundles.Add(new ScriptBundle("~/bundles/jscrop").Include(
                        "~/Scripts/plugins/jsCrop-master/js-crop.js"));

            // jsCropCSS 
            bundles.Add(new StyleBundle("~/bundles/jscropcss").Include(
                   "~/Scripts/plugins/jsCrop-master/js-crop.css"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            //Moment
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                     "~/Scripts/plugins/moment/moment-with-locales.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.min.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/imaskjs").Include(
                      "~/Scripts/plugins/imaskjs/imask.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/jquery.metisMenu.js",
                      "~/Scripts/plugins/pace/pace.min.js",
                      "~/Scripts/app/inspinia.js"));

            bundles.Add(new ScriptBundle("~/bundles/util").Include(
                      "~/Scripts/app/util/constants.js",
                      "~/Scripts/app/util/messages.js",
                      "~/Scripts/app/util/inputs.js",
                      "~/Scripts/app/util/forms.js",
                      "~/Scripts/app/util/pdf.js",
                      "~/Scripts/app/util/extensions.js",
                      "~/Scripts/app/util/buttons.js"));

            bundles.Add(new ScriptBundle("~/plugins/jsPDF").Include(
                      "~/Scripts/plugins/jsPDF/jsPDF.js",
                      "~/Scripts/plugins/jsPDF/jspdf.customfonts.min.js",
                      "~/Scripts/plugins/jsPDF/default_vfs.js"
                      ));

            bundles.Add(new ScriptBundle("~/plugins/html2canvas").Include(
                      "~/Scripts/plugins/html2canvas/html2canvas.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css",
                      "~/Content/plugins/dataTables/responsive.dataTables.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.min.js",
                      "~/Scripts/plugins/dataTables/dataTables.colReorder.min.js",
                      "~/Scripts/plugins/dataTables/datatable-default-config.js",
                      "~/Scripts/plugins/dataTables/dataTables.fixedColumns.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // ChartJs
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                "~/Scripts/plugins/chartjs/Chart.min.js",
                "~/Scripts/plugins/chartjs/Chart.PieceLabel.min.js",
                "~/Scripts/plugins/chartjs/Chart.PluginService.js"
            ));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/plugins/chosen/bootstrap-chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            //SweetAlert
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
            "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            //SweetAlert Css
            bundles.Add(new StyleBundle("~/plugins/sweetalert").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));

            bundles.Add(new StyleBundle("~/bundle/jasny").Include(
                "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/plugins/jasny").Include(
                "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));
        }
    }
}
