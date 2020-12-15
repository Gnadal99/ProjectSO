namespace Client
{
    partial class Principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.consultasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horaYFechaDeUnaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuántasPartidasGanéElDiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowConectados = new System.Windows.Forms.DataGridView();
            this.Desconectar = new System.Windows.Forms.Button();
            this.servicios_rec = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.Sala = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.SalirSala = new System.Windows.Forms.Button();
            this.CrearSala = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.MeEnvian1 = new System.Windows.Forms.Label();
            this.MeEnvian2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Envio = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.MeEnvian = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowConectados)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sala)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // consultasToolStripMenuItem
            // 
            this.consultasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem,
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem,
            this.horaYFechaDeUnaPartidaToolStripMenuItem,
            this.cuántasPartidasGanéElDiaToolStripMenuItem});
            this.consultasToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            this.consultasToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.consultasToolStripMenuItem.Text = "CONSULTAS";
            // 
            // cuantasPartidasHeGanadoEnTotalToolStripMenuItem
            // 
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Name = "cuantasPartidasHeGanadoEnTotalToolStripMenuItem";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Text = "¿Cuántas partidas ha ganado el jugador...";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Click += new System.EventHandler(this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click);
            // 
            // quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem
            // 
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Name = "quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Text = "¿Quién ha ganado una partida de más de 10min?";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Click += new System.EventHandler(this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click);
            // 
            // horaYFechaDeUnaPartidaToolStripMenuItem
            // 
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Name = "horaYFechaDeUnaPartidaToolStripMenuItem";
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Text = "Hora y fecha de una partida";
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Click += new System.EventHandler(this.horaYFechaDeUnaPartidaToolStripMenuItem_Click);
            // 
            // cuántasPartidasGanéElDiaToolStripMenuItem
            // 
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Name = "cuántasPartidasGanéElDiaToolStripMenuItem";
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Text = "¿Cuántas partidas gané el día...";
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Click += new System.EventHandler(this.cuántasPartidasGanéElDiaToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(613, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "*Haz click en el menú superior para hacer una consulta.";
            // 
            // ShowConectados
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.ShowConectados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ShowConectados.BackgroundColor = System.Drawing.Color.Lavender;
            this.ShowConectados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShowConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowConectados.Location = new System.Drawing.Point(31, 52);
            this.ShowConectados.Name = "ShowConectados";
            this.ShowConectados.ReadOnly = true;
            this.ShowConectados.Size = new System.Drawing.Size(137, 150);
            this.ShowConectados.TabIndex = 4;
            this.ShowConectados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShowConectados_CellClick);
            // 
            // Desconectar
            // 
            this.Desconectar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Desconectar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Desconectar.FlatAppearance.BorderSize = 2;
            this.Desconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Desconectar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desconectar.Location = new System.Drawing.Point(616, 42);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(106, 27);
            this.Desconectar.TabIndex = 5;
            this.Desconectar.Text = "Desconectar";
            this.Desconectar.UseVisualStyleBackColor = false;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // servicios_rec
            // 
            this.servicios_rec.AutoSize = true;
            this.servicios_rec.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicios_rec.Location = new System.Drawing.Point(9, 12);
            this.servicios_rec.Name = "servicios_rec";
            this.servicios_rec.Size = new System.Drawing.Size(0, 15);
            this.servicios_rec.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.servicios_rec);
            this.panel1.Location = new System.Drawing.Point(15, 391);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 34);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.ShowConectados);
            this.panel2.Location = new System.Drawing.Point(10, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 283);
            this.panel2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "Usuarios conectados";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(542, 391);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 34);
            this.button1.TabIndex = 10;
            this.button1.Text = "Iniciar partida";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Lavender;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.Sala);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.SalirSala);
            this.panel3.Controls.Add(this.CrearSala);
            this.panel3.Location = new System.Drawing.Point(221, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(268, 283);
            this.panel3.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(84, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "Sala de espera";
            // 
            // Sala
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Sala.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.Sala.BackgroundColor = System.Drawing.Color.Lavender;
            this.Sala.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Sala.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Sala.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.Sala.Location = new System.Drawing.Point(24, 98);
            this.Sala.Name = "Sala";
            this.Sala.Size = new System.Drawing.Size(213, 169);
            this.Sala.TabIndex = 13;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Nombre";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 69;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Estado";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "*No estás en ninguna sala.";
            // 
            // SalirSala
            // 
            this.SalirSala.BackColor = System.Drawing.Color.Transparent;
            this.SalirSala.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SalirSala.FlatAppearance.BorderSize = 2;
            this.SalirSala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SalirSala.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalirSala.Location = new System.Drawing.Point(137, 35);
            this.SalirSala.Name = "SalirSala";
            this.SalirSala.Size = new System.Drawing.Size(75, 30);
            this.SalirSala.TabIndex = 13;
            this.SalirSala.Text = "Salir sala";
            this.SalirSala.UseVisualStyleBackColor = false;
            this.SalirSala.Click += new System.EventHandler(this.SalirSala_Click);
            // 
            // CrearSala
            // 
            this.CrearSala.BackColor = System.Drawing.Color.Transparent;
            this.CrearSala.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CrearSala.FlatAppearance.BorderSize = 2;
            this.CrearSala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrearSala.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrearSala.Location = new System.Drawing.Point(39, 35);
            this.CrearSala.Name = "CrearSala";
            this.CrearSala.Size = new System.Drawing.Size(75, 30);
            this.CrearSala.TabIndex = 0;
            this.CrearSala.Text = "Crear sala";
            this.CrearSala.UseVisualStyleBackColor = false;
            this.CrearSala.Click += new System.EventHandler(this.CrearSala_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Lavender;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.MeEnvian1);
            this.panel4.Controls.Add(this.MeEnvian2);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.Envio);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.MeEnvian);
            this.panel4.Location = new System.Drawing.Point(504, 118);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(218, 212);
            this.panel4.TabIndex = 14;
            // 
            // MeEnvian1
            // 
            this.MeEnvian1.AutoSize = true;
            this.MeEnvian1.Location = new System.Drawing.Point(61, 84);
            this.MeEnvian1.Name = "MeEnvian1";
            this.MeEnvian1.Size = new System.Drawing.Size(0, 13);
            this.MeEnvian1.TabIndex = 19;
            // 
            // MeEnvian2
            // 
            this.MeEnvian2.AutoSize = true;
            this.MeEnvian2.Location = new System.Drawing.Point(61, 71);
            this.MeEnvian2.Name = "MeEnvian2";
            this.MeEnvian2.Size = new System.Drawing.Size(0, 13);
            this.MeEnvian2.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Chat de Sala";
            // 
            // Envio
            // 
            this.Envio.Location = new System.Drawing.Point(37, 111);
            this.Envio.Name = "Envio";
            this.Envio.Size = new System.Drawing.Size(128, 20);
            this.Envio.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(92, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 25);
            this.button3.TabIndex = 16;
            this.button3.Text = "Enviar";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MeEnvian
            // 
            this.MeEnvian.AutoSize = true;
            this.MeEnvian.Location = new System.Drawing.Point(61, 97);
            this.MeEnvian.Name = "MeEnvian";
            this.MeEnvian.Size = new System.Drawing.Size(0, 13);
            this.MeEnvian.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(218, 333);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 28);
            this.label6.TabIndex = 14;
            this.label6.Text = "*Si deseas invitar a un usuario a la sala, debes localizarlo \r\nen la lista de con" +
    "ectados y clickar sobre su nombre.";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(131, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 25);
            this.button2.TabIndex = 20;
            this.button2.Text = "Silenciar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(736, 463);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowConectados)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sala)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem consultasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuantasPartidasHeGanadoEnTotalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horaYFechaDeUnaPartidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuántasPartidasGanéElDiaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView ShowConectados;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Label servicios_rec;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CrearSala;
        private System.Windows.Forms.DataGridView Sala;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button SalirSala;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label MeEnvian;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox Envio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label MeEnvian1;
        private System.Windows.Forms.Label MeEnvian2;
        private System.Windows.Forms.Button button2;
    }
}