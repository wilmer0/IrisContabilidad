﻿namespace IrisContabilidad.modulo_cuenta_por_cobrar
{
    partial class ventana_venta_anular_devolucion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nombreText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioButtonFecha = new System.Windows.Forms.RadioButton();
            this.radioButtonID = new System.Windows.Forms.RadioButton();
            this.radioButtonIdVenta = new System.Windows.Forms.RadioButton();
            this.radioButtonCliente = new System.Windows.Forms.RadioButton();
            this.radioButtonEmpleado = new System.Windows.Forms.RadioButton();
            this.radioButtonTipoVenta = new System.Windows.Forms.RadioButton();
            this.codigoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 569);
            this.panel1.Size = new System.Drawing.Size(1089, 54);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(948, 5);
            this.button1.Text = "Anular Devolución";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(1113, 21);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.Location = new System.Drawing.Point(474, 5);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTipoVenta);
            this.groupBox1.Controls.Add(this.radioButtonEmpleado);
            this.groupBox1.Controls.Add(this.radioButtonCliente);
            this.groupBox1.Controls.Add(this.radioButtonIdVenta);
            this.groupBox1.Controls.Add(this.radioButtonFecha);
            this.groupBox1.Controls.Add(this.radioButtonID);
            this.groupBox1.Controls.Add(this.nombreText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1082, 84);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // nombreText
            // 
            this.nombreText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreText.Location = new System.Drawing.Point(81, 19);
            this.nombreText.Name = "nombreText";
            this.nombreText.Size = new System.Drawing.Size(396, 26);
            this.nombreText.TabIndex = 20;
            this.nombreText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nombreText_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Buscar";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoColumn,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(16, 117);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1082, 446);
            this.dataGridView1.TabIndex = 29;
            // 
            // radioButtonFecha
            // 
            this.radioButtonFecha.AutoSize = true;
            this.radioButtonFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFecha.Location = new System.Drawing.Point(139, 51);
            this.radioButtonFecha.Name = "radioButtonFecha";
            this.radioButtonFecha.Size = new System.Drawing.Size(70, 21);
            this.radioButtonFecha.TabIndex = 24;
            this.radioButtonFecha.Text = "Fecha";
            this.radioButtonFecha.UseVisualStyleBackColor = true;
            // 
            // radioButtonID
            // 
            this.radioButtonID.AutoSize = true;
            this.radioButtonID.Checked = true;
            this.radioButtonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonID.Location = new System.Drawing.Point(81, 51);
            this.radioButtonID.Name = "radioButtonID";
            this.radioButtonID.Size = new System.Drawing.Size(41, 21);
            this.radioButtonID.TabIndex = 23;
            this.radioButtonID.TabStop = true;
            this.radioButtonID.Text = "ID";
            this.radioButtonID.UseVisualStyleBackColor = true;
            // 
            // radioButtonIdVenta
            // 
            this.radioButtonIdVenta.AutoSize = true;
            this.radioButtonIdVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonIdVenta.Location = new System.Drawing.Point(225, 51);
            this.radioButtonIdVenta.Name = "radioButtonIdVenta";
            this.radioButtonIdVenta.Size = new System.Drawing.Size(88, 21);
            this.radioButtonIdVenta.TabIndex = 25;
            this.radioButtonIdVenta.Text = "ID Venta";
            this.radioButtonIdVenta.UseVisualStyleBackColor = true;
            // 
            // radioButtonCliente
            // 
            this.radioButtonCliente.AutoSize = true;
            this.radioButtonCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCliente.Location = new System.Drawing.Point(333, 51);
            this.radioButtonCliente.Name = "radioButtonCliente";
            this.radioButtonCliente.Size = new System.Drawing.Size(76, 21);
            this.radioButtonCliente.TabIndex = 26;
            this.radioButtonCliente.Text = "Cliente";
            this.radioButtonCliente.UseVisualStyleBackColor = true;
            // 
            // radioButtonEmpleado
            // 
            this.radioButtonEmpleado.AutoSize = true;
            this.radioButtonEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEmpleado.Location = new System.Drawing.Point(426, 51);
            this.radioButtonEmpleado.Name = "radioButtonEmpleado";
            this.radioButtonEmpleado.Size = new System.Drawing.Size(97, 21);
            this.radioButtonEmpleado.TabIndex = 27;
            this.radioButtonEmpleado.Text = "Empleado";
            this.radioButtonEmpleado.UseVisualStyleBackColor = true;
            // 
            // radioButtonTipoVenta
            // 
            this.radioButtonTipoVenta.AutoSize = true;
            this.radioButtonTipoVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTipoVenta.Location = new System.Drawing.Point(543, 51);
            this.radioButtonTipoVenta.Name = "radioButtonTipoVenta";
            this.radioButtonTipoVenta.Size = new System.Drawing.Size(105, 21);
            this.radioButtonTipoVenta.TabIndex = 28;
            this.radioButtonTipoVenta.Text = "Tipo Venta";
            this.radioButtonTipoVenta.UseVisualStyleBackColor = true;
            // 
            // codigoColumn
            // 
            this.codigoColumn.FillWeight = 20F;
            this.codigoColumn.HeaderText = "ID";
            this.codigoColumn.Name = "codigoColumn";
            this.codigoColumn.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 40F;
            this.Column1.HeaderText = "Fecha";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 40F;
            this.Column2.HeaderText = "ID Venta";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Cliente";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Empleado";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 60F;
            this.Column5.HeaderText = "Tipo Venta";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // ventana_venta_anular_devolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 635);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ventana_venta_anular_devolucion";
            this.Text = "ventana_venta_anular_devolucion";
            this.Load += new System.EventHandler(this.ventana_venta_anular_devolucion_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox nombreText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioButtonFecha;
        private System.Windows.Forms.RadioButton radioButtonID;
        private System.Windows.Forms.RadioButton radioButtonTipoVenta;
        private System.Windows.Forms.RadioButton radioButtonEmpleado;
        private System.Windows.Forms.RadioButton radioButtonCliente;
        private System.Windows.Forms.RadioButton radioButtonIdVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}