using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.Logic.Drawing
{
    internal interface IDrawable2
    {
        internal void Draw((int x, int y) location);

        internal void Draw(Coordinates coordinates);
    }
}
