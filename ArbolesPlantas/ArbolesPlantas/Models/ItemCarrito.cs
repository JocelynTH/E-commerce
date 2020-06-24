using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArbolesPlantas.Models
{
    public class ItemCarrito
    {
        public static int contadorId = 0;
        public int id { get; set; }
        public Productos producto { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }

        public ItemCarrito(Productos producto, int cantidad)
        {
            this.id = contadorId + 1;
            this.producto = producto;
            this.cantidad = cantidad;
            this.total = producto.precioVenta * cantidad;
            contadorId += 1;
        }

    }
}