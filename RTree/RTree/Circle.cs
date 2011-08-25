using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTree
{
    public class Circle
    {
        private Point center;
        private double radius;

        public Circle(float x, float y, float z, float radius)
            :this(new Point(x, y, z), radius)
        {
        }

        public Circle(Point center, float radius)
        {
            if (center == null) throw new ArgumentNullException();

            this.center = center;
            this.radius = radius;
        }

        public Point Center
        {
            get { return this.center; }
        }

        public double Radius
        {
            get { return this.radius; }
        }

        public bool Contains(Point p)
        {
            //In practice, squaring is often much cheaper than taking the
            //square root and since we're only interested in an ordering,
            //we can of course forego taking the square root:
            double squaredDist = Math.Pow(center.X - p.X, 2) + Math.Pow(center.Y - p.Y, 2);
            return squaredDist <= Math.Pow(radius, 2);
        }

        /// <summary>
        /// Checks if the two-dimensional rectangle is completely inside
        /// the two-dimensional circle.
        /// </summary>
        /// <remarks>
        /// A rectangle is inside a circle if all 4 corners are inside the
        /// circle.
        /// </remarks>
        /// <param name="r">The rectangle.</param>
        /// <returns>True if the rectangle is inside the circle; false
        /// otherwise.</returns>
        public bool Contains(Rectangle r)
        {
            return Contains(new Point(r.min[0], r.min[1])) &&
                Contains(new Point(r.min[0], r.max[1])) &&
                Contains(new Point(r.max[0], r.min[1])) &&
                Contains(new Point(r.max[0], r.max[1]));
        }
    }
}
