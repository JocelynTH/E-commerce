using ArbolesPlantas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArbolesPlantas.Controllers.alan
{
    public class TiendaController : Controller
    {

        Entities db = new Models.Entities();

        // GET: Tienda
        public ActionResult Index(string categoria)
        {

            ViewBag.categoria = categoria;

            if (categoria == "plantas")
            {
                ViewBag.productos = Productos.getPlantas();
            }
            else if (categoria == "arboles")
            {
                ViewBag.productos = Productos.getArboles();
            }

            return View("~/Views/Tienda/Index.cshtml");
        }

        public ActionResult Show(int id)
        {
            Productos producto = Productos.getProducto(id);

            ViewBag.producto = producto;

            return View("~/Views/Tienda/Show.cshtml");
        }


        [HttpPost]
        public ActionResult eliminarDelCarrito(int id)
        {
            Carrito.deleteItem(id);
            return RedirectToAction("ShowCarrito", "Tienda");
        }

        [HttpPost]
        public ActionResult agregarProductoAlCarrito(int id, int cantidad)
        {
            Productos producto = Productos.getProducto(id);
            ItemCarrito item = new ItemCarrito(producto, cantidad);

            Carrito.agregarItemAlCarrito(item);

            ViewBag.message = "Carrito actualizado correctamente";
            return RedirectToAction("Show", "Tienda", new { id = id });
        }

        public ActionResult ShowCarrito()
        {
            ViewBag.carrito = Carrito.getCarrito();
            return View("~/Views/Carrito/Index.cshtml");
        }
    }
}