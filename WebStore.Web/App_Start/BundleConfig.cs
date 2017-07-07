using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebStore.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                //"~/Scripts/vendors/jquery.js",
                //"~/Scripts/vendors/bootstrap.js",
                "~/Scripts/vendors/toastr.js",
                "~/Scripts/vendors/jquery.raty.js",
                "~/Scripts/vendors/respond.src.js",
                "~/Scripts/vendors/angular.js",
                "~/Scripts/vendors/angular-route.js",
                "~/Scripts/vendors/angular-ui-router.js",
                "~/Scripts/vendors/angular-cookies.js",
                "~/Scripts/vendors/angular-validator.js",
                "~/Scripts/vendors/angular-base64.js",
                "~/Scripts/vendors/angular-file-upload.js",
                "~/Scripts/vendors/angucomplete-alt.min.js",
                "~/Scripts/vendors/ui-bootstrap-tpls-0.13.1.js",
                "~/Scripts/vendors/underscore.js",
                "~/Scripts/vendors/raphael.js",
                "~/Scripts/vendors/morris.js",
                "~/Scripts/vendors/jquery.fancybox.js",
                "~/Scripts/vendors/jquery.fancybox-media.js",
                "~/Scripts/vendors/loading-bar.js",
                "~/Scripts/vendors/bootbox.js",
                "~/Scripts/vendors/ngBootbox.js",
                "~/Scripts/vendors/dirPagination.js",
                "~/Scripts/vendors/ckeditor/ckeditor.js",
                "~/Scripts/vendors/ckfinder/ckfinder.js",
                "~/Scripts/vendors/ngCkeditor/ng-ckeditor.js"                
                ));

            //bundles.Add(new ScriptBundle("~/bundles/spa").Include(
            //    "~/Scripts/spa/modules/common.core.js",
            //    "~/Scripts/spa/modules/common.ui.js",
            //    "~/Scripts/spa/app.js",
            //    "~/Scripts/spa/services/apiService.js",
            //    "~/Scripts/spa/services/notificationService.js",
            //    "~/Scripts/spa/services/membershipService.js",
            //    "~/Scripts/spa/services/fileUploadService.js",
            //    "~/Scripts/spa/layout/topBar.directive.js",
            //    "~/Scripts/spa/layout/sideBar.directive.js",
            //    "~/Scripts/spa/layout/customPager.directive.js",
            //    "~/Scripts/spa/directives/rating.directive.js",
            //    "~/Scripts/spa/directives/availableMovie.directive.js",
            //    "~/Scripts/spa/account/loginCtrl.js",
            //    "~/Scripts/spa/account/registerCtrl.js",
            //    "~/Scripts/spa/home/rootCtrl.js",
            //    "~/Scripts/spa/home/indexCtrl.js",
            //    "~/Scripts/spa/customers/customersCtrl.js",
            //    "~/Scripts/spa/customers/customersRegCtrl.js",
            //    "~/Scripts/spa/customers/customerEditCtrl.js",
            //    "~/Scripts/spa/movies/moviesCtrl.js",
            //    "~/Scripts/spa/movies/movieAddCtrl.js",
            //    "~/Scripts/spa/movies/movieDetailsCtrl.js",
            //    "~/Scripts/spa/movies/movieEditCtrl.js",
            //    "~/Scripts/spa/controllers/rentalCtrl.js",
            //    "~/Scripts/spa/rental/rentMovieCtrl.js",
            //    "~/Scripts/spa/rental/rentStatsCtrl.js"
            //    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                //"~/content/css/site.css",
                //"~/content/css/bootstrap.css",
                //"~/content/css/bootstrap-theme.css",
                // "~/content/css/font-awesome.css",
                "~/content/css/morris.css",
                "~/content/css/toastr.css",
                "~/content/css/jquery.fancybox.css",
                "~/content/css/loading-bar.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}