using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.hitboxes
{
    /// <summary>
    /// Hitbox used by TileMapLayers to describe the collision area of collision triggered SceneEvents.
    /// </summary>
    class Eventbox : Hitbox
    {
        /// <summary>
        /// The sceneEvent for this Eventbox.
        /// </summary>
        public SceneEvent sceneEvent;
        /// <summary>
        /// The location this Eventbox occupies on its TileMapLayer. Offsets the rectangles in collisionArea.
        /// </summary>
        public Point location;

        /// <summary>
        /// Creates a Eventbox with the provided location.
        /// </summary>
        /// <param name="location">The location this Eventbox will occupy on its TileMapLayer.</param>
        public Eventbox(Point location)
        {
            this.location = location;
        }
        /// <summary>
        /// Creates a Eventbox with the provided location, sceneEvent, and collisionArea.
        /// </summary>
        /// <param name="location">The location this Eventbox will occupy on its TileMapLayer.</param>
        /// <param name="sceneEvent">The sceneEvent this Eventbox will contain.</param>
        /// <param name="collisionArea">Array of Rectangles that will describe the Tileboxes area.</param>
        public Eventbox(Point location, SceneEvent sceneEvent, Rectangle[] collisionArea) : this(location)
        {
            this.sceneEvent = sceneEvent;
            this.collisionArea = collisionArea;
        }
        /// <summary>
        /// Checks if the provided rectangle intersects with this boxes area.
        /// </summary>
        /// <param name="inRef">Point used as offset on the rectangle foos position.</param>
        /// <param name="foo">The rectangle to be checked.</param>
        /// <returns>True if the rectangle foo intersects any rectangle in collisionArea, False if not.</returns>
        public bool Collision(Point inRef, Rectangle foo)
        {
            Rectangle doo = new Rectangle(
                (int)((inRef.X + foo.X) * Global._baseStretch.X),
                (int)((inRef.Y - foo.Y - foo.Height) * Global._baseStretch.Y),
                (int)(foo.Width * Global._baseStretch.X),
                (int)(foo.Height * Global._baseStretch.Y));

            foreach (Rectangle bar in collisionArea)
            {
                Rectangle baz = new Rectangle(
                (int)((location.X + bar.X) * Global._baseStretch.X),
                (int)((location.Y - bar.Y - bar.Height) * Global._baseStretch.Y),
                (int)(bar.Width * Global._baseStretch.X),
                (int)(bar.Height * Global._baseStretch.Y));
                if (baz.Intersects(doo))
                {
                    Global._currentScene.DoEvent(sceneEvent);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the provided entityBox is colliding with this boxes area.
        /// </summary>
        /// <param name="entityBox">The entityBox to be checked.</param>
        /// <returns>True if entityBoxs collisionArea intersects this Eventboxes collisionArea, False if not.</returns>
        public bool Collision(Entitybox entityBox)
        {
            Rectangle[] foofoo = entityBox.collisionArea;

            foreach (Rectangle foo in foofoo)
            {
                if (Collision(entityBox.characterArea.Location, foo))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Draws all of the rectangles inside of this boxes collisionArea.
        /// Used for debugging.
        /// </summary>
        new public void DrawHitbox()
        {
            foreach (Rectangle foo in collisionArea)
            {
                Rectangle bar = new Rectangle(
                 (int)((location.X + foo.X) * Global._baseStretch.X),
                 (int)((location.Y - foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}