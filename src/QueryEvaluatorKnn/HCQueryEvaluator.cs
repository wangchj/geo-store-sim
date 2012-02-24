using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using RTree;
using HilbertTransform;

namespace QueryEvaluatorKnn
{
    public class HCQueryEvaluator : QueryEvaluator
    {
        /// <summary>
        /// Regular expression used to parse each record (line) of the
        /// data file.
        /// </summary>
        private static string recRex = @"\w+ (\S+) (\S+)";

        private Point2D min;
        private Point2D max;
        private int order;
        private CurveType orien;
        private double dx;
        private double dy;

        /// <summary>
        /// The map between a Hilbert value and points
        /// mapped to the value.
        /// </summary>
        private Dictionary<int, List<Point>> hilbCount;

        /// <summary>
        /// Loads data based on Hilbert Curve order and orientation.
        /// </summary>
        /// <param name="dataPath">The data file.</param>
        /// <param name="order">Hilbert Curve order.</param>
        /// <param name="ori">Hilbert Curve orientation.</param>
        public HCQueryEvaluator(string dataPath, int order, CurveType ori)
        {
            this.order = order;
            this.orien = ori;

            LoadData(dataPath, order, ori);

            this.dx = (max.X - min.X) / (Math.Pow(2, order));
            this.dy = (max.Y - min.Y) / (Math.Pow(2, order));
        }

        private void LoadData(string dataPath, int order, CurveType ori)
        {
            StreamReader streamReader = null;
            hilbCount = new Dictionary<int, List<Point>>();
            Rectangle mbr = GetDataMbr(dataPath);
            min = new Point2D(mbr.MinX, mbr.MinY);
            max = new Point2D(mbr.MaxX, mbr.MaxY);

            try
            {
                streamReader = new StreamReader(dataPath);
                for (string line = streamReader.ReadLine();
                    line != null;
                    line = streamReader.ReadLine())
                {
                    Match match = Regex.Match(line, recRex);
                    if (match.Success)
                    {
                        float lat = float.Parse(match.Groups[2].Value);
                        float lon = float.Parse(match.Groups[1].Value);
                        int hilbVal = HCTransform.Transform(
                            new Point2D(lat, lon), min, max, order, ori);
                        if (hilbCount.ContainsKey(hilbVal))
                        {
                            hilbCount[hilbVal].Add(new Point(lat, lon));
                        }
                        else
                        {
                            List<Point> list = new List<Point>();
                            list.Add(new Point(lat, lon));
                            hilbCount.Add(hilbVal, list);
                        }
                    }
                }
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }

        private Rectangle GetDataMbr(string dataPath)
        {
            
            Rectangle result = null;
            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(dataPath);
                for (string line = streamReader.ReadLine();
                    line != null;
                    line = streamReader.ReadLine())
                {
                    Match match = Regex.Match(line, recRex);
                    if (match.Success)
                    {
                        float lat = float.Parse(match.Groups[2].Value);
                        float lon = float.Parse(match.Groups[1].Value);
                        if (result == null)
                            result = new Rectangle(lat, lon, lat, lon, 0, 0);
                        else
                            result.add(new Rectangle(lat, lon, lat, lon, 0, 0));
                    }
                }

                return result;
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }

        public override int RangeQuery(Point center, float range)
        {
            int result = 0;

            HilbertDistEnumerator en = new HilbertDistEnumerator(
                new Point2D(center.X, center.Y),
                range,
                this.min,
                this.max,
                this.order,
                this.orien);

            foreach (int d in en)
            {
                if (hilbCount.ContainsKey(d))
                    result += hilbCount[d].Count;
            }

            return result;
        }

        public override int KnnQuery(Point center, int k)
        {
            int hilb = HCTransform.Transform(
                new Point2D(center.X, center.Y),
                min, max, order, orien);

            int count = 0;

            for (int i = 0; i < Math.Min(hilb, MaxHilbDist - hilb); i++)
            {
                if (i == 0)
                {
                    count += hilbCount.ContainsKey(hilb) ? hilbCount[hilb].Count : 0;
                    if (count >= k)
                        return RangeQuery(center, MaxObjDist(center, hilb));
                }
                else
                {
                    int c1 = hilbCount.ContainsKey(hilb - i) ? hilbCount[hilb - i].Count : 0;
                    int c2 = hilbCount.ContainsKey(hilb + i) ? hilbCount[hilb + i].Count : 0;

                    count += (c1 + c2);

                    if (count >= k)
                        return RangeQuery(center,
                            Math.Max(MaxObjDist(center, hilb - i),
                                     MaxObjDist(center, hilb + i)));
                }
            }

            //Should not reach here for this experiment.
            //Could reach here only if k is humongously large.
            //TODO: return all numbers in the system.
            return 0;
        }

        /// <summary>
        /// Max distance from p to an object in cell with Hilbert value.
        /// </summary>
        /// <param name="p">The point</param>
        /// <param name="cellHilb">Hilbert value of the cell.</param>
        /// <returns>
        /// The distance to the furthest object. -1 If the cell contains
        /// no object.
        /// </returns>
        private float MaxObjDist(Point p, int cellHilb)
        {
            float max = 0;
            
            if (!hilbCount.ContainsKey(cellHilb))
                return -1;

            foreach (Point q in hilbCount[cellHilb])
            {
                float dist = p.distance(q);
                if (dist > max)
                    max = dist;
            }
            return max;
        }

        /// <summary>
        /// The largest Hilbert Distance with the order.
        /// </summary>
        public int MaxHilbDist
        {
            get
            {
                return ((int)Math.Pow(4, order)) - 1;
            }
        }

       

    }

