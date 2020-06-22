using ProyectoEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEcommerce.Controllers
{
    public class DevolucionController : Controller
    {
        public ecommerceEntities1 db = new ecommerceEntities1();
        // GET: Devolucion

        public ActionResult Index()
        {
            var ent = new ecommerceEntities1();
            return View(ent.devolucion.ToList());
        }

        // GET: Devolucion/Details/5

        public ActionResult Details(int id)
        {
            devolucion de = db.devolucion.FirstOrDefault(elemento => elemento.Id == id);
            return View(de);
        }

        // GET: Devolucion/Create





        public ActionResult Registrodevolucion()
        {
            return View();
        }

        // POST: Devolucion/Create
        [HttpPost]
        public ActionResult Registrodevolucion(devolucion reg)
        {
            if (ModelState.IsValid)
            {
                ecommerceEntities1 db = new ecommerceEntities1();
                db.devolucion.Add(reg);
                db.SaveChanges();
            }
            return View(reg);
        }

        // GET: Devolucion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Devolucion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Devolucion/Delete/5





        public ActionResult Detalle(int id)
        {
            return View(db.devolucion.FirstOrDefault(n => n.Id == id));
        }

        // POST: Devolucion/Delete/5
        [HttpPost]
        public ActionResult Detalle(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                devolucion dev = db.devolucion.FirstOrDefault(n => n.Id == id);
                db.devolucion.Remove(dev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
