using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.hitboxes
{
    /// <summary>
    /// Hitbox used by Entities to describe their collision area and location.
    /// </summary>
    class Entitybox : Hitbox
    {
        /// <summary>
        /// The entity id this Entitybox refers to.
        /// </summary>
        public string reference;
        /// <summary>
        /// Describes the location, width, and height of the entity this Entitybox refers to.
        /// </summary>
        public Rectangle characterArea;

        /// <summary>
        /// Creates a Entitybox with the provided reference and characterArea.
        /// </summary>
        /// <param name="reference">The entity id this Entitybox will refer to.</param>
        /// <param name="characterArea">The location, width, and height of the entity this Entitybox will refer to.</param>
        public Entitybox(string reference, Rectangle characterArea)
        {
            this.reference = reference;
            this.characterArea = characterArea;
        }
        /// <summary>
        /// Creates a Entitybox with the provided reference, characterArea, and collisionArea.
        /// </summary>
        /// <param name="reference">The entity id this Entitybox will refer to.</param>
        /// <param name="characterArea">The location, width, and height of the entity this Entitybox will refer to.</param>
        /// <param name="collisionArea">Array of Rectangles that will describe the Tileboxes area.</param>
        public Entitybox(string reference, Rectangle characterArea, Rectangle[] collisionArea) : this(reference, characterArea)
        {
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
                characterArea.X + bar.X,
                characterArea.Y - bar.Y - bar.Height,
                bar.Width,
                bar.Height);
                if (baz.Intersects(temp))
                {
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
        /// Checks if the provided newCharacterArea collides with any Tileboxes or Eventboxes in the Global current scenes Tilemap.
        /// If newCharacterArea is valid (does not result in collision), then newCharacterArea will become this Entityboxes CharacterArea.
        /// </summary>
        /// <param name="layer">The layer of the Tliemap to be checked.</param>
        /// <param name="newCharacterArea">The position, length, and width of this entity that will be checked for collision.</param>
        /// <returns>True if newCharacterArea is valid (does not result in collision) and the movement is done,
        /// False if newCharacterArea is not valid (does result in collision) and the movement is not done.</returns>
        public bool AttemptMovement(int layer, Rectangle newCharacterArea)
        {
            Rectangle temp = characterArea;
            characterArea = newCharacterArea;
            if (Global._currentScene._tileMap.Collision(layer, this))
            {
                characterArea = temp;
                return false;
            }
            return true;
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
                 characterArea.X + foo.X,
                 characterArea.Y - foo.Y,
                 foo.Width,
                 foo.Height);

                Debug.DrawRectangle(bar);
            }
        }
    }
}
