﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IrisContabilidad.clases;
using IrisContabilidad.modelos;
using IrisContabilidad.modulo_facturacion;
using IrisContabilidad.modulo_sistema;

namespace IrisContabilidad.modulo_cuenta_por_pagar
{
    public partial class ventana_compra_pagos : formBase
    {

        //objetos
        private utilidades utilidades = new utilidades();
        private singleton singleton = new singleton();
        private empleado empleado;
        private compra compra;
        private compra_detalle compraDetalle;
        private ventana_desglose_dinero ventanaDesglose;
        private compra_vs_pagos compraPago;
        private compra_vs_pagos_detalles compraPagoDetalle;
        private suplidor suplidor;


        //modelos
        private modeloCompra modeloCompra = new modeloCompra();
        private modeloSuplidor modeloSuplidor=new modeloSuplidor();
        private modeloEmpleado modeloEmpleado=new modeloEmpleado();
        private ModeloReporte modeloReporte=new ModeloReporte();


        //variables
        bool existe = false;//para saber si existe la unidad actual y el codigo de barra
        private decimal totalPendienteMonto = 0;
        private decimal totalAbonadoMonto = 0;
        private decimal totalDescontado = 0;
        private string metodoPago = "";


        //listas
        private List<compra_vs_pagos> listaCompraPago;
        private List<compra_vs_pagos_detalles> listaCompraPagoDetalle;
        private List<compra> listaCompra;
        private List<compra_detalle> listaCompraDetalle; 


        //variables
        private decimal cantidad_monto = 0;
        private decimal precio_monto = 0;
        private decimal importe_monto = 0;
        private decimal descuento_monto = 0;
        private decimal descuento_porciento = 0;
        private decimal itebis_monto = 0;


