using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArbolesPlantas.Models;

namespace ArbolesPlantas.Controllers
{
    public class FacturasController : Controller
    {
        private Entities db = new Entities();

        // GET: Facturas
        public ActionResult Index()
        {
            var factura = db.Factura.Include(f => f.Compras);
            return View(factura.ToList());
        }

		public ActionResult facturasPorPagar()
		{
			var factura = db.Factura.Include(f => f.Compras);
			var fac = (from f in db.Factura
					   where f.status == 0
					   orderby f.fecha_pago descending
					   orderby f.Compras.Proveedores.razon_social ascending
					   select f).ToList();

			return View(fac);
			//return View(factura.ToList());
		}

		public ActionResult facturasPendientes()
		{
			if (User.Identity.IsAuthenticated)
			{
				if (User.IsInRole("PagoProveedores"))
				{

					var factura = db.Factura.Include(f => f.Compras);
					var fac = (from f in db.Factura
							   where f.status == 1
							   orderby f.fecha_pago descending
							   orderby f.Compras.Proveedores.razon_social ascending
							   select f).ToList();

					return View(fac);

				}
				else
				{
					System.Diagnostics.Debug.WriteLine("No Autentificado otro rol");
					return RedirectToAction("sinPrivilegios", "Home");
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("No Autentificado");
				return RedirectToAction("Login", "Account");
			}
		}

		public ActionResult facturasPagadas()
		{
			if (User.Identity.IsAuthenticated)
			{
				if (User.IsInRole("PagoProveedores"))
				{

					var factura = db.Factura.Include(f => f.Compras);
					var fac = (from f in db.Factura
							   where f.status == 2
							   orderby f.fecha_pago descending
							   orderby f.Compras.Proveedores.razon_social ascending
							   select f).ToList();
					return View(fac);

				}
				else
				{
					System.Diagnostics.Debug.WriteLine("No Autentificado otro rol");
					return RedirectToAction("sinPrivilegios", "Home");
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("No Autentificado");
				return RedirectToAction("Login", "Account");
			}
		}

		// GET: Facturas/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_recepcion,fecha_pago,id_compra,total,status")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", factura.id_compra);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", factura.id_compra);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_recepcion,fecha_pago,id_compra,total,status")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", factura.id_compra);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
