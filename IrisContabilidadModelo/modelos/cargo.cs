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
    
    public partial class cargo
    {
        public cargo()
        {
            this.empleado = new HashSet<empleado>();
            this.empleado1 = new HashSet<empleado>();
        }
    
        public int codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
    
        public virtual ICollection<empleado> empleado { get; set; }
        public virtual ICollection<empleado> empleado1 { get; set; }
    }
}
