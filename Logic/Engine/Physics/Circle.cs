using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Logic.Engine.Physics
{
    /// <summary>
    /// Object class used to describe a circle.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// The radius of the circle.
        /// </summary>
        private int radius;
        /// <summary>
        /// The center of the circle.
        /// </summary>
        private Point center;

        public Texture2D texture;

        /// <summary>
        /// Creates a circle with the provided parameters.
        /// </summary>
        /// <param name="radius">The radius the circle will use.</param>
        /// <param name="center">The center the circle will use.</param>
        public Circle(int radius, Point center)
        {
            this.radius = radius;
            this.center = center;
            GetCircleTexture();
        }
        
        /// <summary>
        /// Determines if the provided point is inside of this circle.
        /// </summary>
        /// <param name="point">The point to be investigated.</param>
        /// <returns>True if the point is inside of this circle, False if not.</returns>
        public bool PointInsideCircle(Point point)
        {
            return (Util.DistanceBetweenPoints(point, center) <= radius);
        }
        /// <summary>
        /// Creates a array containing all points on the circumference of this circle.
        /// </summary>
        /// <returns>A array containing all the points on the circumference of this circle.</returns>
        public Point[] GetCircumferencePoints()
        {
            List<Point> foo = new List<Point>();

            Point trCur = new Point(center.X, center.Y + radius); //top right
            Point rtCur = new Point(center.X + radius, center.Y); //right top
            Point brCur = new Point(center.X, center.Y - radius); //bottom right
            Point rbCur = new Point(center.X + radius, center.Y); //right bottom
            while (trCur.Y >= center.Y + Math.Floor(radius * Math.Cos(Math.PI / 4)))
            {
                foo.Add(trCur); foo.Add(new Point(2 * center.X - trCur.X, trCur.Y)); //adds current top right point and mirrors it across circle center.
                foo.Add(rtCur); foo.Add(new Point(2 * center.X - rtCur.X, rtCur.Y)); //adds current right top point and mirrors it across circle center.
                foo.Add(brCur); foo.Add(new Point(2 * center.X - brCur.X, brCur.Y)); //adds current bottom right point and mirrors it across circle center.
                foo.Add(rbCur); foo.Add(new Point(2 * center.X - rbCur.X, rbCur.Y)); //adds current right bottom point and mirrors it across circle center.

                if (Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y)) - radius) <= Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y - 1)) - radius))
                {
                    trCur = new Point(trCur.X + 1, trCur.Y);
                    rtCur = new Point(rtCur.X, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y);
                    rbCur = new Point(rbCur.X, rbCur.Y - 1);
                }
                else
                {
                    trCur = new Point(trCur.X + 1, trCur.Y - 1);
                    rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y + 1);
                    rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                }
            }

            return foo.Distinct().ToArray();
        }
        /// <summary>
        /// Creates a array containing all the points inside of this circle.
        /// </summary>
        /// <returns>A array containing all the points inside of the circle.</returns>
        public Point[] GetAreaPoints()
        {
            List<Point> foo = new List<Point>();

            Point trCur = new Point(center.X, center.Y + radius); //top right
            Point rtCur = new Point(center.X + radius, center.Y); //right top
            Point brCur = new Point(center.X, center.Y - radius); //bottom right
            Point rbCur = new Point(center.X + radius, center.Y); //right bottom
            while (trCur.Y >= center.Y + Math.Floor(radius * Math.Cos(Math.PI / 4)))
            {
                foo.Add(trCur); int trMirrorX = 2 * center.X - trCur.X; //adds current top right point and mirrors it across circle center, then fills everything between them.
                if (trCur.Y == center.Y + radius)
                {
                    foo.Add(new Point(trMirrorX, trCur.Y));
                }
                else
                {
                    for (int i = trMirrorX; i < trCur.X; i++)
                    {
                        foo.Add(new Point(i, trCur.Y));
                    }
                }

                foo.Add(rtCur); //adds current right top point and mirrors it across circle center, then fills everything between them.
                for (int i = 2 * center.X - rtCur.X; i < rtCur.X; i++)
                {
                    foo.Add(new Point(i, rtCur.Y));
                }

                foo.Add(brCur); int brMirrorX = 2 * center.X - brCur.X; //adds current bottom right point and mirrors it across circle center, then fills everything between them.
                if (brCur.Y == center.Y - radius)
                {
                    foo.Add(new Point(brMirrorX, brCur.Y));
                }
                else
                {
                    for (int i = brMirrorX; i < brCur.X; i++)
                    {
                        foo.Add(new Point(i, brCur.Y));
                    }
                }

                foo.Add(rbCur); foo.Add(new Point(2 * center.X - rbCur.X, rbCur.Y)); //adds current right bottom point and mirrors it across circle center, then fills everything between them.
                for (int i = 2 * center.X - rbCur.X; i < rbCur.X; i++)
                {
                    foo.Add(new Point(i, rbCur.Y));
                }


                if (Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y)) - radius) <= Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y - 1)) - radius))
                {
                    trCur = new Point(trCur.X + 1, trCur.Y);
                    rtCur = new Point(rtCur.X, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y);
                    rbCur = new Point(rbCur.X, rbCur.Y - 1);
                }
                else
                {
                    trCur = new Point(trCur.X + 1, trCur.Y - 1);
                    rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y + 1);
                    rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                }
            }

            return foo.Distinct().ToArray();
        }

        public Texture2D GetCircleTexture()
        {
            if (texture != null)
            {
                return texture;
            }

            Color[] foo = new Color[(radius * 2 + 1) * (radius * 2 + 1)];
            foo = foo.Select(i => Color.Transparent).ToArray(); //Fills the array with the transparent color.

            foreach (Point p in GetAreaPoints())
            {
                int index = ((p.X - center.X + radius) + (((p.Y - center.Y + radius) * (2 * radius + 1))));
                foo[index] = Color.White;
            }

            Texture2D bar = new Texture2D(Global._graphics.GraphicsDevice, 2 * radius + 1, 2 * radius + 1);
            bar.SetData(foo);
            texture = bar;
            return bar;
        }
    }
}
