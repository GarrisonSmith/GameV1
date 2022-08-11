using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Hitbox used by Tiles to describe their collision areas and what they collide with.
    /// </summary>
    public class Tilebox : Hitbox
    {
        /// <summary>
        /// Determines what inclusive movement is needed to walk within this Tileboxes collision area.
        /// </summary>
        public MovementInclusions movementInclusion;
        /// <summary>
        /// Determines if this TileBox has collision with Entities.
        /// </summary>
        public bool entityCollision;

        /// <summary>
        /// Creates a Tilebox with the provided parameters.
        /// </summary>
        /// <param name="movementInclusion">Determines what inclusive movement is needed to walk within this Tileboxes collision area.</param>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="boundings">The rectangles describing the Tileboxes collision area. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="entityCollision">True will result in this Tilebox having collision with Entities, False will not.</param>
        public Tilebox(MovementInclusions movementInclusion, Point position, Rectangle[] boundings, bool entityCollision = true)
        {
            this.movementInclusion = movementInclusion;
            geometry = new HitboxGeometry(position, boundings);
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
        /// <summary>
        /// Draws all of the rectangles inside of this Tilebox collision area.
        /// Black rectangles are inassessible. GreenYellow rectangles have land assessiblity. DarkBlue rectangles have water assessibility.
        /// </summary>
        new public void DrawHitbox()
        {
            switch (movementInclusion)
            {
                case MovementInclusions.inassessible:
                    geometry.Draw(Color.Black);
                    break;
                case MovementInclusions.land:
                    geometry.Draw(Color.GreenYellow);
                    break;
                case MovementInclusions.water:
                    geometry.Draw(Color.DarkBlue);
                    break;
            }
        }

        /// <summary>
        /// Creates a string describing this Tilebox.
        /// </summary>
        /// <returns>A string describing this Tilebox.</returns>
        new public string ToString()
        {
            return "Movement Inclusion: " + movementInclusion + Environment.NewLine + geometry.ToString();
        }
    }
}