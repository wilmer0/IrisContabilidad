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
    
    public partial class pais
    {
        public pais()
        {
            this.region = new HashSet<region>();
        }
    
        public int codigo { get; set; }
        public string nombre { get; set; }
        public byte estado { get; set; }
    
        public virtual ICollection<region> region { get; set; }
    }
}
