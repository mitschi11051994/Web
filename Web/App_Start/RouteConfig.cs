using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            



            routes.MapRoute(
                name: "Entrar Usuario",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblLogin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Crear_Usuario",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblLogin/Index", action = "Create", id = UrlParameter.Optional }
            );




            routes.MapRoute(
                name: "Cliente",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblClient", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "crear Cliente",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblClient", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Editarear Cliente",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblClient", action = "Edit", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Delite Cliente",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblClient", action = "Delete", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "tblContacts",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblContact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "crear tblContacts",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblContact", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "editar tblContacts",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblContact", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "eliminar tblContacts",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblContact", action = "Delete", id = UrlParameter.Optional }
            );




            routes.MapRoute(
                name: "tblReunions1",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblReunion", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "crear tblReunions1",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblReunion", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "EDITAR tblReunions1",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "tblReunion", action = "Edit", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ELIMINAR tblReunions1",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "tblReunion", action = "Delete", id = UrlParameter.Optional }
           );



            routes.MapRoute(
               name: "tblSupport_Tickets",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "tblSupport_Ticket", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "crear tblSupport_Tickets",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "tblSupport_Ticket/Index/", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}
