using System;
using System.Collections.Generic;
using ResultReporting;
using QueryEvaluatorKnn;
using HilbertTransform;

namespace QueryGenerator
{
    public class KnnQueryGenerator
    {
        QueryPointGenerator qp;

        public KnnQueryGenerator()
        {
            qp = new QueryPointGenerator();
        }

        public ExperimentResults<int> HCKnnQuery(
               string dataFile, int order, int min, int max,
               int inc, int trials)
        {
            HCQueryEvaluator qe = new HCQueryEvaluator(dataFile, order, CurveType.UpLeft);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "knn_hc_" + order + '_' + min + '_' + max);

            //Area
            for (int k = 1; k <= 10; k+=inc)
            {
                result.AddCategory(k + " NN");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.KnnQuery(
                        qp.GetRandomQueryPoint(),
                        k));
                }
            }

            return result;
        }

        public ExperimentResults<int> RTreeKnnQuery(
            string dataFile, int fanout, int min, int max,
            int inc, int trials)
        {
            RTreeQueryEvaluator qe = new RTreeQueryEvaluator(dataFile, fanout);
            ExperimentResults<int> result = new ExperimentResults<int>(
                "knn_rtree_" + fanout + '_' + min + '_' + max);

            for (int k = 1; k <= 10; k+=inc)
            {
                result.AddCategory(k + " NN");

                //For each experiment, run trial number of times
                for (int i = 0; i < trials; i++)
                {
                    result.AddResult(qe.KnnQuery(
                        qp.GetRandomQueryPoint(),
                        k));
                }
            }

            return result;
        }
    }
}
