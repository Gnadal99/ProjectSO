﻿using System;
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
        int estado;
        string ListaConectados;
        Socket server;
        string username;
        PartidasGanadas pg;
        HoraFecha hf;
        GanadasDia gd;
        Ganadores10min gm; 

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
        public void setStatus (int a)
        {
            this.estado = a;
        }
        
        public int getStatus()
        {
            return estado;
        }

        public void setUser(string a)  
        {
            this.username = a;
        }

       
 //////////////////////////////////////// ATENDER SERVER/////////////////////////////////////////////

        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {

                    case 1:   // Respuesta a partidas ganadas por un jugador x

                        pg.setrespuesta(mensaje); 
                        break;

                    case 2: // Ganadores de partidas de >10min
                        
                        gm = new Ganadores10min();
                        gm.setLista(mensaje);
                        gm.ShowDialog();
                        break;
                        

                    case 3: // Respuesta a la hora y la fecha de una partida 

                        hf.setrespuesta(mensaje);
                        break;

                    case 4:  // Respuesta a partidas un día x

                        gd.setrespuesta(mensaje);
                        break;

                    case 27: // Respuesta a lista de conectados 

                        string[] vector = new string[5];
                        vector = ListaConectados.Split(',');

                        ShowConectados.RowHeadersVisible = false;
                        ShowConectados.ColumnHeadersVisible = false;
                        ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                        ShowConectados.RowCount = vector.Length;
                        ShowConectados.ColumnCount = 1;

                        int i = 0;
                        while (i < vector.Length)
                        {

                            if (i == 0)
                            {
                                ShowConectados.Rows[i].Cells[0].Value = "Número de conectados: " + vector[i];
                            }
                            else
                            {
                                ShowConectados.Rows[i].Cells[0].Value = vector[i];
                            }
                            i++;
                        }
                        break;

                    case 6: // Respuesta servicios realizados 

                        servicios_rec.Text = "Número total de servicios: " + mensaje;
                        break;

                   

                }
            }
        }


 //////////////////////////////////////// ATENDER SERVER/////////////////////////////////////////////
       

        private void cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            pg = new PartidasGanadas();
            pg.setServer(server);
            pg.ShowDialog();

        }

        private void quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            string mensaje = "2/vacio";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            /*//Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
       
            gm.setLista(mensaje);
            gm.ShowDialog();
            Ganadores10min gm = new Ganadores10min();*/
        }

        private void horaYFechaDeUnaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hf = new HoraFecha();
            hf.setServer(server);
            hf.ShowDialog();
        }

        private void cuántasPartidasGanéElDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gd = new GanadasDia();
            gd.setServer(server);
            gd.setName(username);
            gd.ShowDialog();
        }

        private void MostrarConectados_Click(object sender, EventArgs e)
        {

            string mensaje = "27/vacio";

            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            /* byte[] msg2 = new byte[80];
             server.Receive(msg2);
             ListaConectados = Encoding.ASCII.GetString(msg2).Split('\0')[0];

             string[] vector = new string[5];
             vector = ListaConectados.Split(',');

             ShowConectados.RowHeadersVisible = false;
             ShowConectados.ColumnHeadersVisible = false;
             ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
             ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
             ShowConectados.RowCount = vector.Length;
             ShowConectados.ColumnCount = 1;

             int i = 0;
             while (i < vector.Length)
             {

                 if (i == 0)
                 {
                     ShowConectados.Rows[i].Cells[0].Value = "Número de conectados: " + vector[i];
                 }
                 else
                 {
                     ShowConectados.Rows[i].Cells[0].Value = vector[i];
                 }
                 i++;
             }*/
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            estado = 1;
            string mensaje = "0/"+username;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Lavender;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Close();
        }

        /*private void Servicios_Click(object sender, EventArgs e)
        {
            // Pedir numero de srevicios realizados
            string mensaje = "6/numservicios";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            servicios_rec.Text = "Número total de servicios: " + mensaje;
        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            Juego jk = new Juego();
            jk.ShowDialog();

        }

        private void ShowConectados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Servicios_Click(object sender, EventArgs e)
        {

        }
    }
}
