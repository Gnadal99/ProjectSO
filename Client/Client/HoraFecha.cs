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
using System.Runtime.Remoting.Channels;

namespace Client
{
    public partial class HoraFecha : Form
    {
        //CONSULTA 3: indicar la hora y la fecha de una partida x mediante el ID.

        public HoraFecha()

        {
            InitializeComponent();
        }

        //Establecer el socket
        Socket server;

        public void setServer(Socket a)
        {
            this.server = a;
        }

        private void HoraFecha_Load(object sender, EventArgs e)
        {

        }

        public void setrespuesta(string a)
        {
            //Se escribe en el "label2" la respuesta del servidor.
            if (a == "3/NoExist")//El ID de la partida introducido no existe.
            {
                label2.Text = "La partida con el ID: " + IDpartida.Text + " no existe.";
            }

            else//El ID introducido si existe, por lo tanto, se escribe la hora y la fecha de la partida.
            {
                label2.Text = "La partida fue jugada el " + a + ".";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Se envia el ID de la partida sobre la cual queremos saber la consulta al servidor.
            if (IDpartida.Text != "")
            {
                string mensaje = "3/" + IDpartida.Text;
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
