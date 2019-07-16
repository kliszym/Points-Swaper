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

namespace WordRandomizer
{

    public partial class Form1 : Form
    {
        string filePath = "";
        BufferReader br;
        Partier part;
        Matcher matcher;
        string endAkapit = "";
        bool foundFirst = false;
        int points = 0;


        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Shuffler";
            button1.Enabled = false;
            part = new Partier();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //zrobić klasę do zapisywania i zapisywać do kilku plików, 
            //zacząć numerację w przypadku znalezienia "Akapitzlist"
            //start, 0, 1...., koniec
            string mark = "";
            string data = "";

            while (br.GoNext())
            {             
                mark = br.GetMarkUp();
                data = br.GetData();
                textBox2.Text = br.GetMarkUp();
                textBox3.Text = br.GetData();
                Mark m = new Mark(mark);
                //zmiana numerka przy znalezieniu początku akapitzlist i raz na sam koniec, lub
                // raz na początku i potem cały czas jak znajdzie koniec

                if (!foundFirst)
                {
                    if (m.GetInside() != "w:p")
                    {
                        part.Save(data + mark);
                    }
                    else
                    {
                        endAkapit = m.GetEnd();
                        foundFirst = true;
                        part.Save(data);
                        part.CounterUp();
                        part.Save(mark);
                    }
                }
                else
                {
                    //if (mark.Contains("Akapitzlist"))
                    if(m.GetInside() == "w:numPr")
                    {
                        part.SetTag();
                        points++;
                    }

                    part.Save(data + mark);
                    if(mark == endAkapit)part.CounterUp();
                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            //part.EndPartition();
            matcher = new Matcher(part.counter, points, "document.xml");
            matcher.match();
            part.EndPartition();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
