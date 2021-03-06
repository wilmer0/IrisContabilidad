﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IrisContabilidad.clases;
using IrisContabilidad.modelos;
using IrisContabilidad.modulo_sistema;

namespace IrisContabilidad.modulo_nomina
{
    public partial class ventana_busqueda_grupo_usuario : formBase
    {

        //objetos
        private grupo_usuarios grupoUsuario;

        //listas
        private List<grupo_usuarios> listaGrupoUsuarios;
        
        //modelos
        private modeloGrupoUsuarios modeloGrupoUsuario = new modeloGrupoUsuarios();
       

        //variables 
        public bool mantenimiento = false;
        private int fila = 0;
        int index = 0;


        public ventana_busqueda_grupo_usuario(bool mantenimiento=false)
        {
            InitializeComponent();
            tituloLabel.Text = this.Text;
            this.mantenimiento = mantenimiento;
            loadLista();
        }
        public void loadLista()
        {
            try
            {
                //si la lista esta null se inicializa
                if (listaGrupoUsuarios == null)
                {
                    listaGrupoUsuarios = new List<grupo_usuarios>();
                    listaGrupoUsuarios = modeloGrupoUsuario.getListaCompleta(mantenimiento);
                }
                //se limpia el grid si tiene datos
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                }
                //se agrega todos los datos de la lista en el gridView
                listaGrupoUsuarios.ForEach(x =>
                {
                    dataGridView1.Rows.Add(x.codigo,x.nombre,x.detalles,x.activo);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loadLista.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public grupo_usuarios getObjeto()
        {
            try
            {
                //validar que tenga datos el datagrid
                if (dataGridView1.Rows.Count < 0)
                {
                    return null;
                }
                //para pasar el objeto sucursal desde deonde se llamo
                fila = dataGridView1.CurrentRow.Index;
                grupoUsuario = modeloGrupoUsuario.getGrupoUsuarioById(Convert.ToInt16(dataGridView1.Rows[fila].Cells[0].Value.ToString()));
                return grupoUsuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getObjeto.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public void getAction()
        {
            this.DialogResult = DialogResult.OK;
            getObjeto();
            this.Close();
        }
        public void Salir()
        {
            if (MessageBox.Show("Desea salir?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void ventana_busqueda_grupo_usuario_Load(object sender, EventArgs e)
        {

        }

        private void nombreText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    listaGrupoUsuarios = modeloGrupoUsuario.getListaCompleta();
                    //por nombre
                    
                    listaGrupoUsuarios = listaGrupoUsuarios.FindAll(x => x.nombre.ToLower().Contains(nombreText.Text.ToLower()) || x.detalles.ToLower().Contains(nombreText.Text.ToLower()));
                    loadLista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error buscando.: " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listaGrupoUsuarios = null;
            loadLista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getAction();
        }
    }
}
