﻿using System.Web;
using System.Web.Optimization;

namespace Project.Genius.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui-{version}.js",
						"~/Scripts/jquery.*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
					  "~/Scripts/bootstrap-editable.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                      "~/Scripts/Theme/core.js",
                      "~/Scripts/Theme/custom.js"));

			bundles.Add(new ScriptBundle("~/bundles/pages/inbox").Include(
					  "~/Scripts/Pages/page-inbox.js"));

			bundles.Add(new ScriptBundle("~/bundles/pages/todo").Include(
					  "~/Scripts/Pages/page-todo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
					  "~/Content/bootstrap-editable.css",
					  "~/Content/font-awesome.css",
                      "~/Content/style.css",
					  "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
