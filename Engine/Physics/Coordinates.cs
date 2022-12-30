using Microsoft.VisualBasic;

namespace Fantasy.Engine.Physics
{
    /// <summary>
    /// Describes the coordinates of a position.
    /// </summary>
    internal class Coordinates
    {
        /// <summary>
        /// The top left coordinates of a position.
        /// </summary>
        internal (int X, int Y) TopLeft {  get; set; }

        /// <summary>
        /// The center coordinates of a position.
        /// </summary>
        internal (int X, int Y) Center { get; set; }

        /// <summary>
        /// Creates a Coordinates object with the provided parameters.
        /// </summary>
        /// <param name="XTopLeft">The X value of the top left position.</param>
        /// <param name="YTopLeft">The Y value of the top left position.</param>
        /// <param name="XCenter">The X value of the center position.</param>
        /// <param name="YCenter">The Y value of the center position.</param>
        internal Coordinates(int XTopLeft, int YTopLeft, int XCenter, int YCenter)
        {
            TopLeft = (XTopLeft, YTopLeft);
            Center = (XCenter, YCenter);
        }

        public override string ToString()
        { 
            return "TopLeft: " + TopLeft + ", Center: " + Center;
        }
    }
}
