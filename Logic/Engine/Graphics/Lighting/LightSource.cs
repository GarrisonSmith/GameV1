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
            foreach (Hitbox h in bar)
            {
                if (h.Collision(foo))
                {
                    collisions.Add(h);
                }
            }


            return null;
        }
        private static Texture2D CreateLightShadowTexture(Circle foo, Hitbox[] bar)
        {
            return null;
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
