using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using INFO2017;
using System.Data.SqlClient;

namespace INFO2017
{
    public partial class Form2 : Form
    {
        SqlConnection conexiune = null;

        public Form2()
        {
            InitializeComponent();
            dataGridView1.Hide();

            conexiune = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Poriecte Visual\INFO2017\INFO2017\Database1.mdf;Integrated Security=True");

        }

        private void creereContUtilizatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label5.Show();
            label4.Show();
            label3.Show();
            label2.Show();
            label1.Show();
            textBox5.Show();
            textBox4.Show();
            textBox3.Show();
            textBox2.Show();
            textBox1.Show();
            button1.Show();
            dataGridView1.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexiune.Open();
            string inserare;
            inserare = "insert into Angajatori (Nume_complet, ID, Parola, Telefon, Adresa, Nivelul) values (@nume, @id, @parola, @telefon, @adresa, @nivel)";
            SqlCommand cmd = new SqlCommand(inserare, conexiune);

            string encID = Crypto.Encrypt(textBox2.Text);
            string encPASS = Crypto.Encrypt(textBox3.Text);
            string encTEL = Crypto.Encrypt(textBox4.Text);
            string encADR = Crypto.Encrypt(textBox5.Text);


            cmd.Parameters.AddWithValue("nume", textBox1.Text);
            cmd.Parameters.AddWithValue("id", encID);
            cmd.Parameters.AddWithValue("parola", encPASS);
            cmd.Parameters.AddWithValue("telefon", encTEL);
            cmd.Parameters.AddWithValue("adresa", encADR);
            cmd.Parameters.AddWithValue("nivel", comboBox1.Text.ToString());

            SqlDataReader r = cmd.ExecuteReader();

            if(r!=null)
            {
                MessageBox.Show("Datele au fost introduse cu succes!");
            }

            textBox5.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox1.Text = "";
            conexiune.Close();

        }

        private void vizualizareAngajatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            button1.Hide();
            dataGridView1.Show();
            comboBox1.Hide();
            label6.Hide();

            conexiune.Open();

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Nume complet";
            dataGridView1.Columns[1].Name = "ID";
            dataGridView1.Columns[2].Name = "Parola";
            dataGridView1.Columns[3].Name = "Telefon";
            dataGridView1.Columns[4].Name = "Adresa";
            dataGridView1.Columns[5].Name = "Nivel angajat";

            string select = "select * from Angajatori";
            SqlCommand cmd = new SqlCommand(select, conexiune);
            SqlDataReader r = cmd.ExecuteReader();
            while(r.Read())
            {
                this.dataGridView1.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5]);
            }

            conexiune.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form a = new Form1();
            a.Show();
        }
    }
}
