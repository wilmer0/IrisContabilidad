﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IrisContabilidad.clases;
using IrisContabilidad.modelos;

namespace IrisContabilidad.clases_reportes
{
    public class reporte_ventas_mensuales_grafico_detalles
    {
        public int anoNumero { get; set; }
        public int mesNumero { get; set; }
        public string mesNombre { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoItbis { get; set; }
        public decimal montoDescuento { get; set; }
        public decimal montoSubTotal { get; set; }
        public decimal montoNotaCredito { get; set; }
        public decimal montoNotaDebito { get; set; }
        public decimal porcientoCrecimiento { get; set; }
        public string montoTotalS { get; set; }


        public  reporte_ventas_mensuales_grafico_detalles()
        {
            
        }

        public  reporte_ventas_mensuales_grafico_detalles(venta venta)
        {
            try
            {
                singleton singleton=new singleton();
                
                List<mes> listaMes=new List<mes>();
                listaMes = singleton.obtenerListaMes();
                List<cxc_nota_credito> listaNotaCredito=new List<cxc_nota_credito>();
                List<cxc_nota_debito> listaNotaDebito=new List<cxc_nota_debito>();
                List<venta_detalle> listaVentaDetalle=new List<venta_detalle>();
                listaVentaDetalle = new modeloVenta().getListaVentaDetalle(venta.codigo,false);
                listaNotaCredito = new modeloCxcNotaCredito().getListaByVentaActivo(venta.codigo);
                listaNotaDebito = new modeloCxcNotaDebito().getListaByVentaActivo(venta.codigo);
                this.anoNumero = venta.fecha.Year;
                this.mesNumero = venta.fecha.Month;
                this.mesNombre = listaMes.FindAll(x => x.numeroMes == this.mesNumero).FirstOrDefault().nombre;
                this.montoDescuento = listaVentaDetalle.Sum(s => s.monto_descuento);
                this.montoNotaDebito = listaNotaDebito.Sum(s=> s.monto);
                this.montoNotaCredito = listaNotaCredito.Sum(s => s.monto);
                this.montoTotal = listaVentaDetalle.Sum(s => s.monto_total);
                this.montoItbis = listaVentaDetalle.Sum(s => s.monto_itebis);
                this.montoSubTotal = montoTotal - montoItbis;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reporte_ventas_mensuales_grafico_detalles.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
