using System;

namespace Fantasy.Engine.Physics
{
    internal interface ILocatable
    {
        internal double Distance(Coordinates foo);

        internal Tuple<int, int> GetTopLeft();

        internal Tuple<int, int> GetCenter();
    }
}
