﻿using System.ComponentModel;
using System.Windows.Forms;

namespace IrisContabilidad.modulo_facturacion
{
    partial class ventana_caja_apertura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.montoAperturaText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cajeroText = new System.Windows.Forms.TextBox();
            this.cajeroIdText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 256);
            this.panel1.Size = new System.Drawing.Size(498, 54);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(357, 5);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(522, 21);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.Location = new System.Drawing.Point(179, 5);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // montoAperturaText
            // 
            this.montoAperturaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.montoAperturaText.Location = new System.Drawing.Point(147, 140);
            this.montoAperturaText.Name = "montoAperturaText";
            this.montoAperturaText.Size = new System.Drawing.Size(176, 26);
            this.montoAperturaText.TabIndex = 112;
            this.montoAperturaText.Text = "0.00";
            this.montoAperturaText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.montoAperturaText_KeyDown);
            this.montoAperturaText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.montoAperturaText_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 111;
            this.label3.Text = "Monto Efectivo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.montoAperturaText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cajeroText);
            this.groupBox1.Controls.Add(this.cajeroIdText);
            this.groupBox1.Location = new System.Drawing.Point(16, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 223);
            this.groupBox1.TabIndex = 110;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cajeroText
            // 
            this.cajeroText.BackColor = System.Drawing.Color.White;
            this.cajeroText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cajeroText.Location = new System.Drawing.Point(147, 56);
            this.cajeroText.MaxLength = 200;
            this.cajeroText.Name = "cajeroText";
            this.cajeroText.ReadOnly = true;
            this.cajeroText.Size = new System.Drawing.Size(236, 26);
            this.cajeroText.TabIndex = 77;
            // 
            // cajeroIdText
            // 
            this.cajeroIdText.BackColor = System.Drawing.Color.SkyBlue;
            this.cajeroIdText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cajeroIdText.Location = new System.Drawing.Point(147, 19);
            this.cajeroIdText.Name = "cajeroIdText";
            this.cajeroIdText.Size = new System.Drawing.Size(125, 26);
            this.cajeroIdText.TabIndex = 71;
            this.cajeroIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cajeroIdText_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 78;
            this.label2.Text = "Cajero ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 79;
            this.label1.Text = "Cajero";
            // 
            // ventana_caja_apertura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 322);
            this.Controls.Add(this.groupBox1);
            this.Name = "ventana_caja_apertura";
            this.Text = "ventana_caja_apertura";
            this.Load += new System.EventHandler(this.ventana_caja_apertura_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox montoAperturaText;
        private Label label3;
        private GroupBox groupBox1;
        private TextBox cajeroText;
        private TextBox cajeroIdText;
        private Label label2;
        private Label label1;
    }
}