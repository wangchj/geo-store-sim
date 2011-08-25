using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NN_MBR
{
    public class MBR_Holder
    {
        // list that holds the MBR's
        private List<MBR> MBRList;
        // list that holds the points read in from file
        private List<Point> pointList;
        public MBR_Holder()
        {
            MBRList = new List<MBR>();
            pointList = new List<Point>();
        }

        public void readFromFile(String filename, int BF)
        {

            string rex = @"\w+ (\S+) (\S+)";
            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(filename);
                for (string line = streamReader.ReadLine();
                    line != null;
                    line = streamReader.ReadLine())
                {
                    Match match = Regex.Match(line, rex);
                    if (match.Success)
                    {
                        float lat = float.Parse(match.Groups[2].Value);
                        float lon = float.Parse(match.Groups[1].Value);
                        Point newPoint = new Point();
                        // set X and Y value in point class
                        newPoint.setX(lat);
                        newPoint.setY(lon);
                        // add to list of points
                        pointList.Add(newPoint);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to load data: " + ex.Message);

            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }

            formMBRs(BF);

            //string[] lines = System.IO.File.ReadAllLines(filename);
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    // create variables to hold temporary X and Y values
            //    string tempLine = lines[i];
            //    string tempX;
            //    string tempY;
            //    int space = 0;
            //    int firstSpace = 0;
            //    while (tempLine[firstSpace] != ' ')
            //    {
            //        firstSpace++;
            //    }
            //    firstSpace++;
            //    space = firstSpace;
            //    // parse out the space to have X and Y
            //    while (tempLine[space] != ' ')
            //    {
            //        space++;
            //    }
            //    tempY = tempLine.Substring(firstSpace, space);
            //    tempX = tempLine.Substring(space + 1, tempLine.Length);
            //    Console.WriteLine("X: " + tempX + " Y: " + tempY + ".");
            //    Point newPoint = new Point();
            //    // set X and Y value in point class
            //    newPoint.setX(float.Parse(tempX));
            //    newPoint.setY(float.Parse(tempY));
            //    // add to list of points
            //    pointList.Add(newPoint);
            //}
        }
        public void formMBRs(int BF)
        {
            if (BF > pointList.Count)
            {
                Console.WriteLine("Error. Less Data points than given size for MBRs.");
                Console.ReadLine();
                Environment.Exit(30000);
            }
            // may not be nessesary, but improves speed, maybe.
            // sort MBRs
            // pointList.Sort();

            while ((int)(pointList.Count / BF) > 0)
            {

                List<Point> newPointList = new List<Point>();
                int[] indexList = new int[BF];
                indexList[0] = 0;
                int highest = 0;
                for (int k = 1; k < BF; k++)
                {
                    if (pointList[highest].getDistance(pointList[0]) < pointList[k].getDistance(pointList[0]))
                    {
                        highest = k;
                    }
                    indexList[k] = k;

                }
                for (int j = BF; j < pointList.Count(); j++)
                {
                    if (pointList[highest].getDistance(pointList[0]) > pointList[j].getDistance(pointList[0]))
                    {
                        indexList[highest] = j;
                        for (int k = 1; k < BF; k++)
                        {
                            if (pointList[highest].getDistance(pointList[0]) > pointList[k].getDistance(pointList[0]))
                            {
                                highest = k;
                            }


                        }
                    }

                }
                MBR newMBR = new MBR();
                for (int k = 0; k < BF; k++)
                {
                    for (int l = k; l < BF; l++)
                    {
                        if (indexList[l] > indexList[k])
                        {
                            indexList[l]--;
                        }
                    }
                    newMBR.addPoint(pointList[indexList[k]]);
                    pointList.RemoveAt(indexList[k]);
                }
                newMBR.calculateBounds();
                MBRList.Add(newMBR);

            }
            MBR MBR2 = new MBR();
            for (int i = 0; i < pointList.Count; i++)
            {
                MBR2.addPoint(pointList[i]);
            }
            MBRList.Add(MBR2);
        }

        public List<MBR> MBR
        {
            get { return MBRList; }
        }
    }
}