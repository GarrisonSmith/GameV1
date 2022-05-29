using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.utility
{
    static class Util
    {
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

        public static Point PointFromString(string foo)
        {
            return new Point(
                int.Parse(foo.Substring(foo.IndexOf('X') + 2, foo.IndexOf(' ') - (foo.IndexOf('X') + 2))),
                int.Parse(foo.Substring(foo.IndexOf('Y') + 2, foo.IndexOf('}') - (foo.IndexOf('Y') + 2)))
                );
        }
    }
}
