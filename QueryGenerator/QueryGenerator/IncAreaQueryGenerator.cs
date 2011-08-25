using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HilbertTransform;
using QueryEvaluator;
using ResultReporting;

namespace QueryGenerator
{
    public class IncAreaQueryGenerator
    {
        static int TotalArea = 403932;
        static int kmPerDeg = 111;
        QueryPointGenerator qp;

        public IncAreaQueryGenerator()
        {
            qp = new QueryPointGenerator();
        }

        public ExperimentResults<int> AreaPercentage_RTree_RangeQuery(
            string dataFile, int fanout, double pcMin, double pcMax,
            double inc, int trials)
        {
            RTreeQueryEvaluator qe = new RTreeQueryEvaluator(dataFile, fanout);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "range_rtree_ap_" + fanout + '_' + pcMin + '_' + pcMax);

            //Percentage
            for (double p = pcMin; p <= pcMax; p += inc)
            {
                //Calculate the range for this percenage.
                double area = TotalArea * 0.01 * p;
                double kmRange = Math.Sqrt(area / Math.PI);
                double degRange = kmRange / kmPerDeg;

                result.AddCategory(p + " percent");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.RangeQuery(
                        qp.GetRandomQueryPoint(),
                        (float)degRange));
                }
            }

            return result;
        }

        public ExperimentResults<int> AreaPercentage_HC_RangeQuery(
            string dataFile, int order, double pcMin, double pcMax,
            double inc, int trials)
        {
            HCQueryEvaluator qe = new HCQueryEvaluator(dataFile, order, CurveType.UpLeft);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "range_hc_ap_" + order + '_' + pcMin + '_' + pcMax);

            //Percentage
            for (double p = pcMin; p <= pcMax; p += inc)
            {
                //Calculate the range for this percenage.
                double area = TotalArea * 0.01 * p;
                double kmRange = Math.Sqrt(area / Math.PI);
                double degRange = kmRange / kmPerDeg;

                result.AddCategory(p + " percent");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.RangeQuery(
                        qp.GetRandomQueryPoint(),
                        (float)degRange));
                }
            }

            return result;
        }

        public ExperimentResults<int> AreaPercentage_MBR_RangeQuery(
            string dataFile, int fanout, double pcMin, double pcMax,
            double inc, int trials)
        {
            MbrQueryEvaluator qe = new MbrQueryEvaluator(dataFile, fanout);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "range_mbr_ap_" + fanout + '_' + pcMin + '_' + pcMax);

            //Percentage
            for (double p = pcMin; p <= pcMax; p += inc)
            {
                //Calculate the range for this percenage.
                double area = TotalArea * 0.01 * p;
                double kmRange = Math.Sqrt(area / Math.PI);
                double degRange = kmRange / kmPerDeg;

                result.AddCategory(p + " percent");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.RangeQuery(
                        qp.GetRandomQueryPoint(),
                        (float)degRange));
                }
            }
            Console.WriteLine("Query Done.");
            return result;
        }
        public ExperimentResults<int> AreaSqMile_RTree_RangeQuery(
            string dataFile, int fanout, double min, double max,
            double inc, int trials)
        {
            RTreeQueryEvaluator qe = new RTreeQueryEvaluator(dataFile, fanout);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "range_rtree_sqmi_" + fanout + '_' + min + '_' + max);

            //Percentage
            for (double a = min; a <= max; a += inc)
            {
                //Calculate the range for this percenage.
                double miRange = Math.Sqrt(a / Math.PI);
                double degRange = miRange / 69;

                result.AddCategory(a + " Sq Mi");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.RangeQuery(
                        qp.GetRandomQueryPoint(),
                        (float)degRange));
                }
            }

            return result;
        }

        public ExperimentResults<int> AreaSqMile_HC_RangeQuery(
            string dataFile, int order, double min, double max,
            double inc, int trials)
        {
            HCQueryEvaluator qe = new HCQueryEvaluator(dataFile, order, CurveType.UpLeft);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "range_hc_sqmi_" + order + '_' + min + '_' + max);

            //Area
            for (double a = min; a <= max; a += inc)
            {
                //Calculate the range for this percenage.
                double miRange = Math.Sqrt(a / Math.PI);
                double degRange = miRange / 69;

                result.AddCategory(a + " Sq Mi");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.RangeQuery(
                        qp.GetRandomQueryPoint(),
                        (float)degRange));
                }
            }

            return result;
        }
    }
}
