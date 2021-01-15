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
    public partial class GanadasDia : Form
    {

        //CONSULTA 4: Cuantas partidas ganó el usuario en un día x.

        public GanadasDia()
        {
            InitializeComponent();
        }

        public void setrespuesta (string a)
        {
            //Se escribe en el "label2" la respuesta del servidor, que es el número de partidas ganadas el dia introducido.
            label2.Text = "El día " + textBox1.Text + " ganaste " + a + " partidas.";
        }

        //Variable a la que se le asigna el nombre del usuario que ha iniciado el juego.
        string username;

        //Set para asignar el nombre del usuario que ha iniciado el juego.
        public void setName(string a)
        {
            this.username = a;
        }

        //Se establece el socket.
        Socket server;

        public void setServer(Socket a)
        {
            this.server = a;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Se envía el nombre del jugador y el dia sobre el cual queremos saber la consulta al servidor.
            if (textBox1.Text != "")
            {
                string mensaje = "4/" + username + "-" + textBox1.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void GanadasDia_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
