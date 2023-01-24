using Microsoft.Xna.Framework;
using System;

namespace Fantasy.Engine.Physics
{
	/// <summary>
	/// Represents a two-dimensional shape defined by a top-left point and a center point.
	/// </summary>
	internal class Coordinates
	{
		private Vector2 topLeft;
		private Vector2 center;

		/// <summary>
		/// The top-left point of the shape.
		/// </summary>
		internal Vector2 TopLeft
		{
			get => topLeft;
			set
			{
				if (value.X > center.X && value.Y < center.Y)
				{
					throw new ArgumentException("Coordinate TopLeft value must be to the top left of coordinate Center value.");
				}

				center.X += (value.X - topLeft.X);
				center.Y += (value.Y - topLeft.Y);
				topLeft = value;
			}
		}
		/// <summary>
		/// The center point of the shape.
		/// </summary>
		internal Vector2 Center
		{
			get => center;
			set
			{
				if (value.X < topLeft.X && value.Y > topLeft.Y)
				{
					throw new ArgumentException("Coordinate Center value must be to the bottom right of coordinate TopLeft value.");
				}

				topLeft.X += (value.X - center.X);
				topLeft.Y += (value.Y - center.Y);
				center = value;
			}
		}
		/// <summary>
		/// The width of the shape, calculated as the distance between the center and top-left point multiplied by 2.
		/// </summary>
		internal int Width
		{
			get => (int)(Center.X - TopLeft.X) * 2;
		}
		/// <summary>
		/// The height of the shape, calculated as the distance between the center and top-left point multiplied by 2.
		/// </summary>
		internal int Height
		{
			get => (int)(Center.Y - TopLeft.Y) * 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Coordinates"/> class with the specified top-left and center points.
		/// </summary>
		/// <param name="topLeftX">The x-coordinate of the top-left point.</param>
		/// <param name="topLeftY">The y-coordinate of the top-left point.</param>
		/// <param name="centerX">The x-coordinate of the center point.</param>
		/// <param name="centerY">The y-coordinate of the center point.</param>
		/// <exception cref="ArgumentException">Thrown if the top-left point is not to the top and left of the center point.</exception>
		internal Coordinates(float topLeftX, float topLeftY, float centerX, float centerY)
		{
			if (topLeftX > centerX && topLeftY < centerY)
			{
				throw new ArgumentException("Coordinate TopLeft value must be to the top left of coordinate Center value.");
			}

			topLeft = new Vector2(topLeftX, topLeftY);
			center = new Vector2(centerX, centerY);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Coordinates"/> class with the specified top-left and center points.
		/// </summary>
		/// <param name="topLeft">The top-left point of the shape.</param>
		/// <param name="center">The center point of the shape.</param>
		/// <exception cref="ArgumentException">Thrown if the top-left point is not to the top and left of the center point.</exception>
		internal Coordinates(Vector2 topLeft, Vector2 center)
		{
			if (topLeft.X > center.X && topLeft.Y < center.Y)
			{
				throw new ArgumentException("Coordinate TopLeft value must be to the top left of coordinate Center value.");
			}

			this.topLeft = topLeft;
			this.center = center;
		}

		/// <summary>
		/// Moves the shape up by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to move the shape up by.</param>
		internal void MoveUp(float amount)
		{
			topLeft.Y += amount;
			center.Y += amount;
		}
		/// <summary>
		/// Moves the shape down by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to move the shape down by.</param>
		internal void MoveDown(float amount)
		{
			topLeft.Y -= amount;
			center.Y -= amount;
		}
		/// <summary>
		/// Moves the shape right by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to move the shape right by.</param>
		internal void MoveRight(float amount)
		{
			topLeft.X += amount;
			center.X += amount;
		}
		/// <summary>
		/// Moves the shape left by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to move the shape left by.</param>
		internal void MoveLeft(float amount)
		{
			topLeft.X -= amount;
			center.X -= amount;
		}
		/// <summary>
		/// Returns a string representation of the Coordinates object.
		/// </summary>
		public override string ToString()
		{
			return "TopLeft: " + topLeft + ", Center: " + center;
		}
		/// <summary>
		/// Compares if this Coordinates instance is equal with another.
		/// </summary>
		/// <param name="obj">The object to compare equality with.</param>
		/// <returns>True if this Coordinates object is equivalent to another, False if not.</returns>
		public override bool Equals(object obj)
		{
			Coordinates cord = obj as Coordinates;
			return (TopLeft == cord.TopLeft && Center == cord.center);
		}
	}
}
