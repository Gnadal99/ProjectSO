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

        
    }
}
