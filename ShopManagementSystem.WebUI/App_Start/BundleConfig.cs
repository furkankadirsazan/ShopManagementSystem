using System.Web.Optimization;

namespace ShopManagementSystem.WebUI.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // #Admin
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/Content/AdminTemplate/assets/js/demo1/scripts.bundle.js",
                "~/Content/AdminTemplate/assets/js/demo1/pages/login/login-general.js"
             ));
            bundles.Add(new ScriptBundle("~/bundles/ajax-login").Include(
                "~/Scripts/FunctionsJS/ajax.login.js",
                "~/Scripts/FunctionsJS/ajax.forgetpassword.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/jquery-3.4.1.min.js",
                 "~/Scripts/jquery.validate.min.js",
                 "~/Scripts/jquery.validate.unobtrusive.min.js"
                 ));
            bundles.Add(new ScriptBundle("~/bundles/mandatory").Include(
                    "~/Content/AdminTemplate/assets/vendors/general/jquery/dist/jquery.js",
                    "~/Scripts/umd/popper.js",
                    "~/Content/AdminTemplate/assets/vendors/general/bootstrap/dist/js/bootstrap.min.js",
                    "~/Content/AdminTemplate/assets/vendors/general/js-cookie/src/js.cookie.js",
                    "~/Content/AdminTemplate/assets/vendors/general/moment/min/moment.min.js",
                    "~/Content/AdminTemplate/assets/vendors/general/tooltip.js/dist/umd/tooltip.min.js",
                    "~/Content/AdminTemplate/assets/vendors/general/perfect-scrollbar/dist/perfect-scrollbar.js",
                    "~/Content/AdminTemplate/assets/vendors/general/sticky-js/dist/sticky.min.js",
                    "~/Content/AdminTemplate/assets/vendors/general/wnumb/wNumb.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/optional").Include(
                "~/Content/AdminTemplate/assets/vendors/general/jquery-form/dist/jquery.form.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/block-ui/jquery.blockUI.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-datetime-picker/js/bootstrap-datetimepicker.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-maxlength/src/bootstrap-maxlength.js",
                "~/Content/AdminTemplate/assets/vendors/custom/vendors/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-select/dist/js/bootstrap-select.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-switch/dist/js/bootstrap-switch.js",
                "~/Content/AdminTemplate/assets/vendors/custom/js/vendors/bootstrap-switch.init.js",
                "~/Content/AdminTemplate/assets/vendors/general/select2/dist/js/select2.full.js",
                "~/Content/AdminTemplate/assets/vendors/general/ion-rangeslider/js/ion.rangeSlider.js",
                "~/Content/AdminTemplate/assets/vendors/general/typeahead.js/dist/typeahead.bundle.js",
                "~/Content/AdminTemplate/assets/vendors/general/handlebars/dist/handlebars.js",
                "~/Content/AdminTemplate/assets/vendors/general/inputmask/dist/jquery.inputmask.bundle.js",
                "~/Content/AdminTemplate/assets/vendors/general/inputmask/dist/inputmask/inputmask.date.extensions.js",
                "~/Content/AdminTemplate/assets/vendors/general/inputmask/dist/inputmask/inputmask.numeric.extensions.js",
                "~/Content/AdminTemplate/assets/vendors/general/nouislider/distribute/nouislider.js",
                "~/Content/AdminTemplate/assets/vendors/general/owl.carousel/dist/owl.carousel.js",
                "~/Content/AdminTemplate/assets/vendors/general/autosize/dist/autosize.js",
                "~/Content/AdminTemplate/assets/vendors/general/clipboard/dist/clipboard.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/dropzone/dist/dropzone.js",
                "~/Content/AdminTemplate/assets/vendors/general/markdown/lib/markdown.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-markdown/js/bootstrap-markdown.js",
                "~/Content/AdminTemplate/assets/vendors/general/bootstrap-notify/bootstrap-notify.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/jquery-validation/dist/jquery.validate.js",
                "~/Content/AdminTemplate/assets/vendors/general/jquery-validation/dist/additional-methods.js",
                "~/Content/AdminTemplate/assets/vendors/general/raphael/raphael.js",
                "~/Content/AdminTemplate/assets/vendors/general/morris.js/morris.js",
                "~/Content/AdminTemplate/assets/vendors/general/chart.js/dist/Chart.bundle.js",
                "~/Content/AdminTemplate/assets/vendors/custom/vendors/bootstrap-session-timeout/dist/bootstrap-session-timeout.min.js",
                "~/Content/AdminTemplate/assets/vendors/custom/vendors/jquery-idletimer/idle-timer.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/waypoints/lib/jquery.waypoints.js",
                "~/Content/AdminTemplate/assets/vendors/general/counterup/jquery.counterup.js",
                "~/Content/AdminTemplate/assets/vendors/general/es6-promise-polyfill/promise.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/sweetalert2/dist/sweetalert2.min.js",
                "~/Content/AdminTemplate/assets/vendors/general/jquery.repeater/src/lib.js",
                "~/Content/AdminTemplate/assets/vendors/general/jquery.repeater/src/jquery.input.js",
                "~/Content/AdminTemplate/assets/vendors/general/jquery.repeater/src/repeater.js",
                "~/Content/AdminTemplate/assets/vendors/general/dompurify/dist/purify.js"
                ));



            bundles.Add(new StyleBundle("~/bundles/css").Include(
                     "~/Content/AdminTemplate/assets/vendors/general/tether/dist/css/tether.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-datetime-picker/css/bootstrap-datetimepicker.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-timepicker/css/bootstrap-timepicker.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-daterangepicker/daterangepicker.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-select/dist/css/bootstrap-select.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css",
                     "~/Content/AdminTemplate/assets/vendors/general/select2/dist/css/select2.css",
                     "~/Content/AdminTemplate/assets/vendors/general/ion-rangeslider/css/ion.rangeSlider.css",
                     "~/Content/AdminTemplate/assets/vendors/general/nouislider/distribute/nouislider.css",
                     "~/Content/AdminTemplate/assets/vendors/general/owl.carousel/dist/assets/owl.carousel.css",
                     "~/Content/AdminTemplate/assets/vendors/general/owl.carousel/dist/assets/owl.theme.default.css",
                     "~/Content/AdminTemplate/assets/vendors/general/dropzone/dist/dropzone.css",
                     "~/Content/AdminTemplate/assets/vendors/general/bootstrap-markdown/css/bootstrap-markdown.min.css",
                     "~/Content/AdminTemplate/assets/vendors/general/animate.css/animate.css",
                     "~/Content/AdminTemplate/assets/vendors/general/toastr/build/toastr.css",
                     "~/Content/AdminTemplate/assets/vendors/general/morris.js/morris.css",
                     "~/Content/AdminTemplate/assets/vendors/general/sweetalert2/dist/sweetalert2.css",
                     "~/Content/AdminTemplate/assets/vendors/general/socicon/css/socicon.css",
                     "~/Content/AdminTemplate/assets/vendors/custom/vendors/line-awesome/css/line-awesome.css",
                     "~/Content/AdminTemplate/assets/vendors/custom/vendors/flaticon/flaticon.css",
                     "~/Content/AdminTemplate/assets/vendors/custom/vendors/flaticon2/flaticon.css"
                     ));
            bundles.Add(new StyleBundle("~/bundles/global-css").Include(
                   "~/Content/AdminTemplate/assets/css/demo1/skins/header/base/light.css",
                   "~/Content/AdminTemplate/assets/css/demo1/skins/header/menu/light.css",
                   "~/Content/AdminTemplate/assets/css/demo1/skins/brand/dark.css",
                   "~/Content/AdminTemplate/assets/css/demo1/skins/aside/dark.css"
                   ));

            bundles.Add(new StyleBundle("~/bundles/login-css").Include(
                     "~/Content/AdminTemplate/assets/css/demo1/pages/general/login/login-4.css"
                     ));
            bundles.Add(new StyleBundle("~/bundles/global-bundle").Include(
                   "~/Content/AdminTemplate/assets/css/demo1/style.bundle.css"
                   ));



        }
    }
}