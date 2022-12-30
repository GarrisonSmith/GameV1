using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.Physics
{
    internal interface ILocatable
    {
        internal double Distance(Coordinates foo);

        internal Tuple<int, int> GetTopLeft();

        internal Tuple<int, int> GetCenter();
    }
}
