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
    public class detalleComprasController : Controller
    {
        
        private Entities1 db = new Entities1();

        // GET: detalleCompras
        public ActionResult Index(int id_compra)
        {

            List<detalleCompra> Final = new List<detalleCompra>();
            List<detalleCompra> lista = db.detalleCompra.ToList();
            List<Compras> compras = db.Compras.ToList();
            var id_proveedor = 0;
            var proveedor = " ";
            var total = 0.0;
            var subtotal = 0.0;
            var impuestos = 0.0;

            foreach (detalleCompra d in lista)
            {
                if (d.id_compra == id_compra)
                {
                    Final.Add(d);
                }
            }
            foreach (Compras c in compras)
            {
                if (c.Id == id_compra)
                {
                    id_proveedor = c.id_proveedor;
                    impuestos = c.impuesto;
                    subtotal = c.subtotal;
                    total = c.total;

                    break;
                }
            }
            foreach (Proveedores p in db.Proveedores.ToList())
            {
                if (p.Id == id_proveedor)
                {
                    proveedor = p.nombre_corto;
                    break;
                }
            }
            ViewBag.id_compra = id_compra;
            ViewBag.proveedor = proveedor;
            ViewBag.total = total;
            ViewBag.subtotal = subtotal;
            ViewBag.impuetos = impuestos;

            return View(Final.ToList());
            
        }

        public ActionResult verificar(int id_compra)
        {

            List<detalleCompra> Final = new List<detalleCompra>();
            List<detalleCompra> lista = db.detalleCompra.ToList();
            List<Compras> compras = db.Compras.ToList();
            var id_proveedor = 0;
            var proveedor = " ";
            var total = 0.0;
            var subtotal = 0.0;
            var impuestos = 0.0;

            foreach (detalleCompra d in lista)
            {
                if (d.id_compra == id_compra)
                {
                    Final.Add(d);
                }
            }
            foreach (Compras c in compras)
            {
                if (c.Id == id_compra)
                {
                    id_proveedor = c.id_proveedor;
                    impuestos = c.impuesto;
                    subtotal = c.subtotal;
                    total = c.total;

                    break;
                }
            }
            foreach (Proveedores p in db.Proveedores.ToList())
            {
                if (p.Id == id_proveedor)
                {
                    proveedor = p.nombre_corto;
                    break;
                }
            }
            ViewBag.id_compra = id_compra;
            ViewBag.proveedor = proveedor;
            ViewBag.total = total;
            ViewBag.subtotal = subtotal;
            ViewBag.impuetos = impuestos;

            return View(Final.OrderBy(comp=> comp.Compras.id_proveedor==id_proveedor).ToList());

        }

        [HttpPost]
        public ActionResult verificar(FormCollection form)
        {
            int cont = int.Parse(form["valorCont"]);
            string id_compra = form["idCompra"];
            Factura factura = new Factura();
            factura.fecha_recepcion= System.DateTime.Now;
            factura.fecha_pago= System.DateTime.Now;
            factura.id_compra = int.Parse(id_compra);
            factura.status = 0;
            
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                factura = db.Factura.OrderByDescending(facturas => facturas.Id).ToList().First();
                var id_factura = factura.Id;
                DetalleFactura detalle;
                var total = 0.0;
                for (int i = 1; i < cont; i++)
                {
                    detalle = new DetalleFactura();
                    detalle.cantidad = int.Parse(form["cantidadP" + i]);
                    detalle.recibido = int.Parse(form["recibido" + i]);
                    detalle.faltante = int.Parse(form["faltante" + i]);
                    detalle.motivo = form["motivo" + i];
                    detalle.id_producto = int.Parse(form["idProducto" + i]);
                    detalle.id_factura = id_factura;
                    var precio = float.Parse(form["precioP" + i]);
                    var recibido = int.Parse(form["recibido" + i]);
                    detalle.subtotal = (precio*recibido);

                    total += detalle.subtotal;
                    db.DetalleFactura.Add(detalle);
                    db.SaveChanges();

                    Productos producto = db.Productos.Find(int.Parse(form["idProducto" + i]));
                    var cambio = int.Parse(form["recibido" + i]) + producto.cantidad;
                    db.Database.ExecuteSqlCommand("UPDATE Productos SET cantidad=" + cambio + " WHERE Id=" + int.Parse(form["idProducto" + i]));
                }
                db.Database.ExecuteSqlCommand("UPDATE Factura SET total=" + total + " WHERE Id=" + id_factura);
                db.Database.ExecuteSqlCommand("UPDATE Compras SET status=" + 5 + " WHERE Id=" + int.Parse(id_compra));

                return RedirectToAction("llegadas", "Compras");
            }
            else
            {
                return Content("Datos no ingresados");
            }
        }

        public ActionResult detallesDePedido(int id_compra)
        {
            List<detalleCompra> Final = new List<detalleCompra>();
            List<detalleCompra> lista = db.detalleCompra.ToList();
            List<Compras> compras = db.Compras.ToList();
            var id_proveedor = 0;
            var presupuesto = 0.0;
            var total = 0.0;
            var proveedor=" ";
            var proveedorTel=" ";
            var fecha = System.DateTime.Now;
            
            foreach (detalleCompra d in lista)
            {
                if (d.id_compra == id_compra)
                {
                    Final.Add(d);
                    total += d.precio_compra;
               }
            }
            foreach (Compras c in compras)
            {
                if (c.Id == id_compra)
                {
                    id_proveedor = c.id_proveedor;
                    fecha = c.fecha;
                    break;
                }
            }
            foreach (Proveedores p in db.Proveedores.ToList())
            {
                if (p.Id == id_proveedor)
                {
                    proveedor = p.nombre_corto;
                    proveedorTel = p.telefono;
                    break;
                }
            }
            foreach (Presupuestos pre in db.Presupuestos.ToList())
            {
                if (pre.id_proveedor == id_proveedor)
                {
                    presupuesto = pre.presupuesto;
                    break;
                }
            }
            ViewBag.presupuesto = presupuesto;
            ViewBag.id_compra = id_compra;
            ViewBag.proveedor = proveedor;
            ViewBag.proveedorTel = proveedorTel;
            ViewBag.fecha = fecha;
            ViewBag.total = total;
            return View(Final.ToList());
           
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
        
        public ActionResult Create(int id_compra)
        {
            //List<Productos> 
            var productos = TempData["var"] as List<Productos>;
            
            ViewBag.id_compra = id_compra;
            ViewBag.id_producto = new SelectList(productos, "Id", "nombre");
            ViewBag.lista = productos;

           // return Content(nombres);
            return View();
        }

        // POST: detalleCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_compra,id_producto,cantidad,precio_compra,status")] detalleCompra detalleCompra)
        {
            detalleCompra.status = 1;
            if (ModelState.IsValid)
            {
                db.detalleCompra.Add(detalleCompra);
                db.SaveChanges();

                List<Compras> compras = db.Compras.ToList();
                List<Productos> Final = new List<Productos>();
                List<Productos> lista = db.Productos.ToList();
                var id_proveedor=0;

                foreach (Compras c in compras){
                    if (c.Id == detalleCompra.id_compra)
                    {
                        id_proveedor = c.id_proveedor;
                        break;
                    }
                }
                foreach (Productos p in lista)
                {
                    if (p.id_proveedor == id_proveedor) 
                    {
                        Final.Add(p);
                    }
                }
                TempData["var"] = Final.ToList();
                return RedirectToAction("Create","detalleCompras", new { id_compra = detalleCompra.id_compra });

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
            List<Compras> compras = db.Compras.ToList();
            List<Productos> Final = new List<Productos>();
            List<Productos> lista = db.Productos.ToList();
            var id_proveedor = 0;

            foreach (Compras c in compras)
            {
                if (c.Id == detalleCompra.id_compra)
                {
                    id_proveedor = c.id_proveedor;
                    break;
                }
            }
            foreach (Productos p in lista)
            {
                if (p.id_proveedor == id_proveedor)
                {
                    Final.Add(p);
                }
            }

            ViewBag.id_compra = detalleCompra.id_compra;
            //ViewBag.id_compra = new SelectList(db.Compras, "Id", "Id", detalleCompra.id_compra);
            ViewBag.id_producto = new SelectList(Final, "Id", "nombre", detalleCompra.id_producto);
            ViewBag.lista = Final;
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
                return RedirectToAction("detallesDePedido", new { id_compra= detalleCompra.id_compra});
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
