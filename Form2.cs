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

namespace INFO2017
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
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
            dataGridView1.Hide();
            button3.Hide();
        }

        private void creereContUtilizatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Hide();
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "XMLFile1.xml";
            XmlDocument doc = new XmlDocument();
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("lmge;lm");
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                XmlComment comment = doc.CreateComment("This is an XML Generated File");
                doc.AppendChild(declaration);
                doc.AppendChild(comment);
            }
            else
            {
                doc.Load(path); MessageBox.Show("Continuati?");
            }
            XmlElement root = doc.DocumentElement;
            XmlElement Subroot = doc.CreateElement("Angajat");
            XmlElement nume = doc.CreateElement("Nume_Prenume");
            XmlElement id = doc.CreateElement("ID");
            XmlElement parola = doc.CreateElement("Parola");
            XmlElement telefon = doc.CreateElement("Nr_de_telefon");
            XmlElement adresa = doc.CreateElement("Adresa");

            nume.InnerText = textBox1.Text;
            id.InnerText = textBox2.Text;
            parola.InnerText = textBox3.Text;
            telefon.InnerText = textBox4.Text;
            adresa.InnerText = textBox5.Text;
            Subroot.AppendChild(nume);
            Subroot.AppendChild(id);
            Subroot.AppendChild(parola);
            Subroot.AppendChild(telefon);
            Subroot.AppendChild(adresa);

            root.AppendChild(Subroot);
            doc.AppendChild(root);
            doc.Save(path);
            MessageBox.Show("Datele au fost introduse cu succes");
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
            button3.Hide();

            try
            {
                XmlReader xmlFile;
                xmlFile = XmlReader.Create("XMLFile1.xml", new XmlReaderSettings());
                DataSet ds = new DataSet();
                ds.ReadXml(xmlFile);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form a = new Form1();
            a.Show();
        }

        private void creereContCryptatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Hide();
            label2.Show();
            label3.Show();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Show();
            textBox3.Show();
            textBox4.Hide();
            textBox5.Hide();
            button1.Hide();
            dataGridView1.Hide();
            button3.Show();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dir = textBox2.Text;
            Directory.CreateDirectory("data\\" + dir);

            var sw = new StreamWriter("data\\" + dir + "//data.ls");
            string encuser = Crypto.Encrypt(textBox2.Text);
            string encpass = Crypto.Encrypt(textBox3.Text);

            sw.WriteLine(encuser);
            sw.WriteLine(encpass);
            sw.Close();

            MessageBox.Show("Usser was successfully created");

            textBox3.Text = "";
            textBox2.Text = "";

        }

        //public static bool IsValidLogin(string user, string password)
        //{

        //    XDocument doc = XDocument.Load("XMLFile1.xml");

        //    return doc.Descendants("Subroot")
        //                  .Where(subroot => subroot.Attribute("ID").Value == user
        //                         && subroot.Attribute("Parola").Value == password)
        //                  .Any();
        //}
    }
}
