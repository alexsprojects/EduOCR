using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace EdPerfectInterface
{
    public partial class Form1 : Form
    {
        public List<string> vs = new List<string>();
        public Form1()
        {
            InitializeComponent();
            a();
        }
        void a()
        {
            listBox1.Items.Add("Calibrating Files...");
            //Console.ForegroundColor = ConsoleColor.Green;
            listBox1.ForeColor = Color.LawnGreen;
            listBox1.Items.Add("Done!");
            listBox1.ForeColor = Color.White;
            //Console.ForegroundColor = ConsoleColor.White;

            //  listBox1.Items.Add("Enter String Array[]");
            //  string Wordarray = Console.ReadLine(); //Words have to have a space betwen them
            //  string[] SplitWords = Wordarray.Split(' ');
            //  for (int i = 0; i < SplitWords.Length; i++)
            //  {
            //      listBox1.Items.Add(SplitWords[i]);
            //   }
            
            //Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Graphics graphics = Graphics.FromImage(bitmap as Image);
            //graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            //bitmap.Save("c:\\screenshot.png", System.Drawing.Imaging.ImageFormat.Png);


            //Bitmap img = new Bitmap("TesseractOCRTestImage.png");
            //TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
            //Page page = engine.Process(img, PageSegMode.Auto);
            //string result = page.GetText();
            //listBox1.Items.Add(result);
        }
        Bitmap TakeScreenshot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                return bmp;
               // bmp.Save("screenshot.png");  // saves the image
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Bitmap img = new Bitmap("TesseractOCRTestImage.png");
           for(int i = 0; i < 100; i++)
            {
                Bitmap img = TakeScreenshot();
                TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                Page page = engine.Process(img, PageSegMode.Auto);
                string result = page.GetText();
                listBox1.Items.Add(result);
                string translation = FilterOutput(result);
                AutoWrite(translation);
            }
        }

        void AutoWrite(string translate)
        {
            char[] letters = translate.ToCharArray();
            for(int i = 0; i < letters.Length; i++)
            {
                SendKeys.Send(letters[i].ToString());
            }
            SendKeys.Send("{ENTER}");
        }

        string FilterOutput(string strRes)
        {
            for(int i = 0; i <= vs.Count; i++)
            {
                if (strRes.Contains(vs[i]))
                {
                    if(i % 2 == 0)
                    {
                        DoubleSTR res = new DoubleSTR();
                        res.IndonesianWord = vs[i];
                        res.EnglishWord = vs[i + 1];

                        return res.EnglishWord;
                    }
                    else
                    {
                        DoubleSTR res = new DoubleSTR();
                        res.EnglishWord = vs[i];
                        res.IndonesianWord = vs[i + 1];

                        return res.IndonesianWord;
                    }
                }
            }
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
              listBox1.Items.Add("Enter String Array[]");
            string Wordarray = textBox1.Text; //Words have to have a space betwen them
              string[] SplitWords = Wordarray.Split(' ');
              for (int i = 0; i < SplitWords.Length; i++)
              {
                vs.Add(SplitWords[i]);
                  listBox1.Items.Add(SplitWords[i]);
              }
        }
    }
    class DoubleSTR
    {
        public string EnglishWord;
        public string IndonesianWord;
    }
}
