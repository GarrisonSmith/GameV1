using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.graphics;

namespace Fantasy.Content.Logic.entities
{
    class Character
    {
        public string id;
        public Texture2D spritesheet;
        public bool characterIsMoving;
        public int speed;
        public int layer;
        public Orientation orientation;
        public Animation frames;
        public Point position;
        public Rectangle hitBox;

        public Character(string id, Texture2D spritesheet, int speed, int layer, Orientation orientation, Point position)
        {
            this.id = id;
            this.spritesheet = spritesheet;
            this.speed = speed;
            this.layer = layer;
            this.orientation = orientation;
            this.position = position;
        }

        public void DrawCharacter( SpriteBatch _spriteBatch)
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
                frames.DrawNextFrame(spritesheet, _spriteBatch, new Vector2((position.X - 30), position.Y));
            }
            else
            {
                frames.FinishAnimation(spritesheet, _spriteBatch, new Vector2((position.X - 30), position.Y));
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
}
