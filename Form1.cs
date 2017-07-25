using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using INFO2017;
using System.Data.SqlClient;
using System.Data;

namespace INFO2017
{
    public partial class Form1 : Form
    {

        SqlConnection conexiune = null;

        public Form1()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

            conexiune = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Poriecte Visual\INFO2017\INFO2017\Database1.mdf;Integrated Security=True");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Form y = new Form2();
                y.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nume sau parola incorecte!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form y = new Form3();
            y.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexiune.Open();

            int i = 0;

            string encID = Crypto.Encrypt(textBox3.Text);
            string encPASS = Crypto.Encrypt(textBox4.Text);

            SqlCommand cmd = conexiune.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Angajatori where ID='" + encID + "'and Parola='" + encPASS + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i != 0)
            {
                MessageBox.Show("Bine ati venit!");
                Form a = new Form4();
                a.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("User sau parola incorecte!");
            }

            conexiune.Close();

        }
    }
}
