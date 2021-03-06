﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IrisContabilidad.clases;
using IrisContabilidad.modelos;
using IrisContabilidad.modulo_cuenta_por_pagar;
using IrisContabilidad.modulo_inventario;
using IrisContabilidad.modulo_sistema;

namespace IrisContabilidad.modulo_facturacion
{
    public partial class ventana_facturacion_tienda_v1 : formBase
    {

        //objetos
        utilidades utilidades = new utilidades();
        singleton singleton = new singleton();
        empleado empleado;
        private venta venta;
        private venta_detalle ventaDetalle;
        private producto producto;
        private itebis itebis;
        private unidad unidad;
        private categoria_producto categoria;
        private subCategoriaProducto subCategoria;
        private productoUnidadConversion productoUnidadConversion;
        private cliente cliente;
        private ventana_desglose_dinero ventanaDesglose;
        private producto_precio_venta productoPrecioVenta;
        private cajero cajero;
        private tipo_comprobante_fiscal tipoComprobanteFiscal;
        private venta_detalle_lista ventaDetalleLista;
        private sistemaConfiguracion sistemaConfiguracion;
        private tipo_ventas tipoVenta;
        private sistemaConfiguracion sistemaconfiguracion;

        //modelos
        modeloTipoComprobanteFiscal modeloTipoComprobanteFiscal = new modeloTipoComprobanteFiscal();
        private modeloItebis modeloItebis = new modeloItebis();
        private modeloUnidad modeloUnidad = new modeloUnidad();
        private modeloAlmacen modeloAlmacen = new modeloAlmacen();
        private modeloProducto modeloProducto = new modeloProducto();
        modeloCliente modeloCliente = new modeloCliente();
        ModeloReporte modeloReporte = new ModeloReporte();
        modeloVenta modeloVenta = new modeloVenta();
        modeloComprobanteFiscal modeloComprobantefiscal = new modeloComprobanteFiscal();
        modeloCajero modeloCajero = new modeloCajero();
        modeloSistemaConfiguracion modeloSistemaconfiguracion=new modeloSistemaConfiguracion();
        modeloTipoVentas modeloTipoVentas=new modeloTipoVentas();
        

        //variables
        bool existe = false;//para saber si existe la unidad actual y el codigo de barra
        private decimal totalItebisMonto = 0;
        private decimal totalVentaMonto = 0;
        private decimal cantidadExistencia = 0;
        public bool modoCotizacion = false;
        

        //listas
        private List<producto_vs_codigobarra> listaCodigoBarra;
        private List<productoUnidadConversion> listaProductoUnidadConversion;
        private List<venta> listaVenta;
        private List<unidad> listaUnidad;
        private List<tipo_comprobante_fiscal> listaTipoComprobanteFiscal;
        private List<venta_detalle_lista> listaVentaDetalleLista;
        private List<tipo_ventas> listaVentas; 


        //variables
        private decimal cantidad_monto = 0;
        private decimal precio_monto = 0;
        private decimal importe_monto = 0;
        private decimal descuento_monto = 0;
        private decimal descuento_porciento = 0;
        private decimal itebis_monto = 0;



        public ventana_facturacion_tienda_v1()
        {
            InitializeComponent();
            empleado = singleton.getEmpleado();
            this.tituloLabel.Text = utilidades.GetTituloVentana(empleado, "ventana facturación");
            this.Text = tituloLabel.Text;
            loadVentana();
        }

