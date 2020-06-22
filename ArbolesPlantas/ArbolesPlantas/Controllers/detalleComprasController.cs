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
    public class detalleComprasController : Controller
    {
        private Entities db = new Entities();

        // GET: detalleCompras
        public ActionResult Index()
        {
            var detalleCompra = db.detalleCompra.Include(d => d.Compras).Include(d => d.Productos);
            return View(detalleCompra.ToList());
        }

        // GET: detalleCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleCompra detalleCompra = db.detalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // GET: detalleCompras/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id");
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre");
            return View();
        }

        // POST: detalleCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_compra,id_producto,cantidad,precio_compra,status")] detalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.detalleCompra.Add(detalleCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", detalleCompra.id_compra);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleCompra.id_producto);
            return View(detalleCompra);
        }

        // GET: detalleCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleCompra detalleCompra = db.detalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", detalleCompra.id_compra);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleCompra.id_producto);
            return View(detalleCompra);
        }

        // POST: detalleCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_compra,id_producto,cantidad,precio_compra,status")] detalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", detalleCompra.id_compra);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleCompra.id_producto);
            return View(detalleCompra);
        }

        // GET: detalleCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleCompra detalleCompra = db.detalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // POST: detalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalleCompra detalleCompra = db.detalleCompra.Find(id);
            db.detalleCompra.Remove(detalleCompra);
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
