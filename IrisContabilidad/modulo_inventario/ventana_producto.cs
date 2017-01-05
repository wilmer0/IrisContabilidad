﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IrisContabilidad.clases;
using IrisContabilidad.modelos;
using IrisContabilidad.modulo_sistema;

namespace IrisContabilidad.modulo_inventario
{
    public partial class ventana_producto : formBase
    {

        //objetos
        empleado empleadoSingleton;
        utilidades utilidades = new utilidades();
        singleton singleton = new singleton();
        empleado empleado;
        private producto producto;
        itebis itebis;
        unidad unidadMinima;
        private almacen almacen;


        //modelos
        modeloItebis modeloItebis = new modeloItebis();
        modeloUnidad modeloUnidad=new modeloUnidad();
        modeloAlmacen modeloAlmacen=new modeloAlmacen();
        modeloProducto modeloProducto=new modeloProducto();

        //variables
        private string rutaResources = "";
        private string rutaImagenesProductos = "";


        public ventana_producto()
        {
            InitializeComponent();
            empleadoSingleton = singleton.getEmpleado();
            this.tituloLabel.Text = utilidades.GetTituloVentana(empleadoSingleton, "ventana producto");
            this.Text = tituloLabel.Text;
            rutaResources = Directory.GetCurrentDirectory().ToString() + @"\Resources\";
            rutaImagenesProductos = rutaResources + @"productos\";
            panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + "default1.png");
            loadVentana();
        }
        public void loadVentana()
        {
            try
            {
                if (producto != null)
                {
                    //llenar campos

                    if (empleado.foto != "")
                    {
                        panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + producto.imagen);
                    }
                    else
                    {
                        panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + "default1.png");
                    }
                    activoCheck.Checked = Convert.ToBoolean(empleado.activo);
                }
                else
                {
                    //blanquear campos
                    
                    panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + "default1.png");
                    activoCheck.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadVentana.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ventana_producto_Load(object sender, EventArgs e)
        {

        }

        public void loadItebis()
        {
            try
            {
                if (itebis == null)
                {
                    return;
                }
                itebisIdText.Text = itebis.codigo.ToString();
                itebisText.Text = itebis.nombre;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadItebis.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool validarGetAction()
        {
            try
            {
                //validar itebis
                if (itebis==null)
                {
                    MessageBox.Show("Falta el itbis", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    itebisIdText.Focus();
                    itebisIdText.SelectAll();
                    return false;
                }
                //validar almacen
                if (almacen == null)
                {
                    MessageBox.Show("Falta el almacen", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    almacenIdText.Focus();
                    almacenIdText.SelectAll();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validarGetAction.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public void GetAction()
        {
            try
            {

                if (MessageBox.Show("Desea guardar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                if (!validarGetAction())
                {
                    return;
                }


                bool crear = false;
                if (producto == null)
                {
                    //agrega
                    crear = true;
                    producto = new producto();
                    producto.codigo = modeloProducto.getNext();
                }
                producto.nombre =productoText.Text;
                producto.referencia = referenciaText.Text;
                producto.activo = Convert.ToBoolean(activoCheck.Checked);
                //categoria
                //subcategoria
                producto.punto_maximo = Convert.ToDecimal(puntoMaximoText.Text);
                producto.reorden = Convert.ToDecimal(puntoReordenText.Text);
                producto.codigo_itebis = itebis.codigo;
                producto.codigo_almacen = almacen.codigo;
                producto.codigo_unidad_minima = unidadMinima.codigo;
                if (rutaImagenText.Text != "")
                {
                    producto.imagen = rutaImagenText.Text;
                }
                else
                {
                    producto.imagen = "";
                }
                if (crear)
                {
                    //agrega
                    if (modeloProducto.agregarProducto(producto) == true)
                    {
                        producto = null;
                        MessageBox.Show("Se agregó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        producto = null;
                        MessageBox.Show("No se agregó", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //actualiza
                    if (modeloProducto.modificarProducto(producto) == true)
                    {
                        producto = null;
                        MessageBox.Show("Se modificó", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se modificó", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                producto = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error GetAction.: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            ventana_busqueda_itbis ventana = new ventana_busqueda_itbis();
            ventana.mantenimiento = true;
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                itebis = ventana.getObjeto();
                loadItebis();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e,puntoMaximoText.Text);
        }

        private void puntoReordenText_KeyPress(object sender, KeyPressEventArgs e)
        {
            utilidades.validarTextBoxNumeroDecimal(e, puntoReordenText.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rutaImagenText.Text = file.FileName;
                    panel3.BackgroundImage = Image.FromFile(rutaImagenText.Text);
                }
            }
            catch (Exception)
            {
                rutaImagenText.Text = "";
                panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + "default1.png");
                MessageBox.Show("Debe seleccionar una imagen", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public void salir()
        {
            if (MessageBox.Show("Desea salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAction();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Desea eliminar la foto del empleado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                rutaImagenText.Text = "";
                panel3.BackgroundImage = Image.FromFile(rutaImagenesProductos + @"default1.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadAlmacen()
        {
            try
            {
                if (almacen == null)
                {
                    return;
                }
                almacenIdText.Text = almacen.codigo.ToString();
                almacenText.Text = almacen.nombre;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadAlmacen.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ventana_busqueda_almacen ventana = new ventana_busqueda_almacen();
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                almacen = ventana.getObjeto();
                loadAlmacen();
            }
        }
        public void loadUnidad()
        {
            try
            {
                if (unidadMinima == null)
                {
                    return;
                }
                unidadMinimaIdText.Text = unidadMinima.codigo.ToString();
                unidadMinimaText.Text = unidadMinima.nombre;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadAlmacen.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ventana_busqueda_unidad ventana = new ventana_busqueda_unidad();
            ventana.Owner = this;
            ventana.ShowDialog();
            if (ventana.DialogResult == DialogResult.OK)
            {
                unidadMinima = ventana.getObjeto();
                loadUnidad();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            producto = null;
            loadVentana();
        }
    }
}
