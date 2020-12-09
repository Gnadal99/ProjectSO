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
        string mensaje2;
        delegate void DelegadoParaPonerTexto(string texto);
        DelegadoParaPonerTexto del;

        public Principal()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
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

        public void PonChat(string mensaje)
        {
            this.MeEnvian2.Text = this.MeEnvian1.Text;
            this.MeEnvian1.Text = this.MeEnvian.Text;
            this.MeEnvian.Text = mensaje;
        }

        public void PonServicios(string mensaje)
        {
            this.servicios_rec.Text = mensaje;
        }

        public void PonPartidasGanadas(string mensaje)
        {
            pg.setrespuesta(mensaje); 
        }

        public void PonGanadores10(string mensaje)
        {
            gm = new Ganadores10min();
            gm.setLista(mensaje);
            gm.ShowDialog();
        }

        public void PonHorayFecha (string fecha)
        {
            hf.setrespuesta(fecha);
        }

        public void PonPartidasDia(string mensaje)
        {
            gd.setrespuesta(mensaje);
        }

        public void PonConectados(string mensaje)
        {
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
        }

        public void PonCrearSala(string mensaje)
        {
            string[] vector2 = new string[2];
            vector2 = mensaje.Split(',');
            if (vector2[0] == "correct")
            {
                label4.Text = "Propietario: " + username;
                numSala = Convert.ToInt32(vector2[1]);
                Sala.Rows[miemSala].Cells[0].Value = username;
                Sala.Rows[miemSala].Cells[1].Value = "Aceptada";
                miemSala++;
                CrearSala.Text = "Estas en sala";
            }
            else
            {
                MessageBox.Show("Error al crear la sala");
            }
        }

        public void PonRespuestaInvitar(string mensaje)
        {
            string[] vector3 = new string[2];
            vector3 = mensaje.Split(',');
            if (vector3[0] == "correct")
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
        }

        public void PonInvitacion (string mensaje)
        {
            string[] vector4 = new string[6];
            vector4 = mensaje.Split(',');
            if (CrearSala.Text == "Crear sala")
            {
                DialogResult result = MessageBox.Show("El jugador " + vector4[1] + " te ha invitado a jugar, aceptas?", "Invitacion", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    numSala = Convert.ToInt32(vector4[0]);
                    mensaje = "22/" + Convert.ToString(numSala) + "/" + username + "/" + "accept";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    label4.Text = "Propietario: " + vector4[1];
                    Sala.RowCount = 4;

                    int l = 0;
                    while (l + 2 < vector4.Length)
                    {
                        if (vector4[l] != null)
                        {
                            Sala.Rows[miemSala].Cells[0].Value = vector4[l + 2];
                            Sala.Rows[miemSala].Cells[1].Value = "Aceptado";
                            miemSala++;
                        }
                        l++;
                    }
                    CrearSala.Text = "Estas en sala";
                }
                else
                {
                    numSala = 0;
                    mensaje = "22/" + vector4[0] + "/" + username + "/" + "reject";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            else
            {
                numSala = 0;
                mensaje = "22/" + vector4[0] + "/" + username + "/" + "reject";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        public void PonAceptarRechazar (string mensaje)
        {
            string[] vector5 = new string[2];
            vector5 = mensaje.Split(',');

            if (vector5[1] == "accept")
            {
                int ip = 0;
                while (ip < 4)
                {
                    if (Convert.ToString(Sala.Rows[ip].Cells[0].Value) == vector5[0])
                    {
                        Sala.Rows[ip].Cells[1].Value = "Aceptado";
                    }
                    ip++;
                }
            }
            if (vector5[1] == "reject")
            {
                int ip = 0;
                MessageBox.Show("El usuario " + vector5[0] + " ha rechazado la solicitud.");
                while (ip < 4)
                {
                    if (Convert.ToString(Sala.Rows[ip].Cells[0].Value) == vector5[0])
                    {
                        Sala.Rows[ip].Cells[0].Value = "";
                        Sala.Rows[ip].Cells[1].Value = "";
                        miemSala--;
                    }
                    ip++;
                }
            }
        }

        public void PonSalirSala(string mensaje)
        {
            int il = 0;
            bool encontrado = false;
            while (il < 4)
            {
                if (encontrado)
                {
                    Sala.Rows[il - 1].Cells[0].Value = Sala.Rows[il].Cells[0].Value;
                    Sala.Rows[il - 1].Cells[1].Value = Sala.Rows[il].Cells[1].Value;
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
        }

        public void PonCierreSala(string mensaje)
        {
            int it = 0;
            CrearSala.Text = "Crear sala";
            label4.Text = "No estás en ninguna sala";
            while (it < 4)
            {
                Sala.Rows[it].Cells[0].Value = "";
                Sala.Rows[it].Cells[1].Value = "";
                miemSala--;
                it++;
            }
            miemSala = 0;
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
                if (trozos.Length > 3)
                {
                    mensaje2 = trozos[2].Split('\0')[0];
                }
                string mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {

                    case 1:   // Respuesta a partidas ganadas por un jugador x
                        del = new DelegadoParaPonerTexto(PonPartidasGanadas);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 2: // Ganadores de partidas de > 10min
                        del = new DelegadoParaPonerTexto(PonGanadores10);
                        this.Invoke(del, new Object[] { mensaje });
                        break;


                    case 3: // Respuesta a la hora y la fecha de una partida 
                        del = new DelegadoParaPonerTexto(PonHorayFecha);
                        this.Invoke(del, new Object[] { mensaje + "/" + trozos[2] + "/" + trozos[3] });
                        //string fecha = mensaje + "/" + trozos[2] + "/" + trozos[3];
                        break;

                    case 4:  // Respuesta a partidas un día x
                        del = new DelegadoParaPonerTexto(PonPartidasDia);
                        this.Invoke(del, new Object[] { mensaje });
                        break;


                    case 6: // Respuesta servicios realizados 
                        del = new DelegadoParaPonerTexto(PonServicios);
                        this.Invoke(del, new Object[] { "Número total de servicios: " + mensaje });
                        break;


                    case 7: // Respuesta a lista de conectados
                        del = new DelegadoParaPonerTexto(PonConectados);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 20: //Respuesta a crear sala
                        del = new DelegadoParaPonerTexto(PonCrearSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 21: //Respuesta al invitar
                        del = new DelegadoParaPonerTexto(PonRespuestaInvitar);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 22: //Invitacion
                        del = new DelegadoParaPonerTexto(PonInvitacion);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 23: //Acepta o rechaza la invitacion
                        del = new DelegadoParaPonerTexto(PonAceptarRechazar);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 24://Salir de la sala
                        del = new DelegadoParaPonerTexto(PonSalirSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 25: //Se cierra sala
                        del = new DelegadoParaPonerTexto(PonCierreSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 253: //Recibo el mensaje
                        del = new DelegadoParaPonerTexto(PonChat);
                        this.Invoke(del, new Object[] { trozos[1] + ": " + trozos[2]});
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
            if (CrearSala.Text == "Estas en sala")
            {
                CrearSala.Text = "Crear sala";
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

        private void button3_Click(object sender, EventArgs e)
        {
            //Enviar mensaje
            string mensaje = "25/" + username + "/" + Envio.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void ActivarChat_Click(object sender, EventArgs e)
        {
            if (ActivarChat.Text == "Activar chat")
            {
                //Activar el chat
                ActivarChat.Text = "Desactivar chat";
                string mensaje = "26/" + username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (ActivarChat.Text == "Desactivar chat")
            {
                //Desactivar el chat
                ActivarChat.Text = "Activar chat";
                string mensaje = "27/" + username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        
    }
}
