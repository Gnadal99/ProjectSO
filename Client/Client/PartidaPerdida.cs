using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class PartidaPerdida : Form
    {
        //FORM PÚRAMENTE ESTÉTICO, LE INFORMA AL USUARIO QUE HA PERDIDO LA PARTIDA.

        public PartidaPerdida()
        {
            InitializeComponent();
        }

        private void PartidaPerdida_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
