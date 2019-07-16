using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRandomizer
{
    class Matcher
    {
        BufferReader br;
        StreamWriter sw;
        int count;
        int points;
        string fileName;
        int[] numbers;
        Random r;

        public Matcher(int count, int points, string fileName)
        {
            r = new Random();
            this.count = count + 1;
            this.fileName = fileName;
            this.points = points;
            randomize();
        }

        public void RewriteStart()
        {
            br = new BufferReader("start");
            sw = new StreamWriter(fileName, true);

            while (br.GoNext())
            {
                sw.Write(br.GetData() + br.GetMarkUp());
            }

            br.Close();
            sw.Close();
        }

        private int randomNumber(int[] previous, int length, int max)
        {
            int number = 0;
            number = (int)(r.NextDouble() * max);

            for(int i = 0; i < length; i++)
            {
                if (number == previous[i])
                {
                    if (number != max - 1) number++;
                    else number = 0;
                    i = -1;
                }
            }

            return number;
        }

        private void randomize()
        {
            numbers = new int[points];
            for (int i = 0; i < points; i++)
            {
                numbers[i] = randomNumber(numbers, i, points);
            }
        }

        private void rewrite()
        {
            sw = new StreamWriter(fileName, true);
            sw.Write(br.GetMarkUp());
            while(br.GoNext())
            {
                sw.Write(br.GetData() + br.GetMarkUp());
            }
            sw.Close();
        }

        private void RewriteNumbers()
        {
            int j = 0;
            int startList = 0;
            for(int i = 0; i < count; i++)
            {
                br = new BufferReader(i.ToString());
                if (br.GoNext())
                {
                    if (br.GetData().ToCharArray()[0] == '0')
                    {
                        startList = i + 1;
                        rewrite();
                        br.Close();
                    }
                    else
                    {
                        br.Close();
                        br = new BufferReader((numbers[j] + startList).ToString());
                        if (br.GoNext())
                        {
                            j++;
                            rewrite();
                            br.Close();
                        }
                    }
                }
            }
        }

        public void match()
        {
            RewriteStart();
            RewriteNumbers();

        }


    }
}
