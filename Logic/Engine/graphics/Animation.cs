using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.entities;

namespace Fantasy.Logic.Engine.graphics
{
    /// <summary>
    /// Defines and manages a sprites animations.
    /// </summary>
    class Animation
    {
        /// <summary>
        /// Freezes any and all animations on whatever current frame they are drawing.
        /// </summary>
        public static bool freezeAllAnimations = false;
        
        /// <summary>
        /// Freeze this animation on whatever current frame is being drawn.
        /// </summary>
        public bool freezeAnimation = false;
        /// <summary>
        /// How long (in miliseconds) the current frame will be drawn for. Randomized between minFrameDuration and maxFrameDuration for every new animation frame.
        /// </summary>
        public double frameDuration;
        /// <summary>
        /// The lower bound for how long a animation frame will be drawn for.
        /// </summary>
        public int minFrameDuration;
        /// <summary>
        /// The upper bound for how long a animation frame will be drawn for.
        /// </summary>
        public int maxFrameDuration;
        /// <summary>
        /// The game time (in miliseconds) the current animation frame began being drawn on.
        /// </summary>
        public double lastFrameGameTime;
        /// <summary>
        /// The horizontal position on the spitesheet the current frame occupies.
        /// </summary>
        public int currentFrame;
        /// <summary>
        /// The maximum number of frames the reference spirtsheet offers.
        /// </summary>
        public int maxFrame;
        /// <summary>
        /// The row on the reference spritesheet this animation is using.
        /// </summary>
        public int rowReference;
        /// <summary>
        /// The column on the referece spritesheet this animation is using.
        /// </summary>
        public int columnReference;
        /// <summary>
        /// The row this animation will switch to reference once the current frame has finished its duration.
        /// </summary>
        public int newRow;
        /// <summary>
        /// The width of the animation frame to be drawn.
        /// </summary>
        public int sourceWidth;
        /// <summary>
        /// The height of the animation frame to be drawn.
        /// </summary>
        public int sourceHeight;
        /// <summary>
        /// The current state of the animation.
        /// </summary>
        public AnimationState animationState;

        /// <summary>
        /// Creates a animation with the provided parameters.
        /// </summary>
        /// <param name="minFrameDuration">The lower bound for how long this animations frames will be drawn for.</param>
        /// <param name="maxFrameDuration">The upper bound for how long this animations frames will be drawn for.</param>
        /// <param name="startingFrame">The first frame from the reference spritesheet this animation will draw.</param>
        /// <param name="maxFrame">The maximum number of frames the reference spirtsheet offers this animation.</param>
        /// <param name="rowReference">The row on the reference spritesheet this animation will be using.</param>
        /// <param name="columnReference">The column on the referece spritesheet this animation will be using.</param>
        /// <param name="sourceWidth">The width of the animation frame to be drawn by this animation.</param>
        /// <param name="sourceHeight">The height of the animation frame to be drawn by this animation.</param>
        /// <param name="animationState">The starting state of the animation.</param>
        public Animation(int minFrameDuration, int maxFrameDuration, int startingFrame, int maxFrame, int rowReference, int columnReference, int sourceWidth, int sourceHeight, AnimationState animationState)
        {
            this.minFrameDuration = minFrameDuration;
            this.maxFrameDuration = maxFrameDuration;
            frameDuration = new Random().Next(minFrameDuration, maxFrameDuration);
            currentFrame = startingFrame;
            this.maxFrame = maxFrame;
            this.rowReference = newRow = rowReference;
            this.columnReference = columnReference;
            this.sourceWidth = sourceWidth;
            this.sourceHeight = sourceHeight;
            this.animationState = animationState;
        }
        /// <summary>
        /// Draws the provided texture using the paremeters of this animation.
        /// </summary>
        /// <param name="position">Point describing where this aniamtion will be drawn.</param>
        /// <param name="texture">The referece spritesheet this animations paremeters will be applied to.</param>
        /// <param name="color">The color to be applied to this animation when drawn.</param>
        public void DrawAnimation(Point position, Texture2D texture, Color color)
        {
            if (!freezeAllAnimations && !freezeAnimation)
            {
                switch (animationState)
                {
                    case AnimationState.cycling:
                        if (Global._gameTime.TotalGameTime.TotalMilliseconds - lastFrameGameTime >= frameDuration)
                        {
                            frameDuration = new Random().Next(minFrameDuration, maxFrameDuration);
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
                            frameDuration = new Random().Next(minFrameDuration, maxFrameDuration);
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
            }
            else 
            {
                lastFrameGameTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
            }
            Global._spriteBatch.Draw(
                texture, new Vector2(position.X * Global._baseStretch.X, -position.Y * Global._baseStretch.Y),
                new Rectangle(((currentFrame + columnReference) * sourceWidth), (rowReference * sourceHeight), sourceWidth, sourceHeight),
                color, 0, new Vector2(0, 0),Global._baseStretch, new SpriteEffects(), 0);

        }
        /// <summary>
        /// Changes the orientation of this animation. Used by animated entities.
        /// </summary>
        /// <param name="orientation">The new orientation of this animation.</param>
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

    /// <summary>
    /// Defines different states a animation can be in for drawing.
    /// </summary>
    public enum AnimationState
    {
        cycling,
        finishing,
        idle
    }
}