using Fantasy.Logic.Engine.Hitboxes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Physics;
using System.Text;

namespace Fantasy.Logic.Engine.Graphics.Lighting
{
    public class Lightbox : RectangleSet
    {
        /// <summary>
        /// Two-dimensional array that determines how light passes in and out of this Lightbox.
        /// The first index of outer array is for light pass in logic, The second index of the array is for light pass out logic.
        /// The first, second, third, and forth indexes of the inner array determines the logic for the top, right, bottom, and left sides of the Lightbox.
        /// There is a indepedent array containing instuctions for lightphysics 
        /// </summary>
        public bool[,] lightPhysicsAbove = new bool[2, 4];
        public bool[,] lightPhysicsRight = new bool[2, 4];
        public bool[,] lightPhysicsBelow = new bool[2, 4];
        public bool[,] lightPhysicsLeft = new bool[2, 4];

        /// <summary>
        /// Creates a Lightbox with the provided parameters.
        /// </summary>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="boundings">The rectangles in this RectangleSet. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="lightPhysics">Two-dimensional array that determines how light passes in and out of this Lightbox.</param>
        public Lightbox(Point position, Rectangle[] boundings, bool[,] lightPhysicsAbove, bool[,] lightPhysicsRight, bool[,] lightPhysicsBelow, bool[,] lightPhysicsLeft)
        {
            this.position = position;
            this.boundings = boundings;
            this.lightPhysicsAbove = lightPhysicsAbove;
            this.lightPhysicsRight = lightPhysicsRight;
            this.lightPhysicsBelow = lightPhysicsBelow;
            this.lightPhysicsLeft = lightPhysicsLeft;
        }

