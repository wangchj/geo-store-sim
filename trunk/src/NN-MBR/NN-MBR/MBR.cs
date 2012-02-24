using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NN_MBR
{
    public class MBR
    {
        private float lowX;
        private float upperX;
        private float lowY;
        private float upperY;
        List<Point> points;

        public MBR()
        {
            points = new List<Point>();

        }
        public void addPoint(Point newPoint)
        {
            points.Add(newPoint);
        }
        public void calculateBounds()
        {
             float lowestX, lowestY, highestX, highestY;
             lowestX = points[0].getX();
             lowestY = points[0].getY();
             highestX = points[0].getX();
             highestY = points[0].getY();
             for (int i = 1; i < points.Count; i++)
             {
                 if (lowestX > points[i].getX())
                 {
                     lowestX = points[i].getX();
                 }
                 if (highestX < points[i].getX())
                 {
                     lowestX = points[i].getX();
                 }
                 if (lowestY > points[i].getY())
                 {
                     lowestY = points[i].getY();
                 }
                 if (highestY < points[i].getY())
                 {
                     highestY = points[i].getY();
                 }
             }
             lowX = lowestX;
             upperX = highestX;
             lowY = lowestY;
             upperY = highestY;
        }
        public List<float> getBounts()
        {
            List<float> returnList = new List<float>();
            returnList.Add(lowX);
            returnList.Add(lowY);
            returnList.Add(upperX);
            returnList.Add(upperY);
            return returnList;
        }

        public int getNumPoints()
        {
            return points.Count;
        }

        public List<Point> Points
        {
            get { return points; }
        }
    }
}
