using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SelfOrganization.Models;
using WebMatrix.WebData;

namespace SelfOrganization
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebSecurityInitializer.Instance.EnsureInitialize();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        public class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                using (var context = new UsersContext())
                    context.UserProfiles.Find(1);

                if (!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
        }

        // Call this with WebSecurityInitializer.Instance.EnsureInitialize()
        public class WebSecurityInitializer
        {
            private WebSecurityInitializer() { }
            public static readonly WebSecurityInitializer Instance = new WebSecurityInitializer();
            private bool isNotInit = true;
            private readonly object SyncRoot = new object();
            public void EnsureInitialize()
            {
                if (isNotInit)
                {
                    lock (this.SyncRoot)
                    {
                        if (isNotInit)
                        {
                            isNotInit = false;
                            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                                userTableName: "UserProfile", userIdColumn: "UserId", userNameColumn: "UserName",
                                autoCreateTables: true);
                        }
                    }
                }
            }
        }
    }
}