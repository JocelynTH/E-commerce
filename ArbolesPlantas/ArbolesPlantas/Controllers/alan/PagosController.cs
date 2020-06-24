using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArbolesPlantas.Controllers.alan
{
    public class PagosController : Controller
    {
        [HttpPost]
        public ActionResult Index(double monto)
        {
            ViewBag.monto = monto;
            return View("~/Views/Pagos/Index.cshtml");
        }
    }
}