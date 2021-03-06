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
    
    public partial class Compras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Compras()
        {
            this.detalleCompra = new HashSet<detalleCompra>();
            this.Factura = new HashSet<Factura>();
        }
    
        public int Id { get; set; }
        public int id_proveedor { get; set; }
        public System.DateTime fecha { get; set; }
        public double subtotal { get; set; }
        public double impuesto { get; set; }
        public double total { get; set; }
        public int status { get; set; }
    
        public virtual Proveedores Proveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalleCompra> detalleCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
