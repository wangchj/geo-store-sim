using System;

//   Point.java
//   Java Spatial Index Library
//   Copyright (C) 2002 Infomatiq Limited
//  
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA

// Ported to C# By Dror Gluska, April 9th, 2009
namespace RTree
{

    /// <summary>
    /// Currently hardcoded to 3 dimensions, but could be extended.
    /// author  aled@sourceforge.net
    /// version 1.0b2p1
    /// </summary>
    public class Point
    {
      
      
      
        /// <summary>
        /// Number of dimensions in a point. In theory this
        /// could be exended to three or more dimensions.
        /// </summary>
        private const int DIMENSIONS = 3;

        /// <summary>
        /// The (x, y) coordinates of the point.
        /// </summary>
        internal float[] coordinates;

        /// <summary>
        /// Constructor with z = 0.
        /// </summary>
        /// <param name="x">The x coordinate of the point</param>
        /// <param name="y">The y coordinate of the point</param>
        public Point(float x, float y)
            : this(x, y, 0) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The x coordinate of the point</param>
        /// <param name="y">The y coordinate of the point</param>
        /// <param name="z">The z coordinate of the point</param>
        public Point(float x, float y,float z)
        {
            coordinates = new float[DIMENSIONS];
            coordinates[0] = x;
            coordinates[1] = y;
            coordinates[2] = z;
        }

        public float X
        {
            get { return coordinates[0]; }
            set { coordinates[0] = value; }
        }

        public float Y
        {
            get { return coordinates[1]; }
            set { coordinates[1] = value; }
        }

        public float Z
        {
            get { return coordinates[2]; }
            set { coordinates[2] = value; }
        }

        public float distance(Point p)
        {
            return distance(this, p);
        }

        public static float distance(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}