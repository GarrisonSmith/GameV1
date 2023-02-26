using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Fantasy.Engine.Drawing.Animating.Frames
{
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
		/// Gets the chance for the frame to end after the frame has existed for the minimum duration in milliseconds. 
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
