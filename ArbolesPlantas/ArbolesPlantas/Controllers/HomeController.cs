using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArbolesPlantas.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "PagoProveedores,Compras")]
		public ActionResult homeAdmin()
		{
			ViewBag.nombre = User.Identity.Name;
			return View();
		}

		public ActionResult sinPrivilegios()
		{
			ViewBag.Message = "No tienes privilegíos para ingresar a esta sección";

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}