using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace Client
{
    public partial class Juego : Form
    {
        int njugadores;
        int jugadoresrestantes;
        static int jugador;
        static int sala;
        static Socket server;

        int jugador1vivo = 1;
        int jugador2vivo = 1;
        int jugador3vivo = 1;
        int jugador4vivo = 1;

        PictureBox jugador1;
        PictureBox[] ocupados1;

        PictureBox jugador2;
        PictureBox[] ocupados2;

        PictureBox jugador3;
        PictureBox[] ocupados3;

        PictureBox jugador4;
        PictureBox[] ocupados4;
        int i;

        public void setNJugadores(int a)
        {
            njugadores = a;
            jugadoresrestantes = a;
        }

        public void setJugador(int a)
        {
            jugador = a;
        }
        public void setSala(int a)
        {
            sala = a;
        }

        public void setServer(Socket a)
        {
            server = a;
        }

        public void matarJugador(int a)
        {
            if (a == 1)
            {
                jugador1vivo = 0;
                panel1.Controls.Remove(jugador1);
                jugador1 = null;
                for (int i = 0; i < ocupados1.Length; i++)
                {
                    panel1.Controls.Remove(ocupados1[i]);
                    ocupados1[i] = null;
                }
            }
            else if (a == 2)
            {
                jugador2vivo = 0;
                panel1.Controls.Remove(jugador2);
                jugador2 = null;
                for (int i = 0; i < ocupados2.Length; i++)
                {
                    panel1.Controls.Remove(ocupados2[i]);
                    ocupados2[i] = null;
                }
            }
            else if (a == 3)
            {
                jugador3vivo = 0;
                panel1.Controls.Remove(jugador3);
                jugador3 = null;
                for (int i = 0; i < ocupados3.Length; i++)
                {
                    panel1.Controls.Remove(ocupados3[i]);
                    ocupados3[i] = null;
                }
            }
            else if (a == 4)
            {
                jugador4vivo = 0;
                panel1.Controls.Remove(jugador4);
                jugador4 = null;
                for (int i = 0; i < ocupados4.Length; i++)
                {
                    panel1.Controls.Remove(ocupados4[i]);
                    ocupados4[i] = null;
                }
            }

            this.jugadoresrestantes = this.jugadoresrestantes - 1;
        }
        public void cambiarAutoDireccion(int jug, int dir)
        {
            if (jug == 1)
            {
                if (dir == arriba)
                {
                    if (direccionJugador1 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 0));
                        }

                        jugador1.ClientSize = new Size(15, 30);
                        jugador1.Location = new Point(y1.X - 7, y1.Y);
                        jugador1.Image = (Image)motorotada;
                        
                    }
                    if (direccionJugador1 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(15, 30);
                        jugador1.Location = new Point(y1.X - 7, y1.Y);
                        jugador1.Image = (Image)motorotada;
                    }
                }
                if (dir == abajo)
                {
                    if (direccionJugador1 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(15, 30);
                        jugador1.Location = new Point(y1.X - 7, y1.Y - 30);
                       
                        jugador1.Image = (Image)motorotada;
                    }
                    if (direccionJugador1 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(15, 30);
                        jugador1.Location = new Point(y1.X - 7, y1.Y - 30);
                        
                        jugador1.Image = (Image)motorotada;
                    }
                }
                if (dir == izquierda)
                {
                    if (direccionJugador1 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(30, 15);
                        jugador1.Location = new Point(y1.X, y1.Y - 7);
                       
                        jugador1.Image = (Image)motorotada;
                    }
                    if (direccionJugador1 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(30, 15);
                        jugador1.Location = new Point(y1.X, y1.Y - 7);
                        
                        jugador1.Image = (Image)motorotada;
                    }
                }
                if (dir == derecha)
                {
                    if (direccionJugador1 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(30, 15);
                        jugador1.Location =new Point(y1.X - 30, y1.Y - 7);
                        jugador1.Image = (Image)motorotada;
                    }
                    if (direccionJugador1 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoazul.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                            g.DrawImage(jugador1.Image, new Point(0, 5));
                        }

                        jugador1.ClientSize = new Size(30, 15);
                        jugador1.Location = new Point(y1.X - 30, y1.Y - 7);
                        jugador1.Image = (Image)motorotada;
                    }
                }
                direccionJugador1 = dir;
            }
            else if (jug == 2)
            {
                if (dir == arriba)
                {
                    if (direccionJugador2 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(15, 30);
                        jugador2.Location = new Point(y2.X - 7, y2.Y);
                        jugador2.Image = (Image)motorotada;
                    }
                    if (direccionJugador2 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(15, 30);
                        jugador2.Location = new Point(y2.X - 7, y2.Y);
                        jugador2.Image = (Image)motorotada;
                    }
                }
                if (dir == abajo)
                {
                    if (direccionJugador2 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(15, 30);
                        jugador2.Location = new Point(y2.X - 7, y2.Y - 30);
                        jugador2.Image = (Image)motorotada;
                    }
                    if (direccionJugador2 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(15, 30);
                        jugador2.Location = new Point(y2.X - 7, y2.Y - 30);
                        jugador2.Image = (Image)motorotada;
                    }
                }
                if (dir == izquierda)
                {
                    if (direccionJugador2 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(30, 15);
                        jugador2.Location = new Point(y2.X, y2.Y - 7);
                        jugador2.Image = (Image)motorotada;
                    }
                    if (direccionJugador2 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(30, 15);
                        jugador2.Location = new Point(y2.X, y2.Y - 7);
                        jugador2.Image = (Image)motorotada;
                    }
                }
                if (dir == derecha)
                {
                    if (direccionJugador2 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(30, 15);

                        jugador2.Location = new Point(y2.X - 30, y2.Y - 7);
                        
                        jugador2.Image = (Image)motorotada;
                    }
                    if (direccionJugador2 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoroja.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                            g.DrawImage(jugador2.Image, new Point(0, 5));
                        }

                        jugador2.ClientSize = new Size(30, 15); 
                        jugador2.Location = new Point(y2.X - 30, y2.Y - 7);
                        jugador2.Image = (Image)motorotada;
                    }
                }
                direccionJugador2 = dir;
            }
            else if (jug == 3)
            {
                if (dir == arriba)
                {
                    if (direccionJugador3 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(15, 30);
                        jugador3.Location = new Point(y3.X - 7, y3.Y);
                        jugador3.Image = (Image)motorotada;
                    }
                    if (direccionJugador3 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(15, 30);
                        jugador3.Location = new Point(y3.X - 7, y3.Y);
                        jugador3.Image = (Image)motorotada;
                    }
                }
                if (dir == abajo)
                {
                    if (direccionJugador3 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(15, 30);
                        jugador3.Location = new Point(y3.X - 7, y3.Y - 30);
                        jugador3.Image = (Image)motorotada;
                    }
                    if (direccionJugador3 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(15, 30);
                        jugador3.Location = new Point(y3.X - 7, y3.Y - 30);
                        jugador3.Image = (Image)motorotada;
                    }
                }
                if (dir == izquierda)
                {
                    if (direccionJugador3 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(30, 15);
                        jugador3.Location = new Point(y3.X, y3.Y - 7);
                        jugador3.Image = (Image)motorotada;
                    }
                    if (direccionJugador3 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(30, 15);
                        jugador3.Location = new Point(y3.X, y3.Y - 7);
                        jugador3.Image = (Image)motorotada;
                    }
                }
                if (dir == derecha)
                {
                    if (direccionJugador3 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(30, 15);

                        jugador3.Location = new Point(y3.X - 30, y3.Y - 7);

                        jugador3.Image = (Image)motorotada;
                    }
                    if (direccionJugador3 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoamarilla.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                            g.DrawImage(jugador3.Image, new Point(0, 5));
                        }

                        jugador3.ClientSize = new Size(30, 15);
                        jugador3.Location = new Point(y3.X - 30, y3.Y - 7);
                        jugador3.Image = (Image)motorotada;
                    }
                }
                direccionJugador3 = dir;
            }
            else if (jug == 4)
            {
                if (dir == arriba)
                {
                    if (direccionJugador4 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(15, 30);
                        jugador4.Location = new Point(y4.X - 7, y4.Y);
                        jugador4.Image = (Image)motorotada;
                    }
                    if (direccionJugador4 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(15, 30);
                        jugador4.Location = new Point(y4.X - 7, y4.Y);
                        jugador4.Image = (Image)motorotada;
                    }
                }
                if (dir == abajo)
                {
                    if (direccionJugador4 == izquierda)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(15, 30);
                        jugador4.Location = new Point(y4.X - 7, y4.Y - 30);
                        jugador4.Image = (Image)motorotada;
                    }
                    if (direccionJugador4 == derecha)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(15, 30);
                        jugador4.Location = new Point(y4.X - 7, y4.Y - 30);
                        jugador4.Image = (Image)motorotada;
                    }
                }
                if (dir == izquierda)
                {
                    if (direccionJugador4 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(30, 15);
                        jugador4.Location = new Point(y4.X, y4.Y - 7);
                        jugador4.Image = (Image)motorotada;
                    }
                    if (direccionJugador4 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(30, 15);
                        jugador4.Location = new Point(y4.X, y4.Y - 7);
                        jugador4.Image = (Image)motorotada;
                    }
                }
                if (dir == derecha)
                {
                    if (direccionJugador4 == abajo)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(-90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(30, 15);

                        jugador4.Location = new Point(y4.X - 30, y4.Y - 7);

                        jugador4.Image = (Image)motorotada;
                    }
                    if (direccionJugador4 == arriba)
                    {
                        Bitmap motorotada = new Bitmap("motoverde.png");
                        using (Graphics g = Graphics.FromImage(motorotada))
                        {
                            g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                            g.RotateTransform((float)(90));
                            g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                            g.DrawImage(jugador4.Image, new Point(0, 5));
                        }

                        jugador4.ClientSize = new Size(30, 15);
                        jugador4.Location = new Point(y4.X - 30, y4.Y - 7);
                        jugador4.Image = (Image)motorotada;
                    }
                }
                direccionJugador4 = dir;
            }
        }


        /// Variables para las direcciones
        static int izquierda = 0;
        static int derecha = 1;
        static int arriba = 2;
        static int abajo = 3;


        /// Variables Jugador1
        Point y1 = new Point();
        //static int puntosJugador = 0;
        static int direccionJugador1 = derecha;
        static int columnaJugador1 = 40; 
        static int filaJugador1 = 70;  

        
        //Metodos
        public void CambiarDireccionJugador1(KeyPressEventArgs e)
        {
          
            if (e.KeyChar == 119 && direccionJugador1 != abajo)
            {
                if (direccionJugador1 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(15,30);
                    jugador1.Location = new Point(y1.X - 7, y1.Y);
                    jugador1.Image = (Image)motorotada;
                }
                if (direccionJugador1 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(15, 30);
                    jugador1.Location = new Point(y1.X - 7, y1.Y);
                    jugador1.Image = (Image)motorotada;
                }
                direccionJugador1 = arriba;
                string mensaje = "502/" + sala + "/" + jugador + "/" + arriba;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 97 && direccionJugador1 != derecha)
            {
                if (direccionJugador1 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(30, 15);
                    jugador1.Location = new Point(y1.X, y1.Y - 7);
                    jugador1.Image = (Image)motorotada;
                }
                if (direccionJugador1 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(30, 15);
                    jugador1.Location = new Point(y1.X, y1.Y - 7);
                    jugador1.Image = (Image)motorotada;
                }
                direccionJugador1 = izquierda;
                string mensaje = "502/" + sala + "/" + jugador + "/" + izquierda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 100 && direccionJugador1 != izquierda)
            {
                if (direccionJugador1 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(30, 15);
                    jugador1.Location = new Point(y1.X - 30, y1.Y - 7);
                    jugador1.Image = (Image)motorotada;
                }
                if (direccionJugador1 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(30, 15);
                    jugador1.Location = new Point(y1.X - 30, y1.Y - 7);
                    jugador1.Image = (Image)motorotada;
                }
                direccionJugador1 = derecha;
                string mensaje = "502/" + sala + "/" + jugador + "/" + derecha;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar ==115 && direccionJugador1 != arriba)
            {
                if (direccionJugador1 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(15, 30);
                    jugador1.Location = new Point(y1.X - 7, y1.Y - 30);
                    jugador1.Image = (Image)motorotada;
                }
                if (direccionJugador1 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoazul.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador1.Image.Width / 2, jugador1.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador1.Image.Width / 2, -jugador1.Image.Height / 2);
                        g.DrawImage(jugador1.Image, new Point(0, 5));
                    }

                    jugador1.ClientSize = new Size(15, 30);
                    jugador1.Location = new Point(y1.X - 7, y1.Y - 30);
                    jugador1.Image = (Image)motorotada;
                }
                direccionJugador1 = abajo;
                string mensaje = "502/" + sala + "/" + jugador + "/" + abajo;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
           
        }

        /// Variables Jugador2
        Point y2 = new Point();
        //static int puntosJugador = 0;
        static int direccionJugador2 = izquierda;
        static int columnaJugador2 = 1330;
        static int filaJugador2 = 70;


        //Metodos
        public void CambiarDireccionJugador2(KeyPressEventArgs e)
        {
            if (e.KeyChar == 119 && direccionJugador2 != abajo)
            {
                if (direccionJugador2 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(15, 30);
                    jugador2.Location = new Point(y2.X - 7, y2.Y);
                    jugador2.Image = (Image)motorotada;
                }
                if (direccionJugador2 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(15, 30);
                    jugador2.Location = new Point(y2.X - 7, y2.Y);
                    jugador2.Image = (Image)motorotada;
                }
                direccionJugador2 = arriba;
                string mensaje = "502/" + sala + "/" + jugador + "/" + arriba;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 97 && direccionJugador2 != derecha)
            {
                if (direccionJugador2 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(30, 15);
                    jugador2.Location = new Point(y2.X, y2.Y - 7);
                    jugador2.Image = (Image)motorotada;
                }
                if (direccionJugador2 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(30, 15);
                    jugador2.Location = new Point(y2.X, y2.Y - 7);
                    jugador2.Image = (Image)motorotada;
                }
                direccionJugador2 = izquierda;
                string mensaje = "502/" + sala + "/" + jugador + "/" + izquierda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 100 && direccionJugador2 != izquierda)
            {
                if (direccionJugador2 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(30, 15);
                    jugador2.Location = new Point(y2.X - 30, y2.Y - 7);
                    jugador2.Image = (Image)motorotada;
                }
                if (direccionJugador2 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(30, 15);
                    jugador2.Location = new Point(y2.X - 30, y2.Y - 7);
                    jugador2.Image = (Image)motorotada;
                }
                direccionJugador2 = derecha;
                string mensaje = "502/" + sala + "/" + jugador + "/" + derecha;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 115 && direccionJugador2 != arriba)
            {
                if (direccionJugador2 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(15, 30);
                    jugador2.Location = new Point(y2.X - 7, y2.Y - 30);
                    jugador2.Image = (Image)motorotada;
                }
                if (direccionJugador2 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoroja.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador2.Image.Width / 2, jugador2.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador2.Image.Width / 2, -jugador2.Image.Height / 2);
                        g.DrawImage(jugador2.Image, new Point(0, 5));
                    }

                    jugador2.ClientSize = new Size(15, 30);
                    jugador2.Location = new Point(y2.X - 7, y2.Y - 30);
                    jugador2.Image = (Image)motorotada;
                }
                direccionJugador2 = abajo;
                string mensaje = "502/" + sala + "/" + jugador + "/" + abajo;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        /// Variables Jugador3
        Point y3 = new Point();
        //static int puntosJugador = 0;
        static int direccionJugador3 = derecha;
        static int columnaJugador3 = 40;
        static int filaJugador3 = 730;

        public void CrearOcupados()
        {
            CheckForIllegalCrossThreadCalls = false;
            ocupados1 = new PictureBox[160000];
            ocupados2 = new PictureBox[160000];
            ocupados3 = new PictureBox[160000];
            ocupados4 = new PictureBox[160000];
        }


        //Metodos
        public void CambiarDireccionJugador3(KeyPressEventArgs e)
        {

            if (e.KeyChar == 119 && direccionJugador3 != abajo)
            {
                if (direccionJugador3 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(15, 30);
                    jugador3.Location = new Point(y3.X - 7, y3.Y);
                    jugador3.Image = (Image)motorotada;
                }
                if (direccionJugador3 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(15, 30);
                    jugador3.Location = new Point(y3.X - 7, y3.Y);
                    jugador3.Image = (Image)motorotada;
                }
                direccionJugador3 = arriba;
                string mensaje = "502/" + sala + "/" + jugador + "/" + arriba;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 97 && direccionJugador3 != derecha)
            {
                if (direccionJugador3 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(30, 15);
                    jugador3.Location = new Point(y3.X, y3.Y - 7);
                    jugador3.Image = (Image)motorotada;
                }
                if (direccionJugador3 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(30, 15);
                    jugador3.Location = new Point(y3.X, y3.Y - 7);
                    jugador3.Image = (Image)motorotada;
                }
                direccionJugador3 = izquierda;
                string mensaje = "502/" + sala + "/" + jugador + "/" + izquierda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 100 && direccionJugador3 != izquierda)
            {
                if (direccionJugador3 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(30, 15);
                    jugador3.Location = new Point(y3.X - 30, y3.Y - 7);
                    jugador3.Image = (Image)motorotada;
                }
                if (direccionJugador3 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(30, 15);
                    jugador3.Location = new Point(y3.X - 30, y3.Y - 7);
                    jugador3.Image = (Image)motorotada;
                }
                direccionJugador3 = derecha;
                string mensaje = "502/" + sala + "/" + jugador + "/" + derecha;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 115 && direccionJugador3 != arriba)
            {
                if (direccionJugador3== izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(15, 30);
                    jugador3.Location = new Point(y3.X - 7, y3.Y - 30);
                    jugador3.Image = (Image)motorotada;
                }
                if (direccionJugador3 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoamarilla.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador3.Image.Width / 2, jugador3.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador3.Image.Width / 2, -jugador3.Image.Height / 2);
                        g.DrawImage(jugador3.Image, new Point(0, 5));
                    }

                    jugador3.ClientSize = new Size(15, 30);
                    jugador3.Location = new Point(y3.X - 7, y3.Y - 30);
                    jugador3.Image = (Image)motorotada;
                }
                direccionJugador3 = abajo;
                string mensaje = "502/" + sala + "/" + jugador + "/" + abajo;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

        }

        /// Variables Jugador4
        Point y4 = new Point();
        //static int puntosJugador = 0;
        static int direccionJugador4 = izquierda;
        static int columnaJugador4 = 1330;
        static int filaJugador4 = 730;


        //Metodos
        public void CambiarDireccionJugador4(KeyPressEventArgs e)
        {

            if (e.KeyChar == 119 && direccionJugador4 != abajo)
            {
                if (direccionJugador4 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(15, 30);
                    jugador4.Location = new Point(y4.X - 7, y4.Y);
                    jugador4.Image = (Image)motorotada;
                }
                if (direccionJugador4 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(15, 30);
                    jugador4.Location = new Point(y4.X - 7, y4.Y);
                    jugador4.Image = (Image)motorotada;
                }
                direccionJugador4 = arriba;
                string mensaje = "502/" + sala + "/" + jugador + "/" + arriba;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 97 && direccionJugador4 != derecha)
            {
                if (direccionJugador4 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(30, 15);
                    jugador4.Location = new Point(y4.X, y4.Y - 7);
                    jugador4.Image = (Image)motorotada;
                }
                if (direccionJugador4 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(30, 15);
                    jugador4.Location = new Point(y4.X, y4.Y - 7);
                    jugador4.Image = (Image)motorotada;
                }
                direccionJugador4 = izquierda;
                string mensaje = "502/" + sala + "/" + jugador + "/" + izquierda;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 100 && direccionJugador4 != izquierda)
            {
                if (direccionJugador4 == abajo)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(30, 15);
                    jugador4.Location = new Point(y4.X - 30, y4.Y - 7);
                    jugador4.Image = (Image)motorotada;
                }
                if (direccionJugador4 == arriba)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(30, 15);
                    jugador4.Location = new Point(y4.X - 30, y4.Y - 7);
                    jugador4.Image = (Image)motorotada;
                }
                direccionJugador4 = derecha;
                string mensaje = "502/" + sala + "/" + jugador + "/" + derecha;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (e.KeyChar == 115 && direccionJugador4 != arriba)
            {
                if (direccionJugador4 == izquierda)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(-90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(15, 30);
                    jugador4.Location = new Point(y4.X - 7, y4.Y - 30);
                    jugador4.Image = (Image)motorotada;
                }
                if (direccionJugador4 == derecha)
                {
                    Bitmap motorotada = new Bitmap("motoverde.png");
                    using (Graphics g = Graphics.FromImage(motorotada))
                    {
                        g.TranslateTransform(jugador4.Image.Width / 2, jugador4.Image.Height / 2);
                        g.RotateTransform((float)(90));
                        g.TranslateTransform(-jugador4.Image.Width / 2, -jugador4.Image.Height / 2);
                        g.DrawImage(jugador4.Image, new Point(0, 5));
                    }

                    jugador4.ClientSize = new Size(15, 30);
                    jugador4.Location = new Point(y4.X - 7, y4.Y - 30);
                    jugador4.Image = (Image)motorotada;
                }
                direccionJugador4 = abajo;
                string mensaje = "502/" + sala + "/" + jugador + "/" + abajo;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

        }



        public Juego()
        {
            InitializeComponent();
        }

        private void Juego_Load(object sender, EventArgs e)
        {
            i = 0;

            CrearOcupados();
            
            
            jugador1 = new PictureBox();
            jugador1.ClientSize = new Size(30, 14);
            jugador1.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap image = new Bitmap("motoazul.png");
            jugador1.Image = (Image)image;
            y1.X = columnaJugador1;
            y1.Y = filaJugador1;
            Point posicion = new Point(y1.X - 30, y1.Y - 7);
            jugador1.Location = posicion;
            panel1.Controls.Add(jugador1);

            direccionJugador1 = derecha;
            direccionJugador2 = izquierda;
            direccionJugador3 = derecha;
            direccionJugador4 = izquierda;

            if (njugadores > 1)
            {

                jugador2 = new PictureBox();
                jugador2.ClientSize = new Size(30, 15);
                jugador2.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image2 = new Bitmap("motoroja.png");
                jugador2.Image = (Image)image2;
                y2.X = columnaJugador2;
                y2.Y = filaJugador2;
                jugador2.Location = y2;
                panel1.Controls.Add(jugador2);

            }
            if (njugadores > 2)
            {
                jugador3 = new PictureBox();
                jugador3.ClientSize = new Size(30, 15);
                jugador3.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image3 = new Bitmap("motoamarilla.png");
                jugador3.Image = (Image)image3;
                y3.X = columnaJugador3;
                y3.Y = filaJugador3;
                jugador3.Location = y3;
                panel1.Controls.Add(jugador3);

            }
            if (njugadores > 3)
            {
                jugador4 = new PictureBox();
                jugador4.ClientSize = new Size(30, 15);
                jugador4.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image4 = new Bitmap("motoverde.png");
                jugador4.Image = (Image)image4;
                y4.X = columnaJugador4;
                y4.Y = filaJugador4;
                jugador4.Location = y4;
                panel1.Controls.Add(jugador4);

            }
            timer1.Interval = 50;
            timer1.Start();

        }

        private void Juego_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (jugador==1)
                CambiarDireccionJugador1(e);
            else if (jugador == 2)
                CambiarDireccionJugador2(e);
            else if (jugador == 3)
                CambiarDireccionJugador3(e);
            else if (jugador == 4)
                CambiarDireccionJugador4(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int cseg = (this.i % 20)*5;
            int segundos = this.i/20;
            int minutos = segundos / 60;
            int segundosrestantes = segundos % 60;
            Reloj.Text = "Tiempo de partida : 0:" + Convert.ToString(minutos) + ":" + Convert.ToString(segundosrestantes) + ":" + Convert.ToString(cseg);
            
            if(jugadoresrestantes==1)
            {
                string hora =  DateTime.Now.ToString("HH:mm:ss");
                string dia = DateTime.Today.ToString("dd/MM/yyyy");
                string mensaje = "503/" + sala + "/" + jugador + "/" +dia+"_" + hora ;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                timer1.Stop();
                PartidaGanada partiganada = new PartidaGanada();
                partiganada.ShowDialog();
                this.Close();
            }
            if (jugador1vivo == 1)
            {
                PictureBox puntoocupado1 = new PictureBox();
                puntoocupado1.ClientSize = new Size(5, 5);
                puntoocupado1.Location = y1;
                //puntoocupado.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image = new Bitmap("azul.png");
                puntoocupado1.Image = (Image)image;

                panel1.Controls.Add(puntoocupado1);

                ocupados1[i] = puntoocupado1;
            }
            
            if (njugadores >1 && jugador2vivo == 1)
            {

                PictureBox puntoocupado2 = new PictureBox();
                puntoocupado2.ClientSize = new Size(5, 5);
                puntoocupado2.Location = y2;
                //puntoocupado.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image2 = new Bitmap("rojo.png");
                puntoocupado2.Image = (Image)image2;

                panel1.Controls.Add(puntoocupado2);

                ocupados2[i] = puntoocupado2;
            }

            if (njugadores > 2 && jugador3vivo == 1)
            {

                PictureBox puntoocupado3 = new PictureBox();
                puntoocupado3.ClientSize = new Size(5, 5);
                puntoocupado3.Location = y3;
                //puntoocupado.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image3 = new Bitmap("amarillo.png");
                puntoocupado3.Image = (Image)image3;

                panel1.Controls.Add(puntoocupado3);

                ocupados3[i] = puntoocupado3;
            }

            if (njugadores > 3 && jugador4vivo == 1)
            {

                PictureBox puntoocupado4 = new PictureBox();
                puntoocupado4.ClientSize = new Size(5, 5);
                puntoocupado4.Location = y4;
                //puntoocupado.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap image4 = new Bitmap("verde.png");
                puntoocupado4.Image = (Image)image4;

                panel1.Controls.Add(puntoocupado4);

                ocupados4[i] = puntoocupado4;
            }



            if (jugador1vivo ==1)
            {
                if (direccionJugador1 == arriba)
                {
                    y1.Y = y1.Y - 5;
                }

                if (direccionJugador1 == abajo)
                {
                    y1.Y = y1.Y + 5;
                }

                if (direccionJugador1 == derecha)
                {
                    y1.X = y1.X + 5;
                }

                if (direccionJugador1 == izquierda)
                {
                    y1.X = y1.X - 5;
                }

                int lop1 = 0;
                if (jugador == 1)
                {

                    foreach (PictureBox punt in ocupados1)
                    {
                        if (punt != null)
                        {
                            if (punt.Location == y1)
                            {
                                lop1 = 1;
                                timer1.Stop();
                                jugador1vivo = 0;
                                panel1.Controls.Remove(jugador1);
                                jugador1 = null;
                                for (int i = 0; i < ocupados1.Length; i++)
                                {
                                    panel1.Controls.Remove(ocupados1[i]);
                                    ocupados1[i] = null;
                                }
                                string mensaje = "501/" + sala + "/" + jugador;
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                server.Send(msg);
                                PartidaPerdida partiperdida = new PartidaPerdida();
                                partiperdida.ShowDialog();
                                this.Close();
                            }
                        }

                    }

                    if (njugadores > 1)
                    {
                        foreach (PictureBox punt in ocupados2)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y1)
                                {
                                    lop1 = 1;
                                    timer1.Stop();
                                    jugador1vivo = 0;
                                    panel1.Controls.Remove(jugador1);
                                    jugador1 = null;
                                    for (int i = 0; i < ocupados1.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados1[i]);
                                        ocupados1[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }
                            }
                        }
                    }

                    if (njugadores > 2)
                    {
                        foreach (PictureBox punt in ocupados3)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y1)
                                {
                                    lop1 = 1;
                                    timer1.Stop();
                                    jugador1vivo = 0;
                                    panel1.Controls.Remove(jugador1);
                                    jugador1 = null;
                                    for (int i = 0; i < ocupados1.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados1[i]);
                                        ocupados1[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }
                            }
                        }
                    }
                    if (njugadores > 3)
                    {
                        foreach (PictureBox punt in ocupados4)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y1)
                                {
                                    lop1 = 1;
                                    timer1.Stop();
                                    jugador1vivo = 0;
                                    panel1.Controls.Remove(jugador1);
                                    jugador1 = null;
                                    for (int i = 0; i < ocupados1.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados1[i]);
                                        ocupados1[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }
                            }
                        }
                    }

                    if (y1.X >= 1347 & jugador == 1)
                    {
                        lop1 = 1;
                        string mensaje = "501/" + sala + "/" + jugador;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        timer1.Stop();
                        jugador1vivo = 0;
                        panel1.Controls.Remove(jugador1);
                        jugador1 = null;
                        for (int i = 0; i < ocupados1.Length; i++)
                        {
                            panel1.Controls.Remove(ocupados1[i]);
                            ocupados1[i] = null;
                        }
                        PartidaPerdida partiperdida = new PartidaPerdida();
                        partiperdida.ShowDialog();
                        this.Close();
                    }
                    else if (y1.Y >= 749 & jugador == 1)
                    {
                        lop1 = 1;
                        string mensaje = "501/" + sala + "/" + jugador;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        timer1.Stop();
                        jugador1vivo = 0;
                        panel1.Controls.Remove(jugador1);
                        jugador1 = null;
                        for (int i = 0; i < ocupados1.Length; i++)
                        {
                            panel1.Controls.Remove(ocupados1[i]);
                            ocupados1[i] = null;
                        }
                        PartidaPerdida partiperdida = new PartidaPerdida();
                        partiperdida.ShowDialog();
                        this.Close();
                    }
                    else if (y1.X <= 0 & jugador == 1)
                    {
                        lop1 = 1;
                        string mensaje = "501/" + sala + "/" + jugador;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        timer1.Stop();
                        jugador1vivo = 0;
                        panel1.Controls.Remove(jugador1);
                        jugador1 = null;
                        for (int i = 0; i < ocupados1.Length; i++)
                        {
                            panel1.Controls.Remove(ocupados1[i]);
                            ocupados1[i] = null;
                        }
                        PartidaPerdida partiperdida = new PartidaPerdida();
                        partiperdida.ShowDialog();
                        this.Close();
                    }
                    else if (y1.Y <= 0 & jugador == 1)
                    {
                        lop1 = 1;
                        string mensaje = "501/" + sala + "/" + jugador;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        timer1.Stop();
                        jugador1vivo = 0;
                        panel1.Controls.Remove(jugador1);
                        jugador1 = null;
                        for (int i = 0; i < ocupados1.Length; i++)
                        {
                            panel1.Controls.Remove(ocupados1[i]);
                            ocupados1[i] = null;
                        }
                        PartidaPerdida partiperdida = new PartidaPerdida();
                        partiperdida.ShowDialog();
                        this.Close();
                    }
                }
                if (lop1 == 0)
                {
                    Point posicion=new Point();
                    if (direccionJugador1 == derecha)
                        posicion = new Point(y1.X - 30, y1.Y - 7);
                    else if (direccionJugador1 == izquierda)
                        posicion = new Point(y1.X, y1.Y - 7);
                    else if (direccionJugador1 == arriba)
                        posicion = new Point(y1.X-7, y1.Y);
                    else if (direccionJugador1 == abajo)
                        posicion = new Point(y1.X-7, y1.Y - 30);
                    jugador1.Location = posicion;
                    
                    panel1.Invalidate();
                }
            }
            

            if (njugadores>1)
            {
                if (jugador2vivo == 1)
                {
                    if (direccionJugador2 == arriba)
                    {
                        y2.Y = y2.Y - 5;
                    }

                    if (direccionJugador2 == abajo)
                    {
                        y2.Y = y2.Y + 5;
                    }

                    if (direccionJugador2 == derecha)
                    {
                        y2.X = y2.X + 5;
                    }

                    if (direccionJugador2 == izquierda)
                    {
                        y2.X = y2.X - 5;
                    }

                    int lop2 = 0;
                    if (jugador == 2)
                    {

                        foreach (PictureBox punt in ocupados1)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y2)
                                {
                                    lop2 = 1;
                                    timer1.Stop();
                                    jugador2vivo = 0;
                                    panel1.Controls.Remove(jugador2);
                                    jugador2 = null;
                                    for (int i = 0; i < ocupados2.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados2[i]);
                                        ocupados2[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }
                            }

                        }

                        if (njugadores > 1)
                        {
                            foreach (PictureBox punt in ocupados2)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y2)
                                    {
                                        lop2 = 1;
                                        timer1.Stop();
                                        jugador2vivo = 0;
                                        panel1.Controls.Remove(jugador2);
                                        jugador2 = null;
                                        for (int i = 0; i < ocupados2.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados2[i]);
                                            ocupados2[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (njugadores > 2)
                        {
                            foreach (PictureBox punt in ocupados3)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y2)
                                    {
                                        lop2 = 1;
                                        timer1.Stop();
                                        jugador2vivo = 0;
                                        panel1.Controls.Remove(jugador2);
                                        jugador2 = null;
                                        for (int i = 0; i < ocupados2.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados2[i]);
                                            ocupados2[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }
                        if (njugadores > 3)
                        {
                            foreach (PictureBox punt in ocupados4)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y2)
                                    {
                                        lop2 = 1;
                                        timer1.Stop();
                                        jugador2vivo = 0;
                                        panel1.Controls.Remove(jugador2);
                                        jugador2 = null;
                                        for (int i = 0; i < ocupados2.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados2[i]);
                                            ocupados2[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (y2.X >= 1347 & jugador == 2)
                        {
                            lop2 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador2vivo = 0;
                            panel1.Controls.Remove(jugador2);
                            jugador2 = null;
                            for (int i = 0; i < ocupados2.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados2[i]);
                                ocupados2[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y2.Y >= 749 & jugador == 2)
                        {
                            lop2 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador2vivo = 0;
                            panel1.Controls.Remove(jugador2);
                            jugador2 = null;
                            for (int i = 0; i < ocupados2.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados2[i]);
                                ocupados2[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y2.X <= 0 & jugador == 2)
                        {
                            lop2 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador2vivo = 0;
                            panel1.Controls.Remove(jugador2);
                            jugador2 = null;
                            for (int i = 0; i < ocupados2.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados2[i]);
                                ocupados2[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y2.Y <= 0 & jugador == 2)
                        {
                            lop2 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador2vivo = 0;
                            panel1.Controls.Remove(jugador2);
                            jugador2 = null;
                            for (int i = 0; i < ocupados2.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados2[i]);
                                ocupados2[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                    }
                    if (lop2 == 0)
                    {
                        Point posicion = new Point();
                        if (direccionJugador2 == derecha)
                            posicion = new Point(y2.X - 30, y2.Y - 7);
                        else if (direccionJugador2 == izquierda)
                            posicion = new Point(y2.X, y2.Y - 7);
                        else if (direccionJugador2 == arriba)
                            posicion = new Point(y2.X - 7, y2.Y);
                        else if (direccionJugador2 == abajo)
                            posicion = new Point(y2.X - 7, y2.Y - 30);
                        jugador2.Location = posicion;
                        panel1.Invalidate();
                    }
                }
            }

            if (njugadores > 2)
            {
                if (jugador3vivo == 1)
                {
                    if (direccionJugador3 == arriba)
                    {
                        y3.Y = y3.Y - 5;
                    }

                    if (direccionJugador3 == abajo)
                    {
                        y3.Y = y3.Y + 5;
                    }

                    if (direccionJugador3 == derecha)
                    {
                        y3.X = y3.X + 5;
                    }

                    if (direccionJugador3 == izquierda)
                    {
                        y3.X = y3.X - 5;
                    }

                    int lop3 = 0;
                    if (jugador == 3)
                    {

                        foreach (PictureBox punt in ocupados1)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y3)
                                {
                                    lop3 = 1;
                                    timer1.Stop();
                                    jugador3vivo = 0;
                                    panel1.Controls.Remove(jugador3);
                                    jugador3 = null;
                                    for (int i = 0; i < ocupados3.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados3[i]);
                                        ocupados3[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }
                            }

                        }

                        if (njugadores > 1)
                        {
                            foreach (PictureBox punt in ocupados2)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y3)
                                    {
                                        lop3 = 1;
                                        timer1.Stop();
                                        jugador3vivo = 0;
                                        panel1.Controls.Remove(jugador3);
                                        jugador3 = null;
                                        for (int i = 0; i < ocupados3.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados3[i]);
                                            ocupados3[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (njugadores > 2)
                        {
                            foreach (PictureBox punt in ocupados3)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y3)
                                    {
                                        lop3 = 1;
                                        timer1.Stop();
                                        jugador3vivo = 0;
                                        panel1.Controls.Remove(jugador3);
                                        jugador3 = null;
                                        for (int i = 0; i < ocupados3.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados3[i]);
                                            ocupados3[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }
                        if (njugadores > 3)
                        {
                            foreach (PictureBox punt in ocupados4)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y3)
                                    {
                                        lop3 = 1;
                                        timer1.Stop();
                                        jugador3vivo = 0;
                                        panel1.Controls.Remove(jugador3);
                                        jugador3 = null;
                                        for (int i = 0; i < ocupados3.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados3[i]);
                                            ocupados3[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (y3.X >= 1347 & jugador == 3)
                        {
                            lop3 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador3vivo = 0;
                            panel1.Controls.Remove(jugador3);
                            jugador3 = null;
                            for (int i = 0; i < ocupados3.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados3[i]);
                                ocupados3[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y3.Y >= 749 & jugador == 3)
                        {
                            lop3 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador3vivo = 0;
                            panel1.Controls.Remove(jugador3);
                            jugador3 = null;
                            for (int i = 0; i < ocupados3.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados3[i]);
                                ocupados3[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y3.X <= 0 & jugador == 3)
                        {
                            lop3 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador3vivo = 0;
                            panel1.Controls.Remove(jugador3);
                            jugador3 = null;
                            for (int i = 0; i < ocupados3.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados3[i]);
                                ocupados3[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y3.Y <= 0 & jugador == 3)
                        {
                            lop3 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador3vivo = 0;
                            panel1.Controls.Remove(jugador3);
                            jugador3 = null;
                            for (int i = 0; i < ocupados3.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados3[i]);
                                ocupados3[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                    }
                    if (lop3 == 0)
                    {
                        Point posicion = new Point();
                        if (direccionJugador3 == derecha)
                            posicion = new Point(y3.X - 30, y3.Y - 7);
                        else if (direccionJugador3 == izquierda)
                            posicion = new Point(y3.X, y3.Y - 7);
                        else if (direccionJugador3 == arriba)
                            posicion = new Point(y3.X - 7, y3.Y);
                        else if (direccionJugador3 == abajo)
                            posicion = new Point(y3.X - 7, y3.Y - 30);
                        jugador3.Location = posicion;
                        panel1.Invalidate();
                    }
                }
            }

            if (njugadores > 3)
            {
                if (jugador4vivo == 1)
                {
                    if (direccionJugador4 == arriba)
                    {
                        y4.Y = y4.Y - 5;
                    }

                    if (direccionJugador4 == abajo)
                    {
                        y4.Y = y4.Y + 5;
                    }

                    if (direccionJugador4 == derecha)
                    {
                        y4.X = y4.X + 5;
                    }

                    if (direccionJugador4 == izquierda)
                    {
                        y4.X = y4.X - 5;
                    }

                    int lop4 = 0;
                    if (jugador == 4)
                    {

                        foreach (PictureBox punt in ocupados1)
                        {
                            if (punt != null)
                            {
                                if (punt.Location == y4)
                                {
                                    lop4 = 1;
                                    timer1.Stop();
                                    jugador4vivo = 0;
                                    panel1.Controls.Remove(jugador4);
                                    jugador4 = null;
                                    for (int i = 0; i < ocupados4.Length; i++)
                                    {
                                        panel1.Controls.Remove(ocupados4[i]);
                                        ocupados4[i] = null;
                                    }
                                    string mensaje = "501/" + sala + "/" + jugador;
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                    server.Send(msg);
                                    PartidaPerdida partiperdida = new PartidaPerdida();
                                    partiperdida.ShowDialog();
                                    this.Close();
                                }

                            }

                        }

                        if (njugadores > 1)
                        {
                            foreach (PictureBox punt in ocupados2)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y4)
                                    {
                                        lop4 = 1;
                                        timer1.Stop();
                                        jugador4vivo = 0;
                                        panel1.Controls.Remove(jugador4);
                                        jugador4 = null;
                                        for (int i = 0; i < ocupados4.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados4[i]);
                                            ocupados4[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (njugadores > 2)
                        {
                            foreach (PictureBox punt in ocupados3)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y4)
                                    {
                                        lop4 = 1;
                                        timer1.Stop();
                                        jugador4vivo = 0;
                                        panel1.Controls.Remove(jugador4);
                                        jugador4 = null;
                                        for (int i = 0; i < ocupados4.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados4[i]);
                                            ocupados4[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }
                        if (njugadores > 3)
                        {
                            foreach (PictureBox punt in ocupados4)
                            {
                                if (punt != null)
                                {
                                    if (punt.Location == y4)
                                    {
                                        lop4 = 1;
                                        timer1.Stop();
                                        jugador4vivo = 0;
                                        panel1.Controls.Remove(jugador4);
                                        jugador4 = null;
                                        for (int i = 0; i < ocupados4.Length; i++)
                                        {
                                            panel1.Controls.Remove(ocupados4[i]);
                                            ocupados4[i] = null;
                                        }
                                        string mensaje = "501/" + sala + "/" + jugador;
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        PartidaPerdida partiperdida = new PartidaPerdida();
                                        partiperdida.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }

                        if (y4.X >= 1347 & jugador == 4)
                        {
                            lop4 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador4vivo = 0;
                            panel1.Controls.Remove(jugador4);
                            jugador4 = null;
                            for (int i = 0; i < ocupados4.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados4[i]);
                                ocupados4[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y4.Y >= 749 & jugador == 4)
                        {
                            lop4 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador4vivo = 0;
                            panel1.Controls.Remove(jugador4);
                            jugador4 = null;
                            for (int i = 0; i < ocupados4.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados4[i]);
                                ocupados4[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y4.X <= 0 & jugador == 4)
                        {
                            lop4 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador4vivo = 0;
                            panel1.Controls.Remove(jugador4);
                            jugador4 = null;
                            for (int i = 0; i < ocupados4.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados4[i]);
                                ocupados4[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                        else if (y4.Y <= 0 & jugador == 4)
                        {
                            lop4 = 1;
                            string mensaje = "501/" + sala + "/" + jugador;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                            timer1.Stop();
                            jugador4vivo = 0;
                            panel1.Controls.Remove(jugador4);
                            jugador4 = null;
                            for (int i = 0; i < ocupados4.Length; i++)
                            {
                                panel1.Controls.Remove(ocupados4[i]);
                                ocupados4[i] = null;
                            }
                            PartidaPerdida partiperdida = new PartidaPerdida();
                            partiperdida.ShowDialog();
                            this.Close();
                        }
                    }
                    if (lop4 == 0)
                    {
                        Point posicion = new Point();
                        if (direccionJugador4 == derecha)
                            posicion = new Point(y4.X - 30, y4.Y - 7);
                        else if (direccionJugador4 == izquierda)
                            posicion = new Point(y4.X, y4.Y - 7);
                        else if (direccionJugador4 == arriba)
                            posicion = new Point(y4.X - 7, y4.Y);
                        else if (direccionJugador4 == abajo)
                            posicion = new Point(y4.X - 7, y4.Y - 30);
                        jugador4.Location = posicion;
                        panel1.Invalidate();
                    }
                }
            }
            this.i++;
        }
    }
}
