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
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id");
            return View();
        }

        // POST: OrdenPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_pago,id_factura,cantidad_pago,cantidad_restante,status")] ordenPago ordenPago)
        {
            if (ModelState.IsValid)
            {
                db.ordenPago.Add(ordenPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", ordenPago.id_factura);
            return View(ordenPago);
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
