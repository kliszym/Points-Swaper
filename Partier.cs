using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRandomizer
{
    class Partier
    {
        StreamWriter sw;
        string start = "start";
        string part;
        public int counter { get; private set; } = -1;

        public Partier()
        {
            part = start;
        }

        private void SetCounting()
        {
            part = counter.ToString();
        }

        private void StartTag()
        {
            sw = new StreamWriter(part, true);
            sw.Write('0');
            sw.Close();
        }

        public void SetTag()
        {
            using (FileStream fw = new FileStream(part, FileMode.Open))
            {
                fw.Seek(0, SeekOrigin.Begin);
                sw = new StreamWriter(fw);
                sw.Write('1');
                sw.Close();
            }
        }

        public void CounterUp()
        {
            counter++;
            SetCounting();
            StartTag();
        }

        public void Save(string buffer)
        {
            sw = new StreamWriter(part, true);
            sw.Write(buffer);
            sw.Close();
        }

        public void EndPartition()
        {
            if (File.Exists(start)) File.Delete(start);

            for(int i = 0; i <= counter; i++)
            {
                if (File.Exists(i.ToString())) File.Delete(i.ToString());
            }
        }
    }
}
