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
    public partial class Ganadores10min : Form
    {
        //CONSULTA 2: Ganadores de partidas de con una duracion superior a 10 min.

        public Ganadores10min()
        {
            InitializeComponent();
        }

        //Variable a la que se le asigna la lista de usuarios que son ganadores de partidas de con una duracion superior a 10 min.
        string Lista;

        //Set para asignar la lista de usuarios que son ganadores de partidas de con una duracion superior a 10 min.
        public void setLista(string Lista2)
        {
            this.Lista = Lista2;
        }

        private void Ganadores10min_Load(object sender, EventArgs e)
        {
            //Creacion de la tabla donde se introduciran los nombres que son solucion a la consulta.
            string[] vector = new string[5];
            vector= Lista.Split(' ');
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 5;

            //Se introducen los nombres que son solucion a la consulta.
            int i = 0;
            while (i < vector.Length)
            {
                dataGridView1.Rows[0].Cells[i].Value = vector[i];
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
