using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Engine.Drawing.Animating.Frames
{
	/// <summary>
	/// Represents a single dependent frame of an animation.
	/// Requires infomation from a greater <see cref="Animation"/> object to be drawn.
	/// </summary>
	public readonly struct DependentFrame : IFrame
	{
		private readonly int minDurationMili;
		private readonly int maxDurationExtension;
		private readonly Vector2 offSet;

		/// <summary>
		/// Gets the minimum duration of the frame in milliseconds.
		/// </summary>
		public int MinDurationMili
		{
			get => minDurationMili;
		}
		/// <summary>
		/// Get the maximum about the frame can be extended beyond its minimum duration in milliseconds.
		/// </summary>
		public int MaxDurationExtension
		{
			get => maxDurationExtension;
		}
		/// <summary>
		/// Gets the offset of the frame relative to its position in the animation.
		/// </summary>
		public Vector2 OffSet
		{
			get => offSet;
		}
		/// <summary>
		/// Gets the vertical offset of the frame relative to its position in the animation.
		/// </summary>
		public float VerticalOffset
		{
			get => offSet.Y;
		}
		/// <summary>
		/// Gets the horizontal offset of the frame relative to its position in the animation.
		/// </summary>
		public float HorizontalOffseet
		{
			get => offSet.X;
		}

		/// <summary>
		/// Creates a new frame with the specified minimum and maximum durations, source box, and spritesheet.
		/// </summary>
		/// <param name="minDurationMili">The minimum duration of the frame, in milliseconds.</param>
		/// <param name="maxDurationExtension">The chance for the frame to end after the frame has existed for the minimum duration in milliseconds.</param>
		public DependentFrame(int minDurationMili, byte maxDurationExtension)
		{
			this.minDurationMili = minDurationMili;
			this.maxDurationExtension = maxDurationExtension;
			offSet = new Vector2();
		}
		/// <summary>
		/// Creates a new frame with the specified minimum and maximum durations, offset, source box, and spritesheet.
		/// </summary>
		/// <param name="minDurationMili">The minimum duration of the frame, in milliseconds.</param>
		/// <param name="maxDurationExtension">The chance for the frame to end after the frame has existed for the minimum duration in milliseconds.</param>
		/// <param name="offSet">The offset of the frame, in pixels.</param>
		public DependentFrame(int minDurationMili, byte maxDurationExtension, Vector2 offSet)
		{
			this.minDurationMili = minDurationMili;
			this.maxDurationExtension = maxDurationExtension;
			this.offSet = offSet;
		}

		/// <summary>
		/// Draws the frame at the specified coordinates with the specified color.
		/// </summary>
		/// <param name="spritesheet">The texture containing the sprite sheet.</param>
		/// <param name="coordinates">The coordinates at which to draw the frame.</param>
		/// <param name="sourceBox">The source rectangle of the frame on the sprite sheet.</param>
		/// <param name="color">The color to use when drawing the frame.</param>
		public void DrawFrame(Texture2D spritesheet, Coordinates coordinates, Rectangle sourceBox, Color color)
		{
			SpriteBatchHandler.Draw(spritesheet, coordinates.TopLeft + OffSet, sourceBox, color);
		}
	}
}