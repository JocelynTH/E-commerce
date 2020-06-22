using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class PresupuestosController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Presupuestos
        public ActionResult Index()
        {
            var presupuestos = db.Presupuestos.Include(p => p.Proveedores);
            return View(presupuestos.ToList());
        }

        // GET: Presupuestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            return View(presupuestos);
        }

        // GET: Presupuestos/Create
        public ActionResult Create()
        {
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc");
            return View();
        }

        // POST: Presupuestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_proveedor,presupuesto")] Presupuestos presupuestos)
        {
            if (ModelState.IsValid)
            {
                db.Presupuestos.Add(presupuestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", presupuestos.id_proveedor);
            return View(presupuestos);
        }

        // GET: Presupuestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", presupuestos.id_proveedor);
            return View(presupuestos);
        }

        // POST: Presupuestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_proveedor,presupuesto")] Presupuestos presupuestos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", presupuestos.id_proveedor);
            return View(presupuestos);
        }

        // GET: Presupuestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            return View(presupuestos);
        }

        // POST: Presupuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            db.Presupuestos.Remove(presupuestos);
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
