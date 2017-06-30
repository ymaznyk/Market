using eMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace eMarket
{
    public class Market : System.Web.HttpApplication
    {
        public static ApplicationDbContext DbContext
        {
            get
            {
                if (!HttpContext.Current.Items.Contains("_EntityContext"))
                {
                    HttpContext.Current.Items.Add("_EntityContext", new ApplicationDbContext());
                }
                return HttpContext.Current.Items["_EntityContext"] as ApplicationDbContext;
            }
            set { }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected virtual void Application_BeginRequest()
        {
            HttpContext.Current.Items["_EntityContext"] = new ApplicationDbContext();
        }

        protected virtual void Application_EndRequest()
        {
            var entityContext = HttpContext.Current.Items["_EntityContext"] as ApplicationDbContext;
            if (entityContext != null)
                entityContext.Dispose();
        }
    }
}
