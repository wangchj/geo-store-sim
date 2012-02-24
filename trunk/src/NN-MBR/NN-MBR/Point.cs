using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NN_MBR
{
    public class Point
    {
        private float xPos;
        private float yPos;

        public void setX(float newX)
        {
            xPos = newX;
        }
        public void setY(float newY)
        {
            yPos = newY;
        }

        public float getY()
        {
            return yPos;
        }

        public float getX()
        {
            return xPos;
        }
        public float getDistance(Point distancePoint)
        {
            float returnValue = 1000000000;
            double tempX = 0;
            double tempY = 0;
            tempX = (double)(xPos - distancePoint.getX());
            tempY = (double)(yPos - distancePoint.getY());
            returnValue = (float)Math.Pow(Math.Pow(tempX,2)+Math.Pow(tempY,2),.5);
            return returnValue;
        }
    }
}
