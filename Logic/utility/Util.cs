using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.utility
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
        public static Point GetRectangleCenter(Rectangle bar)
        {
            return new Point((bar.X + (int)(bar.Width / 2)), ((bar.Y - (int)(bar.Height / 2))));
        }
    }
}
