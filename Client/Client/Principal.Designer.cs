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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.consultasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horaYFechaDeUnaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuántasPartidasGanéElDiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(455, 24);
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
            this.consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            this.consultasToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.consultasToolStripMenuItem.Text = "Consultas";
            // 
            // cuantasPartidasHeGanadoEnTotalToolStripMenuItem
            // 
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Name = "cuantasPartidasHeGanadoEnTotalToolStripMenuItem";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Text = "Cuantas partidas ha ganado el jugador...";
            this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem.Click += new System.EventHandler(this.cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click);
            // 
            // quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem
            // 
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Name = "quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Text = "Quien ha ganado una partida de más de 10min";
            this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem.Click += new System.EventHandler(this.quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click);
            // 
            // horaYFechaDeUnaPartidaToolStripMenuItem
            // 
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Name = "horaYFechaDeUnaPartidaToolStripMenuItem";
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.horaYFechaDeUnaPartidaToolStripMenuItem.Text = "Hora y fecha de una partida";
            // 
            // cuántasPartidasGanéElDiaToolStripMenuItem
            // 
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Name = "cuántasPartidasGanéElDiaToolStripMenuItem";
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.cuántasPartidasGanéElDiaToolStripMenuItem.Text = "Cuántas partidas gané el dia ...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario: ";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 297);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}