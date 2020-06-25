using ProyectoEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEcommerce.Controllers
{
    public class OfertaController : Controller
    {

        public ecommerceEntities1 db = new ecommerceEntities1();
        public productos ta = new productos();
        // GET: Oferta
        public ActionResult Index()
        {
            var ent = new ecommerceEntities1();
            return View(ent.ofertas.ToList());
        }

        // GET: Oferta/Details/5
        public ActionResult Detalle(int id)
        {
            ofertas ofe = db.ofertas.FirstOrDefault(elemento => elemento.Id == id);
            return View(ofe);
        }

        // GET: Oferta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oferta/Create
        [HttpPost]
        public ActionResult Create(ofertas reg)
        {
            if (ModelState.IsValid)
            {
                ecommerceEntities1 db = new ecommerceEntities1();
                db.ofertas.Add(reg);
                db.SaveChanges();
            }
            return View(reg);
        }

        // GET: Oferta/Edit/5
        public ActionResult Edit(int id)
        {
            var bookE = (from m in db.ofertas

                         where id == m.Id

                         select m).First();






            return View(bookE);
        }

        // POST: Oferta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ofertas reg)
        {
            try
            {
                
                ofertas p = db.ofertas.FirstOrDefault(n => n.Id == id);
                
               
                if (p.porcentajeDescuento != 0.0)
                {
                    float x = p.precioactual * reg.porcentajeDescuento;
                    p.nuevoprecio = p.precioactual - x;
                    p.porcentajeDescuento = reg.porcentajeDescuento;
                    

                }
               

                db.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Oferta/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.ofertas.FirstOrDefault(n => n.Id == id));
        }

        // POST: Oferta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                ofertas p = db.ofertas.FirstOrDefault(n => n.Id == id);
                db.ofertas.Remove(p);
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
