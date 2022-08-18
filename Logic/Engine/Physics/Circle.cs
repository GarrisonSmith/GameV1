using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Physics
{
    public class Circle
    {

        private int radius;

        private Point center;

        public Circle(int radius, Point center)
        {
            this.radius = radius;
            this.center = center;
        }

        public bool PointInsideCircle(Point point)
        {
            if (Util.DistanceBetweenPoints(point, center) >= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point[] GetPointsInsideCircle()
        {
            List<Point> foo = new List<Point>();

            Point cur = new Point(center.X, center.Y + radius);
            while (cur.Y != center.Y && cur.X != center.X + radius)
            {
                foo.Add(cur);
                if (Util.DistanceBetweenPoints(center, new Point(cur.X + 1, cur.Y)) <= Util.DistanceBetweenPoints(center, new Point(cur.X + 1, cur.Y - 1)) || 
                    Util.DistanceBetweenPoints(center, new Point(cur.X + 1, cur.Y)) - radius <= radius)
                {
                    cur = new Point(cur.X + 1, cur.Y);
                }
                else
                {
                    cur = new Point(cur.X + 1, cur.Y - 1);
                }
            }

            return foo.ToArray();
        }
    }
}
