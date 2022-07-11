using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Hitbox used by Entities to describe their collision area and location.
    /// </summary>
    public class Entitybox : Hitbox
    {
        /// <summary>
        /// Determines if this Entitybox has collision with Events.
        /// </summary>
        public bool eventCollision;
        /// <summary>
        /// Determines if this Entitybox has collision with Entities.
        /// </summary>
        public bool entityCollision;
        /// <summary>
        /// Determines if this Entitybox has collision with Tiles.
        /// </summary>
        public bool tileCollision;

        /// <summary>
        /// Creates a Entitybox with the provided parameters.
        /// </summary>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="boundings">The rectangles describing the Entitybox collision area. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="eventCollision">True will result in this Entitybox having collision with Events, False will not.</param>
        /// <param name="entityCollision">True will result in this Entitybox having collision with Entities, False will not.</param>
        /// <param name="tileCollision">True will result in this Entitybox having collision with Tiles, False will not.</param>
        public Entitybox(Point position, Rectangle[] boundings, bool eventCollision = true, bool entityCollision = true, bool tileCollision = true)
        {
            geometry = new HitboxGeometry(position, boundings);
            this.eventCollision = eventCollision;
            this.entityCollision = entityCollision;
            this.tileCollision = tileCollision;
        }

        /// <summary>
        /// Determines if this Entitybox has collided with the provided Hitbox.
        /// </summary>
        /// <param name="foo">The Hitbox to be investigated.</param>
        /// <returns>True if this Entitybox collides with the provided Hitbox, False if not.</returns>
        new public bool Collision(Hitbox foo)
        {
            if (foo is Entitybox entitybox)
            {
                if (!entityCollision || !entitybox.tileCollision)
                {
                    return false;
                }
            }

            if (foo is Tilebox tilebox)
            {
                if (!tileCollision || !tilebox.entityCollision)
                {
                    return false;
                }
            }

            return false;
        }

        public bool AttemptMovement(int layer, Point newPosition)
        {
            return true;
        }
    }
}
