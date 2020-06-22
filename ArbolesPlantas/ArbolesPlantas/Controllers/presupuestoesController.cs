using ProyectoFinanzas.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProyectoFinanzas.Controllers
{
    public class presupuestoesController : Controller
    {
        private Entities db = new Entities();


        // GET: presupuestoes
        public ActionResult Index()
        {
            var presupuesto = db.presupuesto.Include(p => p.Facturas);
            
            return View(presupuesto.ToList());
        }


        public ActionResult FinanzasCompras()
        {

            // var presupuesto = db.presupuesto.Include(p => p.Facturas);
            var listafiltro = (from p in db.presupuesto where p.status == 1 select p).ToList();
            return View(listafiltro);
        }

        public ActionResult HistorialPresupuestos()
        {

            var presupuesto = db.presupuesto.Include(p => p.Facturas);
            return View(presupuesto.ToList());
        }


        // GET: presupuestoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto presupuesto = db.presupuesto.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        // GET: presupuestoes/Create
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.Facturas, "Id", "Id");
            return View();
        }

        // POST: presupuestoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_factura,fechaAlta,areaSolicita,presupuestoInicial,presupuestoActual,status,total")] presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                db.presupuesto.Add(presupuesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.Facturas, "Id", "Id", presupuesto.id_factura);
            return View(presupuesto);
        }

        // GET: presupuestoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto presupuesto = db.presupuesto.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.Facturas, "Id", "Id", presupuesto.id_factura);
            return View(presupuesto);
        }

        public ActionResult AprobarStatus(int? id)
        {
            presupuesto selected = db.presupuesto.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE presupuesto SET status = 3 WHERE id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasCompras", "presupuestoes");

        }
        public ActionResult RechazarStatus(int? id)
        {
            //            Compras selected = db.Compras.Find(id);

            db.Database.ExecuteSqlCommand("UPDATE presupuesto SET status = 2 WHERE id=" + id);
            db.SaveChanges();
            return RedirectToAction("FinanzasCompras", "presupuestoes");

        }


        // POST: presupuestoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_factura,fechaAlta,areaSolicita,presupuestoInicial,presupuestoActual,status,total")] presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.Facturas, "Id", "Id", presupuesto.id_factura);
            return View(presupuesto);
        }

        // GET: presupuestoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto presupuesto = db.presupuesto.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        // POST: presupuestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            presupuesto presupuesto = db.presupuesto.Find(id);
            db.presupuesto.Remove(presupuesto);
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
