using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.Logic.Mapping
{
    internal class GameMap : IGameComponent
    {
        List<MapLayer> Layers;

        internal GameMap(Game game)
        {
            Layers = new List<MapLayer>();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
