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
    [Authorize]
    public class ComprasController : Controller
    {
        private Entities1 db = new Entities1();
        private int isAdmin()
        {
            AspNetUsers users = (AspNetUsers)Session["userDatosAsp"];
            if (users == null || users.AspNetRoles.FirstOrDefault().Name == "Cliente")
            {
                return 3;
            }
            else
            {
                if (users.AspNetRoles.FirstOrDefault().Name == "Admin" || users.AspNetRoles.FirstOrDefault().Name == "Compras")
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        // GET: Compras
        public ActionResult Index()
        {
            if (isAdmin() == 1)
            {
                List<Compras> compras = db.Compras.ToList();
                List<Compras> final = new List<Compras>();
                foreach (Compras c in compras)
                {
                    if (c.status != 0)
                    {
                        final.Add(c);
                    }
                }
                return View(final.OrderByDescending(compra=> compra.Id).ToList());
            }
            else
            {
                if (isAdmin() == 2)
                {
                    return RedirectToAction("About", "Home", new { error = true });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            
        }

        public ActionResult llegadas()
        {
            if (isAdmin() == 1)
            {
                List<Compras> compras = db.Compras.ToList();
                List<Compras> final = new List<Compras>();
                foreach (Compras c in compras)
                {
                    if (c.status == 4 || c.status== 6)
                    {
                        final.Add(c);
                    }
                }
                return View(final.ToList());
            }
            else
            {
                if (isAdmin() == 2)
                {
                    return RedirectToAction("About", "Home", new { error = true });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        public ActionResult Existencias(int? id)
        {
            if (isAdmin() == 1)
            {
                ViewBag.id_proveedores = new SelectList(db.Proveedores, "Id", "nombre_corto");
                if (id == null)
                {
                    ViewBag.actual = -1;
                    List<Productos> lista = db.Productos.Where(productos => productos.cantidad < productos.stock).OrderBy(prod => (prod.cantidad - prod.stock)).ToList();
                    return View(lista);
                }
                else
                {
                    ViewBag.actual = id;
                    ViewBag.id = id;
                   // ViewBag.id_proveedores = new SelectList(db.Proveedores, "Id", "nombre_corto");
                    List<Productos> lista = db.Productos.Where(productos => productos.cantidad < productos.stock).OrderBy(prod => (prod.cantidad - prod.stock)).ToList();
                    return View(lista);
                }
            }
            else
            {
                if (isAdmin() == 2)
                {
                    return RedirectToAction("About", "Home", new { error = true });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }

        [HttpPost]
        public ActionResult Existencias(FormCollection form)
        {
            string clave;
            string numero;
            string id_proveedor = form["id_proveedores"];
            Compras compra = new Compras();
            compra.fecha = System.DateTime.Now;
            compra.id_proveedor = int.Parse(id_proveedor);
            compra.subtotal = 0;
            compra.impuesto = 0;
            compra.total = 0;
            compra.status = 0;
            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                compra = db.Compras.OrderByDescending(compras => compras.Id).ToList().First();
                foreach (var key in form.AllKeys)
                {
                    clave = key.ToString();
                    if (clave.StartsWith("cantidad"))
                    {
                        numero = clave.ElementAt(clave.Length-1).ToString();
                        string id_producto = form["producto" + numero];
                        string cantidad = form["cantidad" + numero];
                        Productos producto = db.Productos.Find(int.Parse(id_producto));
                        var precio = producto.precio_compra;

                        detalleCompra detalle = new detalleCompra();
                        detalle.id_compra = compra.Id;
                        detalle.id_producto= producto.Id;
                        detalle.cantidad = int.Parse(cantidad);
                        detalle.precio_compra = (int.Parse(cantidad) * precio);
                        detalle.status = 0;
                        db.detalleCompra.Add(detalle);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("detallesDePedido", "detalleCompras", new { id_compra = compra.Id });
            }
            else
            {
                return RedirectToAction("mensaje","Home");
            }
            
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc");
            return View();
        }

        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_proveedor,fecha,subtotal,impuesto,total,status")] Compras compras)
        {
            
            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", compras.id_proveedor);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", compras.id_proveedor);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_proveedor,fecha,subtotal,impuesto,total,status")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "Id", "rfc", compras.id_proveedor);
            return View(compras);
        }

        public ActionResult cambiaEstado(int id)
        {
            List<detalleCompra> lista = db.detalleCompra.ToList();
            var cantidad = 0;
            var id_producto = 0;
            Compras compra = db.Compras.Find(id);
            var total = 0;
            if (ModelState.IsValid){
                switch (compra.status){
                    case 2:
                        db.Database.ExecuteSqlCommand("UPDATE Compras SET status=" + 0 + " WHERE Id=" + id);
                        db.SaveChanges();
                        break;

                    case 3:
                        Compras com = db.Compras.Find(id);
                        var id_proveedor = com.id_proveedor;
                        var presupuesto = 0.0;
                        var id_presupuesto = 0;
                        foreach (Presupuestos pre in db.Presupuestos.ToList())
                        {
                            if (pre.id_proveedor == id_proveedor)
                            {
                                id_presupuesto = pre.Id;
                                presupuesto = pre.presupuesto;
                                break;
                            }
                        }
                        db.Database.ExecuteSqlCommand("UPDATE Presupuestos SET presupuesto=" + (presupuesto - com.total) + " WHERE Id=" + id_presupuesto);
                        db.Database.ExecuteSqlCommand("UPDATE Compras SET status=" + 4 + " WHERE Id=" + id);
                        db.SaveChanges();

                        break;

                    case 4:
                        foreach (detalleCompra d in lista)
                        {
                            if (d.id_compra == id)
                            {
                                cantidad = d.cantidad;
                                id_producto = d.id_producto;
                                Productos producto = db.Productos.Find(id_producto);
                                total = cantidad + producto.cantidad;
                                db.Database.ExecuteSqlCommand("UPDATE Productos SET cantidad=" + total + " WHERE Id=" + id_producto);
                            }
                        }

                        db.Database.ExecuteSqlCommand("UPDATE Compras SET status=" + 5 + " WHERE Id=" + id);
                        db.SaveChanges();
                        break;
                }
                return RedirectToAction("Index", "Compras");
            }
            return Content("No guardo ");
        }

        public ActionResult editarCompra(int id)
        { 
            List<detalleCompra> lista = db.detalleCompra.ToList();
            List<Presupuestos> listaP = db.Presupuestos.ToList();
            var total = 0.0;
            var id_presupuesto = 0;

            Compras compra = db.Compras.Find(id);
            foreach (detalleCompra d in lista)
            {
                if (d.id_compra == id)
                {
                    total += d.precio_compra;
                }
            }
            foreach (Presupuestos p in listaP)
            {
                if (p.id_proveedor == compra.id_proveedor)
                {
                    id_presupuesto = p.Id;
                    break;
                }
            }
            compra.subtotal = total;
            compra.impuesto = total * .15;
            compra.total = compra.subtotal + compra.impuesto;
            Presupuestos presupuesto = db.Presupuestos.Find(id_presupuesto);
            if (total < presupuesto.presupuesto)
            {
                compra.status = 4;
                presupuesto.presupuesto -= compra.total;
            }
            else
            {
                compra.status = 1;
            }
                        
            if (ModelState.IsValid)
            {
                db.Entry(presupuesto).State = EntityState.Modified;
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Compras");
            }
            return Content("No guardo :(");
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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
