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
        public const int buffer_size = 200;
        string filePath = "";
        BufferReader br;

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Randomizer";
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //zrobić klasę do zapisywania i zapisywać do kilku plików, 
            //zacząć numerację w przypadku znalezienia "Akapitzlist"
           //start, 0, 1...., koniec
            if (br.GoNext())
            {
                textBox2.Text = br.GetMarkUp();
                textBox3.Text = br.GetData();
            }
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
                    br = new BufferReader(filePath);
                    textBox1.Text = filePath;
                    button1.Enabled = true;
                }
                catch (Exception)
                {
                    button1.Enabled = false;
                    MessageBox.Show("An Error Occures");
                }
            }
        }
    }
}
