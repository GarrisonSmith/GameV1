using Fantasy.Engine.Mapping.Tiling;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fantasy.Engine.Mapping
{
    internal class GameMap : GameComponent
    {
        internal readonly string tileMapName;
        internal readonly Dictionary<int, MapLayer> Layers;

        internal GameMap(Game game, string mapName) : base(game)
        {
            XmlDocument mapElement = new XmlDocument();
            mapElement.Load(@"Content\tilemaps\" + mapName + ".xml");
        }

        internal GameMap(Game game, XmlElement mapElement) : base(game)
        {
            tileMapName = mapElement.GetAttribute("name");
            Layers = new Dictionary<int, MapLayer>();
            foreach (XmlElement tileElement in mapElement.GetElementsByTagName("Engine.Logic.Mapping.Tiling.Tile"))
            {
                Tile.CreateTile(tileElement);
            }

            foreach (XmlElement layerElement in mapElement.SelectSingleNode("Layers"))
            {
                int layer = int.Parse(layerElement.GetAttribute("id"));
                Layers.Add(layer, new MapLayer(layer));
            }
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine(tileMapName);
        }
    }
}
