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
    public class ProveedoresController : Controller
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
                if(users.AspNetRoles.FirstOrDefault().Name=="Admin" || users.AspNetRoles.FirstOrDefault().Name == "Compras")
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }


        // GET: Proveedores
        public ActionResult Index()
        {

            if (isAdmin() == 1)
            {
                return View(db.Proveedores.ToList());
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

        public ActionResult agregarCompra(int id)
        {
            Compras compra = new Compras();
            compra.fecha = System.DateTime.Now ;
            compra.id_proveedor = id;
            compra.subtotal = 0;
            compra.impuesto = 0;
            compra.total = 0;
            compra.status = 0;
            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                List<Productos> Final = new List<Productos>();
                List<Productos> lista = db.Productos.ToList();
                compra = db.Compras.OrderByDescending(compras => compras.Id).ToList().First();
                foreach(Productos p in lista)
                {
                    if(p.id_proveedor== id)
                    {
                        Final.Add(p);
                    }
                }
                string nombres="";
                foreach (Productos p in Final)
                {
                    if (p.id_proveedor == id)
                    {
                        nombres += p.nombre + " ";
                    }
                }
                TempData["var"] = Final.ToList();
                return RedirectToAction("Create", "detalleCompras", new { id_compra = compra.Id });
            }
            return Content("No guardo :(");
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            if (isAdmin() == 1)
            {
                return View();
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
        

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,rfc,razon_social,nombre_corto,descripcion,direccion,email,telefono,status")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,rfc,razon_social,nombre_corto,descripcion,direccion,email,telefono,status")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedores);
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
