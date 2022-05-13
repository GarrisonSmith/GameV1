using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.graphics
{
    class Animation
    {
        public int rowReference = 0;
        public int currentFrame = 0;
        public int maxFrame = 0;
        public int frameMinDrawAmount = 15;
        public int frameIncrement = 10;
        public Rectangle drawArea = new Rectangle(0, 0, 64, 128);

        public Animation(int rowReference, int maxFrame)
        {
            this.rowReference = rowReference;
            this.maxFrame = maxFrame;
            this.drawArea = new Rectangle(currentFrame * 64, rowReference * 128, 64, 128);
        }

        public Animation(int rowReference, int maxFrame, int currentFrame) : this(rowReference, maxFrame)
        {
            this.currentFrame = currentFrame;
        }

        public void DrawNextFrame(Texture2D spritesheet,  SpriteBatch _spriteBatch, int layer, Vector2 position)
        {
            if (frameIncrement < frameMinDrawAmount)
            {
                _spriteBatch.Draw(spritesheet, position, drawArea, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), layer);
                frameIncrement++;
            }
            else
            {
                frameIncrement = 0;
                if (currentFrame + 1 <= maxFrame)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame = 0;
                }
                drawArea = new Rectangle(currentFrame * 64, rowReference * 128, 64, 128);
                _spriteBatch.Draw(spritesheet, position, drawArea, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), layer);
            }
        }
        public void FinishAnimation(Texture2D spritesheet,  SpriteBatch _spriteBatch, int layer, Vector2 position)
        {
            if (frameIncrement < frameMinDrawAmount)
            {
                _spriteBatch.Draw(spritesheet, position, drawArea, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), layer);
                frameIncrement++;
            }
            else
            {
                frameIncrement = 10;
                drawArea = new Rectangle(0, rowReference * 128, 64, 128);
                _spriteBatch.Draw(spritesheet, position, drawArea, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), layer);
            }
        }
    }
}
