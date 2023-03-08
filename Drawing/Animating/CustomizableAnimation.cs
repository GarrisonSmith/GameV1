using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Fantasy.Engine.Drawing.Animating.Frames;
using System;

namespace Fantasy.Engine.Drawing.Animating
{
	/// <summary>
	/// A class that defines a collection of <see cref="IndependentFrame"/> for an animation and manages the animation logic.
	/// </summary>
	public class CustomizableAnimation : Animation
	{
		private IndependentFrame[] frames;

		/// <summary>
		/// Gets the array of frames that define the animation.
		/// </summary>
		public IndependentFrame[] Frames
		{
			get => frames;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomizableAnimation"/> class.
		/// </summary>
		/// <param name="animatedSubject">The object that will be animated.</param>
		/// <param name="frames">The collection of frames for the animation.</param>
		/// <param name="startingFrameIndex">The index of the initial active frame.</param>
		public CustomizableAnimation(ILocatable animatedSubject, IndependentFrame[] frames, byte startingFrameIndex = 0)
		{ 
			this.animatedSubject = animatedSubject;
			this.frames = frames;
			activeFrameIndex = startingFrameIndex;
			if (startingFrameIndex >= frames.Length)
			{
				activeFrameIndex = 0;
			}
			currentFrameDuration = TimeSpan.Zero;
			currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, frames[activeFrameIndex].MinDurationMili + Random.Next(frames[activeFrameIndex].MaxDurationExtension));
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
				currentFrameMaxDuration = new TimeSpan(0, 0, 0, 0, frames[activeFrameIndex].MinDurationMili + Random.Next(frames[activeFrameIndex].MaxDurationExtension));
			}
		}
		/// <summary>
		/// Draws the currently active frame of the animation at the specified coordinates and color.
		/// </summary>
		/// <param name="coordinates">The coordinates at which to draw the frame.</param>
		/// <param name="color">The color to apply to the frame.</param>
		public void DrawCurrentFrame(Coordinates coordinates, Color color)
		{
			frames[ActiveFrameIndex].DrawFrame(coordinates, color);
		}
	}
}