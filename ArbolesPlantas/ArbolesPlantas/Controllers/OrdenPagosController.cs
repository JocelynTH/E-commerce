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
    public class OrdenPagosController : Controller
    {
        private Entities db = new Entities();

        // GET: OrdenPagos
        public ActionResult Index()
        {
            var ordenPago = db.ordenPago.Include(o => o.Factura);
            return View(ordenPago.ToList());
        }

		// GET: OrdenPagos
		public ActionResult historialPagos()
		{
			var ordenPago = db.ordenPago.Include(o => o.Factura);
			var orden = (from o in db.ordenPago
						 where o.status == 1
						 select o).ToList();
			return View(orden);
		}

		// GET: OrdenPagos
		public ActionResult pagosPendientes()
		{
			var ordenPago = db.ordenPago.Include(o => o.Factura);
			var orden = (from o in db.ordenPago
						 where o.status == 0
						 select o).ToList();
			return View(orden);
		}

		// GET: OrdenPagos/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenPago ordenPago = db.ordenPago.Find(id);
            if (ordenPago == null)
            {
                return HttpNotFound();
            }
            return View(ordenPago);
        }

        // GET: OrdenPagos/Create
        public ActionResult Create(int? id)
        {
			//ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id");
			//return View();


			//if (User.Identity.IsAuthenticated)
			//{
			//	if (User.IsInRole("PagoProveedores"))
			//	{
					
					Factura factura = db.Factura.Find(id);
					ordenPago o = new ordenPago();
					o.id_factura = factura.Id;
					o.cantidad_pago = factura.total;
					o.cantidad_restante = 0;

					return View(o);
					
			//	}
			//	else
			//	{
			//		System.Diagnostics.Debug.WriteLine("No Autentificado otro rol");
			//		return RedirectToAction("sinPrivilegios", "Home");
			//	}
			//}
			//else
			//{
			//	System.Diagnostics.Debug.WriteLine("No Autentificado");
			//	return RedirectToAction("Login", "Account");
			//}
		}

        // POST: OrdenPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_pago,id_factura,cantidad_pago,cantidad_restante,status")] ordenPago ordenPago)
        {
			DateTime dateTime = DateTime.Today;

			System.Diagnostics.Debug.WriteLine(dateTime.ToString("dd/MM/yyyy"));
			System.Diagnostics.Debug.WriteLine("Fecha pago" + ordenPago.fecha_pago);
			System.Diagnostics.Debug.WriteLine("ID fatura" + ordenPago.id_factura);
			System.Diagnostics.Debug.WriteLine("Cantidad pago" + ordenPago.cantidad_pago);
			System.Diagnostics.Debug.WriteLine("Cantidad restante" + ordenPago.cantidad_restante);
			System.Diagnostics.Debug.WriteLine("Status " + ordenPago.status);
			ordenPago.fecha_pago = dateTime;
			ordenPago.status = 0;
			ordenPago.cantidad_restante = 0;

			db.ordenPago.Add(ordenPago);
			db.SaveChanges();

			//status de factura 0: no pagada, 1: esperando aprobaciòn, 2: aprobada, 3: rechazada
			Factura facturaP = db.Factura.Find(ordenPago.id_factura);
			facturaP.status = 1;
			db.Entry(facturaP).State = EntityState.Modified;
			db.SaveChanges();

			return RedirectToAction("pagosPendientes", "OrdenPagos");
		}

        // GET: OrdenPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenPago ordenPago = db.ordenPago.Find(id);
            if (ordenPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", ordenPago.id_factura);
            return View(ordenPago);
        }

        // POST: OrdenPagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_pago,id_factura,cantidad_pago,cantidad_restante,status")] ordenPago ordenPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", ordenPago.id_factura);
            return View(ordenPago);
        }

        // GET: OrdenPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenPago ordenPago = db.ordenPago.Find(id);
            if (ordenPago == null)
            {
                return HttpNotFound();
            }
            return View(ordenPago);
        }

        // POST: OrdenPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ordenPago ordenPago = db.ordenPago.Find(id);
            db.ordenPago.Remove(ordenPago);
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
