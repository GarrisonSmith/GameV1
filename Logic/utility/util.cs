using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.utility
{
    static class util
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
    }
}
