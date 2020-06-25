using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinanzas.Models;

namespace ProyectoFinanzas.Controllers
{
    public class SeguimientoesController : Controller
    {
        private Entities db = new Entities();

        // GET: Seguimientoes
        public ActionResult Index()
        {
            var seguimiento = db.Seguimiento.Include(s => s.detalleCompra);
            return View(seguimiento.ToList());
        }
        public ActionResult SeguimientoPedido()
        {
            var seguimiento = db.Seguimiento.Include(s => s.detalleCompra);
            return View();
        }

        // GET: Seguimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            return View(seguimiento);
        }

        // GET: Seguimientoes/Create
        public ActionResult Create()
        {
            ViewBag.id_detalleCompra = new SelectList(db.detalleCompra, "Id", "Id");
            return View();
        }

        // POST: Seguimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_detalleCompra,guia,fechaSalida,fechaLlegada,destino,origen,status")] Seguimiento seguimiento)
        {
            if (ModelState.IsValid)
            {
                db.Seguimiento.Add(seguimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_detalleCompra = new SelectList(db.detalleCompra, "Id", "Id", seguimiento.id_detalleCompra);
            return View(seguimiento);
        }

        // GET: Seguimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_detalleCompra = new SelectList(db.detalleCompra, "Id", "Id", seguimiento.id_detalleCompra);
            return View(seguimiento);
        }

        // POST: Seguimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_detalleCompra,guia,fechaSalida,fechaLlegada,destino,origen,status")] Seguimiento seguimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seguimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_detalleCompra = new SelectList(db.detalleCompra, "Id", "Id", seguimiento.id_detalleCompra);
            return View(seguimiento);
        }

        // GET: Seguimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            return View(seguimiento);
        }

        // POST: Seguimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            db.Seguimiento.Remove(seguimiento);
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
