using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.Screen;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Hitbox used by TileMapLayers to describe the collision area of collision triggered SceneEvents.
    /// </summary>
    public class Eventbox : Hitbox
    {
        /// <summary>
        /// The identification of this Eventbox.
        /// </summary>
        public string id;
        /// <summary>
        /// Describes what this Eventbox does.
        /// </summary>
        public string discription;
        /// <summary>
        /// The sceneEvent for this Eventbox.
        /// </summary>
        public SceneEvent sceneEvent;
        /// <summary>
        /// Determines if this Eventbox has collision with Entities.
        /// </summary>
        public bool entityCollision;

        /// <summary>
        /// Creates a Eventbox with the provided parameters.
        /// </summary>
        /// <param name="id">The identification of this Eventbox.</param>
        /// <param name="discription">Describes what this Eventbox will do.</param>
        /// <param name="sceneEvent">The SceneEvent that will trigger upon this Eventbox detecting a collision.</param>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="visualArea">Describes the the visual area of the Tilebox. The rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="boundings">The rectangles describing the Eventbox collision area. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        /// <param name="entityCollision">True will result in this Eventbox having collision with Entities, False will not.</param>
        public Eventbox(string id, string discription, SceneEvent sceneEvent, Point position, Rectangle visualArea, Rectangle[] boundings, bool entityCollision = true)
        {
            this.id = id;
            this.discription = discription;
            this.sceneEvent = sceneEvent;
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
                if (!entityCollision || !entitybox.eventCollision)
                {
                    return false;
                }
            }

            return geometry.Intersection(foo.geometry);
        }
    }
}