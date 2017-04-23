using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.IO;

namespace
{
    class Class1
    {


        void StairCase(int n)
        {
            string printOut = "";
            string emptySpace = " ";

            for (int i = 0; i < n; i++)
            {
                printOut.PadLeft((n - 1) - i);
                printOut += "#";

                Console.Write(printOut);
            }
        }
    }
}
