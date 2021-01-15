using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Client
{
    public partial class Inicio : Form
    {
        //FORM DE INICIO, su funcion es dar la imagen de comienzo/portada del juego.

        public Inicio()
        {
            InitializeComponent();
        }

        //Contador de segundos.
        int cont = 0;

        private void Inicio_Load(object sender, EventArgs e)
        {
            //Comienzo y definicion del timer/reloj.
            timer1.Start();
            timer1.Interval = 1250;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //Al clickar el botón se cerrará el fomr, para comenzar la transición de inicio y posteriormente acceder al panel principal.
            Close();
        }

        //Parpadeo del botón "Start".
        private void timer1_Tick(object sender, EventArgs e)
        {
            //En cada "tick" del timer se cambia el fondo del panel para crear el efecto de parpadeo.
            if (cont % 2 == 0)//Si los segundos son pares, el color de fondo será el blanco.
                panelcolor.BackColor = Color.White;
            
            else//Si los segundos son inpares, el color de fondo será el negro.
                panelcolor.BackColor = Color.Transparent;

            cont++;
        }
    }
}
