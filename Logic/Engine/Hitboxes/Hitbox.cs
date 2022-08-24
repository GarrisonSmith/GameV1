using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Physics;

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
        public HitboxGeometry geometry;
        /// <summary>
        /// Two-dimensional array that determines how light pass through and out of this hitbox.
        /// The first index of outer array is for light pass through logic, The second index of the out array is for light pass out logic.
        /// The first, second, third, and forth indexes of the inner array determines the logic for the top, right, bottom, and left sides of the hitbox.
        /// </summary>
        public bool[,] lightPhysics = new bool[2, 4];

        /// <summary>
        /// Generic inherited constructor. 
        /// </summary>
        public Hitbox() { }

        /// <summary>
        /// Gets this Hitboxes position as a point.
        /// </summary>
        /// <returns>Point representing this Hitboxes position.</returns>
        public Point GetPointPosition()
        {
            return geometry.position;
        }
        /// <summary>
        /// Gets this Hitboxes position as a Vector2.
        /// </summary>
        /// <param name="invertY">Determines if the Y value of the returned Vector2 is inversed. Used for drawing.</param>
        /// <returns>Vector2 representing this Hitboxes position.</returns>
        public Vector2 GetVectorPosition(bool invertY = true)
        {
            if (invertY)
            {
                return new Vector2(geometry.position.X, -geometry.position.Y);
            }
            else
            {
                return new Vector2(geometry.position.X, geometry.position.Y);
            }
        }
        /// <summary>
        /// Determines if this Hitbox has collided with the provided Hitbox.
        /// </summary>
        /// <param name="foo">The Hitbox to be investigated.</param>
        /// <returns>True if this Hitbox collides with the provided Hitbox, False if not.</returns>
        public bool Collision(Hitbox foo)
        {
            return geometry.Intersection(foo.geometry);
        }
        /// <summary>
        /// Determines if this Hitbox has collided with the provided circle.
        /// </summary>
        /// <param name="foo">The Circle to be investigated.</param>
        /// <returns>True if this Hitbox collides with the provided Circle, False if not.</returns>
        public bool Collision(Circle foo)
        {
            return geometry.Intersection(foo);
        }
        /// <summary>
        /// Draws all of the rectangles inside of this Hitboxes collision area.
        /// </summary>
        /// <param name="color">The color for the hitbox to be drawn with.</param>
        public void DrawHitbox(Color color)
        {
            geometry.Draw(color);
        }
    }
}