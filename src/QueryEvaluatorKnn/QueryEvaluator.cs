using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTree;

namespace QueryEvaluatorKnn
{
    public abstract class QueryEvaluator
    {
        public abstract int RangeQuery(Point center, float range);
        public abstract int KnnQuery(Point center, int k);

        public static bool Intersect(Rectangle rec, Circle cir)
        {
            if (rec == null || cir == null)
                throw new ArgumentNullException();

            if (rec.distance(cir.Center) == 0)
                return true;
            if (rec.distance(cir.Center) > cir.Radius)
                return false;
            return true;
        }

        public static bool Intersect(double x1, double y1, double x2, double y2, Circle cir)
        {
            Rectangle rec = new Rectangle((float)x1,
                (float)y1,
                (float)x2,
                (float)y2,
                0,
                0);
            return Intersect(rec, cir);
        }
    }
}
