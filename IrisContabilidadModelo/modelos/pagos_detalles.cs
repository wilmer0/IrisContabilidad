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
    
    public partial class pagos_detalles
    {
        public int codigo { get; set; }
        public int cod_pago { get; set; }
        public int cod_compra { get; set; }
        public int cod_metodo_pago { get; set; }
        public Nullable<decimal> monto_pagado { get; set; }
        public Nullable<decimal> monto_descontado { get; set; }
        public Nullable<byte> estado { get; set; }
    
        public virtual compra compra { get; set; }
        public virtual metodo_pago metodo_pago { get; set; }
        public virtual pagos pagos { get; set; }
    }
}