        /// <summary>
        /// Returns a array containing the line segments which from the rectangle boundings that result in a light collision based on this Lightboxes light pass in/out logic.
        /// </summary>
        /// <param name="center">The point from which light is originating from.</param>
        /// <returns>Array containing lines (lines are tuple in the form <Point, Point>, with each point describing a end of the line segment.) that result in a light collision.</returns>
        public Tuple<Point, Point>[] GetLightCollisionLines(Point center)
        {
            List<Tuple<Point, Point>> lightLineCollisions = new List<Tuple<Point, Point>>();

            foreach (Rectangle rec in GetAbsoluteBoundings())
            {
                Tuple<Point, Point> top = new Tuple<Point, Point>(new Point(rec.X, rec.Y), new Point(rec.X + rec.Width - 1, rec.Y));
                Tuple<Point, Point> right = new Tuple<Point, Point>(new Point(rec.X + rec.Width - 1, rec.Y), new Point(rec.X + rec.Width - 1, rec.Y - rec.Height + 1));
                Tuple<Point, Point> bottom = new Tuple<Point, Point>(new Point(rec.X, rec.Y - rec.Height + 1), new Point(rec.X + rec.Width - 1, rec.Y - rec.Height + 1));
                Tuple<Point, Point> left = new Tuple<Point, Point>(new Point(rec.X, rec.Y), new Point(rec.X, rec.Y - rec.Height + 1));

                if (Util.PointInsideRectangle(center, rec)) //center is inside of rec.
                {
                    if (Lines.PointOnLinearAxisLineSegment(top, center)) //center is on rec top.
                    {
                        bool[,] lightPhysics = lightPhysicsBelow;
                        if (lightPhysics[0, 0]) //checks if rec top allows for light pass in.
                        {
                            if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                            if (!lightPhysics[1, 2]) //check if rec bottoms does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        else if (!lightPhysics[1, 0]) //check if rec top allows for light pass out.
                        {
                            lightLineCollisions.Add(top);
                        }
                    }
                    if (Lines.PointOnLinearAxisLineSegment(right, center)) //center is on rec right.
                    {
                        bool[,] lightPhysics = lightPhysicsLeft;
                        if (lightPhysics[0, 1]) //checks if rec right allows for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 2]) //check if rec bottoms does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        else if (!lightPhysics[1, 0]) //check if rec right allows for light pass out.
                        {
                            lightLineCollisions.Add(right);
                        }
                    }
                    if (Lines.PointOnLinearAxisLineSegment(bottom, center)) //center is on rec bottom.
                    {
                        bool[,] lightPhysics = lightPhysicsAbove;
                        if (lightPhysics[0, 2]) //checks if rec bottom allows for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        else if (!lightPhysics[1, 0]) //check if rec right allows for light pass out.
                        {
                            lightLineCollisions.Add(bottom);
                        }
                    }
                    if (Lines.PointOnLinearAxisLineSegment(left, center)) //center is on rec left.
                    {
                        bool[,] lightPhysics = lightPhysicsRight;
                        if (lightPhysics[0, 3]) //checks if rec left allows for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec bottom does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                        }
                        else if (!lightPhysics[1, 0]) //check if rec right allows for light pass out.
                        {
                            lightLineCollisions.Add(left);
                        }
                    }
                    if (!Lines.PointOnLinearAxisLineSegment(top, center) && !Lines.PointOnLinearAxisLineSegment(right, center) &&
                        !Lines.PointOnLinearAxisLineSegment(bottom, center) && !Lines.PointOnLinearAxisLineSegment(left, center)) //center is not on rec lines.
                    {
                        bool[,] lightPhysics = lightPhysicsBelow;
                        if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                        {
                            lightLineCollisions.Add(top);
                        }
                        if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                        {
                            lightLineCollisions.Add(right);
                        }
                        if (!lightPhysics[1, 2]) //check if rec bottom does not allow for light pass out.
                        {
                            lightLineCollisions.Add(left);
                        }
                        if (!lightPhysics[1, 3]) //check if rec left does not allow for light pass out.
                        {
                            lightLineCollisions.Add(bottom);
                        }
                    }
                }
                else if (rec.X <= center.X && rec.X + rec.Width >= center.X) //center is vertically aligned with rec side.
                {
                    if (rec.Y < center.Y) //rec is below center.
                    {
                        bool[,] lightPhysics = lightPhysicsBelow;
                        if (lightPhysics[0, 0]) //checks if rec top allows for light pass in.
                        {
                            if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                            if (!lightPhysics[1, 2]) //check if rec bottoms does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        else
                        {
                            lightLineCollisions.Add(top);
                        }
                    }
                    else //rec is above center.
                    {
                        bool[,] lightPhysics = lightPhysicsAbove;
                        if (lightPhysics[0, 2]) //checks if rec bottom allows for light pass in.
                        {
                            if (!lightPhysics[1, 1]) //check if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                            if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 3]) //checks if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        else
                        {
                            lightLineCollisions.Add(bottom);
                        }
                    }
                }
                else if (rec.Y >= center.Y && rec.Y - rec.Height <= center.Y) //center is horizontally aligned with rec side.
                {
                    if (rec.X > center.X) //rec is right of center.
                    {
                        bool[,] lightPhysics = lightPhysicsRight;
                        if (lightPhysics[0, 3]) //checks if rec left allows for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //check if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 2]) //check if rec bottoms does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 1]) //checks if rec right does not allow for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                        }
                        else
                        {
                            lightLineCollisions.Add(left);
                        }
                    }
                    else //rec is left of center.
                    {
                        bool[,] lightPhysics = lightPhysicsLeft;
                        if (lightPhysics[0, 1]) //checks if rec right allows for light pass in.
                        {
                            if (!lightPhysics[1, 3]) //check if rec left does not allow for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                            if (!lightPhysics[1, 2]) //check if rec bottoms does not allow for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 0]) //checks if rec top does not allow for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                        }
                        else
                        {
                            lightLineCollisions.Add(right);
                        }
                    }
                }
                else //no side is aligned with center.
                {
                    if (top.Item1.X < center.X && top.Item1.Y > center.Y) //rec is to the top left of the center.
                    {
                        bool[,] lightPhysics = Util.AddBoolArrays(lightPhysicsAbove, lightPhysicsLeft);
                        if (lightPhysics[0, 1] || lightPhysics[0, 2]) //checks if right or bottom allow for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //checks if rec top allows for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 3]) //check if rec left allows for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        if (!lightPhysics[0, 1])
                        {
                            lightLineCollisions.Add(right);
                        }
                        if (!lightPhysics[0, 2])
                        {
                            lightLineCollisions.Add(bottom);
                        }
                    }
                    else if (top.Item1.X > center.X && top.Item1.Y > center.Y) //rec is to the top right of the center.
                    {
                        bool[,] lightPhysics = Util.AddBoolArrays(lightPhysicsAbove, lightPhysicsRight);
                        if (lightPhysics[0, 3] || lightPhysics[0, 2]) //checks if left or bottom allow for light pass in.
                        {
                            if (!lightPhysics[1, 0]) //check if rec top allows for light pass out.
                            {
                                lightLineCollisions.Add(top);
                            }
                            if (!lightPhysics[1, 1]) //check if rec right allows for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                        }
                        if (!lightPhysics[0, 3])
                        {
                            lightLineCollisions.Add(left);
                        }
                        if (!lightPhysics[0, 2])
                        {
                            lightLineCollisions.Add(bottom);
                        }
                    }
                    else if (top.Item1.X < center.X && top.Item1.Y < center.Y) //rec is to the bottom left of the center.
                    {
                        bool[,] lightPhysics = Util.AddBoolArrays(lightPhysicsBelow, lightPhysicsLeft);
                        if (lightPhysics[0, 1] || lightPhysics[0, 0]) //checks if right or top allow for light pass in.
                        {
                            if (!lightPhysics[1, 3]) //checks if rec bottoms allows for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 3]) //check if rec left allows for light pass out.
                            {
                                lightLineCollisions.Add(left);
                            }
                        }
                        if (!lightPhysics[0, 1])
                        {
                            lightLineCollisions.Add(right);
                        }
                        if (!lightPhysics[0, 0])
                        {
                            lightLineCollisions.Add(top);
                        }
                    }
                    else if (top.Item1.X > center.X && top.Item1.Y < center.Y) //rec is to the bottom right of the center.
                    {
                        bool[,] lightPhysics = Util.AddBoolArrays(lightPhysicsBelow, lightPhysicsRight);
                        if (lightPhysics[0, 3] || lightPhysics[0, 0]) //checks if left or top allow for light pass in.
                        {
                            if (!lightPhysics[1, 3]) //checks if rec bottoms allows for light pass out.
                            {
                                lightLineCollisions.Add(bottom);
                            }
                            if (!lightPhysics[1, 1]) //check if rec right allows for light pass out.
                            {
                                lightLineCollisions.Add(right);
                            }
                        }
                        if (!lightPhysics[0, 3])
                        {
                            lightLineCollisions.Add(left);
                        }
                        if (!lightPhysics[0, 0])
                        {
                            lightLineCollisions.Add(top);
                        }
                    }
                }
            }

            return lightLineCollisions.ToArray();
        }

        /// <summary>
        /// Creates a string describing this Lightbox.
        /// </summary>
        /// <returns>A string describing this Lightbox.</returns>
        new public string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach (Rectangle bounding in boundings)
            {
                text.Append(new Rectangle(bounding.X + position.X, bounding.Y + position.Y, bounding.Width, bounding.Height) + Environment.NewLine);
            }
            text.Append("__Light In__" + Environment.NewLine);
            text.Append("Top:" + lightPhysicsAbove[0, 0].ToString() + " Right:" + lightPhysicsAbove[0, 1].ToString() + " Bottom:" + lightPhysicsAbove[0, 2].ToString() + " Left:" + lightPhysicsAbove[0, 3].ToString() + Environment.NewLine);

            text.Append("__Light Out__" + Environment.NewLine);
            text.Append("Top:" + lightPhysicsAbove[1, 0].ToString() + " Right:" + lightPhysicsAbove[1, 1].ToString() + " Bottom:" + lightPhysicsAbove[1, 2].ToString() + " Left:" + lightPhysicsAbove[1, 3].ToString());

            return text.ToString();
        }
    }
}
