using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArbolesPlantas
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Tienda",
                url: "tienda/{categoria}",
                defaults: new { controller = "Tienda", action = "Index", categoria = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Producto",
                url: "producto/{id}",
                defaults: new { controller = "Tienda", action = "Show" }
            );

            routes.MapRoute(
                name: "ShowCarrito",
                url: "carrito",
                defaults: new { controller = "Tienda", action = "ShowCarrito" }
            );

            routes.MapRoute(
                name: "AgregarAlCarrito",
                url: "agregarProducto",
                defaults: new { controller = "Tienda", action = "agregarProductoAlCarrito" }
            );

            routes.MapRoute(
                name: "EliminarDelCarrito",
                url: "eliminarProducto",
                defaults: new { controller = "Tienda", action = "eliminarDelCarrito" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
