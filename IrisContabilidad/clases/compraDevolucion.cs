﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisContabilidad.clases
{
    public class compraDevolucion
    {
        public int codigo { get; set; }
        public int codigo_compra { get; set; }
        public DateTime fecha { get; set; }
        public bool activo { get; set; }
        public int codigo_empleado { get; set; }
        public string descripcion { get; set; }
        public string ncf { get; set; }
    }
}
