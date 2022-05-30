using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

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
                 (int)((characterArea.X + foo.X) * Global._baseStretch.X),
                 (int)((characterArea.Y - foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}
