using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NH4CookbookHelpers.Queries.Model;

namespace SetUpSession
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SessionFactory = ProductModel
             .CreateExampleSessionFactory(true, conf => {
                 conf.SetProperty("current_session_context_class", "web");
             });
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest(object sender,
EventArgs e)
        {
            //var session = CurrentSessionContext.Unbind(SessionFactory);
            //session.Dispose();
        }
    }
}
