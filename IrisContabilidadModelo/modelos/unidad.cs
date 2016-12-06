//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IrisContabilidadModelo.modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class unidad
    {
        public unidad()
        {
            this.entrada_salida_inventario = new HashSet<entrada_salida_inventario>();
            this.inventario = new HashSet<inventario>();
            this.inventario_reparacion = new HashSet<inventario_reparacion>();
            this.producto = new HashSet<producto>();
            this.producto_unidad_conversion = new HashSet<producto_unidad_conversion>();
            this.compra_detalle = new HashSet<compra_detalle>();
            this.factura_detalle = new HashSet<factura_detalle>();
            this.producto_codigobarra = new HashSet<producto_codigobarra>();
            this.producto1 = new HashSet<producto>();
        }
    
        public int codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public string unidad_abreviada { get; set; }
    
        public virtual ICollection<entrada_salida_inventario> entrada_salida_inventario { get; set; }
        public virtual ICollection<inventario> inventario { get; set; }
        public virtual ICollection<inventario_reparacion> inventario_reparacion { get; set; }
        public virtual ICollection<producto> producto { get; set; }
        public virtual ICollection<producto_unidad_conversion> producto_unidad_conversion { get; set; }
        public virtual ICollection<compra_detalle> compra_detalle { get; set; }
        public virtual ICollection<factura_detalle> factura_detalle { get; set; }
        public virtual ICollection<producto_codigobarra> producto_codigobarra { get; set; }
        public virtual ICollection<producto> producto1 { get; set; }
    }
}
