using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RTree;

namespace QueryEvaluator
{
    public class MbrQueryEvaluator:QueryEvaluator
    {

        private List<Group> groups;

        /// <summary>
        /// Construct an instance with data and RTree max node.
        /// </summary>
        /// <param name="dataPath">The data of the RTree. This file
        /// must be in the specified format.</param>
        /// <param name="maxNode">The maximum number of entries in a
        /// node of the RTree.</param>
        public MbrQueryEvaluator(string dataPath, int maxNode)
        {
            if (dataPath == null) throw new ArgumentNullException();
            LoadData(dataPath, maxNode);
        }

        /// <summary>
        /// Loads a list of groups
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="cap"></param>
        private void LoadData(string dataPath, int cap)
        {
            List<Rectangle> entries = LoadDataEntries(dataPath);
            groups = LoadGroups(entries, cap);
        }

        /// <summary>
        /// Loads point data from a file.
        /// </summary>
        /// <remarks>
        /// Though this method returns a list of Rectangles, the rectangles
        /// are points. That is x1 and x2 are the same, and y1 and y2 are the
        /// same.
        /// </remarks>
        /// <param name="dataPath">The data file</param>
        /// <returns></returns>
        private List<Rectangle> LoadDataEntries(string dataPath)
        {
            string rex = @"\w+ (\S+) (\S+)";
            StreamReader streamReader = null;
            List<Rectangle> result = new List<Rectangle>();

            try
            {
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
                        result.Add(new Rectangle(lat, lon, lat, lon, 0, 0));
                    }
                }
                return result;
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }

        /// <summary>
        /// Builds groups based on a list of entries. The input list of
        /// entries will be modified.
        /// </summary>
        /// <param name="entries">List of entries</param>
        /// <param name="cap">Capacity for each group</param>
        /// <returns></returns>
        private List<Group> LoadGroups(List<Rectangle> entries, int cap)
        {
            List<Group> result = new List<Group>();

            while (entries.Count != 0)
            {
                Rectangle head = entries[0];
                entries.RemoveAt(0);
                Group group = new Group(cap, head);

                for (int i = 1; i < cap && entries.Count != 0; i++)
                {
                    float minDist = -1;
                    int minIndex = 0;

                    for (int j = 0; j < entries.Count; j++)
                    {
                        float dist = head.distance(entries[j]);
                        if (minDist == -1 || dist < minDist)
                        {
                            minDist = dist;
                            minIndex = j;
                        }
                    }

                    Rectangle minDistRect = entries[minIndex];
                    entries.RemoveAt(minIndex);
                    group.Add(minDistRect);
                }

                result.Add(group);
            }

            return result;
        }

        public override int RangeQuery(Point center, float range)
        {
            return 0;
            //return RangeQuery(center, range, rtree.RootNode);
        }

        private int RangeQuery(Point center, float range, Node<int> node)
        {
            return 0;
            //Circle coverRange = new Circle(center, range);
            //int resultCount = 0;

            //if (Intersect(node.getMBR(), new Circle(center, range)))
            //{
            //    if (node.isLeaf())
            //    {
            //        resultCount = node.getEntryCount();
            //    }
            //    else
            //    {
            //        for (int i = 0; i < node.getEntryCount(); i++)
            //        {
            //            int childId = node.getId(i);
            //            Node<int> childNode = rtree.NodeMap[childId];
            //            resultCount += RangeQuery(center, range, childNode);
            //        }
            //    }
            //}

            //return resultCount;
        }

        public override int KnnQuery(Point center, int k)
        {
            throw new NotImplementedException();
        }

        class Group
        {
            Rectangle mbr;
            int capacity;
            List<Rectangle> entries;

            public Group(int cap)
                :this(cap, null)
            {}

            public Group(int cap, Rectangle head)
            {
                if (cap < 1)
                    throw new ArgumentOutOfRangeException();
                
                capacity = cap;
                entries = new List<Rectangle>();

                if (head == null)
                    return;

                entries.Add(head);
                mbr = head;
            }

            public bool Add(Rectangle entry)
            {
                if (entries.Count == capacity)
                    return false;

                entries.Add(entry);

                if (mbr == null)
                    mbr = entry;
                else
                    mbr.add(entry);

                return true;
            }
        }
    }
}
