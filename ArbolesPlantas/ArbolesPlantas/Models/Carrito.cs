using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbolesPlantas.Models
{
    public class Carrito
    {
        public List<ItemCarrito> productos { get; set; }
        public double precioEnvio { get; set; }

        public Carrito()
        {
            productos = new List<ItemCarrito>();
            precioEnvio = 0;
        }

        public static Carrito getCarrito()
        {
            var carrito = System.Web.HttpContext.Current.Session["carrito"];

            if (carrito == null)
            {
                System.Web.HttpContext.Current.Session["carrito"] = new Carrito();

                return (Carrito)System.Web.HttpContext.Current.Session["carrito"];
            }

            return (Carrito)carrito;
        }

        public static void setCarrito(Carrito c)
        {

            System.Web.HttpContext.Current.Session["carrito"] = c;
        }

        public static int getNumeroDeItems()
        {
            Carrito carrito = Carrito.getCarrito();
            return carrito.productos.Count();
        }

        public double getSubTotal()
        {
            double total = 0;

            foreach (ItemCarrito item in this.productos)
            {
                total += item.total;
            }

            return total;
        }

        public double getPrecioEnvio()
        {
            return this.precioEnvio;
        }

        public double getTotal()
        {
            return this.getSubTotal() + this.getPrecioEnvio();
        }

        public static void agregarItemAlCarrito(ItemCarrito item)
        {
            /**
             * Recuperar carrito de la sesión
             * */
            Carrito carrito = Carrito.getCarrito();
            /**
             * Agregar producto al carrito
             * */
            carrito.productos.Add(item);
        }

        public static Boolean deleteItem(int id)
        {
            Carrito carrito = Carrito.getCarrito();

            foreach (ItemCarrito item in carrito.productos)
            {
                if (item.id == id)
                {
                    carrito.productos.Remove(item);
                    return true;
                }
            }
            return false;
        }

    }
}