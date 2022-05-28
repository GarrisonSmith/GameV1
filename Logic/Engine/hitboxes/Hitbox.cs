using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Hitbox
    {
        public Rectangle[] area;

        public Hitbox() { }

        public void DrawHitbox()
        {
            foreach (Rectangle foo in area)
            {
                Rectangle bar = new Rectangle(
                 (int)(foo.X * Global._baseStretch.X),
                 (int)(-(foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}
