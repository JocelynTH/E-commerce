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
    public class DetalleProductoesController : Controller
    {
        private Entities db = new Entities();

        // GET: DetalleProductoes
        public ActionResult Index()
        {
            var detalleProducto = db.DetalleProducto.Include(d => d.Productos);
            return View(detalleProducto.ToList());
        }

        public ActionResult FinanzasProductos()
        {
            var listafiltro = (from p in db.DetalleProducto where p.status == 1 where p.porcentajeDescuento == 0 select p).ToList();
            return View(listafiltro);
        }
        public ActionResult FinanzasProductosOferta()
        {
            var listafiltro = (from p in db.DetalleProducto where p.status == 1 where p.porcentajeDescuento!=0 select p).ToList();
            return View(listafiltro);
        }
        public ActionResult HistorialProductos()
        {
            var detalleProducto = db.DetalleProducto.Include(d => d.Productos);
            return View(detalleProducto.ToList());
        }

        public ActionResult AprobarStatus(int? id)
        {
            DetalleProducto selected = db.DetalleProducto.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE DetalleProducto SET status = 3 WHERE Id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasProductos", "DetalleProductoes");

        }
        public ActionResult RechazarStatus(int? id)
        {
            //            Compras selected = db.Compras.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE DetalleProducto SET status = 2 WHERE Id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasProductos", "DetalleProductoes");

        }

        // GET: DetalleProductoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre");
            return View();
        }

        // POST: DetalleProductoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_producto,fechaAlta,areaSolicita,precio_anterior,precio_nuevo,status,razon,porcentajeDescuento")] DetalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.DetalleProducto.Add(detalleProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleProducto.id_producto);
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleProducto.id_producto);
            return View(detalleProducto);
        }

        // POST: DetalleProductoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_producto,fechaAlta,areaSolicita,precio_anterior,precio_nuevo,status,razon,porcentajeDescuento")] DetalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleProducto.id_producto);
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // POST: DetalleProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            db.DetalleProducto.Remove(detalleProducto);
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
