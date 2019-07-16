using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRandomizer
{
    class Mark
    {
        string inside;
        string[] attributes;

        public Mark(string markUpStart)
        {
            string temp = "";
            for(int i = 1; i < markUpStart.Length; i++)
            {
                if (markUpStart[i] == ' ' || markUpStart[i] == '>') break;
                temp += markUpStart[i];
            }

            inside = temp;
        }

        public string GetInside()
        {
            return inside;
        }

        public string GetStart()
        {
            return '<' + inside + '>';
        }

        public string GetEnd()
        {
            return "</" + inside + '>';
        }

        public string[] GetAttributes()
        {
            return attributes;
        }
    }
}
