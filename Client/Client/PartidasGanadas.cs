using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;


namespace Client
{
    public partial class PartidasGanadas : Form
    {

        //CONSULTA 1: Cuantas partidas ha ganado un jugador x.

        public void setrespuesta(string a)
        {
            //Se escribe en el "label2" la respuesta del servidor.
             if (a == "1/NoExist")//El usuario introducido no existe.
                {
                    label2.Text = "El jugador " + usernameconsulta.Text + " no existe.";
                }

             else//El usuario introducido si existe, por lo tanto, se escribe el número de partidas ganadas.
                {
                    label2.Text = "El jugador " + usernameconsulta.Text + " ha ganado " + a + " partidas.";
                }
        }

        public PartidasGanadas()
        {
            InitializeComponent();
        }

        //Establecer el socket.
        Socket server;

        public void setServer(Socket a)   
        {
            this.server = a;
        }

        private void PartidasGanadas_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Enviar el nombre del jugador sobre el cual queremos saber la consulta al servidor.
            if (usernameconsulta.Text != "")
            {
                string mensaje = "1/" + usernameconsulta.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
