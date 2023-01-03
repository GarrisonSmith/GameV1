using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fantasy.Engine.Logic.Mapping
{
    internal class GameMap : GameComponent
    {
        string tileMapName;

        List<MapLayer> Layers;

        internal GameMap(Game game, string tileMapName) : base(game)
        {
            Layers = new List<MapLayer>();
            game.Content.Load<XmlDocument>(@"tilemaps\" + tileMapName);
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine(tileMapName);
        }
    }
}
