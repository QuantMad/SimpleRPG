using System;

namespace SimpleRPG.Core
{
    class Chank
    {
        public char[] val = new char[2];

        public Chank()
        {

        }

        public Chank(string val)
        {
            Set(val);
        }

        public void Clear()
        {
            val[0] = ' ';
            val[1] = ' ';
        }

        public void Set(String str)
        {
            if (str.Length != 2)
            {
                throw new IndexOutOfRangeException();
            }

            val[0] = str[0];
            val[1] = str[1];
        }

        public override string ToString()
        {
            return val[0].ToString() + val[1].ToString();
        }
    }
}
