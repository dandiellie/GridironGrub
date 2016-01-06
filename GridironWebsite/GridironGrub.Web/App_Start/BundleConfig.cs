using System.Web;
using System.Web.Optimization;

namespace GridironGrub.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/braintree").Include(
                      "~/Scripts/braintree.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-route.js",
                      "~/Scripts/angular-animate.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/angular-moment.js",
                      "~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                      "~/app/app.js",
                      // Services
                      "~/app/components/login/loginService.js",
                      "~/app/components/password/passwordService.js",
                      "~/app/components/admin/employees/employeesService.js",
                      "~/app/components/admin/orders/ordersService.js",
                      "~/app/components/admin/park/parkService.js",
                      "~/app/components/customer/shop/shopService.js",
                      "~/app/components/customer/order/orderService.js",
                      "~/app/components/manager/managerService.js",
                      "~/app/components/runner/runnerService.js",
                      "~/app/components/vendor/vendorService.js",
                      // Controllers
                      "~/app/components/login/loginController.js",
                      "~/app/components/password/passwordController.js",
                      "~/app/components/admin/employees/employeesController.js",
                      "~/app/components/admin/orders/ordersController.js",
                      "~/app/components/admin/park/parkController.js",
                      "~/app/components/customer/shop/shopController.js",
                      "~/app/components/customer/order/orderController.js",
                      "~/app/components/manager/managerController.js",
                      "~/app/components/runner/runnerController.js",
                      "~/app/components/vendor/vendorController.js"));
        }
    }
}
