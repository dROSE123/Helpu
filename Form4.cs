using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace INFO2017
{
    public partial class Form4 : Form
    {
        Choices list = new Choices();
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();


        public Form4()
        {
            InitializeComponent();
            label1.Hide();
            button4.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;

          //  foreach (var v in ss.GetInstalledVoices().Select(v => v.VoiceInfo))
          //  {
          //      MessageBox.Show(v.Description);
          //      MessageBox.Show(v.Gender.ToString());
          //      MessageBox.Show(v.Age.ToString());
          //  }

            list.Add(new string[] { "hello", "how are you", "facebook", "thank", "close", "mom", "notepad", "send", "search", "stop", "print", "alternosfera", "word", "excel", "ana" });
            Grammar gr = new Grammar(new GrammarBuilder(list));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            //            ss.SpeakAsync("hello ladies and gentlemen i am helpu and i am here for you");
            //            ss.SpeakAsync("What can i do for you?");

        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);

            //object path = null;
            switch (e.Result.Text.ToString())
            {
                //case "hello":
                //    ss.SpeakAsync("hello");
                //    break;

                case "how are you":
                    ss.SpeakAsync("I'm fine. How about you?");
                    break;

                case "thank":
                    ss.SpeakAsync("The pleasure was mine");
                    break;

           //     case "alternosfera":
             //       ss.SpeakAsync("we all know that Alternosfera is better than Metallica");
               //         break;

                case "facebook":
                    System.Diagnostics.Process.Start("https://www.facebook.com/");
                    break;

                case "search":
                    ss.SpeakAsync("what?");
                    System.Diagnostics.Process.Start("https://www.google.ro/?gws_rd=ssl");
                    break;

                case "close":
                    System.Windows.Forms.Application.Exit();
                    break;

                case "notepad":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try {
                            label1.Text = openFileDialog1.FileName;
                            ss.SpeakAsync(File.ReadAllText(label1.Text));
                        }

                        catch 
                        {
                            MessageBox.Show("Tipul fisierului invalid!");
                        }   
                    }
                    break;

                case "word":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {

                            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                            label1.Text = openFileDialog1.FileName;
                            object readFromPath = label1.Text;

                            Document doc = app.Documents.Open(ref readFromPath); ;

                            foreach (Paragraph objParagraph in doc.Paragraphs)
                                ss.SpeakAsync(objParagraph.Range.Text.Trim());

                            ((_Document)doc).Close();
                            ((Microsoft.Office.Interop.Word._Application)app).Quit();
                        }

                        catch
                        {
                            MessageBox.Show("Tipul fisierului invalid!");
                        }
                    }
                    break;

                case "excel":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {

                            label1.Text = openFileDialog1.FileName;

                            Microsoft.Office.Interop.Excel.Application xlApp;
                            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                            Microsoft.Office.Interop.Excel.Range range;

                            string str;
                            int rCnt;
                            int cCnt;
                            int rw = 0;
                            int cl = 0;

                            xlApp = new Microsoft.Office.Interop.Excel.Application();
                            xlWorkBook = xlApp.Workbooks.Open(label1.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                            range = xlWorkSheet.UsedRange;
                            rw = range.Rows.Count;
                            cl = range.Columns.Count;


                            for (rCnt = 1; rCnt <= rw; rCnt++)
                            {
                                for (cCnt = 1; cCnt <= cl; cCnt++)
                                {
                                    str = (string)(range.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                    ss.SpeakAsync(str);
                                }
                            }

                            xlWorkBook.Close(true, null, null);
                            xlApp.Quit();

                            Marshal.ReleaseComObject(xlWorkSheet);
                            Marshal.ReleaseComObject(xlWorkBook);
                            Marshal.ReleaseComObject(xlApp);

                        }

                        catch
                        {
                            MessageBox.Show("Tipul fisierului invalid!");
                        }

                    }
                    break;

                case "send":
                    Form a = new Form5();
                    a.Show();
                    break;

             //   case "print":
               //     if(printDialog1.ShowDialog() == DialogResult.OK)
                 //   {
                   //     printDialog1.Document = printDocument1;
                   //    printDocument1.Print();
                    //}
                      //  break;

                case "stop":
                    sre.RecognizeAsyncStop();
                    button3.Enabled = true;
                    button4.Enabled = false;
                    break;

                //case "ana":
                //    ss.SpeakAsync("Yaa, a pice of cake who thinks she is beautiful ");
                //    break;

                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button3.Enabled = true;
            button4.Enabled = false;
        }

        private void Form4_Move(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "Important notice", "Somthing important has come up. Click this to know more.",ToolTipIcon.Info);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button3.Enabled = true;
            button4.Enabled = false;
         //   System.Windows.Forms.Application.Exit();

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form a = new Form1();
            a.Show();
        }

        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

    }
}
