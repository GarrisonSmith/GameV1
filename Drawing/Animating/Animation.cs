using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Fantasy.Engine.Drawing.Animating
{
	/// <summary>
	/// An abstract class representing an animation for a <see cref="ISubDrawable"/> subject.
	/// </summary>
	public abstract class Animation
	{
		private readonly static Random random = new();
		private readonly static List<Animation> activeAnimations = new();

		/// <summary>
		/// Random object used throughout animation and frame logic.
		/// </summary>
		public static Random Random
		{
			get => random;
		}
		/// <summary>
		/// List containing all active animations that currently exist.
		/// </summary>
		public static List<Animation> ActiveAnimations {
			get => activeAnimations;
		}
		/// <summary>
		/// Updates all active animations.
		/// </summary>
		public static void UpdateActiveAnimations(GameTime gameTime)
		{
			foreach (Animation foo in ActiveAnimations)
			{
				if (foo is SpritesheetAnimation)
				{
					((SpritesheetAnimation)foo).Update(gameTime);
				}
			}
		}

		protected bool active;
		protected ISubDrawable animatedSubject;
		protected byte activeFrameIndex;
		protected TimeSpan currentFrameDuration;
		protected TimeSpan currentFrameMaxDuration;

        /// <summary>
        /// Describes if the animation is active and being updated.
        /// Instantiated to true when all Animation object are created.
        /// </summary>
        public bool Active
		{
			get => active;
			set {
				if (active == value)
				{
					return;
				}
				
				active = value;
                if (active)
                {
                    ActiveAnimations.Add(this);
                }
                else
                {
                    ActiveAnimations.Remove(this);
                }
            }
		}
		/// <summary>
		/// The subject being animated.
		/// </summary>
		public ISubDrawable AnimatedSubject
		{
			get => animatedSubject;
		}
		/// <summary>
		/// The index of the currently active frame in the animation.
		/// </summary>
		public byte ActiveFrameIndex
		{
			get => activeFrameIndex;
		}
		/// <summary>
		/// The amount of time the current frame has been active.
		/// </summary>
		public TimeSpan CurrentFrameDuration
		{
			get => currentFrameDuration;
		}
		/// <summary>
		/// The total amount of time the current frame will be active for.
		/// </summary>
		public TimeSpan CurrentFrameMaxDuration
		{ 
			get => currentFrameMaxDuration;
		}
		
		/// <summary>
		/// Generic inherited constructor.
		/// </summary>
		public Animation()
		{
			active = true;
			ActiveAnimations.Add(this);
		}
	}
}