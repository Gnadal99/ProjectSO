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
        //FORM PRINCIPAL: dónde se encuentra el menú principal y desde dónde se abren la mayoría de los otros formularios.

        //Variables globales.
        int estado;
        int numSala;
        int miemSala;
        string username;
        string mensaje2;
        int sonido;

        //Variables necesarias para establecer la conexión cliente-servidor.
        Socket server;
        Thread atender;
        
        //Creacion de los forms de las consultas.
        PartidasGanadas pg;
        HoraFecha hf;
        GanadasDia gd;
        Ganadores10min gm;
        Juego jk;

        //Creacion de los delegados para la solución al problema de los threads.
        delegate void DelegadoParaPonerTexto(string texto);
        delegate void DelegadoParaMover(string mensaje, string mensaje2);
        DelegadoParaMover delmover;
        DelegadoParaPonerTexto del;

        //Variable para reproducir la música.
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //Se carga la música al inicio del programa.
            player.SoundLocation = "segunda.wav";
            player.Play();
            sonido = 1;

            estado = 0;
            miemSala = 0;

            //Establecer la conexión cliente-servidor.
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

            //Guia para los creadores para ver qué usuario está conectado en cada cliente.
            label1.Text = "Usuario: " + username;
        }

        //Getters y setters que son utilizados para pasar los datos entre los diversos formularios.
        public void setServer(Socket a)   
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

        //Funciones para los delegados como solución al problema de los threads.
        public void PonChat(string mensaje)
        {
            //Creacion del chat.
            this.MeEnvian2.Text = this.MeEnvian1.Text;
            this.MeEnvian1.Text = this.MeEnvian.Text;
            this.MeEnvian.Text = mensaje;
        }

        public void PonServicios(string mensaje)
        {
            //Escribir el número de servicios realicados al servidor.
            this.servicios_rec.Text = mensaje;
        }

        public void PonPartidasGanadas(string mensaje)
        {
            //Pasar la respuesta recibida del servidor al formulario "PartidasGanadas".
            pg.setrespuesta(mensaje); 
        }
        public void IniciaJuego(string mensaje, string mensaje2)
        {
            //Creación del form del juego.
            jk = new Juego();
            jk.setNJugadores(Convert.ToInt32(mensaje));
            jk.setJugador(Convert.ToInt32(mensaje2));
            jk.setSala(numSala);
            jk.setServer(server);
            jk.ShowDialog();
        }
        public void PonGanadores10(string mensaje)/////////////////////////////////////////////////////heyyyy
        {
            //Pasar la respuesta recibida del servidor al formulario "PartidasGanadas".
            gm = new Ganadores10min();
            gm.setLista(mensaje);
            gm.ShowDialog();
        }

        public void PonHorayFecha (string fecha)
        {
            //Pasar la respuesta recibida del servidor al formulario "HoraFecha".
            hf.setrespuesta(fecha);
        }

        public void PonPartidasDia(string mensaje)
        {
            //Pasar la respuesta recibida del servidor al formulario "GanadasDia".
            gd.setrespuesta(mensaje);
        }

        public void PonConectados(string mensaje)
        {
            //Creación de la tabla dónde se muestran los usuarios conectados.
            string[] vector = new string[5];
            vector = mensaje.Split(',');
            ShowConectados.RowHeadersVisible = false;
            ShowConectados.ColumnHeadersVisible = false;
            ShowConectados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ShowConectados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShowConectados.RowCount = vector.Length;
            ShowConectados.ColumnCount = 1;

            //Rellenar la tabla dónde se muestran los usuarios conectados.
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
            ///////////////////////////////////////////////////////////////////////////////////////heyyyy
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
                MessageBox.Show("Error al crear la sala.");
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

            else if (vector5[1] == "reject")
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

        public void mueveJugador(string mensaje, string mensaje2)
        {
            jk.cambiarAutoDireccion(Convert.ToInt32(mensaje), Convert.ToInt32(mensaje2));
        }

        public void mataJugador(string mensaje)
        {
            jk.matarJugador(Convert.ToInt32(mensaje));
        }

        //Fuunción principal.
        /////////////////////////////////////ATENDER SERVER////////////////////////(////////////////////////////
        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos y dividimos en trozos el mensaje del servidor.
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                if (trozos.Length > 3)
                {
                    mensaje2 = trozos[2].Split('\0')[0];
                }
                string mensaje = trozos[1].Split('\0')[0];

                //Determinamos los casos según el código recibido.
                switch (codigo)
                {

                    case 1://Respuesta a partidas ganadas por un jugador x.
                        del = new DelegadoParaPonerTexto(PonPartidasGanadas);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 2://Respuesta a ganadores de partidas de con una duracion superior a 10 min.
                        del = new DelegadoParaPonerTexto(PonGanadores10);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 3://Respuesta a hora y fecha de una partida x.
                        del = new DelegadoParaPonerTexto(PonHorayFecha);
                        this.Invoke(del, new Object[] { mensaje + "/" + trozos[2] + "/" + trozos[3] });
                        break;

                    case 4: //Respuesta a partidas un día x.
                        del = new DelegadoParaPonerTexto(PonPartidasDia);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 6://Respuesta servicios realizados.
                        del = new DelegadoParaPonerTexto(PonServicios);
                        this.Invoke(del, new Object[] { "Número total de servicios: " + mensaje });
                        break;

                    case 7: //Respuesta a lista de conectados.
                        del = new DelegadoParaPonerTexto(PonConectados);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 20: //Respuesta a crear sala.
                        del = new DelegadoParaPonerTexto(PonCrearSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 21: //Respuesta al invitar.
                        del = new DelegadoParaPonerTexto(PonRespuestaInvitar);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 22: //Invitación.
                        del = new DelegadoParaPonerTexto(PonInvitacion);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 23: //Acepta o rechaza la invitación.
                        del = new DelegadoParaPonerTexto(PonAceptarRechazar);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 24://Salir de la sala.
                        del = new DelegadoParaPonerTexto(PonSalirSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 25: //Cierre de la sala.
                        del = new DelegadoParaPonerTexto(PonCierreSala);
                        this.Invoke(del, new Object[] { mensaje });
                        break;

                    case 253: //Se recibe el mensaje para el chat.
                        if (button2.Text == "Silenciar")//Únicamente si el usuario está activo, se trata el mensaje.
                        {
                            del = new DelegadoParaPonerTexto(PonChat);
                            this.Invoke(del, new Object[] { trozos[1] + ": " + trozos[2] });
                        }
                        break;
                    case 500:
                        //Creación del form de transición/cuenta atrás.
                        Transicion transicion = new Transicion();
                        transicion.setNJugador(Convert.ToInt32(trozos[2]));
                        transicion.ShowDialog();

                        ThreadStart ts = delegate { IniciaJuego(mensaje,trozos[2]); };
                        Thread tjuego = new Thread(ts);
                        tjuego.Start();
                        break;


                    case 501:
                        del = new DelegadoParaPonerTexto(mataJugador);
                        this.Invoke(del, new Object[] { mensaje });
                        break;
                    case 502:
                        delmover = new DelegadoParaMover(mueveJugador);
                        this.Invoke(delmover, new Object[] { mensaje , trozos[2] });
                        break;
                }
            }
        }
        ///////////////////////////////////////ATENDER SERVER//////////////////////////////////////////////////

        ///////////////////////////////////////MENÚ DESPLEGABLE////////////////////////////////////////////////
        private void cuantasPartidasHeGanadoEnTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opción para crear el form "PartidasGnadas" y consultar las partidas ganadas por un jugador x.
            pg = new PartidasGanadas();
            pg.setServer(server);
            pg.ShowDialog();
        }

        private void quienHaGanadoUnaPartidaDeMásDe10minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////heyyy
            string mensaje = "2/vacio";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void horaYFechaDeUnaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opción para crear el form "HoraFecha" y consultar la hora y la fecha de una partida x.
            hf = new HoraFecha();
            hf.setServer(server);
            hf.ShowDialog();
        }

        private void cuántasPartidasGanéElDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opción para crear el form "GanadasDia" y consultar las partidas el día x.
            gd = new GanadasDia();
            gd.setServer(server);
            gd.setName(username);
            gd.ShowDialog();
        }

        //Botón de desconexión.
        private void Desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            estado = 1;
            string mensaje = "0/"+username;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Desconexión.
            atender.Abort();
            this.BackColor = Color.Lavender;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Close();
        }

        //Botón para iniciar la partida.
        private void button1_Click(object sender, EventArgs e)
        {
            if (label4.Text.Split(' ')[1] == username)
            {
                //Mensaje de inicio de juego
                string mensaje = "500/" + numSala;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                
            }
            else
            {
                MessageBox.Show("La partida tiene que ser iniciada por el propietario de la sala");
            }
        }

        //Botón para crear la sala.
        private void CrearSala_Click(object sender, EventArgs e)
        {
            //Si la sala no está creada, se procede a enviar el mensaje al servidor para crearla.
            if (CrearSala.Text == "Crear sala")
            {
                //Mensaje de creación de la sala.
                string mensaje = "20/" + username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                Sala.RowCount = 4;
            }
        }

        //Evento que permite al usuario clickar en otro usuario conectado para invitarlo a la sala.
        private void ShowConectados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Invitación a la sala.
            DialogResult result = MessageBox.Show("Deseas invitar a este jugador?", "Invitar jugador", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //Mensaje de invitación a la sala.
                string mensaje = "21/" + Convert.ToString(numSala) + "/" + username + "/" + ShowConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        //Botón para salir de la sala.
        private void SalirSala_Click(object sender, EventArgs e)
        {
            //Si la sala está creada, se procede a enviar el mensaje al servidor para salir.
            if (CrearSala.Text == "Estas en sala")
            {
                CrearSala.Text = "Crear sala";//Permite volver a crear una sala nueva.
                string mensaje = "24/" + Convert.ToString(numSala) + "/" + username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                label4.Text = "No estás en ninguna sala";

                //Vaciar la tabla dela sala.
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

        //Botón para escribir en el chat.
        private void button3_Click(object sender, EventArgs e)
        {
            if (CrearSala.Text == "Estas en sala")//únicamente reciben mensajes los usuarios que forman parte de una misma sala.
            {
                //Mensaje al servidor.
                string mensaje = "25/" + numSala + "/" + username + "/" + Envio.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        //Botón para activar y desactivar el chat de mensajes.
        private void button2_Click(object sender, EventArgs e)
        {
            //Se silencia el chat.
            if(button2.Text == "Silenciar")
            {
                button2.Text = "Dejar de silenciar";
            }

            //Se deja de silenciar el chat.
            else if (button2.Text == "Dejar de silenciar")
            {
                button2.Text = "Silenciar";
            }
        }

        //Botón para pausar la música.
        private void musica_Click(object sender, EventArgs e)
        {
            if (sonido ==1)
            {
                player.Stop();
                Bitmap image = new Bitmap("activarsonido.png");
                musica.BackgroundImage = (Image)image;
                sonido = 0;
            }
            else if (sonido == 0)
            {
                player.Play();
                Bitmap image = new Bitmap("sssss3.png");
                musica.BackgroundImage = (Image)image;
                sonido = 1;
            }

        }
    }
}
