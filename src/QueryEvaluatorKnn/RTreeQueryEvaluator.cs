using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RTree;

namespace QueryEvaluatorKnn
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public override int KnnQuery(Point center, int k)
        {

            //Stage 1

            SortedList<float, List<Node<int>>> nearestNodes =
                new SortedList<float, List<Node<int>>>();
            Dictionary<int, Node<int>> nodeMap = rtree.NodeMap;
            foreach (Node<int> node in nodeMap.Values)
            {
                if (!node.isLeaf())
                    continue;

                //Get distance between center and node MBR.
                float dist = node.getMBR().distance(center);
                
                //See if this node can be ignored.
                if (nearestNodes.Count >= k &&
                        dist >= nearestNodes.Keys[nearestNodes.Count - 1])
                    continue;

                if (nearestNodes.ContainsKey(dist))
                {
                    nearestNodes[dist].Add(node);
                }
                else
                {
                    List<Node<int>> l = new List<Node<int>>();
                    l.Add(node);
                    nearestNodes.Add(dist, l);
                }
                
                //if (nearestNodes.Count > 2 * k)
                //    nearestNodes.Capacity = k;
            }

            //nearestNodes.Capacity = k;

            //Stage 2
            //Get a list of closest objects and their
            //containing node.
            SortedList<float, List<int>> nearestObj = new SortedList<float, List<int>>();
            foreach (List<Node<int>> list in nearestNodes.Values)
            {
                foreach (Node<int> node in list)
                {
                    for (int i = 0; i < node.getEntryCount(); i++)
                    {
                        Rectangle rec = node.getEntry(i);
                        float dist = rec.distance(center);

                        //If the distance is larger than k-th object, ignore.
                        if (nearestObj.Count >= k &&
                            dist >= nearestObj.Keys[nearestObj.Count - 1])
                            continue;

                        if (nearestObj.ContainsKey(dist))
                        {
                            nearestObj[dist].Add(node.NodeId);
                        }
                        else
                        {
                            List<int> l = new List<int>();
                            l.Add(node.NodeId);
                            nearestObj.Add(dist, l);
                        }
                    }

                    //if (nearestObj.Count > 2 * k)
                    //    nearestObj.Capacity = k;
                }
            }

            //if (nearestObj.Count > k)
            //    nearestObj.Capacity = k;


            //Stage 3

            HashSet<int> NodeIdSet = new HashSet<int>();
            int result = 0;
            int count = 0;
            foreach (List<int> list in nearestObj.Values)
            {
                foreach (int id in list)
                {
                    count++;
                    if (!NodeIdSet.Contains(id))
                    {
                        result += nodeMap[id].getEntryCount();
                        NodeIdSet.Add(id);
                    }
                    if (count >= k)
                        return result;
                }
            }

            return result;
        }
    }
}
