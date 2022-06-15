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
                 foo.X,
                 -foo.Y,
                 foo.Width,
                 foo.Height);

                Debug.DrawRectangle(bar);
            }
        }
    }
}
