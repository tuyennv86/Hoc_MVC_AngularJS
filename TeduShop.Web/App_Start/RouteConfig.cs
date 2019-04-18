using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeduShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Login",
               url: "dang-nhap.html",
               defaults: new { controller = "Acount", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Contac",
              url: "Lien-he.html",
              defaults: new { controller = "Contac", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "Register",
               url: "dang-ky.html",
               defaults: new { controller = "Acount", action = "Register", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ShoppongCart",
               url: "gio-hang.html",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "About",
               url: "gioi-thieu.html",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }//neu nhieu controler cung ten thi them namspace vao khong trung thi thoi
           );
            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Product",
               url: "{alias}.p-{id}.html",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Tags",
              url: "Tags/{tag}.html",
              defaults: new { controller = "Product", action = "ListProductTag", tag = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
