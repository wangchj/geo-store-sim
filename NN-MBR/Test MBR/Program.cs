using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NN_MBR;


namespace Test_MBR
{
    class Program
    {
        static void Main(string[] args)
        {
            MBR_Holder mbrs = new MBR_Holder();
            mbrs.readFromFile("out.txt");
            mbrs.formMBRs(3);
            List<MBR> mbrlist = new List<MBR>();
            mbrlist = mbrs.MBR;
            Console.ReadLine();
        }
    }
}
