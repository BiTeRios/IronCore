using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using IronCore.BusinessLogic.DBModel;
using IronCore.Mappings;

namespace IronCore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IMapper MapperInstance { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            MapperInstance = config.CreateMapper();
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;                     

            FormsAuthenticationTicket ticket;
            try
            {
                ticket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;                                         
            }
            if (ticket == null || ticket.Expired) return;      

            string[] roles = string.IsNullOrEmpty(ticket.UserData)
                             ? new string[] { }                 
                             : ticket.UserData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var identity = new FormsIdentity(ticket);
            var principal = new GenericPrincipal(identity, roles);

            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;
        }
    }
}