        public void loadVentana()
        {
            try
            {
                sistemaConfiguracion = modeloSistemaconfiguracion.getSistemaConfiguracion();
                loadTipoVentas();
                loadTipoVentaPorDefecto();
                loadListaComprobantesFiscales();

                if (sistemaConfiguracion.codigoIdiomaSistema == 1)
                {
                    //espanol
                    fechaLabelText.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
                else
                {
                    //ingles
                    fechaLabelText.Text = DateTime.Today.ToString("yyyy/MM/dd");
                }
                if (venta != null)
                {
                    clienteIdText.Focus();
                    clienteIdText.SelectAll();

                    //llenar campos
                    cliente = modeloCliente.getClienteById(venta.codigo_cliente);
                    clienteIdText.Text = cliente.codigo.ToString();
                    clienteText.Text = cliente.nombre;
                    //numeroFacturaText.Text = venta.numero_factura;
                    tipoVentaComboBox.Enabled = false;
                    tipoVentaComboBox.Text = venta.tipo_venta;
                    tipoComprobanteCombo.Enabled = false;
                    numerocComprobanteFiscalText.Text = venta.ncf;
                    //fechaInicialText.Enabled = false;
                    //fechaInicialText.Text = venta.fecha.ToString("d");
                    //fechaFinalText.Enabled = false;
                    //fechaFinalText.Text = venta.fecha_limite.ToString("d");
                    detalleText.Enabled = false;
                    detalleText.Text = venta.detalle;

                    //llenar el detalle de la venta
                    dataGridView1.Rows.Clear();
                    listaVentaDetalleLista = modeloVenta.getListaVentaDetalle(venta.codigo);
                    //loadListaVentaDetalle();
                    //botonImprimir.Visible = true;
                }
                else
                {
                    clienteIdText.Focus();
                    clienteIdText.SelectAll();


                    //blanquear campos
                    cliente = modeloCliente.getClienteById(1);
                    loadCliente();
                    //numeroFacturaText.Text = "";
                    numerocComprobanteFiscalText.Text = "";
                    tipoVentaComboBox.Enabled = true;
                    tipoVentaComboBox.Text = "";
                    //fechaInicialText.Text = DateTime.Today.ToString("d");
                    //fechaFinalText.Text = DateTime.Today.ToString("d");
                    detalleText.Text = "Any description";
                    //limpiar el detalle de la compra
                    listaVentaDetalleLista = new List<venta_detalle_lista>();
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                    }
                    //fechaInicialText.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    //fechaFinalText.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    //botonImprimir.Visible = false;
                }
                //para tomar el comprobante que tiene en el combo seleccionado en el momento
                getTipocomprobante();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadCliente()
        {
            try
            {
                if (cliente == null)
                {
                    cliente = modeloCliente.getClienteById(1);
                    clienteIdText.Text = cliente.codigo.ToString();
                    clienteText.Text = cliente.nombre;
                    return;
                }
                clienteIdText.Text = cliente.codigo.ToString();
                clienteText.Text = cliente.nombre;

            }
            catch (Exception)
            {

            }
        }

        public bool validarGetAcion()
        {
            try
            {
                //validar que el usuario actual es cajero
                cajero = modeloCajero.getCajeroByIdEmpleado(empleado.codigo);
                if (cajero == null)
                {
                    MessageBox.Show("Su usuario no es cajero, no puede realizar ventas", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //si tiene una compra existente abierta
                if (venta != null)
                {
                    clienteIdText.Focus();
                    clienteIdText.SelectAll();
                    MessageBox.Show("Tiene una venta existente abierta debe limpiar antes de continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //si tiene cliente seleccionado
                if (cliente == null)
                {
                    clienteIdText.Focus();
                    clienteIdText.SelectAll();
                    MessageBox.Show("Falta el cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //si tiene productos en el grid
                if (dataGridView1.Rows.Count == 0)
                {
                    productoIdText.Focus();
                    productoIdText.SelectAll();
                    MessageBox.Show("No hay productos para realizar la venta", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //tipo de venta
                if (tipoVentaComboBox.Text.Trim() == "")
                {
                    tipoVentaComboBox.Focus();
                    tipoVentaComboBox.SelectAll();
                    MessageBox.Show("Falta el tipo de compra", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                ////fecha inicial
                //DateTime fecha1;
                //if (DateTime.TryParse(fechaInicialText.Text, out fecha1) == false)
                //{
                //    fechaInicialText.Focus();
                //    fechaInicialText.SelectAll();
                //    MessageBox.Show("Formato de fecha no es valido", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                ////fecha a credito
                //DateTime fecha2;
                //if (DateTime.TryParse(fechaFinalText.Text, out fecha2) == false)
                //{
                //    fechaFinalText.Focus();
                //    fechaFinalText.SelectAll();
                //    MessageBox.Show("Formato de fecha no es valido", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}


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

                //en esta ventana siempre va ser true el crear, siempre se va crear
                bool crear = true;
                venta = new venta();
                venta.codigo = modeloVenta.getNext();
                //venta.numero_factura = numeroFacturaText.Text;
                venta.codigo_cliente = cliente.codigo;
                //venta.fecha = Convert.ToDateTime(fechaInicialText.Text);
                venta.fecha = DateTime.Today;
                //venta.fecha_limite = Convert.ToDateTime(fechaFinalText.Text);
                venta.fecha_limite = DateTime.Today;
                venta.ncf = modeloComprobantefiscal.getNextComprobanteFiscalByTipoId(cajero.codigo_caja, Convert.ToInt16(tipoComprobanteCombo.SelectedValue));
                venta.tipo_venta = tipoVentaComboBox.Text;
                venta.codigo_tipo_venta = Convert.ToInt16(tipoVentaComboBox.SelectedValue.ToString());
                venta.activo = true;
                venta.pagada = false;
                venta.codigo_sucursal = empleado.codigo_sucursal;
                venta.codigo_empleado = empleado.codigo;
                venta.codigo_empelado_anular = 0;
                venta.motivo_anulada = "";
                venta.detalle = detalleText.Text;
                venta.codigo_tipo_comprobante = Convert.ToInt16(tipoComprobanteCombo.SelectedValue);

                if (crear == true)
                {
                    //agregar
                    //validar si la venta es al contado para proceder hacer el cobro
                    if (venta.codigo_tipo_venta==1)
                    {
                        ventanaDesglose = new ventana_desglose_dinero(venta, listaVentaDetalleLista);
                        venta = null;
                        ventanaDesglose.ShowDialog();
                        if (ventanaDesglose.DialogResult == DialogResult.OK)
                        {
                            loadVentana();
                        }
                    }
                    else
                    {
                        //la venta no es al contado entonces solo se guarda pero no hay desglose de pago
                        if (modeloVenta.agregarVenta(venta, listaVentaDetalleLista) == true)
                        {
                            if (MessageBox.Show("Se agregó, desea Imprimir la venta?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                modeloReporte.imprimirVenta(venta.codigo);
                            }
                            venta = null;
                            loadVentana();
                        }
                        else
                        {
                            venta = null;
                            MessageBox.Show("No se agregó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                venta = null;
                MessageBox.Show("Error getAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getTipocomprobante()
        {
            try
            {
                tipoComprobanteFiscal = modeloTipoComprobanteFiscal.getTipoComprobanteById(Convert.ToInt16(tipoComprobanteCombo.SelectedValue.ToString()));
                //MessageBox.Show("Tipo NCF->>" + tipoComprobanteFiscal.nombre + "-->secuencia-->" +tipoComprobanteFiscal.secuencia);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error cambiando de tipo de comprobante fiscal", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ventana_facturacion_touch_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }

        public void eliminarProducto()
        {
            try
            {
                //validar que tenga filas el datagrid
                if (dataGridView1 == null || dataGridView1.Rows.Count < 0)
                {
                    return;
                }
                if (listaVentaDetalleLista == null)
                {
                    return;
                }
                if (listaVentaDetalleLista.Count == 0)
                {
                    return;
                }
                int fila = 0;
                fila = dataGridView1.CurrentRow.Index;
                if (fila >= 0)
                {
                    listaVentaDetalleLista.RemoveAt(fila);
                    loadVentaDetalleLista();
                    //dataGridView1.Rows.Remove(dataGridView1.Rows[fila]);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminarProducto.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void agregarProducto()
        {
            try
            {
                if (listaVentaDetalleLista == null)
                {
                    listaVentaDetalleLista=new List<venta_detalle_lista>(); 
                }
                //validaciones
                
                //validar que tenga producto seleccionado
                if (producto == null)
                {
                    productoIdText.Focus();
                    productoIdText.SelectAll();
                    MessageBox.Show("Debe seleccionar un producto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //validar que tiene unidad seleccionada
                if (unidad == null)
                {
                    unidadComboText.Focus();
                    MessageBox.Show("Debe seleccionar una unidad", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //validar que tenga cantidad 
                if (cantidadText.Text == "")
                {
                    cantidadText.Focus();
                    cantidadText.SelectAll();
                    MessageBox.Show("Falta la cantidad", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDecimal(cantidadText.Text) <= 0)
                {
                    cantidadText.Focus();
                    cantidadText.SelectAll();
                    MessageBox.Show("Falta la cantidad", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //validar que tenga precio 
                if (precioText.Text == "")
                {
                    precioText.Focus();
                    precioText.SelectAll();
                    
                    MessageBox.Show("Falta el precio del producto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //validar que tenga descuento o que sea 0
                if (descuentoText.Text == "")
                {
                    descuentoText.Text = "0.00";
                }
                //validar que tenga importe
                if (importeText.Text == "")
                {
                    importeText.Focus();
                    importeText.SelectAll();
                    MessageBox.Show("Falta el importe", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //validar que si existe el producto y unidad se sume la cantidad
                int fila = 0;
                existe = false;
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                foreach(var x in listaVentaDetalleLista)
                {
                    //if (row.Cells[0].Value.ToString() == producto.codigo.ToString() && row.Cells[2].Value.ToString() == unidad.codigo.ToString())
                    if (x.codigoProducto == producto.codigo && x.codigoUnidad == unidad.codigo)
                    {
                        existe = true;
                        //son iguales se sacan los valores de los textBox
                        //para saber si el porciento descuento sea siempre 50->0.50 o 23->0.23
                        if (Convert.ToDecimal(descuentoText.Text) > 1)
                        {
                            descuentoText.Text = (Convert.ToDecimal(descuentoText.Text) / 100).ToString();
                        }

                        //datos de los textboxs
                        cantidad_monto = Convert.ToDecimal(cantidadText.Text);
                        precio_monto = Convert.ToDecimal(precioText.Text);
                        importe_monto = Convert.ToDecimal(importeText.Text);
                        itebis = modeloItebis.getItebisById(producto.codigo_itebis);

                        //sumar y procesar
                        //cantidad_monto += Convert.ToDecimal(row.Cells[4].Value.ToString());
                        cantidad_monto += x.cantidad;
                        importe_monto = cantidad_monto * precio_monto;
                        itebis_monto = Convert.ToDecimal(itebis.porciento * Convert.ToDecimal(importe_monto));
                        descuento_monto = Convert.ToDecimal(descuentoText.Text) * importe_monto;
                        importe_monto = importe_monto - descuento_monto;


                        //asignar los nuevos valores en el grid o la lista detalle lista
                        //row.Cells[4].Value = cantidad_monto.ToString("N");
                        x.cantidad= cantidad_monto;
                        //row.Cells[5].Value = precio_monto.ToString("N");
                        x.precio = precio_monto;
                        //row.Cells[6].Value = itebis_monto.ToString("N");
                        x.itbis = itebis_monto;
                        //row.Cells[7].Value = descuento_monto.ToString("N");
                        x.descuento = descuento_monto;
                        //row.Cells[8].Value = importe_monto.ToString("N");
                        x.total = importe_monto;
                        x.subTotal = x.total - x.itbis;
                    }
                    //si no se repite el producto y unidad entonces se agrega los valores del textbox
                }

                if (existe == false)
                {
                    importe_monto = Convert.ToDecimal(cantidadText.Text) * Convert.ToDecimal(precioText.Text);
                    itebis = modeloItebis.getItebisById(producto.codigo_itebis);
                    itebis_monto = itebis.porciento * importe_monto;
                    //para saber si el porciento descuento sea siempre 50->0.50 o 23->0.23
                    if (Convert.ToDecimal(descuentoText.Text) > 1)
                    {
                        descuentoText.Text = (Convert.ToDecimal(descuentoText.Text) / 100).ToString();
                    }
                    descuento_monto = Convert.ToDecimal(descuentoText.Text) * importe_monto;
                    importe_monto = importe_monto - descuento_monto;
                    //creando el objeto del detalle de venta de la lista
                    if (listaVentaDetalleLista == null)
                    {
                        listaVentaDetalleLista=new List<venta_detalle_lista>();
                    }
                    ventaDetalleLista=new venta_detalle_lista();
                    ventaDetalleLista.producto = producto;
                    ventaDetalleLista.codigoProducto = producto.codigo;
                    ventaDetalleLista.referencia = producto.referencia;
                    ventaDetalleLista.nombreProducto = producto.nombre;
                    ventaDetalleLista.codigoBarra = "";
                    ventaDetalleLista.unidad = unidad;
                    ventaDetalleLista.codigoUnidad = unidad.codigo;
                    ventaDetalleLista.nombreUnidad = unidad.nombre;
                    ventaDetalleLista.descuento = descuento_monto;
                    ventaDetalleLista.itbis = itebis_monto;
                    ventaDetalleLista.precio = Convert.ToDecimal(precioText.Text);
                    ventaDetalleLista.cantidad = Convert.ToDecimal(cantidadText.Text);
                    ventaDetalleLista.total = importe_monto;
                    ventaDetalleLista.subTotal = ventaDetalleLista.total - ventaDetalleLista.itbis;
                    listaVentaDetalleLista.Add(ventaDetalleLista);
                    //dataGridView1.Rows.Add(producto.codigo.ToString(), producto.nombre, unidad.codigo.ToString(), unidad.nombre, cantidadText.Text, precioText.Text, itebis_monto.ToString("N"), descuento_monto.ToString("N"), importe_monto.ToString("N"));
                    loadVentaDetalleLista();
                }
                fila++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error agregarProducto.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadVentaDetalleLista()
        {
            try
            {
                if (listaVentaDetalleLista == null)
                {
                    return;
                }
                if (listaVentaDetalleLista.Count==0)
                {
                    return;
                }
                dataGridView1.Rows.Clear();
                foreach (var x in listaVentaDetalleLista)
                {
                    dataGridView1.Rows.Add(x.nombreProducto, x.nombreUnidad, x.cantidad, x.precio.ToString("N"), x.itbis.ToString("N"), x.descuento.ToString("N"), x.total.ToString("N"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentaDetalleLista.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        public void loadProducto()
        {
            try
            {
                productoIdText.Text = "";
                productoText.Text = "";
                if (producto == null)
                {
                    return;
                }
                productoIdText.Text = producto.codigo.ToString();
                productoText.Text = producto.nombre;

                loadUnidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadProducto.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadTipoVentas()
        {
            try
            {
                if (listaVentas == null)
                {
                    listaVentas=new List<tipo_ventas>();
                }
                listaVentas = modeloTipoVentas.getListaCompleta();
                tipoVentaComboBox.DisplayMember = "nombre";
                tipoVentaComboBox.ValueMember = "codigo";
                tipoVentaComboBox.DataSource = listaVentas;
                tipoVentaComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTipoVentas.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void loadTipoVentaPorDefecto()
        {
            try
            {
                sistemaConfiguracion=new sistemaConfiguracion();
                sistemaConfiguracion = modeloSistemaconfiguracion.getSistemaConfiguracion();
                if (sistemaConfiguracion.codigoTipoVentaDefecto != null)
                {
                    tipoVentaComboBox.SelectedValue = sistemaConfiguracion.codigoTipoVentaDefecto;
                }
                /*CON
                  COT
                  CRE
                  PED
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadTipoVentaDefecto.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void loadListaComprobantesFiscales()
        {
            try
            {
                if (listaTipoComprobanteFiscal == null)
                {
                    listaTipoComprobanteFiscal=new List<tipo_comprobante_fiscal>();
                }
                listaTipoComprobanteFiscal = modeloTipoComprobanteFiscal.getListaCompleta();
                tipoComprobanteCombo.DisplayMember = "nombre";
                tipoComprobanteCombo.ValueMember = "codigo";
                tipoComprobanteCombo.DataSource = listaTipoComprobanteFiscal;
                tipoComprobanteCombo.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadListacomprobanteFiscal.: " + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void loadUnidad()
        {
            try
            {
                if (producto == null)
                {
                    unidadComboText.Text = "";
                    unidadComboText.DataSource = null;
                    unidadComboText.DisplayMember = null;
                    return;
                }
                listaUnidad = modeloUnidad.getListaByProducto(producto.codigo);
                unidadComboText.DataSource = listaUnidad;
                unidadComboText.ValueMember = "codigo";
                unidadComboText.DisplayMember = "nombre";
                if (listaUnidad.Count > 0)
                {
                    unidad = modeloUnidad.getUnidadById(listaUnidad[0].codigo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadUnidad.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void getPrecioVentaProductoUnidad()
        {
            try
            {
                if (producto == null)
                {
                    return;
                }
                if (unidadComboText.Text == "")
                {
                    return;
                }
                precioText.Text = modeloProducto.getPrecioProductoUnidad(producto.codigo, Convert.ToInt16(unidadComboText.SelectedValue)).precio_venta1.ToString("N");
            }
            catch (Exception ex)
            {
                precioText.Text = "0.00";
                //MessageBox.Show("Error getPrecioVentaProductoUnidad.:" + ex.ToString(), "", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        public void getInventarioByProductoUnidad()
        {
            try
            {
                cantidadExistencia = 0;
                if (producto == null)
                {
                    productoIdText.Focus();
                    productoIdText.SelectAll();
                    MessageBox.Show("Debe seleccionar un producto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (unidadComboText.Text == "")
                {
                    return;
                }
                unidad = modeloUnidad.getUnidadById(Convert.ToInt16(unidadComboText.SelectedValue.ToString()));
                //MessageBox.Show(unidadComboText.SelectedValue + "-" + unidadComboText.Text);
                cantidadExistencia = modeloProducto.getExistenciaByProductoAndUnidad(producto.codigo, Convert.ToInt16(unidadComboText.SelectedValue.ToString()));
                //existenciaText.Text = cantidadExistencia.ToString("N");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error getInventarioByProductoUnidad.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void calularImporte()
        {
            try
            {
                if (descuentoText.Text == "")
                {
                    descuentoText.Text = "0.00";
                }
                if (cantidadText.Text == "")
                {
                    importeText.Text = "";
                    return;
                }
                if (precioText.Text == "")
                {
                    importeText.Text = "";
                    return;
                }

                itebis = modeloItebis.getItebisById(producto.codigo_itebis);
                cantidad_monto = Convert.ToDecimal(cantidadText.Text);
                precio_monto = Convert.ToDecimal(precioText.Text);
                descuento_monto = Convert.ToDecimal(descuentoText.Text);
                importe_monto = cantidad_monto * precio_monto;
                descuento_monto = importe_monto * descuento_porciento;
                importe_monto = importe_monto - descuento_monto;
                itebis_monto = importe_monto * itebis.porciento;
                importeText.Text = importe_monto.ToString("N");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calularImporte.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productoIdText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    button4_Click(null, null);
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    //id
                    producto = null;
                    if (producto == null)
                    {
                        //referencia
                        producto = modeloProducto.getProductoByReferencia(productoIdText.Text);
                    }
                    if (producto == null)
                    {
                        //codigo barra
                        producto = modeloProducto.getProductoByCodigoBarra(productoIdText.Text);
                    }
                    if (producto == null)
                    {
                        //by id
                        producto = modeloProducto.getProductoById(Convert.ToInt16(productoIdText.Text));
                    }

                    if (producto != null)
                    {
                        loadProducto();
                    }
                    unidadComboText.Focus();
                    unidadComboText.SelectAll();
                    loadUnidad();
                }
            }
            catch (Exception)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ventana_busqueda_producto ventana = new ventana_busqueda_producto();
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                producto = ventana.getObjeto();
                loadProducto();
                unidadComboText.Focus();
                unidadComboText.SelectAll();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ventana_busqueda_cliente ventana = new ventana_busqueda_cliente();
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                cliente = ventana.getObjeto();
                loadCliente();
                tipoVentaComboBox.Focus();
                tipoVentaComboBox.SelectAll();
            }
        }

        private void unidadComboText_TextChanged(object sender, EventArgs e)
        {
            getPrecioVentaProductoUnidad();
        }

        private void unidadComboText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cantidadText.Focus();
                cantidadText.SelectAll();
            }
        }

        private void cantidadText_TextChanged(object sender, EventArgs e)
        {
            calularImporte();
        }

        private void cantidadText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                precioText.Focus();
                precioText.SelectAll();
            }
        }

        private void cantidadText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, cantidadText.Text);
        }

        private void precioText_TextChanged(object sender, EventArgs e)
        {
            calularImporte();
        }

        private void precioText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                descuentoText.Focus();
                descuentoText.SelectAll();
            }
        }

        private void precioText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, cantidadText.Text);
        }

        private void descuentoText_TextChanged(object sender, EventArgs e)
        {
            calularImporte();
        }

        private void descuentoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                importeText.Focus();
                importeText.SelectAll();
            }
        }

        private void descuentoText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, descuentoText.Text);
        }

        private void importeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                button20.Focus();
            }
        }

        private void calcularTotal()
        {
            try
            {
                if (dataGridView1.Rows.Count <= 0)
                {
                    //totalItebisText.Text = "0.00";
                    //totalCompraText.Text = "0.00";
                    return;
                }

                totalItebisMonto = 0;
                totalVentaMonto = 0;
                foreach (var x in listaVentaDetalleLista)
                {
                    totalItebisMonto += x.itbis;
                    totalVentaMonto += x.total;
                }

                totalItebisText.Text = totalItebisMonto.ToString("N");
                totalVentaText.Text = totalVentaMonto.ToString("N");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calcularTotal.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void button20_Click(object sender, EventArgs e)
        {
            agregarProducto();
            productoIdText.Focus();
            productoIdText.SelectAll();
            calcularTotal();
            loadVentaDetalleLista();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            eliminarProducto();
            productoIdText.Focus();
            productoIdText.SelectAll();
            calcularTotal();
            loadVentaDetalleLista();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            producto = null;
            unidad = null;
            listaVentaDetalleLista = null;
            loadVentana();
        }

        public void salir()
        {
            if (MessageBox.Show("Desea salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ventana_facturacion_touch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(null,null);

            }else if (e.KeyCode == Keys.F5)
            {   
                button3_Click(null,null);

            }else if (e.KeyCode == Keys.F2)
            {
                button1_Click(null,null);
            }
        }

    }
}
