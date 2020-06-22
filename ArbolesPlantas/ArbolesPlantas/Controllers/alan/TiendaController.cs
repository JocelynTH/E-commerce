using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArbolesPlantas.Controllers.alan
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index(string categoria)
        {
            ViewBag.categoria = categoria;
            return View("~/Views/Shared/alan/tienda/Index.cshtml");
        }
    }
}