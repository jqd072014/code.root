using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CBTest
{
    public class PIEKeyboardContext
    {
        public byte[] InputData { get; set; }

        public string GetKeyCode(byte[] inputData)
        {
            var keyData = inputData.Take(13).Skip(3);  // 13, or 19
            if (keyData.All(b => b == 0)) return "KeyUp";

            int i=0;
            string ret = "";
            foreach(var b in keyData )
            {
                if (b!=0)
                {
                  ret+=  Convert.ToChar(i + 65)+b.ToString();  // e.g A1, D2, E128,F32... with mutiple key pressed.
                }
                i++;
            }
            return ret;
        }


    }
}

