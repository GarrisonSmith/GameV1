using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Graphics.tilemap;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Physics;
using Fantasy.Logic.Engine.Hitboxes;
using System.Collections.Generic;

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

            return null;
        }
        private static Texture2D CreateLightShadowTexture(Circle foo, Hitbox[] bar)
        {
            Color[] textureArray = new Color[foo.GetCircleTexture().Width * foo.GetCircleTexture().Height];
            foo.GetCircleTexture().GetData(textureArray);

            foreach (Hitbox box in bar)
            {
                bool[,] lightPhysics = box.lightPhysics;
                foreach (Rectangle rec in box.geometry.boundings)
                {
                    if (Util.PointInsideRectangle(foo.center, rec)) //circle center is inside of rectangle.
                    { 
                    
                    }
                    else if (rec.X <= foo.center.X && rec.X + rec.Width >= foo.center.X) //circle center is vertically aligned with rectangle side.
                    {
                        if (rec.Y < foo.center.Y) //rectangle is below circle center.
                        {
                            Point topLeft = new Point(rec.X, rec.Y); Point topRight = new Point(rec.X + rec.Width, rec.Y);

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

            return null;
        }

        public static Color[] ReplaceSlice(Color[] data, int length, int width, Point origin, Point pointOne, Point pointTwo, Color replacementColor)
        {
            int xChange_One = pointOne.X - origin.X; int yChange_one = pointOne.Y - origin.Y;
            int xChange_Two = pointTwo.X - origin.X; int yChange_two = pointTwo.Y - origin.Y;
            double change_One = xChange_One / yChange_one; double change_two = xChange_Two / yChange_two;

            
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
