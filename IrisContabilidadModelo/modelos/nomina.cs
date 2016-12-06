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
    
    public partial class nomina
    {
        public nomina()
        {
            this.nomina_detalle = new HashSet<nomina_detalle>();
        }
    
        public int codigo { get; set; }
        public System.DateTime fecha_inicial { get; set; }
        public System.DateTime fecha_final { get; set; }
        public int cod_empleado { get; set; }
        public int cod_tipo { get; set; }
        public int cod_sucursal { get; set; }
        public Nullable<bool> activo { get; set; }
        public string abierta_cerrada { get; set; }
        public Nullable<int> cod_empleado_cerrada { get; set; }
    
        public virtual empleado empleado { get; set; }
        public virtual ICollection<nomina_detalle> nomina_detalle { get; set; }
        public virtual sucursal sucursal { get; set; }
        public virtual nomina_tipos nomina_tipos { get; set; }
    }
}