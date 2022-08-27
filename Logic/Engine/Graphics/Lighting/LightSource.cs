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
            Color[] textureArray = foo.GetTextureData();

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
                            textureArray = ReplaceSlice(foo, topLeft, topRight, Color.Transparent);
                        }
                        else //rectanlge is above circle center.
                        {

                        }
                    }
                    else if (rec.Y >= foo.center.Y && rec.Y - rec.Height <= foo.center.Y) //circle center is horizontally aligned with rectangle side.
                    {
                        if (rec.X > foo.center.X) //rectangle is right of circle center.
                        {

                        }
                        else //rectanlge is left of circle center.
                        {

                        }
                    }
                    else //no side is aligned with circle center.
                    {

                    }
                }
            }

            Texture2D texture = new Texture2D(Global._graphics.GraphicsDevice, foo.radius * 2 + 1, foo.radius * 2 + 1);
            texture.SetData(textureArray);
            return texture;
        }

        public static Color[] ReplaceSlice(Circle foo, Point pointOne, Point pointTwo, Color replacementColor)
        {
            int textureWidth = foo.GetTexture().Width;
            Tuple<double, double> lineOne = Util.LineFormula(pointOne, foo.center);
            Tuple<double, double> lineTwo = Util.LineFormula(pointTwo, foo.center);

            sbyte directionOne, directionTwo; //determines which direction pointOne and pointTwo need to move towards.
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

            Point topLeft = new Point(foo.center.X - foo.radius, foo.center.Y + foo.radius), progOne = foo.center, progTwo = foo.center, progOneLast = foo.center, progTwoLast = foo.center;
            int pIndex;
            bool insideOne = true, insideTwo = true;
            Color[] curData = foo.GetTextureData();
            List<Point> replacementPoints;
            while (insideOne || insideTwo)
            {
                replacementPoints = new List<Point>(); //contains this iterations points to be replaced.

                //Generates the points of a ray from foo's center toward pointOne that are inside of foo and beyond pointOne.
                progOne = new Point(progOne.X + directionOne, Util.XOnLine(lineOne, progOne.X + directionOne));
                for (int y = Math.Min(progOne.Y, progOneLast.Y); y < Math.Max(progOne.Y, progOneLast.Y); y++)
                {
                    replacementPoints.Add(new Point(progOne.X, y));
                }

                replacementPoints.Add(progOne);

                if (insideOne! && !foo.PointInsideCircle(progOneLast))
                {
                    insideOne = false;
                }

                //Generates the points of a ray from foo's center toward pointTwo that are inside of foo and beyond pointTwo.
                progTwo = new Point(progTwo.X + directionTwo, Util.XOnLine(lineTwo, progTwo.X + directionTwo));
                for (int y = Math.Min(progTwo.Y, progTwoLast.Y); y < Math.Max(progTwo.Y, progTwoLast.Y); y++)
                {
                    replacementPoints.Add(new Point(progTwo.X, y));
                }

                replacementPoints.Add(progTwo);

                if (insideTwo! && !foo.PointInsideCircle(progTwoLast))
                {
                    insideTwo = false;
                }

                //Gets the points between progOne and progTwo.
                if (directionOne == directionTwo) //lines opening horizontally < or > from center.
                {
                    for (int y = Math.Min(progOne.Y, progTwo.Y) + 1; y < Math.Max(progOne.Y, progTwo.Y); y++)
                    {
                        replacementPoints.Add(new Point(progOne.X, y));
                    }
                }
                else //lines opening vertically ^ or v from center.
                {
                    for (int x = progTwo.X + 1; x < progTwo.X + 100; x++)
                    {
                        replacementPoints.Add(new Point(x , progOne.Y));
                    }
                }

                progOneLast = progOne;
                progTwoLast = progTwo;
                //replaces the determines points
                foreach (Point p in replacementPoints)
                {
                    pIndex = (p.X - topLeft.X) + textureWidth * (topLeft.Y - p.Y);
                    if (0 <= pIndex && pIndex < curData.Length && foo.PointInsideCircle(p))
                    {
                        curData[pIndex] = replacementColor;
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
