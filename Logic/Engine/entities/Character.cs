using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.graphics.tilemap;

namespace Fantasy.Logic.Engine.entities
{
    class Character : Entity
    {
        public bool characterIsMoving;
        public int speed;
        public Orientation orientation;
        public Animation frames;

        public Character() { }

        public Character(string id, string type, string spriteSheetName, int layer, Entitybox hitbox, int speed, Orientation orientation)
        {
            this.id = id;
            this.type = type;
            this.spriteSheetName = spriteSheetName;
            this.layer = layer;
            this.speed = speed;
            this.orientation = orientation;
            this.hitbox = hitbox;
            this.hitbox.collisionArea = new Rectangle[] { new Rectangle(8, 116, 48, 16) };

            spriteSheet = Global._content.Load<Texture2D>("character-sets/" + spriteSheetName);
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

            if (characterIsMoving)
            {
                frames.animationState = AnimationState.cycling;
            }
            else if (frames.animationState != AnimationState.idle)
            {
                frames.animationState = AnimationState.finishing;
            }

            if (characterIsMoving)
            {
                frames.DrawAnimation(hitbox.characterArea.Location, spriteSheet, Color.White);
            }
            else
            {
                frames.DrawAnimation(hitbox.characterArea.Location, spriteSheet, Color.White);
            }
        }

        public void MoveCharacter(Orientation direction)
        {
            characterIsMoving = true;
            int tempSpeed = speed;
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

            if (direction != orientation)
            {
                frames.ChangeOrientation(direction);
                orientation = direction;
            }
        }

        public void ForceMoveCharacter(Orientation direction)
        {
            characterIsMoving = true;
            switch (direction)
            {
                case Orientation.up:
                    hitbox.characterArea.Y += speed;
                    break;
                case Orientation.right:
                    hitbox.characterArea.X += speed;
                    break;
                case Orientation.left:
                    hitbox.characterArea.X -= speed;
                    break;
                case Orientation.down:
                    hitbox.characterArea.Y -= speed;
                    break;
            }
            if (direction != orientation)
            {
                frames.ChangeOrientation(direction);
                orientation = direction;
            }
        }
        public void SetCharacterPosition(Point posistion)
        {
            hitbox.characterArea.X = posistion.X;
            hitbox.characterArea.Y = posistion.Y;
        }

        public void StopCharacter()
        {
            characterIsMoving = false;
        }
    }
}
