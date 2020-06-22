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
    public class DetalleFacturasController : Controller
    {
        private Entities db = new Entities();

        // GET: DetalleFacturas
        public ActionResult Index()
        {
            var detalleFactura = db.DetalleFactura.Include(d => d.Factura).Include(d => d.Productos);
            return View(detalleFactura.ToList());
        }

		// GET: DetalleFacturas
		public ActionResult DetallesFac(int id_factura)
		{
			List<detalleCompra> Final = new List<detalleCompra>();
			List<detalleCompra> lista = db.detalleCompra.ToList();
			List<DetalleFactura> listaDF = db.DetalleFactura.ToList();
			List<DetalleFactura> FinalDF = new List<DetalleFactura>();
			List<Compras> compras = db.Compras.ToList();
			List<Factura> facturas = db.Factura.ToList();
			var id_proveedor = 0;
			var proveedor = " ";
			var total = 0.0;

			//foreach (detalleCompra d in lista)
			//{
			//	if (d.id_compra == id_compra)
			//	{
			//		Final.Add(d);
			//	}
			//}
			foreach (DetalleFactura d in listaDF)
			{
				if (d.Factura.Id == id_factura)
				{
					FinalDF.Add(d);
				}
			}
			//foreach (Factura f in facturas)
			//{
			//	if (f.Id == id_factura)
			//	{
			//		id_proveedor = f.Compras.id_proveedor;
			//		impuestos = f.Compras.impuesto;
			//		subtotal = f.Compras.subtotal;
			//		total = f.Compras.total;
			//		break;
			//	}
			//}
			foreach (Proveedores p in db.Proveedores.ToList())
			{
				if (p.Id == id_proveedor)
				{
					proveedor = p.nombre_corto;
					break;
				}
			}
			var factura = db.Factura.Find(id_factura);
			ViewBag.id_compra = factura.Compras.Id;
			ViewBag.proveedor = factura.Compras.Proveedores.razon_social;
			ViewBag.total = factura.total;

			return View(FinalDF.ToList());

			//var detalleCompra = db.detalleCompra.Include(d => d.Compras).Include(d => d.Productos);
			//return View(detalleCompra.ToList());
		}

		// GET: DetalleFacturas/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Create
        public ActionResult Create()
        {
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id");
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre");
            return View();
        }

        // POST: DetalleFacturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_factura,id_producto,cantidad,recibido,subtotal,motivo")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                db.DetalleFactura.Add(detalleFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", detalleFactura.id_factura);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleFactura.id_producto);
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", detalleFactura.id_factura);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleFactura.id_producto);
            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_factura,id_producto,cantidad,recibido,subtotal,motivo")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_factura = new SelectList(db.Factura, "Id", "Id", detalleFactura.id_factura);
            ViewBag.id_producto = new SelectList(db.Productos, "Id", "nombre", detalleFactura.id_producto);
            return View(detalleFactura);
        }

        // GET: DetalleFacturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            if (detalleFactura == null)
            {
                return HttpNotFound();
            }
            return View(detalleFactura);
        }

        // POST: DetalleFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleFactura detalleFactura = db.DetalleFactura.Find(id);
            db.DetalleFactura.Remove(detalleFactura);
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
