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
        }
        /// <summary>
        /// The center point of the shape.
        /// </summary>
        internal Vector2 Center
        {
            get => center;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinates"/> class with the specified top-left and center points.
        /// </summary>
        /// <param name="xTopLeft">The x-coordinate of the top-left point.</param>
        /// <param name="yTopLeft">The y-coordinate of the top-left point.</param>
        /// <param name="xCenter">The x-coordinate of the center point.</param>
        /// <param name="yCenter">The y-coordinate of the center point.</param>
        /// <exception cref="ArgumentException">Thrown if the top-left point is not to the top and left of the center point.</exception>
        internal Coordinates(float xTopLeft, float yTopLeft, float xCenter, float yCenter)
        {
            if (xTopLeft > xCenter && yTopLeft < yCenter)
            {
                throw new ArgumentException("Coordinate TopLeft value must be to the top left of coordinate Center value.");
            }
            
            topLeft = new Vector2(xTopLeft, yTopLeft);
            center = new Vector2(xCenter, yCenter);
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
        /// Sets the top left coordinate of the shape.
        /// </summary>
        /// <param name="xTopLeft">The x-coordinate of the top left point.</param>
        /// <param name="yTopLeft">The y-coordinate of the top left point.</param>
        /// <exception cref="ArgumentException">Thrown if the top left point is not to the top left of the center point.</exception>
        internal void SetTopLeft(float xTopLeft, float yTopLeft)
        {
            if (xTopLeft > center.X && yTopLeft < center.Y)
            {
                throw new ArgumentException("Coordinate TopLeft value must be to the top left of coordinate Center value.");
            }

            center.X += (xTopLeft - topLeft.X); center.Y += (yTopLeft - topLeft.Y);
            topLeft.X = xTopLeft; topLeft.Y = yTopLeft;
        }
        /// <summary>
        /// Sets the center coordinate of the shape.
        /// </summary>
        /// <param name="xCenter">The x-coordinate of the center point.</param>
        /// <param name="yCenter">The y-coordinate of the center point.</param>
        /// <exception cref="ArgumentException">Thrown if the center point is not to the bottom right of the top left point.</exception>
        internal void SetCenter(float xCenter, float yCenter)
        {
            if (xCenter < topLeft.X && yCenter > topLeft.Y)
            {
                throw new ArgumentException("Coordinate Center value must be to the bottom right of coordinate TopLeft value.");
            }

            topLeft.X += (xCenter - center.X);
            topLeft.Y += (yCenter - center.Y);
            center.X = xCenter;
            center.Y = yCenter;
        }
        /// <summary>
        /// Returns a string representation of the Coordinates object.
        /// </summary>
        public override string ToString()
        { 
            return "TopLeft: " + topLeft + ", Center: " + center;
        }
    }
}
