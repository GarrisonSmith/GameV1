using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Graphics.tilemap;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Physics;
using System.Collections.Generic;
using System;

namespace Fantasy.Logic.Engine.Graphics.Lighting
{
    public class LightSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foo"></param>
        /// <param name="layer"></param>
        public static void StaticCastCircleOnLayer(Circle foo, int layer)
        {
            Global._currentScene.fbao = new List<Tuple<Point, Point>>();
            
            Lightbox[] bar = Global._currentScene._spriteManager.GetLayerStaticLightboxes(layer);
            foreach (Lightbox box in bar)
            {
                if (foo.Intersection(box.GetAbsoluteBoundings()))
                {
                    foreach (Tuple<Point, Point> l in box.GetLightCollisionLines(foo.center))
                    {
                        Global._currentScene.fbao.Add(l);
                        ReplaceSlice(foo, l.Item1, l.Item2, Color.Transparent);
                    }
                }
            }
        }

        public static void DynamicCastCircleOnLayer(Circle foo, int layer)
        {
            Global._currentScene.fbao = new List<Tuple<Point, Point>>();

            Lightbox[] bar = Global._currentScene._spriteManager.GetLayerDynamicLightboxes(layer);
            foreach (Lightbox box in bar)
            {
                if (foo.Intersection(box.GetAbsoluteBoundings()))
                {
                    foreach (Tuple<Point, Point> l in box.GetLightCollisionLines(foo.center))
                    {
                        Global._currentScene.fbao.Add(l);
                        ReplaceSlice(foo, l.Item1, l.Item2, Color.Transparent);
                    }
                }
            }
        }
        /// <summary>
        /// Replaces a slice of the circle foos texture with the provided replacement color. pointOne and pointTwo act as clamp points for casting the replacement area.
        /// </summary>
        /// <remarks>pontOne and pointTwo must be in either a vertical or horizontal line.</remarks>
        /// <param name="foo">The circle to be used.</param>
        /// <param name="pointOne">The first clamp point.</param>
        /// <param name="pointTwo">The second clamp point.</param>
        /// <param name="replacementColor">The color that will replace all points inside of the slice.</param>
        public static void ReplaceSlice(Circle foo, Point pointOne, Point pointTwo, Color replacementColor)
        {
            if (pointOne.X != pointTwo.X && pointOne.Y != pointTwo.Y)
            {
                return;
            }
            
            int textureWidth = foo.GetTexture().Width;
            Tuple<double, double> lineOne = Lines.LineFormula(pointOne, foo.center);
            Tuple<double, double> lineTwo = Lines.LineFormula(pointTwo, foo.center);

            Point topLeft = new Point(foo.center.X - foo.radius, foo.center.Y + foo.radius), progOne = pointOne, progTwo = pointTwo, progOneLast = pointOne, progTwoLast = pointTwo;
            int pIndex;
            bool doneOne = false, doneTwo = false;
            Color[] curData = foo.GetTextureData();
            List<Point> linePointsOne = new List<Point>(), linePointsTwo = new List<Point>();
            linePointsOne.Add(progOne); linePointsTwo.Add(progTwo);

            //determines which direction pointOne and pointTwo need to move towards.
            sbyte directionOne, directionTwo;
            if (pointOne.X == foo.center.X)
            {
                directionOne = 0;
                doneOne = true;
                while (Math.Abs(progOne.Y - foo.center.Y) <= foo.radius)
                {
                    if (progOne.Y <= foo.center.Y)
                    {
                        progOne = new Point(progOne.X, progOne.Y - 1);
                        linePointsOne.Add(progOne);
                    }
                    else
                    {
                        progOne = new Point(progOne.X, progOne.Y + 1);
                        linePointsOne.Add(progOne);
                    }
                }
            }
            else if (pointOne.X > foo.center.X)
            {
                directionOne = 1;
            }
            else
            {
                directionOne = -1;
            }
            if (pointTwo.X == foo.center.X)
            {
                directionTwo = 0;
                doneTwo = true;
                while (Math.Abs(progTwo.Y - foo.center.Y) <= foo.radius && directionOne != directionTwo)
                {
                    if (progTwo.Y <= foo.center.Y)
                    {
                        progTwo = new Point(progTwo.X, progTwo.Y - 1);
                        linePointsTwo.Add(progTwo);
                    }
                    else
                    {
                        progTwo = new Point(progTwo.X, progTwo.Y + 1);
                        linePointsTwo.Add(progTwo);
                    }
                }
            }
            else if (pointTwo.X > foo.center.X)
            {
                directionTwo = 1;
            }
            else
            {
                directionTwo = -1;
            }

            //generates the points on the lines from pointOne and pointTwo.
            while (!doneOne || !doneTwo)
            {
                //Generates the points of a ray from foo's center toward pointOne that are inside of foo and beyond pointOne.
                if (!doneOne)
                {
                    progOne = new Point(progOne.X + directionOne, Lines.GetY(lineOne, progOne.X + directionOne));
                    if (directionOne != directionTwo)
                    {
                        for (int y = Math.Min(progOne.Y, progOneLast.Y); y < Math.Max(progOne.Y, progOneLast.Y); y++)
                        {
                            linePointsOne.Add(new Point(progOne.X, y));
                        }
                    }
                    linePointsOne.Add(progOne);
                    progOneLast = progOne;
                    if (!foo.PointInsideCircle(progOne))
                    {
                        doneOne = true;
                        while (Math.Abs(progOne.X - foo.center.X) <= foo.radius && directionOne == directionTwo)
                        {
                            progOne = new Point(progOne.X + directionOne, progOne.Y);
                            linePointsOne.Add(progOne);
                        }
                        while (Math.Abs(progOne.Y - foo.center.Y) <= foo.radius && directionOne != directionTwo)
                        {
                            if (progOne.Y <= foo.center.Y)
                            {
                                progOne = new Point(progOne.X, progOne.Y - 1);
                                linePointsOne.Add(progOne);
                            }
                            else 
                            {
                                progOne = new Point(progOne.X, progOne.Y + 1);
                                linePointsOne.Add(progOne);
                            }
                        }
                    }
                }

                //Generates the points of a ray from foo's center toward pointTwo that are inside of foo and beyond pointTwo.
                if (!doneTwo)
                {
                    progTwo = new Point(progTwo.X + directionTwo, Lines.GetY(lineTwo, progTwo.X + directionTwo));
                    if (directionOne != directionTwo)
                    {
                        for (int y = Math.Min(progTwo.Y, progTwoLast.Y); y < Math.Max(progTwo.Y, progTwoLast.Y); y++)
                        {
                            linePointsTwo.Add(new Point(progTwo.X, y));
                        }
                    }
                    linePointsTwo.Add(progTwo);
                    progTwoLast = progTwo;
                    if (!foo.PointInsideCircle(progTwo))
                    {
                        doneTwo = true;
                        while (Math.Abs(progTwo.X - foo.center.X) <= foo.radius && directionOne == directionTwo)
                        {
                            progTwo = new Point(progTwo.X + directionTwo, progTwo.Y);
                            linePointsTwo.Add(progTwo);
                        }
                        while (Math.Abs(progTwo.Y - foo.center.Y) <= foo.radius && directionOne != directionTwo)
                        {
                            if (progTwo.Y <= foo.center.Y)
                            {
                                progTwo = new Point(progTwo.X, progTwo.Y - 1);
                                linePointsTwo.Add(progTwo);
                            }
                            else
                            {
                                progTwo = new Point(progTwo.X, progTwo.Y + 1);
                                linePointsTwo.Add(progTwo);
                            }
                        }
                    }
                }
            }

            //Gets the points between progOne and progTwo and replaces their colors.
            if (directionOne == directionTwo) //lines opening horizontally > or < from center.
            {
                if (pointOne.X > foo.center.X && pointTwo.X > foo.center.X) // < from center
                {
                    for (int x = Math.Min(pointOne.X, pointTwo.X); x <= foo.center.X + foo.radius; x++)
                    {
                        int yOne;
                        if (linePointsOne.Exists(p => p.X == x))
                        {
                            yOne = linePointsOne.Find(p => p.X == x).Y;
                        }
                        else
                        {
                            yOne = pointOne.Y;
                        }

                        int yTwo;
                        if (linePointsTwo.Exists(p => p.X == x))
                        {
                            yTwo = linePointsTwo.Find(p => p.X == x).Y;
                        }
                        else
                        {
                            yTwo = pointTwo.Y;
                        }

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
                    for (int x = Math.Max(pointOne.X, pointTwo.X); x >= -Math.Abs(foo.center.X - foo.radius); x--)
                    {
                        int yOne;
                        if (linePointsOne.Exists(p => p.X == x))
                        {
                            yOne = linePointsOne.Find(p => p.X == x).Y;
                        }
                        else
                        {
                            yOne = pointOne.Y;
                        }

                        int yTwo;
                        if (linePointsTwo.Exists(p => p.X == x))
                        {
                            yTwo = linePointsTwo.Find(p => p.X == x).Y;
                        }
                        else
                        {
                            yTwo = pointTwo.Y;
                        }

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
                        int xOne = pointOne.X;
                        int xTwo = pointTwo.X;

                        if (linePointsOne.Exists(p => p.Y == y))
                        {
                            xOne = linePointsOne.Find(p => p.Y == y).X;
                        }
                        if (linePointsTwo.Exists(p => p.Y == y))
                        {
                            xTwo = linePointsTwo.Find(p => p.Y == y).X;
                        }

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
                        int xOne = pointOne.X;
                        int xTwo = pointTwo.X;

                        if (linePointsOne.Exists(p => p.Y == y))
                        {
                            xOne = linePointsOne.Find(p => p.Y == y).X;
                        }
                        if (linePointsTwo.Exists(p => p.Y == y))
                        {
                            xTwo = linePointsTwo.Find(p => p.Y == y).X;
                        }

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

            foreach (Point p in linePointsOne)
            {
                //Debug.DrawPoint(p, Color.CornflowerBlue);
            }
            foreach (Point p in linePointsTwo)
            {
                //Debug.DrawPoint(p, Color.CornflowerBlue);
            }

            foo.SetTexture(curData);
        }

        /// <summary>
        /// 
        /// </summary>
        int layer;
        
        public Circle[] staticCircles;

        public Circle[] dynamicCircles;

        public float[] transparency;

        public Color[] color;

        private double lastGametime;

        private int frame;

        public LightSource(int layer, Point position, int[] intensity, float[] transparency, Color[] color)
        {
            this.layer = layer;
            int lowestLength = (transparency.Length < intensity.Length) ? ((transparency.Length < color.Length) ? transparency.Length : color.Length) : ((intensity.Length < color.Length) ? intensity.Length : color.Length);
            staticCircles = new Circle[lowestLength];
            dynamicCircles = new Circle[lowestLength];
            this.transparency = transparency;
            this.color = color;
            frame = 0;

            for (int i = 0; i < lowestLength; i++)
            {
                Circle foo = new Circle(intensity[i], position);
                StaticCastCircleOnLayer(foo, layer);
                staticCircles[i] = foo;
                Circle bar = new Circle(intensity[i], position, false);
                bar.SetTexture(foo.GetTextureData());
                dynamicCircles[i] = bar;
            }
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

            for (int i = 0; i < dynamicCircles.Length; i++)
            {
                DynamicCastCircleOnLayer(dynamicCircles[i], layer);
                dynamicCircles[i].Draw(color[i] * transparency[i]);
                dynamicCircles[i].SetTexture(staticCircles[i].GetTextureData());
            }

        }
    }
}
