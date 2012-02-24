using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryGenerator;
using ResultReporting;

namespace UnifiedSim
{
    class Program
    {
        static void Main(string[] args)
        {
            IncAreaQueryGenerator qg = new IncAreaQueryGenerator();
            ExperimentResults<int> result = null;

            //Range RTree 100 Pc
            Console.WriteLine("Range RTree 100 Pc" + DateTime.Now.ToString("g"));
            result = qg.AreaPercentage_RTree_RangeQuery(
                "out.txt", 100, 0.01, 0.1, 0.01, 500);
            result.OutputResult();
            Console.WriteLine(DateTime.Now.ToString("g"));

            //Range RTree 150 Pc
            Console.WriteLine("Range RTree 150 Pc" + DateTime.Now.ToString("g"));
            result = qg.AreaPercentage_RTree_RangeQuery(
                "out.txt", 150, 0.01, 0.1, 0.01, 500);
            result.OutputResult();
            Console.WriteLine(DateTime.Now.ToString("g"));

           // //Range RTree 200 Pc
           // Console.WriteLine("Range RTree 200 Pc");
           // result = qg.AreaPercentage_RTree_RangeQuery(
           //     "out.txt", 200, 0.1, 1, 0.1, 500);
           // result.OutputResult();

           // result = qg.AreaPercentage_RTree_RangeQuery(
           //     "out.txt", 200, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();

           // result = qg.AreaPercentage_RTree_RangeQuery(
           //     "out.txt", 300, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();

           // //Range HC 10 Pc
           // Console.WriteLine("Range HC 10 Pc");
           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 10, 0.1, 1, 0.1, 500);
           // result.OutputResult();

           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 10, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();


           // //Range HC 12 Pc
           // Console.WriteLine("Range HC 12 Pc");
           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 12, 0.1, 1, 0.1, 500);
           //result.OutputResult();

           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 12, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();

           // //Range HC 13 Pc
           // Console.WriteLine("Range HC 13 Pc");
           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 13, 0.1, 1, 0.1, 500);
           // result.OutputResult();

           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 13, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();

           // //Range HC 14 Pc
           // Console.WriteLine("Range HC 14 Pc");
           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 14, 0.1, 1, 0.1, 500);
           // result.OutputResult();

           // result = qg.AreaPercentage_HC_RangeQuery(
           //     "out.txt", 14, 0.01, 0.1, 0.01, 500);
           // result.OutputResult();

            //Console.WriteLine("Range HC 16 " + DateTime.Now.ToString("g"));
            //result = qg.AreaPercentage_HC_RangeQuery(
            //    "out.txt", 16, 0.01, 0.1, 0.01, 500);
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));

           // //Range RTree 200 Area
           // Console.WriteLine("Range RTree 200 Area");
           // result = qg.AreaSqMile_RTree_RangeQuery(
           //     "out.txt", 200, 1, 10, 1, 500);
           // result.OutputResult();

            //result = qg.AreaPercentage_RTree_RangeQuery(
            //    "out.txt", 200, 0.01, 0.1, 0.01, 500);
            //result.OutputResult();


            ////Range MBR 200 Area
            //Console.WriteLine("Range MBR 200 Area " + DateTime.Now.ToString("g"));
            //result = qg.AreaPercentage_MBR_RangeQuery(
            //    "out.txt", 200, 0.01, 0.1, 0.01, 1);
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));


            ////Range HC 10 Area
            //Console.WriteLine("Range HC 10 Area");
            //result = qg.AreaSqMile_HC_RangeQuery(
            //    "out.txt", 10, 1, 10, 1, 500);
            //result.OutputResult();
            ////Range HC 12 Area
            //Console.WriteLine("Range HC 12 Area");
            //result = qg.AreaSqMile_HC_RangeQuery(
            //    "out.txt", 12, 1, 10, 1, 500);
            //result.OutputResult();


            KnnQueryGenerator g = new KnnQueryGenerator();
            ////KNN HC 10 
            //Console.WriteLine("KNN HC 10");
            //result = g.HCKnnQuery(
            //    "out.txt",
            //    10,         //Order
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();


            ////KNN HC 12 
            //Console.WriteLine("KNN HC 12");
            //result = g.HCKnnQuery(
            //    "out.txt",
            //    12,         //Order
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();

            ////KNN HC 14 
            //Console.WriteLine("KNN HC 14 " + DateTime.Now.ToString("g"));
            //result = g.HCKnnQuery(
            //    "out.txt",
            //    14,         //Order
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));

            ////KNN HC 16 
            //Console.WriteLine("KNN HC 16 " + DateTime.Now.ToString("g"));
            //result = g.HCKnnQuery(
            //    "out.txt",
            //    16,         //Order
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));

            ////KNN RTree 100 
            //Console.WriteLine("KNN RTree 100" + DateTime.Now.ToString("g"));
            //result = g.RTreeKnnQuery(
            //    "out.txt",
            //    100,        //Fanout
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));

            ////KNN RTree 150 
            //Console.WriteLine("KNN RTree 150" + DateTime.Now.ToString("g"));
            //result = g.RTreeKnnQuery(
            //    "out.txt",
            //    150,        //Fanout
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();
            //Console.WriteLine(DateTime.Now.ToString("g"));

            ////KNN RTree 200 
            //Console.WriteLine("KNN RTree 200");
            //result = g.RTreeKnnQuery(
            //    "out.txt",
            //    200,        //Fanout
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();

            ////KNN RTree 300 
            //Console.WriteLine("KNN RTree 300");
            //result = g.RTreeKnnQuery(
            //    "out.txt",
            //    300,        //Fanout
            //    1,          //min
            //    10,         //max
            //    1,          //inc
            //    500         //trials
            //    );
            //result.OutputResult();

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
