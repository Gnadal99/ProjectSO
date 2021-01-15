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
    public partial class Transicion : Form
    {
        //FORM DE TRANSICIÓN, TIENE LA FUNCIÓN DE REPRESENTAR UNA CUENTA ATRÁS PARA INICIAR LA PARTIDA.

        public Transicion()
        {
            InitializeComponent();
        }
        private int njugador;

        public void setNJugador(int a)
        {
            this.njugador = a;
        }
        //Declaración de PictureBoxes para las imágenes de los paneles.
        PictureBox moto = new PictureBox();
        PictureBox numero3 = new PictureBox();
        PictureBox numero2 = new PictureBox();
        PictureBox numero1 = new PictureBox();

        //Contador de segundos.
        int cont = 1;

        private void Transicion_Load(object sender, EventArgs e)
        {
            //Reloj/timer.
            timer.Interval = 1000;
            timer.Start();

            //Se introduce el gif de la moto en el panel.
            moto.Size = new Size(715, 330);
            moto.Location = new Point(0, 0);
            moto.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap image = new Bitmap("moto.gif");
            moto.Image = (Image)image;
            panel1.Controls.Add(moto);
            if (njugador == 1)
            {
                jugador.Text = "Eres el jugador azul";
                jugador.ForeColor= Color.Blue;
            }
            if (njugador == 2)
            {
                jugador.Text = "Eres el jugador rojo";
                jugador.ForeColor = Color.Red;
            }
            if (njugador == 3)
            {
                jugador.Text = "Eres el jugador amarillo";
                jugador.ForeColor = Color.Yellow;
            }
            if (njugador == 4)
            {
                jugador.Text = "Eres el jugador verde";
                jugador.ForeColor = Color.Green;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //En cada "tick" del reloj se cambiará la imagen de los segundos y estos incrementarán en el contador.

            if (cont == 1)//Primer segundo.
            {
                numero3.Size = new Size(180, 125);
                numero3.Location = new Point(10, 10);
                numero3.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image = new Bitmap("3-remove.jpg");
                numero3.Image = (Image)image;
                panel2.Controls.Add(numero3);
            }

            else if (cont == 2)//Segundo segundo.
            {
                panel2.Controls.Remove(numero3);
                numero2.Size = new Size(180, 125);
                numero2.Location = new Point(10, 10);
                numero2.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image = new Bitmap("2-removebg-preview.jpg");
                numero2.Image = (Image)image;
                panel2.Controls.Add(numero2);
            }

            else if (cont == 3)//Tercer segundo.
            {
                panel2.Controls.Remove(numero2);
                numero1.Size = new Size(180, 125);
                numero1.Location = new Point(10, 10);
                numero1.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image = new Bitmap("1-removebg-preview.jpg");
                numero1.Image = (Image)image;
                panel2.Controls.Add(numero1);
            }

            else //Al acabar los tres segundos, se cierra el form para empezar la partida.
            {
                Close();
            }

            cont++; //Incrementamos el contador de segundos. 
        }
    }
}
