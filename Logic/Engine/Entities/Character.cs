using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Physics;

namespace Fantasy.Logic.Engine.entities
{
    public class Character : Entity
    {
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
            frames = new Animation(400, 400, 0, 3, 0, 0, 64, 128, AnimationState.idle);
        }

        public void DrawCharacter()
        {
            frames.ChangeOrientation(orientation);

            if (movement != EntityMovementState.idle)
            {
                frames.animationState = AnimationState.cycling;
            }
            else if (frames.animationState != AnimationState.idle)
            {
                frames.animationState = AnimationState.finishing;
            }

            frames.DrawAnimation(hitbox.GetPosition(), spriteSheet, Color.White);
        }
    }
}
