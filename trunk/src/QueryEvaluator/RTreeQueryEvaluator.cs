using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RTree;

namespace QueryEvaluator
{
    public class RTreeQueryEvaluator:QueryEvaluator
    {

        private RTree<int> rtree;

        /// <summary>
        /// Construct an instance with data and RTree max node.
        /// </summary>
        /// <param name="dataPath">The data of the RTree. This file
        /// must be in the specified format.</param>
        /// <param name="maxNode">The maximum number of entries in a
        /// node of the RTree.</param>
        public RTreeQueryEvaluator(string dataPath, int maxNode)
        {
            if (dataPath == null) throw new ArgumentNullException();
            if ((this.rtree = LoadRTree(dataPath, maxNode)) == null)
                throw new ApplicationException("Unable to load RTree");
        }


        private RTree<int> LoadRTree(string dataPath, int maxNode)
        {
            string rex = @"\w+ (\S+) (\S+)";
            RTree<int> rtree = null;
            StreamReader streamReader = null;
            int count = 0;

            try
            {
                rtree = new RTree<int>(maxNode, 0);
                streamReader = new StreamReader(dataPath);
                for (string line = streamReader.ReadLine();
                    line != null;
                    line = streamReader.ReadLine())
                {
                    Match match = Regex.Match(line, rex);
                    if (match.Success)
                    {
                        float lat = float.Parse(match.Groups[2].Value);
                        float lon = float.Parse(match.Groups[1].Value);
                        rtree.Add(new Rectangle(lat, lon, lat, lon, 0, 0), count);
                        count++;
                    }
                }

                return rtree;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to load RTree data: " +
                    ex.Message);
                return null;
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }

        public override int RangeQuery(Point center, float range)
        {
            return RangeQuery(center, range, rtree.RootNode);
        }

        private int RangeQuery(Point center, float range, Node<int> node)
        {
            Circle coverRange = new Circle(center, range);
            int resultCount = 0;

            if (Intersect(node.getMBR(), new Circle(center, range)))
            {
                if (node.isLeaf())
                {
                    resultCount = node.getEntryCount();
                }
                else
                {
                    for (int i = 0; i < node.getEntryCount(); i++)
                    {
                        int childId = node.getId(i);
                        Node<int> childNode = rtree.NodeMap[childId];
                        resultCount += RangeQuery(center, range, childNode);
                    }
                }
            }

            return resultCount;
        }

        public override int KnnQuery(Point center, int k)
        {
            throw new NotImplementedException();
        }
    }
}
