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
    
    public partial class nomina_conceptos
    {
        public nomina_conceptos()
        {
            this.nomina_detalle = new HashSet<nomina_detalle>();
        }
    
        public int codigo { get; set; }
        public string nombre { get; set; }
        public sbyte aumenta_sueldo { get; set; }
        public Nullable<sbyte> activo { get; set; }
    
        public virtual ICollection<nomina_detalle> nomina_detalle { get; set; }
    }
}
