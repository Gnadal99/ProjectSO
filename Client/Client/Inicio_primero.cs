using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Inicio_primero : Form
    {
        //FORM DE TRANSICIÓN INTRODUCTORIO AL JUEGO.

        public Inicio_primero()
        {
            InitializeComponent();
        }

        //Declaración del PictureBox que se utilizará para el gif. 
        PictureBox welcome = new PictureBox();

        private void Inicio_primero_Load(object sender, EventArgs e)
        {
            //Se define un timer con el tiempo de duración del gif para que este no se repita.
            timer.Interval = 2550;
            timer.Start();

            //Se introduce el gif en el panel.
            welcome.Size = new Size(850, 550);
            welcome.Location = new Point(0, 0);
            welcome.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap image = new Bitmap("nombre.gif");
            welcome.Image = (Image)image;
            panel1.Controls.Add(welcome);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Al hacer "tick" se cierra el form.
            Close();
        }
    }
}
