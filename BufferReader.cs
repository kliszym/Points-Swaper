using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRandomizer
{
    class BufferReader
    {
        StreamReader sr;
        string fileName;
        const int bufferSize = 100;
        int result = 0;
        char[] buffer = new char[bufferSize];
        string markUp = "";
        string data = "";
        int markUpStart = 0;
        int markUpEnd = 0;
        bool markUpStarted = false;
        int iterator = 0;

       public BufferReader(string fileName)
        {
            sr = new StreamReader(fileName);
        }

        private string CharToString(char[] array, int start, int count)
        {
            string ret = "";
            for(int i = start; i < start + count; i++)
            {
                ret += array[i];
            }
            return ret;
        }

        private int FindSign(char sign)
        {
            for( ; iterator < result; iterator++)
            {
                if (buffer[iterator] == sign) return iterator;
            }
            return bufferSize;
        }

        private bool FindStartMarkUp()
        {
            int temp = FindSign('<');
            if( temp != bufferSize)
            {
                markUpStart = temp;
                return true;
            }
            return false;
        }

        private bool FindEndMarkUp()
        {
            int temp = FindSign('>');
            if (temp != bufferSize)
            {
                markUpEnd = temp;
                return true;
            }
            return false;
        }

        private bool EndOfBuffer()
        {
            return iterator == result;
        }

        private bool FindMarkUp()
        {
            if (EndOfBuffer())
            {
                if(markUpStarted)
                {
                    markUp += CharToString(buffer, markUpStart, result - markUpStart);
                    Read();
                    return false;
                }
            }
            if (markUpStarted)
            {
                if(FindEndMarkUp())
                {
                    markUpStarted = false;
                    markUp += CharToString(buffer, markUpStart, markUpEnd - markUpStart + 1);
                    return true;
                }

            }
            else
            {
                markUpStarted = true;
                markUp = "";
                while (!FindStartMarkUp())
                {
                    Read();
                    if (result == 0) break;
                }
            }
            return false;
        }    
        
        private void Read()
        {
            result = sr.ReadBlock(buffer, 0, bufferSize);
            iterator = 0;
            markUpStart = 0;
        }

        public string GetMarkUp()
        {
            while (!FindMarkUp())if(result == 0) return null;
            return markUp;
        }
    }
}
