using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.Utility
{
    /// <summary>
    /// Static class containg generally useful methods used throughout the project.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Determines if the provided point foo is inside of the provided rectangle bar. A point on top of a rectangles border is inside that rectangle.
        /// </summary>
        /// <param name="foo">The point to be investigated.</param>
        /// <param name="bar">The rectangle to be investigated</param>
        /// <returns>True if foo is inside of bar, False if not.</returns>
        public static bool PointInsideRectangle(Point foo, Rectangle bar)
        {
            return (bar.X <= foo.X && foo.X <= (bar.X + bar.Width)) && (bar.Y >= foo.Y && foo.Y >= (bar.Y - bar.Height));
        }
        /// <summary>
        /// Gets the top left point of the provided rectangle foo as a Point.
        /// </summary>
        /// <param name="foo">The rectangle to be used.</param>
        /// <returns>A point containing the top left point of the provided rectangle foo.</returns>
        public static Point GetTopLeftPoint(Rectangle foo)
        {
            return new Point(foo.X, foo.Y);
        }
        /// <summary>
        /// Gets the top left point of the provided rectangle foo as a Vector2.
        /// </summary>
        /// <param name="foo">The rectangle to be used.</param>
        /// <param name="invertY">Determines if the Y value of the returned Vector is inverted.</param>
        /// <returns>A Vector2 containing the top left point of the provided rectangle foo.</returns>
        public static Vector2 GetTopLeftVector(Rectangle foo, bool invertY = false)
        {
            if (invertY)
            {
                return new Vector2(foo.X, -foo.Y);
            }
            else
            {
                return new Vector2(foo.X, foo.Y);
            }
        }
        /// <summary>
        /// Gets the center point of the provided rectangle foo.
        /// </summary>
        /// <param name="foo">The rectangle tot be used.</param>
        /// <returns>A point that is in the center of of the provided rectangle foo.</returns>
        public static Point GetCenter(Rectangle foo)
        {
            return new Point(foo.X + (foo.Width / 2), foo.Y - (foo.Height / 2));
        }
        /// <summary>
        /// Constructs a Xna Point object from the provided string foo, foo must be of the form "{X:0 Y:0}".
        /// Will return the first point in a string if other characters exist or if another point exits, example: 
        /// "Lorem {X:0 Y:0} ipsum {X:1 Y:1}" will return the point {X: 0 Y:0}.
        /// </summary>
        /// <param name="foo">The string to be referenced.</param>
        /// <returns>A point with the parameters described inside the string foo.</returns>
        public static Point PointFromString(string foo)
        {
            return new Point(
                int.Parse(foo.Substring(foo.IndexOf("X:") + 2, foo.IndexOf('Y') - (foo.IndexOf("X:") + 3))),
                int.Parse(foo.Substring(foo.IndexOf("Y:") + 2, foo.IndexOf('}') - (foo.IndexOf("Y:") + 2)))
                );
        }
        /// <summary>
        /// Constructs a Xna Rectangle object from the provided string foo, foo must be of the form "{X:0 Y:0 Width:0 Height:0}".
        /// Will return the first rectangle in a string if other characters exist or if another rectangle exits, example:
        /// "Lorem {X:0 Y:0 Width:0 Height:0} ipsum {X:1 Y:1 Width:1 Height:1}" will return the rectangle {X: 0 Y:0 Width:0 Height:0}.
        /// </summary>
        /// <param name="foo">The string to be referenced.</param>
        /// <returns>A rectangle with the parameters described inside the string foo.</returns>
        public static Rectangle RectangleFromString(string foo)
        {
            return new Rectangle(
                    int.Parse(foo.Substring(foo.IndexOf("X:") + 2, foo.IndexOf('Y') - (foo.IndexOf("X:") + 3))),
                    int.Parse(foo.Substring(foo.IndexOf("Y:") + 2, foo.IndexOf('W') - (foo.IndexOf("Y:") + 3))),
                    int.Parse(foo.Substring(foo.IndexOf("Width:") + 6, foo.IndexOf('H') - (foo.IndexOf("Width:") + 7))),
                    int.Parse(foo.Substring(foo.IndexOf("Height:") + 7, foo.IndexOf('}') - (foo.IndexOf("Height:") + 7)))
                    );
        }
        /// <summary>
        /// Determines the distance between two points.
        /// </summary>
        /// <param name="foo">A point to be investigated.</param>
        /// <param name="bar">A point to be investigated.</param>
        /// <returns>The distance between foo and bar.</returns>
        public static double DistanceBetweenPoints(Point foo, Point bar)
        {
            return Math.Sqrt(Math.Pow(foo.X - bar.X, 2) + Math.Pow(foo.Y - bar.Y, 2));
        }
        /// <summary>
        /// Creates and returns a array of points describing the perimeter of the provided rectangle.
        /// </summary>
        /// <param name="foo">The rectangle to referenced.</param>
        /// <returns>Point array containing the points on the provided rectangles perimeter.</returns>
        public static Point[] RectanglePerimeterPoints(Rectangle foo)
        {
            Point[] arr = new Point[foo.Width * 2 + foo.Height * 2];
            int index = 0;

            for (int i = 0; i < foo.Width; i++)
            {
                arr[index] = new Point(foo.X + i, foo.Y); index++;
                arr[index] = new Point(foo.X + i, foo.Y - (foo.Height - 1)); index++;
            }
            for (int i = 0; i < foo.Height; i++)
            {
                arr[index] = new Point(foo.X, foo.Y - i); index++;
                arr[index] = new Point(foo.X + (foo.Width - 1), foo.Y - i); index++;
            }

            return arr;
        }
        /// <summary>
        /// Determines if two rectangles intersect one another. Rectangles only sharing a perimeter edge return false.
        /// </summary>
        /// <param name="foo">The first rectangle to be referenced.</param>
        /// <param name="bar">The second rectangle to be referenced.</param>
        /// <returns>True if the two rectangles intersect anywhere, False if not.</returns>
        public static bool RectanglesIntersect(Rectangle foo, Rectangle bar)
        {
            return !(foo.X + foo.Width <= bar.X || bar.X + bar.Width <= foo.X || foo.Y - foo.Height >= bar.Y || bar.Y - bar.Height >= foo.Y);
        }
    }
}