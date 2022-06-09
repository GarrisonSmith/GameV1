using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.hitboxes
{
    /// <summary>
    /// Hitbox used by Tiles to describe their collision areas.
    /// </summary>
    class Tilebox : Hitbox
    {
        /// <summary>
        /// The name of the tile this Tilebox refers to.
        /// </summary>
        public string reference;

        /// <summary>
        /// Creates a Tilebox with the provided reference.
        /// </summary>
        /// <param name="reference">The name of the tile this Tilebox will refer to.</param>
        public Tilebox(string reference)
        {
            this.reference = reference;
        }
        /// <summary>
        /// Creates a Tilebox with the provided reference and collisionArea.
        /// </summary>
        /// <param name="reference">The name of the tile this Tilebox will refer to.</param>
        /// <param name="collisionArea">Array of Rectangles that will describe the Tileboxes area.</param>
        public Tilebox(string reference, Rectangle[] collisionArea) : this(reference)
        {
            this.collisionArea = collisionArea;
        }
        /// <summary>
        /// Checks if the provided rectangle intersects with this boxes area.
        /// </summary>
        /// <param name="inRef">Point used as offset on the rectangle foos position.</param>
        /// <param name="thisRef">Point used as offset on the rectangles in collisionAreas position.</param>
        /// <param name="foo">The rectangle to be checked.</param>
        /// <returns>True if the rectangle foo intersects any rectangle in collisionArea, False if not.</returns>
        public bool Collision(Point inRef, Point thisRef, Rectangle foo)
        {
            Rectangle doo = new Rectangle(
                inRef.X + foo.X,
                inRef.Y - foo.Y - foo.Height,
                foo.Width,
                foo.Height);

            foreach (Rectangle bar in collisionArea)
            {
                Rectangle baz = new Rectangle(
                thisRef.X + bar.X,
                thisRef.Y - bar.Y - bar.Height,
                bar.Width,
                bar.Height);
                if (baz.Intersects(doo))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the provided entityBox is colliding with this boxes area.
        /// </summary>
        /// <param name="thisRef">Point used as offset on the rectangles in collisionAreas position.</param>
        /// <param name="entityBox">The entityBox to be checked.</param>
        /// <returns>True if entityBoxs collisionArea intersects this Tileboxes collisionArea, False if not.</returns>
        public bool Collision(Point thisRef, Entitybox entityBox)
        {
            Rectangle[] foofoo = entityBox.collisionArea;
            
            foreach (Rectangle foo in foofoo)
            {
                if (Collision(entityBox.characterArea.Location, thisRef, foo))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Draws all of the rectangles inside of this boxes collisionArea with the offset from the point thisRef.
        /// Used for debugging.
        /// </summary>
        /// <param name="thisRef">Point used as offset on the rectangles in collisionAreas position.</param>
        public void DrawHitbox(Point thisRef)
        {
            foreach (Rectangle foo in collisionArea)
            {
                Rectangle bar = new Rectangle(
                 thisRef.X + foo.X,
                 thisRef.Y - foo.Y,
                 foo.Width,
                 foo.Height);

                Debug.DrawRectangle(bar);
            }
        }
    }
}
