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
    
    public partial class empleado_historial_datos
    {
        public int codigo { get; set; }
        public int cod_empleado { get; set; }
        public string nombre { get; set; }
        public string identificacion { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public int cod_situacion { get; set; }
        public Nullable<int> cod_sexo { get; set; }
        public int cod_cargo { get; set; }
        public int cod_sucursal { get; set; }
        public Nullable<int> cod_departamento { get; set; }
        public Nullable<decimal> sueldo { get; set; }
        public Nullable<int> cod_empleado_cambio { get; set; }
        public System.DateTime fecha { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<int> cod_tipo_nomina { get; set; }
    
        public virtual departamento departamento { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual empleado empleado1 { get; set; }
        public virtual nomina_tipos nomina_tipos { get; set; }
        public virtual sexo sexo { get; set; }
        public virtual situacion_empleado situacion_empleado { get; set; }
        public virtual sucursal sucursal { get; set; }
    }
}
