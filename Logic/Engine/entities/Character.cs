using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.physics;

namespace Fantasy.Logic.Engine.entities
{
    class Character : Entity
    {
        public Orientation orientation;
        public Animation frames;

        public Character() { }

        public Character(string id, string type, Texture2D spriteSheet, int layer, Entitybox hitbox, MoveSpeed speed, Orientation orientation)
        {
            this.id = id;
            this.type = type;
            this.spriteSheet = spriteSheet;
            this.layer = layer;
            this.speed = speed;
            this.orientation = orientation;
            this.hitbox = hitbox;
            this.hitbox.collisionArea = new Rectangle[] { new Rectangle(8, 116, 48, 16) };
        }

        public void DrawCharacter()
        {
            if (frames == null)
            {
                switch (orientation)
                {
                    case Orientation.up:
                        frames = new Animation(400, 400, 0, 3, 3, 0, 64, 128, AnimationState.idle);
                        break;
                    case Orientation.right:
                        frames = new Animation(400, 400, 0, 3, 1, 0, 64, 128, AnimationState.idle);
                        break;
                    case Orientation.left:
                        frames = new Animation(400, 400, 0, 3, 2, 0, 64, 128, AnimationState.idle);
                        break;
                    case Orientation.down:
                        frames = new Animation(400, 400, 0, 3, 0, 0, 64, 128, AnimationState.idle);
                        break;
                }
            }

            if (movement != EntityMovementState.idle)
            {
                frames.animationState = AnimationState.cycling;
            }
            else if (frames.animationState != AnimationState.idle)
            {
                frames.animationState = AnimationState.finishing;
            }

            frames.DrawAnimation(hitbox.characterArea.Location, spriteSheet, Color.White);
        }
    }
}
