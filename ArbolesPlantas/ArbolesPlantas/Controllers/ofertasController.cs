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
    public class ofertasController : Controller
    {
        private Entities db = new Entities();

        // GET: ofertas
        public ActionResult Index()
        {
            var ofertas = db.ofertas.Include(o => o.Productos);
            return View(ofertas.ToList());
        }
        public ActionResult HistorialOfertas()
        {
            var ofertas = db.ofertas.Include(o => o.Productos);
            return View(ofertas.ToList());
        }

        public ActionResult FinanzasOfertas()
        {
            var listafiltro = (from p in db.ofertas where p.status == 1 select p).ToList();
            return View(listafiltro);
        }

        public ActionResult AprobarStatus(int? id)
        {
            ofertas selected = db.ofertas.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE ofertas SET status = 3 WHERE id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasOfertas", "ofertas");

        }
        public ActionResult RechazarStatus(int? id)
        {
            //            Compras selected = db.Compras.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE ofertas SET status = 2 WHERE id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasOfertas", "ofertas");

        }

        // GET: ofertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas ofertas = db.ofertas.Find(id);
            if (ofertas == null)
            {
                return HttpNotFound();
            }
            return View(ofertas);
        }

        // GET: ofertas/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre");
            return View();
        }

        // POST: ofertas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_producto,nombreOferta,fechaAlta,fechaBaja,tipoOferta,porcentajeDescuento,status")] ofertas ofertas)
        {
            if (ModelState.IsValid)
            {
                db.ofertas.Add(ofertas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", ofertas.id_producto);
            return View(ofertas);
        }

        // GET: ofertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas ofertas = db.ofertas.Find(id);
            if (ofertas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", ofertas.id_producto);
            return View(ofertas);
        }

        // POST: ofertas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_producto,nombreOferta,fechaAlta,fechaBaja,tipoOferta,porcentajeDescuento,status")] ofertas ofertas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ofertas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", ofertas.id_producto);
            return View(ofertas);
        }

        // GET: ofertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ofertas ofertas = db.ofertas.Find(id);
            if (ofertas == null)
            {
                return HttpNotFound();
            }
            return View(ofertas);
        }

        // POST: ofertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ofertas ofertas = db.ofertas.Find(id);
            db.ofertas.Remove(ofertas);
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
