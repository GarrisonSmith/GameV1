using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.physics;

namespace Fantasy.Logic.Engine.entities
{
    /// <summary>
    /// Describes a basic entity, inherited by more complex entity types.
    /// </summary>
    class Entity
    {
        /// <summary>
        /// String used to identify this entity.
        /// </summary>
        public string id;
        /// <summary>
        /// The type of this enity. TODO possibly should be switched to a enum eventually.
        /// </summary>
        public string type;
        /// <summary>
        /// The layer this entity currently occupies.
        /// </summary>
        public int layer;
        /// <summary>
        /// This entities hitbox for collision on a TileMap or other entities. Also stores the characters location.
        /// </summary>
        public Entitybox hitbox;
        /// <summary>
        /// The spritesheet this entity uses when drawing or when animated.
        /// </summary>
        public Texture2D spriteSheet;
        /// <summary>
        /// Describes the MoveSpeed of the entity.
        /// </summary>
        public MoveSpeed speed;

        /// <summary>
        /// Generic inheriated constructor.
        /// </summary>
        public Entity() { }
        /// <summary>
        /// Creates a Entity with the provided parameters.
        /// </summary>
        /// <param name="id">String that will be used to identify this entity.</param>
        /// <param name="type">The type this enity will be.</param>
        /// <param name="spriteSheet">The spritesheet this enity will use when drawing or when animated.</param>
        /// <param name="layer">The layer this entity will occupy.</param>
        /// <param name="hitbox">This entities hitbox for collision on a TileMap or other entities.</param>
        /// <param name="speed">Describes the MoveSpeed of the entity.</param>
        public Entity(string id, string type, Texture2D spriteSheet, int layer, Entitybox hitbox, MoveSpeed speed)
        {
            this.id = id;
            this.type = type;
            this.spriteSheet = spriteSheet;
            this.layer = layer;
            this.hitbox = hitbox;
            this.speed = speed;
        }

        public void MoveEntity(Orientation direction)
        {
            int tempSpeed = speed.MovementAmount();
            Rectangle newCharacterArea;
            do
            {
                newCharacterArea = hitbox.characterArea;
                switch (direction)
                {
                    case Orientation.up:
                        newCharacterArea.Y += tempSpeed;
                        break;
                    case Orientation.right:
                        newCharacterArea.X += tempSpeed;
                        break;
                    case Orientation.left:
                        newCharacterArea.X -= tempSpeed;
                        break;
                    case Orientation.down:
                        newCharacterArea.Y -= tempSpeed;
                        break;
                }
                tempSpeed--;
            } while (!hitbox.AttemptMovement(layer, newCharacterArea) && tempSpeed != 0);
        }
        /// <summary>
        /// Draws the collision area of the hitbox of this entity, 
        /// </summary>
        public void DrawHitbox()
        {
            hitbox.DrawHitbox();
        }
    }
}