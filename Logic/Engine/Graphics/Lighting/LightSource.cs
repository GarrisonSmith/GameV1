using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Graphics.Lighting
{
    public class LightSource
    {
        public int intensity;

        public Color color;

        public Point position;

        public LightSource(int intensity, Color color)
        {
            this.intensity = intensity;
            this.color = color;
        }

        public void Draw()
        {
            Global._spriteBatch.Draw(Debug.debug,
                new Vector2(position.X, -position.Y),
                new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                new Vector2(1, 1), new SpriteEffects(), 0);
        }
    }
}
