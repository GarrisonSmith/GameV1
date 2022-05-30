using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.hitboxes
{
    /// <summary>
    /// Generic Hitbox inherited by other collision boxes.
    /// </summary>
    class Hitbox
    {
        /// <summary>
        /// Array of Rectangles that describe the boxes area.
        /// </summary>
        public Rectangle[] collisionArea;

        /// <summary>
        /// Generic inherited constructor. 
        /// </summary>
        public Hitbox() { }
        /// <summary>
        /// Draws all of the rectangles inside of this boxes collisionArea.
        /// Used for debugging.
        /// </summary>
        public void DrawHitbox()
        {
            foreach (Rectangle foo in collisionArea)
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
