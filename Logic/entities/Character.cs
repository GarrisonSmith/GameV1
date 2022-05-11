using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.Graphics;

namespace Fantasy.Content.Logic.Entities
{
    class Character
    {
        public string id;
        public Texture2D spritesheet;
        public bool characterIsMoving;
        public int speed;
        public Orientation orientation;
        public Animation frames;
        public Point position;
        public Rectangle hitBox;

        public Character(Texture2D spritesheet, Point position, string id, int speed, Orientation orientation)
        {
            this.spritesheet = spritesheet;
            this.position = position;
            this.id = id;
            this.speed = speed;
            this.orientation = orientation;
        }

        public void DrawCharacter(Vector2 stretch, SpriteBatch _spriteBatch, int layer)
        {
            if (frames == null)
            {
                switch (orientation)
                {
                    case Orientation.forward:
                        frames = new Animation(0, 3);
                        break;
                    case Orientation.right:
                        frames = new Animation(1, 3);
                        break;
                    case Orientation.left:
                        frames = new Animation(2, 3);
                        break;
                    case Orientation.backward:
                        frames = new Animation(3, 3);
                        break;
                }
            }

            if (characterIsMoving)
            {
                frames.DrawNextFrame(spritesheet, stretch, _spriteBatch, layer, new Vector2((position.X - 30), position.Y));
            }
            else
            {
                frames.FinishAnimation(spritesheet, stretch, _spriteBatch, layer, new Vector2((position.X - 30), position.Y));
            }
        }

        public void MoveCharacter(Orientation direction)
        {
            characterIsMoving = true;
            switch (direction)
            {
                case Orientation.forward:
                    position.Y += speed;
                    break;
                case Orientation.right:
                    position.X += speed;
                    break;
                case Orientation.left:
                    position.X -= speed;
                    break;
                case Orientation.backward:
                    position.Y -= speed;
                    break;
            }
            if (direction != orientation)
            {
                frames = null;
                orientation = direction;
            }
        }

        public void StopCharacter()
        {
            characterIsMoving = false;
        }
    }

    public enum Orientation
    {
        forward,
        right,
        left,
        backward
    }
}
