using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RTree;
using NN_MBR;

namespace QueryEvaluatorKnn
{
    public class MbrEvaluator : QueryEvaluator
    {
        private List<MBR> mbrs;

        /// <summary>
        /// Construct an instance with data and RTree max node.
        /// </summary>
        /// <param name="dataPath">The data of the RTree. This file
        /// must be in the specified format.</param>
        /// <param name="maxNode">The maximum number of entries in a
        /// node of the RTree.</param>
        public MbrEvaluator(string dataPath, int maxNode)
        {
            if (dataPath == null) throw new ArgumentNullException();
            LoadData(dataPath, maxNode);
        }


        private void LoadData(string dataPath, int maxNode)
        {
            MBR_Holder loader = new MBR_Holder();
            loader.readFromFile(dataPath, maxNode);
            mbrs = loader.MBR;
        }

        public override int RangeQuery(RTree.Point center, float range)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public override int KnnQuery(RTree.Point center, int k)
        {
            return 0;
            /*
            //Stage 1

            SortedList<float, List<MBR>> nearestMBR =
                new SortedList<float, List<MBR>>();
            foreach (MBR mbr in this.mbrs)
            {
                //Get distance between center and node MBR.
                List<float> bounds = mbr.getBounts();
                Rectangle tempRec = new Rectangle(bounds[0], bounds[1], bounds[2], bounds[3], 0, 0);
                float dist = tempRec.distance(center);
                
                //See if this node can be ignored.
                if (nearestMBR.Count >= k &&
                        dist >= nearestMBR.Keys[nearestMBR.Count - 1])
                    continue;

                if (nearestMBR.ContainsKey(dist))
                {
                    nearestMBR[dist].Add(mbr);
                }
                else
                {
                    List<MBR> l = new List<MBR>();
                    l.Add(mbr);
                    nearestMBR.Add(dist, l);
                }
                
                //if (nearestNodes.Count > 2 * k)
                //    nearestNodes.Capacity = k;
            }

            //nearestNodes.Capacity = k;

            //Stage 2
            //Get a list of closest objects and their
            //containing node.
            SortedList<float, List<int>> nearestObj = new SortedList<float, List<int>>();
            foreach (List<MBR> list in nearestMBR.Values)
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

            return result;*/
        }
    }
}
