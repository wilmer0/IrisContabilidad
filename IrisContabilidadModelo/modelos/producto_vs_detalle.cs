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
    
    public partial class producto_vs_detalle
    {
        public int codigo_producto { get; set; }
        public int codigo_detalle { get; set; }
        public string descripcion { get; set; }
    
        public virtual producto producto { get; set; }
        public virtual producto_detalle producto_detalle { get; set; }
    }
}
