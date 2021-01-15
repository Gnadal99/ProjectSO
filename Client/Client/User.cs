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
using System.Threading;

namespace Client
{   
    public partial class User : Form
    {
        //FORM DE INICIO DE SESIÓN: de dónde se cargan los forms de entrada al juego y dónde hay el código que permite al usuario acceder y registrarse en el.

        //Variables necesarias para la conexión cliente-servidor.
        int estado = 0;
        Socket server;
        int puerto = 50005;

        //Variable de cración del formulario principal que deriva de este form.
        Principal prin;

        //Variable para poner la imagen.
        PictureBox welcome = new PictureBox();

        //Variable para reproducir música.
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public User()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Se carga la música de inicio des de la creación del formulario.
            player.SoundLocation = "primera.wav";
            player.Play();

            //Creación de la "portada" del juego.
            Inicio start = new Inicio();
            start.ShowDialog();

            //Creacion del fomulario de entrada al juego.
            Inicio_primero start2 = new Inicio_primero();
            start2.ShowDialog();

            //Código para poner la foto en el panel del formulario.
            welcome.Size = new Size(750, 300);
            welcome.Location = new Point(10, 10);
            welcome.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap image = new Bitmap("tron-logo-font-download1.jpg");
            welcome.Image = (Image)image;
            panel.Controls.Add(welcome);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && password.Text != "")
            {
                //Creamos un IPEndPoint con la dirección IP y puerto del servidor al que deseamos conectarnos.
                IPAddress direc = IPAddress.Parse("147.83.117.22");//147.83.117.22  192.168.56.101
                IPEndPoint ipep = new IPEndPoint(direc, puerto);

                //Creamos el socket .
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Intentamos conectar el socket.
                try
                {
                    server.Connect(ipep);
                }

                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos el error y salimos del programa con return.
                    MessageBox.Show("No he podido conectar con el servidor" + ex.Message);
                    return;
                }

                //Variables necesarias para el mensaje al servidor.
                string user = username.Text;
                string pass = password.Text;
                string mensaje = "100/" + user + "/" + pass;

                //Envío del mensaje al servidor.
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Se recibe la respuesta del servidor.
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                //Inicio de sesión correcto.
                if (mensaje == "100/Correct")
                {
                    //Stop a la musica de inicio.
                    player.Stop();

                    //Creación y transferencia de datos al formulaio principal.
                    prin = new Principal();
                    prin.setServer(server);
                    prin.setUser(user);
                    this.Hide();
                    prin.ShowDialog();

                    //Recibimos el estado del usuario del formulario principal por si este desea desconectarse del juego.
                    estado = prin.getStatus();

                    if (estado == 0)
                    {
                        //Mensaje de desconexión.
                        mensaje = "0/";
                        msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Desconexión.
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                    }

                    this.Close();
                }

                //Se informa al usuario mediante un MesssageBox del motivo por el cual el inicio de sesión ha fallado.
                else 
                {
                    //Usuario inexistente en la base de datos del juego.
                    if (mensaje == "100/NoUser")
                    {
                        MessageBox.Show("El usuario introducido no existe, porfavor regístrese.");
                    }

                    //La contraseña introducida por el usuarios es errónea.
                    else if (mensaje == "100/Incorrect")
                    {
                        MessageBox.Show("Contraseña incorrecta.");
                    }

                    //Mensaje de desconexión.
                    mensaje = "0/";
                    msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Desconexión.
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con la dirección IP y puerto del servidor al que deseamos conectarnos.
            IPAddress direc = IPAddress.Parse("147.83.117.22");//147.83.117.22  192.168.56.101
            IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket.
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Intentamos conectar el socket.
            try
            {
                server.Connect(ipep);
                MessageBox.Show("Conectado.");
            }

            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos el error y salimos del programa con return.
                MessageBox.Show("No se ha podido conectar con el servidor." + ex.Message);
                return;
            }

            //Variables necesarias para el mensaje al servidor.
            string user = username.Text;
            string pass = password.Text;
            string mensaje = "101/" + user + "/" + pass;

            //Envío del mensaje al servidor.
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Se recibe la respuesta del servidor.
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            //Se informa al usuario mediante un MesssageBox de cómo ha ido el registro.
            if (mensaje == "101/Correct")
            {
                MessageBox.Show("Registro completado.");
            }

            else if (mensaje == "101/Incorrect")
            {
                MessageBox.Show("El usuario ya existe.");
            }

            else if (mensaje == "101/Incorrect2")
            {
                MessageBox.Show("Error de registro, inténtalo de nuevo.");
            }

            //Desconexión.
            mensaje = "0/";
            msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

    }
}
