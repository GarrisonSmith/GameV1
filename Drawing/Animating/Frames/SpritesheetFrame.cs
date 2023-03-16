using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Fantasy.Engine.Drawing.Animating.Frames
{
	/// <summary>
	/// Represents a single dependent frame of an animation.
	/// Only contains information about the offset of the frame and requires information from a greater <see cref="Animation"/> object to be drawn.
	/// </summary>
	public readonly struct SpritesheetFrame
	{
		private readonly Vector2 offSet;

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
		public SpritesheetFrame()
		{
			offSet = new Vector2();
		}
        /// <summary>
        /// Creates a new frame with the specified minimum and maximum durations, offset, source box, and spritesheet.
        /// </summary>
        /// <param name="frameElement">XmlElement describe this frame.</param>
        public SpritesheetFrame(XmlElement frameElement)
		{
			offSet = new Vector2(float.Parse(frameElement.GetAttribute("verticalOffset")), float.Parse(frameElement.GetAttribute("horizontalOffset")));
		}

		/// <summary>
		/// Draws the frame at the specified BoundingBox2 with the specified color.
		/// </summary>
		/// <param name="spritesheet">The texture containing the sprite sheet.</param>
		/// <param name="BoundingBox2">The BoundingBox2 at which to draw the frame.</param>
		/// <param name="sourceBox">The source rectangle of the frame on the sprite sheet.</param>
		/// <param name="color">The color to use when drawing the frame.</param>
		public void DrawFrame(Texture2D spritesheet, BoundingBox2 BoundingBox2, Rectangle sourceBox, Color color)
		{
			SpriteBatchHandler.Draw(spritesheet, BoundingBox2.TopLeft + OffSet, sourceBox, color);
		}
	}
}