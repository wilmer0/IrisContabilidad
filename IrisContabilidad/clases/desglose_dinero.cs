﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisContabilidad.clases
{
    public class desglose_dinero
    {
        public decimal efectivo { get; set; }
        public decimal tarjeta { get; set; }
        public decimal deposito { get; set; }
        public decimal cheque { get; set; }
        public decimal devuelto { get; set; }
        public decimal descuento { get; set; }
        public decimal monto_esperado { get; set; }

    }
}
