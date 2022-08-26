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
                            Point topLeft = new Point(rec.X, rec.Y); Point topRight = new Point(rec.X + rec.Width, rec.Y);
                            textureArray = ReplaceSlice(foo, foo.center, topLeft, topRight, Color.Transparent);
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

        public static Color[] ReplaceSlice(Circle foo, Point origin, Point pointOne, Point pointTwo, Color replacementColor)
        {
            int textureWidth = foo.GetTexture().Width;
            Tuple<double, double> slopeOne = Util.LineFormula(pointOne, origin);
            Tuple<double, double> slopeTwo = Util.LineFormula(pointTwo, origin);

            sbyte directionOne, directionTwo;
            if (pointOne.X >= origin.X)
            {
                directionOne = 1;
            }
            else
            {
                directionOne = -1;
            }
            if (pointTwo.X >= origin.X)
            {
                directionTwo = 1;
            }
            else
            {
                directionTwo = -1;
            }

            Point topLeft = new Point(foo.center.X - foo.radius, foo.center.Y + foo.radius), progOne = origin, progTwo = origin;//progOne = pointOne, progTwo = pointTwo; 
            int y, indexOne = -1, indexTwo = -1; 
            bool insideOne = true, insideTwo = true;
            Color[] curData = foo.GetTextureData();
            while (insideOne && insideTwo)
            {
                y = (int)Math.Round(slopeOne.Item1 * (progOne.X + directionOne) + slopeOne.Item2);
                progOne = new Point(progOne.X + directionOne, y);
                if (foo.PointInsideCircle(progOne))
                {
                    indexOne = (progOne.X - topLeft.X) + textureWidth * (topLeft.Y - progOne.Y);
                }
                else
                {
                    insideOne = false;
                }

                y = (int)Math.Round(slopeTwo.Item1 * (progTwo.X + directionTwo) + slopeTwo.Item2);
                progTwo = new Point(progTwo.X + directionTwo, y);
                if (foo.PointInsideCircle(progTwo))
                {
                    indexTwo = (progTwo.X - topLeft.X) + textureWidth * (topLeft.Y - progTwo.Y);
                }
                else
                {
                    insideTwo = false;
                }

                if (0 <= indexOne && indexOne < curData.Length && insideOne)
                {
                    curData[indexOne] = replacementColor;
                }
                if (0 <= indexTwo && indexTwo < curData.Length && insideTwo)
                {
                    curData[indexTwo] = replacementColor;
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
