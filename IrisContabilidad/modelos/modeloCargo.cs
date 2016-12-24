﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IrisContabilidad.clases;

namespace IrisContabilidad.modelos
{
    public class modeloCargo
    {
            //objetos
            utilidades utilidades = new utilidades();

        

       

            //agregar cargo
            public bool agregarCargo(cargo cargoAPP)
            {
                try
                {
                    cargo cargo=new cargo();
                    int activo = 0;
                    string sql = "select *from cargo where nombre='" + cargoAPP.nombre + "'";
                    DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Existe un cargo con ese nombre", "", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return false;
                    }

                    cargo.id = cargoAPP.id;
                    cargo.nombre = cargoAPP.nombre;
                    if (cargoAPP.activo == true)
                    {
                        activo = 1;
                    }
                    
                    sql="insert into cargo(id,nombre,activo) values('"+cargoAPP.id+"','"+cargoAPP.nombre+"','"+activo.ToString()+"')";
                    //MessageBox.Show(sql);
                    ds=utilidades.ejecutarcomando_mysql(sql);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error agregarCargo.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //modificar cargo
            public bool modificarCargo(cargo cargoAPP)
            {
                try
                {
                    int activo = 0;
                    string sql = "select *from cargo where nombre='" + cargoAPP.nombre + "' and id!='"+cargoAPP.id+"'";
                    DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Existe un cargo con ese nombre", "", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return false;
                    }
                    if (cargoAPP.activo == true)
                    {
                        activo = 1;
                    }
                    sql = "update cargo set nombre='" + cargoAPP.nombre + "',activo='"+activo.ToString()+"' where id='"+cargoAPP.id+"'";
                    ds=utilidades.ejecutarcomando(sql);
                    MessageBox.Show(sql);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error modificarCargo.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


        //obtener el codigo siguiente cargo
        public int getNext()
        {
            try
            {
                string sql = "select max(id)from cargo";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                int id = (int)ds.Tables[0].Rows[0][0];
                if (id == null || id==0)
                {
                    id = 1;
                }
                else
                {
                    id += 1;
                }
                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getNext.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        //get cargo
        public cargo getCargoById(int id)
        {
            try
            {
                cargo cargo = new cargo();
                string sql = "select id,nombre,activo from cargo where codigo='" + id + "'";
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cargo.id = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                    cargo.nombre = ds.Tables[0].Rows[0][1].ToString();
                    cargo.activo = Convert.ToBoolean(ds.Tables[0].Rows[0][2].ToString());
                }
                return cargo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getCargoById.:" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        //get lista completa
        public List<cargo> getListaCompleta(bool mantenimiento = false)
        {
            try
            {

                List<cargo> lista = new List<cargo>();
                string sql = "";
                sql = "select id,nombre,activo from cargo";
                if (mantenimiento == true)
                {
                    sql += " where activo=1";
                }
                DataSet ds = utilidades.ejecutarcomando_mysql(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        cargo  cargo= new cargo();
                        cargo.id = Convert.ToInt16(row[0].ToString());
                        cargo.nombre = row[1].ToString();
                        cargo.activo = Convert.ToBoolean(row[2].ToString());
                        lista.Add(cargo);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getListaCompleta.:" + ex.ToString(), "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
