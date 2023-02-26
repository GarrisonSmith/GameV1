﻿using System;

namespace Fantasy.Engine.Drawing.Animating
{
	/// <summary>
	/// An abstract class representing an animation for a given subject.
	/// </summary>
	/// <typeparam name="T">The type of the animated subject.</typeparam>
	public abstract class Animation<T>
	{
		private readonly static Random random = new();

		/// <summary>
		/// Random object used throughout animation and frame logic.
		/// </summary>
		public static Random Random
		{
			get => random;
		}
		
		protected T animatedSubject;
		protected byte activeFrameIndex;
		protected TimeSpan currentFrameDuration;
		protected TimeSpan currentFrameMaxDuration;

		/// <summary>
		/// The subject being animated.
		/// </summary>
		public T AnimatedSubject
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
	}
}
