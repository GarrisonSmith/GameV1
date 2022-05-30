﻿using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.utility
{
    /// <summary>
    /// Static class containg generally useful methods used throughout the project.
    /// </summary>
    static class Util
    {
        /// <summary>
        /// Determines if the provided point foo is inside of the provided rectange bar.
        /// </summary>
        /// <param name="foo">The point to be investigated.</param>
        /// <param name="bar">The rectangle to be investigated</param>
        /// <returns>True if foo is inside of bar, False if not.</returns>
        public static bool PointInsideRectangle(Point foo, Rectangle bar)
        {
            if ((bar.X <= foo.X && foo.X <= (bar.X + bar.Width)) && (bar.Y >= foo.Y && foo.Y >= (bar.Y - bar.Height)))
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
