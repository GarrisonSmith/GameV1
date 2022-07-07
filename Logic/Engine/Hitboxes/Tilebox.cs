using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Hitbox used by Tiles to describe their collision areas and what they collide with.
    /// </summary>
    public class Tilebox : Hitbox
    {
        /// <summary>
        /// Determines if this TileBox has collision with Entities.
        /// </summary>
        public bool entityCollision;

        /// <summary>
        /// Creates a Tilebox with the provided parameters.
        /// </summary>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="visualArea">Describes the the visual area of the Tilebox. The rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="boundings">The rectangles describing the Tileboxes collision area. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="entityCollision">True will result in this Tilebox having collision with Entities, False will not.</param>
        public Tilebox(Point position, Rectangle visualArea, Rectangle[] boundings, bool entityCollision = true)
        {
            geometry = new HitboxGeometry(position, visualArea, boundings);
            this.entityCollision = entityCollision;
        }

        /// <summary>
        /// Determines if this Tilebox has collided with the provided Hitbox.
        /// </summary>
        /// <param name="foo">The Hitbox to be investigated.</param>
        /// <returns>True if this Tilebox collides with the provided Hitbox, False if not.</returns>
        new public bool Collision(Hitbox foo)
        {
            if (foo is Entitybox entitybox)
            {
                if (!entityCollision || !entitybox.tileCollision)
                {
                    return false;
                }
            }
            
            return geometry.Intersection(foo.geometry);
        }
    }
}