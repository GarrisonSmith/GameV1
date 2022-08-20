using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Graphics;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Physics;

namespace Fantasy.Logic.Engine.entities
{
    /// <summary>
    /// Describes a animated character.
    /// </summary>
    public class Character : Entity
    {
        /// <summary>
        /// The animations used by this character.
        /// </summary>
        public Animation frames;

        /// <summary>
        /// Generic inherited constructor.
        /// </summary>
        public Character() { }
        /// <summary>
        /// Creates a Character with the provided parameters.
        /// </summary>
        /// <param name="id">String that will be used to identify this entity.</param>
        /// <param name="type">The type this enity will be.</param>
        /// <param name="spriteSheet">The spritesheet this enity will use when drawing or when animated.</param>
        /// <param name="layer">The layer this entity will occupy.</param>
        /// <param name="entityVisualDimensions">Describes the visual dimensions of the entity away from the hitbox position.</param>
        /// <param name="hitbox">This entities hitbox for collision on a TileMap or other entities.</param>
        /// <param name="speed">Describes the MoveSpeed of the entity.</param>
        /// <param name="orientation">The initial orientation for the character.</param>
        public Character(string id, string type, Texture2D spriteSheet, int layer, Point entityVisualDimensions, Entitybox hitbox, MoveSpeed speed, Orientation orientation)
        {
            this.id = id;
            this.type = type;
            this.spriteSheet = spriteSheet;
            this.layer = layer;
            this.entityVisualDimensions = entityVisualDimensions;
            this.speed = speed;
            this.orientation = orientation;
            this.hitbox = hitbox;
            frames = new Animation(spriteSheet, 400, 400, 0, 4, 0, 0, 64, 128, AnimationState.idle);
        }

        /// <summary>
        /// Draws the current frame of the characters animation.
        /// </summary>
        override public void Draw()
        {
            frames.ChangeOrientation(orientation);

            if (movement != EntityMovementState.idle)
            {
                frames.animationState = AnimationState.cycling;
            }
            else
            {
                frames.animationState = AnimationState.finishing;
            }

            frames.Draw(hitbox.GetVectorPosition(), Color.White);
        }
    }
}
