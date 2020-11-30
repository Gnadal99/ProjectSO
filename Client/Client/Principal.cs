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
using System.Threading;

namespace Client
{
    public partial class Principal : Form
    {
        int estado;
        int numSala;
        int miemSala;
        Socket server;
        string username;
        PartidasGanadas pg;
        HoraFecha hf;
        GanadasDia gd;
        Ganadores10min gm;
        Thread atender;

        public Principal()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            estado = 0;
            miemSala = 0;
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
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
                string mensaje = trozos[1].Split('\0')[0];

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
                        string fecha = mensaje + "/" + trozos[2] + "/" + trozos[3];
                        hf.setrespuesta(fecha);
                        break;

                    case 4:  // Respuesta a partidas un día x

                        gd.setrespuesta(mensaje);
                        break;


                    case 6: // Respuesta servicios realizados 

                        servicios_rec.Text = "Número total de servicios: " + mensaje;
                        break;


                    case 7: // Respuesta a lista de conectados 

                        string[] vector = new string[5];
                        vector = mensaje.Split(',');

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

                    case 20: //Respuesta a crear sala
                        string[] vector2 = new string[2];
                        vector2 = mensaje.Split(',');
                        if (vector2[0]=="correct")
                        {
                            label4.Text = "Propietario: " + username;
                            numSala = Convert.ToInt32(vector2[1]);
                            Sala.Rows[miemSala].Cells[0].Value = username;
                            Sala.Rows[miemSala].Cells[1].Value = "Aceptada";
                            miemSala++;
                            CrearSala.Text = "Sala creada";

                        }
                        else
                        {
                            MessageBox.Show("Error al crear la sala");
                        }

                        
                        break;

                    case 21: //Respuesta al invitar
                        string[] vector3 = new string[2];
                        vector3 = mensaje.Split(',');
                        if (vector3[0]=="correct")
                        {
                            MessageBox.Show("Invitación realizada con exito, espera a que acepte.");
                            Sala.Rows[miemSala].Cells[0].Value = vector3[1];
                            Sala.Rows[miemSala].Cells[1].Value = "Pendiente";
                            miemSala++;

                        }
                        else if (vector3[0] == "noexist")
                        {
                            MessageBox.Show("La sala no existe");
                        }
                        else if (vector3[0] == "llena")
                        {
                            MessageBox.Show("La sala esta llena");
                        }
                        break;

                    case 22: //Invitacion
                        string[] vector4 = new string[6];
                        vector4 = mensaje.Split(',');

                        DialogResult result = MessageBox.Show("El jugador "+vector4[1] + " te ha invitado a jugar, aceptas?", "Invitacion", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            numSala = Convert.ToInt32(vector4[0]);
                            mensaje = "22/" + Convert.ToString(numSala) + "/" + username + "/" + "accept";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);

                            label4.Text = "Propietario: " + vector4[1];
                            Sala.RowCount = 4;

                            int l = 0;
                            while (l+2<vector4.Length)
                            {
                                if (vector4[l]!=null)
                                {
                                    Sala.Rows[miemSala].Cells[0].Value = vector4[l+2];
                                    Sala.Rows[miemSala].Cells[1].Value = "Aceptado";
                                    miemSala++;
                                }
                                l++;
                            }
                        }
                        else
                        {
                            numSala = Convert.ToInt32(vector4[0]);
                            mensaje = "22/" + Convert.ToString(numSala) + "/" + username + "/" + "reject";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }

                        break;

                    case 23: //acepta o no invitacion
                        string[] vector5 = new string[2];
                        vector5 = mensaje.Split(',');

                        if (vector5[1] == "accept")
                        {
                            int it = 0;
                            while (it < 4)
                            {
                                if (Convert.ToString(Sala.Rows[it].Cells[0].Value) == vector5[0])
                                {
                                    Sala.Rows[it].Cells[1].Value = "Aceptado";
                                }
                                it++;
                            }
                        }
                        if (vector5[1] == "reject")
                        {
                            int it = 0;
                            MessageBox.Show("El usuario "+vector5[0]+" ha rechazado la solicitud.");
                            while (it < 4)
                            {
                                if (Convert.ToString(Sala.Rows[it].Cells[0].Value) == vector5[0])
                                {
                                    Sala.Rows[it].Cells[0].Value = "";
                                    Sala.Rows[it].Cells[1].Value = "";
                                    miemSala--;
                                }
                                it++;
                            }
                        }

                        break;

                    case 24://Salir de la sala
                        int il = 0;
                        bool encontrado = false;
                        while (il < 4)
                        {
                            if (encontrado)
                            {
                                Sala.Rows[il-1].Cells[0].Value = Sala.Rows[il].Cells[0].Value;
                                Sala.Rows[il-1].Cells[1].Value = Sala.Rows[il].Cells[1].Value;
                            }
                            if (!encontrado)
                            {
                                if (Convert.ToString(Sala.Rows[il].Cells[0].Value) == mensaje)
                                {
                                    encontrado = true;
                                    miemSala--;
                                }
                            }
                            il++;
                        }
                        Sala.Rows[3].Cells[0].Value = "";
                        Sala.Rows[3].Cells[1].Value = "";
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

       

        private void Desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            estado = 1;
            string mensaje = "0/"+username;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Lavender;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Close();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            Juego jk = new Juego();
            jk.ShowDialog();

        }

        private void CrearSala_Click(object sender, EventArgs e)
        {
            if (CrearSala.Text == "Crear sala")
            {
                string mensaje = "20/" + username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                Sala.RowCount = 4;
            }
        }

        private void ShowConectados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseas invitar a este jugador?", "Invitar jugador", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string mensaje = "21/" + Convert.ToString(numSala) + "/" + username + "/" + ShowConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
        }

        private void SalirSala_Click(object sender, EventArgs e)
        {
            string mensaje = "24/" + Convert.ToString(numSala) + "/" + username;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            label4.Text = "No estás en ninguna sala";
            int it = 0;
            while (it < 4)
            {
                Sala.Rows[it].Cells[0].Value = "";
                Sala.Rows[it].Cells[1].Value = "";
                miemSala--;
                it++;
            }
            miemSala = 0;
        }
    }
}
