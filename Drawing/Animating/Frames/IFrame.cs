using Microsoft.Xna.Framework;

namespace Fantasy.Engine.Drawing.Animating.Frames
{
	/// <summary>
	/// Defines a contract frame that can be used by frame objects.
	/// </summary>
	public interface IFrame
	{
		/// <summary>
		/// Gets the minimum duration of the frame in milliseconds.
		/// </summary>
		public int MinDurationMili { get; }
		/// <summary>
		/// Get the maximum about the frame can be extended beyond its minimum duration in milliseconds.
		/// </summary>
		public int MaxDurationExtension { get; }
		/// <summary>
		/// Gets the offset of the frame relative to its position in the animation.
		/// </summary>
		public Vector2 OffSet { get; }
		/// <summary>
		/// Gets the vertical offset of the frame relative to its position in the animation.
		/// </summary>
		public float VerticalOffset { get; }
		/// <summary>
		/// Gets the horizontal offset of the frame relative to its position in the animation.
		/// </summary>
		public float HorizontalOffseet { get; }

	}
}
