using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.graphics;


namespace Fantasy.Content.Logic.entities
{
    class Character : Entity
    {
        public bool characterIsMoving;
        public int speed;
        public Orientation orientation;
        public Animation frames;

        public Character() { }

        public Character(string id, string spriteSheetName, int layer, Rectangle positionBox, int speed, Orientation orientation)
        {
            this.id = id;
            this.spriteSheetName = spriteSheetName;
            this.layer = layer;
            this.positionBox = positionBox;
            this.speed = speed;
            this.orientation = orientation;

            spriteSheet = Global._content.Load<Texture2D>("character-sets/"+spriteSheetName);
        }

        public void DrawCharacter(Vector2 _stretch, SpriteBatch _spriteBatch)
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
                frames.DrawAnimation(new Point(positionBox.X, positionBox.Y), spriteSheet, Color.White, _stretch);
            }
            else
            {
                frames.DrawAnimation(new Point(positionBox.X, positionBox.Y), spriteSheet, Color.White, _stretch);
            }
        }

        public void MoveCharacter(Orientation direction)
        {
            characterIsMoving = true;
            switch (direction)
            {
                case Orientation.up:
                    positionBox.Y += speed;
                    break;
                case Orientation.right:
                    positionBox.X += speed;
                    break;
                case Orientation.left:
                    positionBox.X -= speed;
                    break;
                case Orientation.down:
                    positionBox.Y -= speed;
                    break;
            }
            if (direction != orientation)
            {
                frames.ChangeOrientation(direction);
                orientation = direction;
            }
        }

        public void StopCharacter()
        {
            characterIsMoving = false;
        }
    }
}
