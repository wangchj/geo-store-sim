using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryEvaluator;
using HilbertTransform;
using RTree;

namespace HCSim
{
    class Program
    {

        private static int TotalArea = 403932;
        private static int kmPerDeg = 111;

        //Two points of the line that run through california.
        static float x1 = 38.74638156586607f;
        static float y1 = -121.6881929723431f;
        static float x2 = 34.19009567603121f;
        static float y2 = -116.3039867148634f;

        //Line between the two points
        static float slope = (y2 - y1) / (x2 - x1);
        static float yint = y1 - (slope * x1);

        static float xdiff = x1 - x2;

        static void Main(string[] args)
        {
            //Test data
            //HCQueryEvaluator qe = new HCQueryEvaluator("CA2.txt", 4, CurveType.UpLeft);
            //int r = qe.RangeQuery(new Point(3f, 5f), 2f);
            
            //CA real data
            //HCQueryEvaluator qe1 = new HCQueryEvaluator("CA.txt", 15, CurveType.UpLeft);
            //int r1 = qe1.RangeQuery(new Point(35f, -118f), 3f);

            
            //for (int i = 1; i < 11; i++)
            //{
            //    HCQueryEvaluator eval = new HCQueryEvaluator("CA.txt", i, CurveType.UpLeft);
            //    Console.WriteLine(i.ToString() + ": " +
            //        eval.RangeQuery(new Point(35f, -118f), 1f));
            //}

            
            HCQueryEvaluator qe = new HCQueryEvaluator("CA.txt", 12, CurveType.UpLeft);

            
            //From 1 to 5 percent
            for (int p = 1; p <= 5; p++)
            {
                //Calculate the range for this percenage.
                double area = TotalArea * 0.1 * p;
                double kmRange = Math.Sqrt(area / Math.PI);
                double degRange = kmRange / kmPerDeg;

                Console.WriteLine(p + " percent");

                //For each experiment, run 100 times
                for (int i = 0; i < 100; i++)
                {
                    double x = RandX();
                    Console.WriteLine(qe.RangeQuery(
                        new Point((float)x, (float)y(x)),
                        (float)degRange));
                }
            }
             
        }

        static Random rand = new Random();

        static double RandX()
        {
            return xdiff * rand.NextDouble() + x2;
        }

        static double y(double x)
        {
            return slope * x + yint;
        }
    }
}
