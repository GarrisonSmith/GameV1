using Fantasy.Engine.Drawing.Animating.Frames;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using System;

namespace Fantasy.Engine.Drawing.Animating
{
	/// <summary>
	/// A class that defines a collection of <see cref="SpritesheetFrame"/> for an animation and manages the animation logic.
	/// </summary>
	public class SpritesheetAnimation : Animation
	{
		private readonly int minDurationMili;
		private readonly int maxDurationExtensionMili;
		private Rectangle sheetBox;
		private readonly SpritesheetFrame[] frames;

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
		public SpritesheetAnimation(ISubDrawable animatedSubject, SpritesheetFrame[] frames, byte startingFrameIndex = 0)
		{
			this.animatedSubject = animatedSubject;
			sheetBox = animatedSubject.SheetBox;
			this.frames = frames;
			activeFrameIndex = startingFrameIndex;
			if (startingFrameIndex >= frames.Length)
			{
				activeFrameIndex = 0;
			}
			sheetBox.X = AnimatedSubject.TextureSourceTopLeft.X + (activeFrameIndex * sheetBox.Width);
			currentFrameDuration = TimeSpan.Zero;
			currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, minDurationMili + Random.Next(maxDurationExtensionMili));
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
				sheetBox.X = AnimatedSubject.TextureSourceTopLeft.X + (activeFrameIndex * sheetBox.Width);
				currentFrameDuration = TimeSpan.Zero;
				currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, minDurationMili + Random.Next(maxDurationExtensionMili));
			}
		}
		/// <summary>
		/// Draws the currently active frame of the animation at the specified coordinates and color.
		/// </summary>
		/// <param name="coordinates">The coordinates at which to draw the frame.</param>
		/// <param name="color">The color to apply to the frame.</param>
		public void DrawCurrentFrame(Coordinates coordinates, Color color)
		{
			frames[ActiveFrameIndex].DrawFrame(AnimatedSubject.Spritesheet, coordinates, sheetBox, color);
		}
	}
}