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
    
    public partial class sistema
    {
        public int codigo { get; set; }
        public string ruta_imagen_productos { get; set; }
        public string ruta_backup { get; set; }
        public string ruta_logo_empresa { get; set; }
        public int codigo_moneda { get; set; }
        public sbyte permisos_por_grupos_usuarios { get; set; }
        public decimal autorizar_pedidos_apartir { get; set; }
        public decimal limite_egreso_caja { get; set; }
        public sbyte usar_comprobantes { get; set; }
        public Nullable<System.DateTime> fecha_vencimiento { get; set; }
        public sbyte ver_imagen_fact_touch { get; set; }
        public sbyte ver_nombre_fact_touch { get; set; }
        public Nullable<decimal> porciento_propina { get; set; }
    
        public virtual moneda moneda { get; set; }
    }
}