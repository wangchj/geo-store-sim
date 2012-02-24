using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTree;

namespace QueryGenerator
{
    public class QueryPointGenerator
    {
        Random rand;

        static double[] recs =
        {
            //Rect 1
            38.48215869605057f, -123.5917422219071f,    //min x, min y
            41.80111639308209f, -120.2796416342462f,    //max x, max y
            //Rect 2
            36.08335460507635f, -121.9228148383769f,    //min x, min y 
            38.61648717150925f, -119.1503319736332f,    //max x, max y
            //Rect 3
            34.75929840623879f, -120.6492868681822f,    
            37.94895404620413f, -117.9272070691563f,

            34.26853682342921f, -119.415240177104f,
            37.37199603753574f, -117.1268428588685f,

            33.81708849280906f, -118.3986960955361f,
            36.70379205823752f, -116.4226342951345f,

            33.42967498003513f, -117.75085536016f,
            36.06005834476176f, -115.7521839491179f,

            32.90342837547054f, -117.4002023547573f,
            35.48727852869508f, -115.0667056702966f

        };

        static int minx = 0, miny = 1, maxx = 2, maxy = 3;

        public QueryPointGenerator()
        {
            rand = new Random();
        }

        public Point GetRandomQueryPoint()
        {
            int recNum = rand.Next(recs.Length / 4);
            double dx = recs[recNum * 4 + maxx] - recs[recNum * 4 + minx];
            double dy = recs[recNum * 4 + maxy] - recs[recNum * 4 + miny];
            double rx = dx * rand.NextDouble() + recs[recNum * 4 + minx];
            double ry = dy * rand.NextDouble() + recs[recNum * 4 + miny];
            return new Point((float)rx, (float)ry);
        }

    }
}
