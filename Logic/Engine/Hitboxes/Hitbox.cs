using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Generic Hitbox inherited by other collision boxes.
    /// </summary>
    public abstract class Hitbox
    {
        /// <summary>
        /// Set of rectangles that describes the boxes area.
        /// </summary>
        public RectangleSet geometry;

        /// <summary>
        /// Generic inherited constructor. 
        /// </summary>
        public Hitbox() { }

        /// <summary>
        /// Determines if this Tilebox has collided with the provided Hitbox.
        /// </summary>
        /// <param name="foo">The Hitbox to be investigated.</param>
        /// <returns>True if this Tilebox collides with the provided Hitbox, False if not.</returns>
        public bool Collision(Hitbox foo)
        {
            return geometry.Intersection(foo.geometry);
        }
        /// <summary>
        /// Draws all of the rectangles inside of this boxes collisionArea.
        /// </summary>
        /// <param name="drawSegments">True results in overlapping perimeters being drawn, False results in only unique perimeter values being drawn.</param>
        public void DrawHitbox(bool drawSegments = false)
        {
            geometry.Draw(drawSegments);
        }
    }
}