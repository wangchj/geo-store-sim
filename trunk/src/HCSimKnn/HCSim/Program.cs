using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryEvaluatorKnn;
using HilbertTransform;
using RTree;

namespace HCSimKnn
{
    class Program
    {
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
            HCQueryEvaluator qe = new HCQueryEvaluator("CA.txt", 12, CurveType.UpLeft);
            //Console.WriteLine(qe.KnnQuery(new Point(3f, 5f), 1));
            
            //From 1 to 5 percent
            for (int k = 1; k <= 10; k++)
            {
                Console.WriteLine(k + " Nearest Neighbor");

                //For each experiment, run 100 times
                for (int i = 0; i < 500; i++)
                {
                    double x = RandX();
                    Console.WriteLine(qe.KnnQuery(
                        new Point((float)x, (float)y(x)), k));
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
