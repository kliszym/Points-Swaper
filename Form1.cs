using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Xml;

namespace WordRandomizer
{

    public partial class Form1 : Form
    {
        public const int buffer_size = 100;
        string filePath = "";
        string previous_data = "";

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Randomizer";
        }

        void read_buffer(StreamReader sr, char[] buffer)
        {
            sr.ReadBlock(buffer, 0, buffer_size);
        }

        bool find_pointer_start(char[] buffer, StreamWriter sw)
        {
            string buf = buffer.ToString();


            int index = 0;
            int saved_index = -1;
            int start_markup = -1;

            string subbuffer = previous_data + buf;
            if (subbuffer.Contains("<w:p"))
            {
                index++;    
            }
            previous_data = buf.Substring(buf.Length - 4, 3);
             


            return true;
        }

        void find_pointer_end(char[] buffer)
        {

        }

        private int unzip()
        {
            int index = 0;
            char[] buffer = new char[buffer_size];
            string path = textBox1.Text;
            if (!File.Exists(path)) return -1;
   //         int counter = 0;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            StreamReader sr = new StreamReader(path);

            StreamWriter sw = new StreamWriter("" + index + ".txt", true);

            string line;

            while (sr.ReadBlock(buffer, 0, buffer_size) != 0)
            {
                
                sw.WriteLine(line);
                sw.WriteLine("" + index);
                //                sw.Write(xr)
                //       xr.NodeType;
                index++;
            }

            sr.Close();
            sw.Close();

            return index;
        }

        private void place_random()
        {

        }

        private void randomize()
        {
            if(unzip() != null)place_random();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            randomize();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Choose Word File";
            dialog.Filter = "Word Files (*.docx) | *.docx | XML tries (*.xml) | *.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePath = dialog.FileName;
                    textBox1.Text = filePath;
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occures");
                }
            }
        }
    }
}
