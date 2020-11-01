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
    public partial class Principal : Form
    {
        Socket server;
        string username;

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            label1.Text = "Usuario: " + username;
        }

        public void setServer(Socket a)   //Se utiliza para pasar transpasar los datos entre formularios
        {
            this.server = a;
        }

        public void setUser(string a)  
        {
            this.username = a;
        }

        private void cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartidasGanadas pg = new PartidasGanadas();
            pg.setServer(server);
            pg.ShowDialog();

        }

        private void quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ganadores10min gm = new Ganadores10min();

            string mensaje = "2/vacio";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
       
            gm.setLista(mensaje);
            gm.ShowDialog();
        }

        private void horaYFechaDeUnaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoraFecha hf = new HoraFecha();
            hf.setServer(server);
            hf.ShowDialog();
        }

        private void cuántasPartidasGanéElDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GanadasDia gd = new GanadasDia();
            gd.setServer(server);
            gd.setName(username);
            gd.ShowDialog();
        }
    }
}
