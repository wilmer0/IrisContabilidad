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
    
    public partial class almacen
    {
        public almacen()
        {
            this.entrada_salida_inventario = new HashSet<entrada_salida_inventario>();
            this.producto = new HashSet<producto>();
        }
    
        public int codigo { get; set; }
        public string nombre { get; set; }
        public int cod_sucursal { get; set; }
        public bool activo { get; set; }
    
        public virtual sucursal sucursal { get; set; }
        public virtual ICollection<entrada_salida_inventario> entrada_salida_inventario { get; set; }
        public virtual ICollection<producto> producto { get; set; }
    }
}