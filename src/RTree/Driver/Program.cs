using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTree;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            RTree<int> rtree1 = new RTree<int>(4, 0);
            //rtree1.Add(new Rectangle(30, 30, 30, 30, 0, 0), 101);
            //rtree1.Add(new Rectangle(12, 12, 12, 12, 0, 0), 102);
            //rtree1.Add(new Rectangle(14, 14, 14, 14, 0, 0), 103);
            //rtree1.Add(new Rectangle(16, 16, 16, 16, 0, 0), 104);
            //rtree1.Add(new Rectangle(35, 35, 35, 35, 0, 0), 105);

            rtree1.Add(new Rectangle(2, 6, 2, 6, 0, 0), 1);
            rtree1.Add(new Rectangle(2.5f, 6, 2.5f, 6, 0, 0), 2);
            rtree1.Add(new Rectangle(3, 5, 3, 5, 0, 0), 3);
            rtree1.Add(new Rectangle(3, 6, 3, 6, 0, 0), 4);
            rtree1.Add(new Rectangle(3.5f, 6, 3.5f, 6, 0, 0), 5);
            rtree1.Add(new Rectangle(4, 6, 4, 6, 0, 0), 6);
            rtree1.Add(new Rectangle(1.5f, 5.5f, 1.5f, 5.5f, 0, 0), 7);
            rtree1.Add(new Rectangle(1.5f, 5.1f, 1.5f, 5.1f, 0, 0), 8);
            rtree1.Add(new Rectangle(3.5f, 5.5f, 3.5f, 5.5f, 0, 0), 9);
            rtree1.Add(new Rectangle(4, 5.5f, 4, 5.5f, 0, 0), 10);
            rtree1.Add(new Rectangle(4.5f, 5.5f, 4.5f, 5.5f, 0, 0), 11); //k
            Console.WriteLine("End Of Program");
        }
    }
}
