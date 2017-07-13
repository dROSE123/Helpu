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
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SpeechSynthesizer s = new SpeechSynthesizer();
        //    switch(textBox1.Text)
        //    {
        //        case "hello":
        //            s.Speak("Hello,Horia!");
        //            break;
        //        case "how are you?":
        //            s.Speak("I'm fine.How it's going with you?");
        //            break;
        //        case "how old are you?":
        //            s.Speak("yooo, men. I am a computer.I do not have an age. Fuck you! Go fuck yourself!!!");
        //            break;
        //        default:
        //            s.Speak("Shaaoorrmmaa pa veataa");
        //            break;       
        //    }
            
        //}


        //vorbire/auzire

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;

            list.Add(new string[] {"hello","how","facebook","thank","close", "mom","file","send","search","stop","print","alternosfera" , "word" , "excel", "ana"});
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

            ss.SpeakAsync("hello ladies and gentlemen i am helpu and i am here for you");
            ss.SpeakAsync("What can i do for you?");
            
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //object path = null;
            switch (e.Result.Text.ToString())
            {
                //case "hello":
                //    ss.SpeakAsync("hello");
                //    break;

                case "how":
                    ss.SpeakAsync("I'm fine. How are you?");
                    break;

                case "thank":
                    ss.SpeakAsync("The pleasure was mine");
                    break;

                case "alternosfera":
                    ss.SpeakAsync("we all know that Alternosfera is better than Metallica");
                        break;

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

                case "file":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        label1.Text = openFileDialog1.FileName;
                        ss.SpeakAsync(File.ReadAllText(label1.Text));
                    }
                    break;

                case "word":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                    break;

                case "excel":
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
                        _excelApp.Visible = true;

                        //label1.Text = openFileDialog1.FileName;

                        string fileName = @"C:\Users\N.Horatiu\Desktop\a.xlsx";

                        //string fileName = label1.Text;

                        Workbook workbook = _excelApp.Workbooks.Open(fileName,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing);
     
                        Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

                        Microsoft.Office.Interop.Excel.Range excelRange = worksheet.UsedRange;

                        object[,] valueArray = (object[,])excelRange.get_Value(
                                    XlRangeValueDataType.xlRangeValueDefault);

                        for (int row = 1; row <= worksheet.UsedRange.Rows.Count; ++row)
                        {
                            for (int col = 1; col <= worksheet.UsedRange.Columns.Count; ++col)
                            {
                                ss.SpeakAsync(valueArray[row, col].ToString());
                            }
                        }

                        workbook.Close(false, Type.Missing, Type.Missing);
                        Marshal.ReleaseComObject(workbook);

                        _excelApp.Quit();
                        Marshal.FinalReleaseComObject(_excelApp);
                    }
                    break;

                case "send":
                    Form a = new Form5();
                    a.Show();
                    break;

                case "print":
                    printDialog1.Document = printDocument1;
                    if(printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                        break;

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
            System.Windows.Forms.Application.Exit();

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
    }
}
