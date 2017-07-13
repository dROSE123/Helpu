using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace INFO2017
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("horatiu.kuhaku@gmail.com".ToString());
            mail.To.Add("horatiuyo@gmail.com".ToString());
            mail.Subject = "Cerere angajat".ToString();
            mail.Body = textBox1.Text;

            //SmtpClient sc = new SmtpClient("smtp.gmail.com");
            //sc.Port = 25;
            //sc.Credentials = new NetworkCredential("horatiuyo@gamil.com".ToString(), "dwcdtryv7727".ToString());
            //sc.EnableSsl = true;
            //sc.Send(mail);

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("horatiu.kuhaku@gmail.com", "dwcdtryv7727");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            MessageBox.Show("Mesajul a fost trimis!");
            textBox1.Text = "";

        }
    }
}
