﻿namespace IrisContabilidad.clases
{
    public class cuadre_caja_detalle
    {
        public int codigo_cuadre_caja { get; set; }
        public decimal monto_efectivo { get; set; }
        public decimal monto_egreso { get; set; }
        public decimal monto_ingreso { get; set; }
        public decimal monto_sobrante { get; set; }
        public decimal monto_faltante { get; set; }
        public decimal montoEfectivoIngresadoCajero { get; set; } //este monto es el que el cajero ingresa manualmente la sumatoria del desglose de efectivo al momento de cuadrar la caja
        public decimal monto_efectivo_esperado { get; set; }//monto efectivo esperado
        public decimal montoEfectivoAperturaCaja { get; set; } //apertura
        

        //facturado o ventas
        public decimal montoFacturaContado { get; set; }
        public decimal montoFacturaCotizacion { get; set; }
        public decimal montoFacturaPedido { get; set; }
        public decimal montoFacturaCredito { get; set; }
        public decimal montoFacturadoEfectivo { get; set; }

        //cobros de ventas
        public decimal montoCobrosEfectivo { get; set; }
        public decimal montoCobrosDeposito { get; set; }
        public decimal montoCobrosCheque { get; set; }
        public decimal montoCobrosTarjeta { get; set; }


        //montos de pagos
        public decimal montoPagoEfectivo { get; set; }
        public decimal montoPagoDeposito { get; set; }
        public decimal montoPagoCheque { get; set; }

        //gasto
        public decimal montoGasto { get; set; }

        //notas credito y debito
        public decimal montoNotasDebito { get; set; }
        public decimal montoNotasCredito { get; set; }

        //devolucion
        public decimal montoVentaDevolucion { get; set; }

        //documento
        public int codigoDocumento { get; set; } //para tener el codigo del documento
        public string tipoDocumento { get; set; } //para saber si el documento es una venta, pago, cobro, nc, nd etc...
        public decimal montoDocumento { get; set; } //para saber el monto de cada documento por separado
        public string fechaDocumentoS { get; set; } // para saber la fecha de creacion del documento

        public cuadre_caja_detalle()
        {

        }
    }
}
