using Microsoft.Xna.Framework;

namespace Fantasy.Engine.Drawing.Animating.Frames
{
	/// <summary>
	/// Defines a contract frame that can be used by frame objects.
	/// </summary>
	public interface IFrame
	{
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
