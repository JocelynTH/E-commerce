//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArbolesPlantas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int id_factura { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public int recibido { get; set; }
        public double subtotal { get; set; }
        public string motivo { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
