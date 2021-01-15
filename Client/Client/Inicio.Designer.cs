namespace Client
{
    partial class Inicio
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelcolor = new System.Windows.Forms.Panel();
            this.panelcolor.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Start.BackgroundImage = global::Client.Properties.Resources.start;
            this.Start.FlatAppearance.BorderSize = 2;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.Start.Location = new System.Drawing.Point(12, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(353, 73);
            this.Start.TabIndex = 0;
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(602, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Catalina Mosteiro y Gerard Nadal";
            // 
            // panelcolor
            // 
            this.panelcolor.Controls.Add(this.Start);
            this.panelcolor.Location = new System.Drawing.Point(240, 386);
            this.panelcolor.Name = "panelcolor";
            this.panelcolor.Size = new System.Drawing.Size(376, 95);
            this.panelcolor.TabIndex = 1;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BackgroundImage = global::Client.Properties.Resources.luuuuuu;
            this.ClientSize = new System.Drawing.Size(810, 533);
            this.Controls.Add(this.panelcolor);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.panelcolor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelcolor;
    }
}