using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Graphics.tilemap;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Physics;
using Fantasy.Logic.Engine.Hitboxes;
using System.Collections.Generic;
using System;

namespace Fantasy.Logic.Engine.Graphics.Lighting
{
    public class LightSource
    {
        public static Texture2D CastCircleOnLayer(Circle foo, int layer)
        {
            List<Hitbox> collisions = new List<Hitbox>();

            Hitbox[] bar = Global._currentScene._spriteManager.GetLayerHitboxes(layer);
            foreach (Hitbox box in bar)
            {
                if (box.Collision(foo))
                {
                    collisions.Add(box);
                }
            }
            foo.SetTexture(CreateLightShadowTexture(foo, collisions.ToArray()));
            return foo.GetTexture();
        }
        private static Texture2D CreateLightShadowTexture(Circle foo, Hitbox[] bar)
        {
            Texture2D texture = new Texture2D(Global._graphics.GraphicsDevice, foo.radius * 2 + 1, foo.radius * 2 + 1);
            texture.SetData(foo.GetTextureData());

            foreach (Hitbox box in bar)
            {
                bool[,] lightPhysics = box.lightPhysics;

                foreach (Rectangle rec in box.geometry.GetAbsoluteBoundings())
                {
                    if (Util.PointInsideRectangle(foo.center, rec)) //circle center is inside of rectangle.
                    {

                    }
                    else if (rec.X <= foo.center.X && rec.X + rec.Width >= foo.center.X) //circle center is vertically aligned with rectangle side.
                    {
                        if (rec.Y < foo.center.Y) //rectangle is below circle center.
                        {
                            Point topLeft = new Point(rec.X, rec.Y); Point topRight = new Point(rec.X + rec.Width - 1, rec.Y);
                            texture.SetData(ReplaceSlice(foo, topLeft, topRight, Color.Transparent));
                        }
                        else //rectanlge is above circle center.
                        {
                            Point bottomLeft = new Point(rec.X, rec.Y - rec.Height + 1); Point bottomRight = new Point(rec.X + rec.Width - 1, rec.Y - rec.Height + 1);
                            texture.SetData(ReplaceSlice(foo, bottomLeft, bottomRight, Color.Transparent));
                        }
                    }
                    else if (rec.Y >= foo.center.Y && rec.Y - rec.Height <= foo.center.Y) //circle center is horizontally aligned with rectangle side.
                    {
                        if (rec.X > foo.center.X) //rectangle is right of circle center.
                        {
                            Point top = new Point(rec.X, rec.Y); Point bottom = new Point(rec.X, rec.Y - rec.Height + 1);
                            texture.SetData(ReplaceSlice(foo, top, bottom, Color.Transparent));
                        }
                        else //rectanlge is left of circle center.
                        {
                            Point top = new Point(rec.X + rec.Width - 1, rec.Y); Point bottom = new Point(rec.X + rec.Width - 1, rec.Y - rec.Height + 1);
                            texture.SetData(ReplaceSlice(foo, top, bottom, Color.Transparent));
                        }
                    }
                    else //no side is aligned with circle center.
                    {

                    }
                    foo.SetTexture(texture);
                }
            }

            return texture;
        }
        /// <summary>
        /// Replaces a slice of the circle foos texture with the provided replacement color. pointOne and pointTwo act as clamp points for casting the replacement area.
        /// </summary>
        /// <remarks>pontOne and pointTwo must be in either a vertical or horizontal line.</remarks>
        /// <param name="foo">The circle to be used.</param>
        /// <param name="pointOne">The first clamp point.</param>
        /// <param name="pointTwo">The second clamp point.</param>
        /// <param name="replacementColor">The color that will replace all points inside of the slice.</param>
        /// <returns>A color array describing foos texture with the slice replaced with the replacementColor.</returns>
        public static Color[] ReplaceSlice(Circle foo, Point pointOne, Point pointTwo, Color replacementColor)
        {
            if (pointOne.X != pointTwo.X && pointOne.Y != pointTwo.Y)
            {
                return foo.GetTextureData();
            }
            
            int textureWidth = foo.GetTexture().Width;
            Tuple<double, double> lineOne = Util.LineFormula(pointOne, foo.center);
            Tuple<double, double> lineTwo = Util.LineFormula(pointTwo, foo.center);

            //determines which direction pointOne and pointTwo need to move towards.
            sbyte directionOne, directionTwo;
            if (pointOne.X >= foo.center.X)
            {
                directionOne = 1;
            }
            else
            {
                directionOne = -1;
            }
            if (pointTwo.X >= foo.center.X)
            {
                directionTwo = 1;
            }
            else
            {
                directionTwo = -1;
            }

            //generates the points on the lines from pointOne and pointTwo.
            Point topLeft = new Point(foo.center.X - foo.radius, foo.center.Y + foo.radius), progOne = foo.center, progTwo = foo.center, progOneLast = foo.center, progTwoLast = foo.center;
            int pIndex;
            bool doneOne = false, doneTwo = false;
            Color[] curData = foo.GetTextureData();
            List<Point> linePointsOne = new List<Point>(), linePointsTwo = new List<Point>();
            linePointsOne.Add(progOne); linePointsTwo.Add(progTwo);
            while (!doneOne || !doneTwo)
            {
                //Generates the points of a ray from foo's center toward pointOne that are inside of foo and beyond pointOne.
                progOne = new Point(progOne.X + directionOne, Util.XOnLine(lineOne, progOne.X + directionOne));
                if (directionOne != directionTwo)
                {
                    for (int y = Math.Min(progOne.Y, progOneLast.Y); y < Math.Max(progOne.Y, progOneLast.Y); y++)
                    {
                        linePointsOne.Add(new Point(progOne.X, y));
                    }
                }
                linePointsOne.Add(progOne);
                progOneLast = progOne;
                doneOne = ((directionOne != directionTwo && Math.Abs(progOne.Y - foo.center.Y) > foo.radius) || (directionOne == directionTwo && Math.Abs(progOne.X - foo.center.X) > foo.radius));

                //Generates the points of a ray from foo's center toward pointTwo that are inside of foo and beyond pointTwo.
                progTwo = new Point(progTwo.X + directionTwo, Util.XOnLine(lineTwo, progTwo.X + directionTwo));
                if (directionOne != directionTwo)
                {
                    for (int y = Math.Min(progTwo.Y, progTwoLast.Y); y < Math.Max(progTwo.Y, progTwoLast.Y); y++)
                    {
                        linePointsTwo.Add(new Point(progTwo.X, y));
                    }
                }
                linePointsTwo.Add(progTwo);
                progTwoLast = progTwo;
                doneTwo = ((directionOne != directionTwo && Math.Abs(progTwo.Y - foo.center.Y) > foo.radius) || (directionOne == directionTwo && Math.Abs(progTwo.X - foo.center.X) > foo.radius)); ;
            }

            //Gets the points between progOne and progTwo and replaces their colors.
            if (directionOne == directionTwo) //lines opening horizontally > or < from center.
            {
                if (pointOne.X > foo.center.X && pointTwo.X > foo.center.X) // < from center
                {
                    for (int x = Math.Min(pointOne.X, pointTwo.X); x <= foo.center.X + foo.radius; x++)
                    {
                        int yOne = linePointsOne.Find(p => p.X == x).Y;
                        int yTwo = linePointsTwo.Find(p => p.X == x).Y;
                        for (int y = Math.Min(yOne, yTwo); y <= Math.Max(yOne, yTwo); y++)
                        {
                            Point p = new Point(x, y);
                            pIndex = (p.X - topLeft.X) + textureWidth * (topLeft.Y - p.Y);
                            if (0 <= pIndex && pIndex < curData.Length && foo.PointInsideCircle(p))
                            {
                                curData[pIndex] = replacementColor;
                            }
                        }
                    }
                }
                else if (pointOne.X < foo.center.X && pointTwo.X < foo.center.X) // > from center
                {
                    for (int x = Math.Max(pointOne.X, pointTwo.X); x >= foo.center.X - foo.radius; x--)
                    {
                        int yOne = linePointsOne.Find(p => p.X == x).Y;
                        int yTwo = linePointsTwo.Find(p => p.X == x).Y;
                        for (int y = Math.Min(yOne, yTwo); y <= Math.Max(yOne, yTwo); y++)
                        {
                            Point p = new Point(x, y);
                            pIndex = (p.X - topLeft.X) + textureWidth * (topLeft.Y - p.Y);
                            if (0 <= pIndex && pIndex < curData.Length && foo.PointInsideCircle(p))
                            {
                                curData[pIndex] = replacementColor;
                            }
                        }
                    }
                }
            }
            else //lines opening vertically ^ or v or / or \ from center.
            {
                if (pointOne.Y > foo.center.Y && pointTwo.Y > foo.center.Y) // v from center
                {
                    for (int y = Math.Min(pointOne.Y, pointTwo.Y); y <= foo.center.Y + foo.radius; y++)
                    {
                        int xOne = linePointsOne.Find(p => p.Y == y).X;
                        int xTwo = linePointsTwo.Find(p => p.Y == y).X;
                        for (int x = Math.Min(xOne, xTwo); x <= Math.Max(xOne, xTwo); x++)
                        {
                            Point p = new Point(x, y);
                            pIndex = (p.X - topLeft.X) + textureWidth * (topLeft.Y - p.Y);
                            if (0 <= pIndex && pIndex < curData.Length && foo.PointInsideCircle(p))
                            {
                                curData[pIndex] = replacementColor;
                            }
                        }
                    }
                }
                else if (pointOne.Y < foo.center.Y && pointTwo.Y < foo.center.Y) // ^ from center
                {
                    for (int y = Math.Max(pointOne.Y, pointTwo.Y); y >= foo.center.Y - foo.radius; y--)
                    {
                        int xOne = linePointsOne.Find(p => p.Y == y).X;
                        int xTwo = linePointsTwo.Find(p => p.Y == y).X;
                        for (int x = Math.Min(xOne, xTwo); x <= Math.Max(xOne, xTwo); x++)
                        {
                            Point p = new Point(x, y);
                            pIndex = (p.X - topLeft.X) + textureWidth * (topLeft.Y - p.Y);
                            if (0 <= pIndex && pIndex < curData.Length && foo.PointInsideCircle(p))
                            {
                                curData[pIndex] = replacementColor;
                            }
                        }
                    }
                }
            }

            return curData;
        }

        public Point position;

        public float[] transparency;

        public int[][] intensity;

        public Color[] color;

        private double lastGametime;

        private int frame;

        public LightSource(Point position, float[] transparency, int[][] intensity, Color[] color)
        {
            this.position = position;
            this.transparency = transparency;
            this.intensity = intensity;
            this.color = color;
            frame = 0;
        }

        public void Draw()
        {
            if (Global._gameTime.TotalGameTime.TotalMilliseconds - lastGametime >= 400)
            {
                lastGametime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                if (frame == 1)
                {
                    frame = 0;
                }
                else
                {
                    frame = 1;
                }
            }

            //gets the lowest length of the arrays transparency, intensity, and color.
            int lowestLength = (transparency.Length < intensity.Length) ? ((transparency.Length < color.Length) ? transparency.Length : color.Length) : ((intensity.Length < color.Length) ? intensity.Length : color.Length);

            Texture2D circle;
            for (int i = 0; i < lowestLength; i++)
            {
                circle = Circle.GetCircleTexture(intensity[i][frame]);
                Global._spriteBatch.Draw(circle, new Rectangle(position.X - circle.Width / 2, -(position.Y + circle.Height / 2), circle.Width, circle.Height), color[i] * transparency[i]);
            }

        }
    }
}
