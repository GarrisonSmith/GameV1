using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.entities;

namespace Fantasy.Content.Logic.graphics
{
    class Animation
    {
        public double frameDuration;
        public double lastFrameGameTime;
        public int currentFrame;
        public int maxFrame;
        public int rowReference;
        public int columnReference;
        public int newRow;
        public int sourceWidth;
        public int sourceHeight;
        public AnimationState animationState;

        public Animation(double frameDuration, int startingFrame, int maxFrame, int rowReference, int columnReference, int sourceWidth, int sourceHeight, AnimationState animationState)
        {
            this.frameDuration = frameDuration;
            currentFrame = startingFrame;
            this.maxFrame = maxFrame;
            this.rowReference = newRow = rowReference;
            this.columnReference = columnReference;
            this.sourceWidth = sourceWidth;
            this.sourceHeight = sourceHeight;
            this.animationState = animationState;
        }

        public void DrawAnimation(Point position, Texture2D texture, Color color, Vector2 _stretch)
        {
            switch (animationState)
            {
                case AnimationState.cycling:
                    if (Global._gameTime.TotalGameTime.TotalMilliseconds - lastFrameGameTime >= frameDuration)
                    {
                        lastFrameGameTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        if (currentFrame < maxFrame)
                        {
                            currentFrame++;
                        }
                        else
                        {
                            currentFrame = 0;
                        }
                    }
                    rowReference = newRow;
                    break;

                case AnimationState.finishing:
                    if (Global._gameTime.TotalGameTime.TotalMilliseconds - lastFrameGameTime >= frameDuration)
                    {
                        lastFrameGameTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        if (currentFrame < maxFrame)
                        {
                            currentFrame++;
                        }
                        else
                        {
                            currentFrame = 0;
                        }
                        currentFrame -= (currentFrame % 2);
                        animationState = AnimationState.idle;
                    }
                    rowReference = newRow;
                    break;

                case AnimationState.idle:
                    //changes nothing about what is being drawn.
                    break;
            }
            Global._spriteBatch.Draw(texture,
                new Vector2(position.X * _stretch.X, -position.Y * _stretch.Y),
                new Rectangle(((currentFrame + columnReference) * sourceWidth), (rowReference * sourceHeight), sourceWidth, sourceHeight),
                color, 0, new Vector2(0, 0),
                _stretch, new SpriteEffects(), 0);
        }

        public void ChangeOrientation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.up:
                    newRow = 3;
                    break;
                case Orientation.right:
                    newRow = 1;
                    break;
                case Orientation.left:
                    newRow = 2;
                    break;
                case Orientation.down:
                    newRow = 0;
                    break;
            }
        }
    }

    public enum AnimationState
    {
        cycling,
        finishing,
        idle
    }
}