        public ventana_compra_pagos()
        {
            InitializeComponent();
            empleado = singleton.getEmpleado();
            this.tituloLabel.Text = utilidades.GetTituloVentana(empleado, "ventana compra pagos");
            this.Text = tituloLabel.Text;
            loadVentana();
        }
        public void loadVentana()
        {
            try
            {
                suplidorIdText.Focus();
                suplidorIdText.SelectAll();


                metodoPagoComboBox.SelectedIndex = 0;
                suplidor = null;
                loadSuplidor();
                dataGridView1.Rows.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool validarGetAcion()
        {
            try
            {
                if (suplidor == null)
                {
                    suplidorIdText.Focus();
                    suplidorIdText.SelectAll();
                    MessageBox.Show("Debe seleccionar un suplidor", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (dataGridView1.Rows.Count==0)
                {
                    suplidorIdText.Focus();
                    suplidorIdText.SelectAll();
                    MessageBox.Show("No hay facturas", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarGetAcion.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        public void getAction()
        {
            try
            {
                if (!validarGetAcion())
                {
                    return;
                }

                //pago enzabezado
                compraPago = new compra_vs_pagos();
                compraPago.codigo = modeloCompra.getNextPago();
                compraPago.fecha = DateTime.Today;
                compraPago.detalle = "";
                compraPago.cod_empleado = empleado.codigo;
                compraPago.activo = true;
                compraPago.cod_empleado_anular = 0;
                compraPago.motivo_anulado = "";
                compraPago.cuadrado = false;


                //pago detalle
                listaCompraPagoDetalle=new List<compra_vs_pagos_detalles>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //validar que tenga monto > 0 y que tenga metodo de pago
                    if (Convert.ToDecimal(row.Cells[8].Value.ToString()) > 0 && Convert.ToDecimal(row.Cells[9].Value.ToString()) >=0  && (row.Cells[10].Value.ToString() != ""))
                    {
                        compraPagoDetalle = new compra_vs_pagos_detalles();
                        compraPagoDetalle.codigo = 0;
                        compraPagoDetalle.codigo_pago = compraPago.codigo;
                        compraPagoDetalle.codigo_compra = Convert.ToInt16(row.Cells[0].Value.ToString());
                        if (row.Cells[10].Value.ToString().ToLower() == "efe")
                        {
                            compraPagoDetalle.codigo_metodo_pago = 1;
                        }
                        if (row.Cells[10].Value.ToString().ToLower() == "dep")
                        {
                            compraPagoDetalle.codigo_metodo_pago = 2;
                        }
                        if (row.Cells[10].Value.ToString().ToLower() == "che")
                        {
                            compraPagoDetalle.codigo_metodo_pago = 3;
                        }
                        compraPagoDetalle.monto_pagado = Convert.ToDecimal(row.Cells[8].Value.ToString());
                        compraPagoDetalle.monto_descontado = Convert.ToDecimal(row.Cells[9].Value.ToString());
                        compraPagoDetalle.monto_sub_total = compraPagoDetalle.monto_pagado - compraPagoDetalle.monto_descontado;
                        compraPagoDetalle.activo = true;
                        listaCompraPagoDetalle.Add(compraPagoDetalle);
                    }
                }
                if((modeloCompra.setCompraPago(compraPago, listaCompraPagoDetalle)==true))
                {
                    loadSuplidor();
                    if (MessageBox.Show("Se agregó el pago, desea imprimir el pago?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        modeloReporte.imprirmirCompraPago(compraPago.codigo);
                    }
                }
                else
                {
                    loadSuplidor();
                    MessageBox.Show("No se agregó el pago", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                compra = null;
                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void eliminar()
        {
            try
            {
                //validar que tenga filas el datagrid
                if (dataGridView1 == null || dataGridView1.Rows.Count < 0)
                {
                    return;
                }
                int fila = 0;
                fila = dataGridView1.CurrentRow.Index;
                if (fila >= 0)
                {
                    dataGridView1.Rows[fila].Cells[8].Value = "0.00";
                    dataGridView1.Rows[fila].Cells[9].Value = "0.00";
                    dataGridView1.Rows[fila].Cells[10].Value = "";
                }
                calcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminando.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool validarAgregar()
        {
            try
            {
                int fila = 0;
                fila = dataGridView1.CurrentRow.Index;
                //validaciones
                //monto no esta en blanco
                if (montoAbonoText.Text == "")
                {
                    montoAbonoText.Focus();
                    montoAbonoText.SelectAll();
                    MessageBox.Show("Falta el abono", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //que descuento no este en blanco
                //el monto es mayor que cero
                if (montoDescuentoText.Text == "")
                {
                    montoDescuentoText.Focus();
                    montoDescuentoText.SelectAll();
                    MessageBox.Show("Falta el descuento", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToDecimal(montoAbonoText.Text) < 0)
                {
                    montoAbonoText.Focus();
                    montoAbonoText.SelectAll();
                    MessageBox.Show("El abono debe ser mayor que cero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToDecimal(montoDescuentoText.Text) < 0)
                {
                    montoDescuentoText.Focus();
                    montoDescuentoText.SelectAll();
                    MessageBox.Show("El descuento debe ser por lo menos cero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //monto abonar no sobrepase el monto pendiente
                if (Convert.ToDecimal(montoAbonoText.Text) > Convert.ToDecimal(dataGridView1.Rows[fila].Cells[7].Value.ToString()))
                {
                    montoAbonoText.Focus();
                    montoAbonoText.SelectAll();
                    MessageBox.Show("El abono debe ser menor que el monto pendiente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //validar que el descuento sea menor o igual al monto a pagar
                //monto descuento no sobrepase el monto de abono
                if (Convert.ToDecimal(montoDescuentoText.Text) > Convert.ToDecimal(montoAbonoText.Text))
                {
                    montoDescuentoText.Focus();
                    montoDescuentoText.SelectAll();
                    MessageBox.Show("El descuento debe ser menor o igual que el monto deabono", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarAgregar.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        public void agregar()
        {
            try
            {
                if (validarAgregar() == false)
                {
                    return;
                }
                int fila = 0;
                fila = dataGridView1.CurrentRow.Index;
                getMetodoPago();
                if (fila >= 0)
                {
                    dataGridView1.Rows[fila].Cells[8].Value = Convert.ToDecimal(montoAbonoText.Text).ToString("N");
                    dataGridView1.Rows[fila].Cells[9].Value = Convert.ToDecimal(montoDescuentoText.Text).ToString("N");
                    dataGridView1.Rows[fila].Cells[10].Value =metodoPago.ToString();
                }
                calcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error agregar.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calcularTotal()
        {
            try
            {
                totalPendienteText.Text = "0.00";
                totalAbonadoText.Text = "0.00";
                totalDescontadoText.Text = "0.00";

                if (dataGridView1.Rows.Count == 0 || dataGridView1.Rows == null)
                {
                    return;
                }

                totalPendienteMonto = 0;
                totalAbonadoMonto = 0;
                totalDescontado = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    totalPendienteMonto += Convert.ToDecimal(row.Cells[7].Value);
                    totalAbonadoMonto += Convert.ToDecimal(row.Cells[8].Value);
                    totalDescontado += Convert.ToDecimal(row.Cells[9].Value);
                }

                totalPendienteText.Text = totalPendienteMonto.ToString("N");
                totalAbonadoText.Text = totalAbonadoMonto.ToString("N");
                totalDescontadoText.Text = totalDescontado.ToString("N");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error calcularTotal.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void salir()
        {
            if (MessageBox.Show("Desea salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
      
        private void ventana_compra_pagos_Load(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            agregar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea procesar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getAction();
                calcularTotal();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            suplidor = null;
            loadVentana();
        }

        public void loadCompras()
        {
            try
            {
                dataGridView1.Rows.Clear();

                if (suplidor == null)
                {
                    return;
                }
                listaCompra = modeloCompra.getListaCompraBySuplidor(suplidor.codigo);
                //filtrando las compra que esten activa, que no esten pagada y que no sean a contado
                listaCompra = listaCompra.FindAll(x => x.pagada == false && x.activo==true && x.codigo_tipo_compra==2 || x.codigo_tipo_compra==4).ToList();
                listaCompra = listaCompra.OrderByDescending(x => x.codigo).ToList();
                listaCompra = listaCompra.OrderByDescending(x => x.codigo).ToList();
                foreach(var x in listaCompra)
                {
                    decimal montoPendiente = 0;
                    montoPendiente = modeloCompra.getMontoPendienteBycompra(x.codigo);
                    empleado = modeloEmpleado.getEmpleadoById(x.codigo_empleado);
                    if (montoPendiente > 0)
                    {
                        dataGridView1.Rows.Add(x.codigo, x.fecha.ToString("dd/MM/yyyy"), utilidades.getDiasByRangoFecha(x.fecha_limite, DateTime.Today), empleado.nombre, x.tipo_compra, x.ncf, x.fecha_limite.ToString("dd/MM/yyyy"), montoPendiente.ToString("N"),"0.00","0.00","");
                    }
                    else
                    {
                        modeloCompra.setCompraPagada(x.codigo);
                    }
                }
                calcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadCompras.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadSuplidor()
        {
            try
            {
                suplidorIdText.Text = "";
                suplidorText.Text = "";
                if (suplidor != null)
                {
                    suplidorIdText.Text = suplidor.codigo.ToString();
                    suplidorText.Text = suplidor.nombre;
                    loadCompras();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadSuplidor.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ventana_busqueda_suplidor ventana=new ventana_busqueda_suplidor();
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                suplidor = ventana.getObjeto();
                loadSuplidor();
            }
            calcularTotal();
        }

        private void suplidorIdText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    button5_Click(null, null);
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    metodoPagoComboBox.Focus();
                    metodoPagoComboBox.SelectAll();

                    suplidor = modeloSuplidor.getSuplidorById(Convert.ToInt16(suplidorIdText.Text));
                    loadSuplidor();
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        public void getMetodoPago()
        {
            try
            {
                if (metodoPagoComboBox.Text == "")
                {
                    return;
                }
                if (metodoPagoComboBox.Text.ToLower() == "efectivo")
                {
                    metodoPago = "EFE";
                }
                else if (metodoPagoComboBox.Text.ToLower() == "deposito")
                {
                    metodoPago = "DEP";
                }
                else if (metodoPagoComboBox.Text.ToLower() == "cheque")
                {
                    metodoPago = "CHE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getMetodoPago.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void montoAbonoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                montoDescuentoText.Focus();
                
            }
        }

        private void button20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Focus();
            }
            if (e.KeyCode == Keys.Tab)
            {
                button19.Focus();
            }
        }

        private void ventana_compra_pagos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    salir();

                }
                if (e.KeyCode == Keys.F2)
                {
                    int cantItems = 0;
                    cantItems = metodoPagoComboBox.Items.Count;
                    if (metodoPagoComboBox.SelectedIndex == (cantItems - 1))
                    {
                        metodoPagoComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        metodoPagoComboBox.SelectedIndex += 1;
                    }
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void metodoPagoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                montoAbonoText.Focus();
                montoAbonoText.SelectAll();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter ||e.KeyCode == Keys.Tab)
            {
                metodoPagoComboBox.Focus();
                metodoPagoComboBox.SelectAll();
            }
        }

        private void montoDescuentoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                button20.Focus();

            }
        }

        private void montoDescuentoText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, montoAbonoText.Text);
        }

        private void montoAbonoText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, montoAbonoText.Text);
        }
    }
}
