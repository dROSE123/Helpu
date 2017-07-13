using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using INFO2017;

namespace INFO2017
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

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

            //string strXml;
            //XmlDocument doc = new XmlDocument();
            //try
            //{
            //    using (StreamReader sr = new StreamReader("XMLFile1.xml"))
            //    {
            //        strXml = sr.ReadToEnd();
            //    }

            //    doc.LoadXml(strXml);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //public static bool IsValidLogin(string user, string password)
            //{

            //    XDocument doc = XDocument.Load("XMLFile1.xml");

            //    return doc.Descendants("id")
            //                  .Where(Subroot => Subroot.Attribute("ID").Value == user
            //                         && Subroot.Attribute("Parola").Value == password)
            //                  .Any();
            //}

            XmlDocument doc = new XmlDocument();

            string filename = @"D:\Poriecte Visual\INFO2017\INFO2017\bin\Debug\XMLFile1.xml";

            doc.Load(filename);

            var Username = "";
            var Password = "";

            foreach (XmlNode node in doc.SelectNodes("//Angajat"))
            {
                Username = node.SelectSingleNode(".//ID").InnerText;
                Password = node.SelectSingleNode(".//Parola").InnerText;

                if (( Username.Equals(textBox3.Text) && Password.Equals(textBox4.Text) ) || ( textBox3.Text == "admin" && textBox4.Text == "admin" ) )
                {
                    Form a = new Form4();
                    a.Show();
                    this.Hide();
                    break;
                }

                else
                {
                   MessageBox.Show("Id sau parola gresite");
                    break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dir = textBox5.Text;
            var sr = new StreamReader("data\\" + dir + "//data.ls");

            string encuser = sr.ReadLine();
            string encpass = sr.ReadLine();

            string decuser = Crypto.Decrypt(encuser);
            string decpass = Crypto.Decrypt(encpass);

            if(textBox5.Text == decuser && textBox6.Text == decpass)
            {
                MessageBox.Show("Wellcome to the private area!");
                this.Hide();
                Form a = new Form4();
                a.Show();

            }
            else
            {
                MessageBox.Show("Sorry, user or password are wrong :(");
            }
            

        }
    }
}
