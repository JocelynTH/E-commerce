using ProyectoEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoEcommerce.Controllers
{
    public class ProductoController : Controller
    {

        public ecommerceEntities1 db = new ecommerceEntities1();
        public productos ta = new productos();
        
        
        // GET: Producto
        public ActionResult Index()
        {
            var ent = new ecommerceEntities1();
            return View(ent.productos.ToList());
        }


        public ActionResult Index2()
        {
            var ent = new ecommerceEntities1();
            return View(ent.productos.ToList());
        }





        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            productos pro = db.productos.FirstOrDefault(elemento => elemento.Id == id);
            return View(pro);
        }

        
        
        
        
        
        
        
        
        
        
        
        // GET: Producto/Create
        public ActionResult Registroproducto()
        {
            return View();
        }

       
       // POST: Producto/Create
        [HttpPost]
        public ActionResult Registroproducto(productos reg)
        {
            if (ModelState.IsValid)
            {
                ecommerceEntities1 db = new ecommerceEntities1();
                db.productos.Add(reg);
                db.SaveChanges();
            }
            return View(reg);
        }

       
        
        
        
        
        
        
        
        // GET: Producto/Edit/5
        public ActionResult Editar(int id )
        {
            var bookE = (from m in db.productos

                         where id == m.Id

                         select m).First();

            

            


            return View(bookE);

        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, productos reg)
        {

            try
            {


                productos p = db.productos.FirstOrDefault(n => n.Id== id);

                if (p.precio_venta_anterior == 0 && p.precio_venta_nuevo != 0)
                {
                    p.precio_venta_anterior = p.precio_venta_nuevo;
                    p.precio_venta_nuevo = reg.precio_venta_nuevo;
                }
                else if(p.precio_venta_anterior != 0 && p.precio_venta_nuevo != 0)
                {
                    p.precio_venta_anterior = p.precio_venta_nuevo;
                    p.precio_venta_nuevo = reg.precio_venta_nuevo;
                }else if(p.precio_venta_anterior == 0 && p.precio_venta_nuevo == 0)
                {
                    p.precio_venta_anterior = p.precio_venta_nuevo;
                    p.precio_venta_nuevo = reg.precio_venta_nuevo;
                }
                else if (p.precio_venta_anterior != 0 && p.precio_venta_nuevo == 0)
                {
                    p.precio_venta_anterior = p.precio_venta_nuevo;
                    p.precio_venta_nuevo = reg.precio_venta_nuevo;
                }












                db.SaveChanges();


                
               
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        
        
        
        
        
        
        
        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.productos.FirstOrDefault(n=>n.Id==id));
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                productos pro = db.productos.FirstOrDefault(n => n.Id == id);
                db.productos.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
















        public ActionResult Editarpublicar(int id)
        {
            var bookE = (from m in db.productos

                         where id == m.Id

                         select m).First();

            return View(bookE);
        }


        [HttpPost]
        public ActionResult Editarpublicar(int id, productos reg)
        {

            try
            {
                productos p = db.productos.FirstOrDefault(n => n.Id == id);

                p.nombre = reg.nombre;
                p.descripcion = reg.descripcion;
                p.precio_compra = reg.precio_compra;
                p.precio_venta_anterior = reg.precio_venta_anterior;
                p.precio_venta_nuevo = reg.precio_venta_nuevo;
                p.cantidad = reg.cantidad;
                p.stock = reg.stock;
                p.tipo = reg.tipo;
                p.status = reg.status;
                p.id_proveedor = reg.id_proveedor;
                p.Imagen = reg.Imagen;

                db.SaveChanges();






                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }








        public ActionResult Tienda()
        {
            
            var ent = new ecommerceEntities1();
            return View(ent.productos.ToList());

            
        }


    }
}
