using Fantasy.Engine.Drawing.Animating.Frames;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Fantasy.Engine.Drawing.Animating
{
	internal class SpritesheetAnimation : Animation
	{
		private int minDurationMili;
		private int maxDurationExtension;
		private Point firstFrameTopLeft;
		private Rectangle sourceBox;
		private Texture2D spritesheet;
		private SpritesheetFrame[] frames;

		public Rectangle SourceBox
		{
			get => sourceBox;
		}

		public Texture2D Spritesheet
		{ 
			get => spritesheet;
		}
		/// <summary>
		/// Gets the array of frames that define the animation.
		/// </summary>
		public SpritesheetFrame[] Frames
		{
			get => frames;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpritesheetAnimation"/> class.
		/// </summary>
		/// <param name="animatedSubject">The object that will be animated.</param>
		/// <param name="frames">The collection of frames for the animation.</param>
		/// <param name="startingFrameIndex">The index of the initial active frame.</param>
		public SpritesheetAnimation(ILocatable animatedSubject, SpritesheetFrame[] frames, byte startingFrameIndex = 0)
		{
			this.animatedSubject = animatedSubject;
			this.frames = frames;
			activeFrameIndex = startingFrameIndex;
			if (startingFrameIndex >= frames.Length)
			{
				activeFrameIndex = 0;
			}
			currentFrameDuration = TimeSpan.Zero;
			currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, minDurationMili + Random.Next(maxDurationExtension));
		}
		/// <summary>
		/// Updates the current animation frame based on the elapsed game time.
		/// </summary>
		/// <param name="gameTime">The game time elapsed since the last update.</param>
		public void Update(GameTime gameTime)
		{
			currentFrameDuration += gameTime.ElapsedGameTime;
			if (CurrentFrameDuration >= CurrentFrameMaxDuration)
			{
				activeFrameIndex++;
				if (activeFrameIndex >= frames.Length)
				{
					activeFrameIndex = 0;
				}
				currentFrameDuration = TimeSpan.Zero;
				currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, minDurationMili + Random.Next(maxDurationExtension));
			}
		}
		/// <summary>
		/// Draws the currently active frame of the animation at the specified coordinates and color.
		/// </summary>
		/// <param name="coordinates">The coordinates at which to draw the frame.</param>
		/// <param name="color">The color to apply to the frame.</param>
		public void DrawCurrentFrame(Coordinates coordinates, Color color)
		{
			frames[ActiveFrameIndex].DrawFrame(spritesheet, coordinates, sourceBox, color);
		}
	}
}