    /// <summary>
    /// Iterates over a list of Hilbert values (distances) inside
    /// and range query region.
    /// </summary>
    class HilbertDistEnumerator : IEnumerator<int>, IEnumerable<int>
    {
        private bool reset;

        /// <summary>
        /// The bottom left corner of the region.
        /// </summary>
        private Point2D min;
        /// <summary>
        /// The upper righ corner of the region.
        /// </summary>
        private Point2D max;
        /// <summary>
        /// The order of the Hilbert curve.
        /// </summary>
        int order;
        /// <summary>
        /// The orientation of the Hilbert Curve.
        /// </summary>
        CurveType orien;

        /// <summary>
        /// The center of the query range (the query point).
        /// </summary>
        private Point2D queryPt;
        /// <summary>
        /// The query search range. Radius of the circular query region.
        /// </summary>
        private double range;

        /// <summary>
        /// Width of each Hilbert cell with respect to x.
        /// </summary>
        private double dx;
        /// <summary>
        /// Width of each Hilbert cell with respect to y.
        /// </summary>
        private double dy;

        /// <summary>
        /// A temporary variable to track the current x value.
        /// </summary>
        private double x;
        /// <summary>
        /// A temporary variable to track the current y value.
        /// </summary>
        private double y;

        public HilbertDistEnumerator(Point2D p, double range, Point2D min, Point2D max,
            int order, CurveType orien)
        {
            if (min == null || max == null)
                throw new ArgumentNullException();

            this.queryPt = p;
            this.range = range;
            this.min = min;
            this.max = max;
            this.order = order;
            this.orien = orien;

            Reset();
        }

        /// <summary>
        /// Sets the width of Hilbert cell.
        /// </summary>
        private void setDx()
        {
            dx = (max.X - min.X) / (Math.Pow(2, order));
        }

        /// <summary>
        /// Sets the width of Hilbert cell.
        /// </summary>
        private void setDy()
        {
            dy = (max.Y - min.Y) / (Math.Pow(2, order));
        }

        /// <summary>
        /// Gets the x min boundary of the Hilbert cell that contains
        /// the point p.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <returns>The x lower bound of the Hilbert cell.</returns>
        /// <exception cref="System.NullPointerException">
        /// If p is null.</exception>
        private double GetCellMinX(Point2D p)
        {
            //double dif = (p.X - min.X) % dx;
            //return p.X - dif;
            return GetCellMinX(p.X);
        }

        private double GetCellMinX(double x)
        {
            double dif = (x - min.X) % dx;
            return x - dif;
        }

        /// <summary>
        /// Gets the y min boundary of the Hilbert cell that contains
        /// the point p.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <returns>The y lower bound of the Hilbert cell.</returns>
        /// <exception cref="System.NullPointerException">
        /// If p is null.</exception>
        private double GetCellMinY(Point2D p)
        {
            //double dif = (p.Y - min.Y) % dy;
            //return p.Y - dif;
            return GetCellMinY(p.Y);
        }

        private double GetCellMinY(double y)
        {
            double dif = (y - min.Y) % dy;
            return y - dif;
        }

        private double GetCellMaxX(Point2D p)
        {
            return GetCellMinX(p) + dx;
        }

        private double GetCellMaxX(double x)
        {
            return GetCellMinX(x) + dx;
        }

        private double GetCellMaxY(Point2D p)
        {
            return GetCellMinY(p) + dy;
        }

        private double GetCellMaxY(double y)
        {
            return GetCellMinY(y) + dy;
        }

        #region IEnumerator<int> Members

        /// <summary>
        /// Get the current Hilbert value.
        /// </summary>
        public int Current
        {
            get
            {
                return HCTransform.Transform(
                  new Point2D(x, y),
                  min,
                  max,
                  order,
                  orien);
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// No implementation.
        /// </summary>
        public void Dispose(){}

        #endregion

        #region IEnumerator Members

        /// <summary>
        /// Get the current Hilbert value.
        /// </summary>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Move the iterator to the next Hilbert value.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            double tx = x;
            double ty = y;

            while (true)
            {
                if (reset)
                {
                    tx = Math.Max(queryPt.X - range, min.X + dx / 2);
                    ty = Math.Max(queryPt.Y - range, min.Y + dy / 2);
                    reset = false;
                }
                else
                {
                    tx = tx + dx;
                    if (GetCellMinX(tx) > queryPt.X + range || tx > max.X)
                    {
                        tx = Math.Max(queryPt.X - range, min.X + dx / 2);
                        ty = ty + dy;

                        if (GetCellMinY(ty) > queryPt.Y + range || ty > max.Y)
                            return false;
                    }
                }

                if (QueryEvaluator.Intersect(
                    GetCellMinX(tx), GetCellMinY(ty),
                    GetCellMaxX(tx), GetCellMaxY(ty),
                    new Circle(new Point((float)queryPt.X, (float)queryPt.Y),
                        (float)range)))
                {
                    x = tx;
                    y = ty;
                    return true;
                }  
            }

            //if (tx > queryPt.X + range || tx > max.X)
            //{
            //    double ty = y + dy;
            //    if (ty > queryPt.Y + range || ty > max.Y)
            //        return false;
            //    y = ty;
            //    x = Math.Max(queryPt.X - range, min.X + dx / 2);
            //}
            
            //x = tx;
            //return true;
        }

        /// <summary>
        /// Resets the enumeration of Hilbert values.
        /// </summary>
        /// <remarks>
        /// Internally, each instance keep track of the current x and
        /// y values and uses these to calculate the Hilbert Value within
        /// the bounding box (min, max). This method reset x and y to
        /// the minimum value.
        /// 
        /// This method should be called after properties have been
        /// changed.
        /// </remarks>
        public void Reset()
        {
            setDx();
            setDy();
            
            reset = true;
        }

        #endregion

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
    }
}
