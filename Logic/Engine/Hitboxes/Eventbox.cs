using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.screen;
using Fantasy.Logic.Engine.Utility;

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
            Rectangle temp = new Rectangle(
                inRef.X + foo.X,
                inRef.Y - foo.Y - foo.Height,
                foo.Width,
                foo.Height);

            foreach (Rectangle bar in collisionArea)
            {
                Rectangle baz = new Rectangle(
                location.X + bar.X,
                location.Y - bar.Y - bar.Height,
                bar.Width,
                bar.Height);
                if (baz.Intersects(temp))
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
                 location.X + foo.X,
                 location.Y - foo.Y,
                 foo.Width,
                 foo.Height);

                Debug.DrawRectangle(bar);
            }
        }
    }
}