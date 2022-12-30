using Fantasy.Engine.Logic.Drawing;
using Fantasy.Engine.Logic.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fantasy.Engine.Logic.Mapping
{
    internal class MapLayer : DrawableGameComponent
    {

        internal int Layer { get; set; }

        internal Dictionary<(int row, int col), Tile> Map { get; set; }

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        internal MapLayer(Game game, XmlElement layerElement) : base(game)
        { 
            Layer = int.Parse(layerElement.GetAttribute("name"));
            Map = new Dictionary<(int, int), Tile>();
            foreach (XmlElement tileElement in layerElement)
            { 
                (int row, int col) mapKey = ( int.Parse(tileElement.GetAttribute("mapRow")), int.Parse(tileElement.GetAttribute("mapCol")) );
                Map.Add(mapKey, Tile.LookUpTile(tileElement, mapKey));
            }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        internal Tile GetTile((int row, int col) foo)
        {
            return Map[foo];
        }

        internal Tile GetTile(Coordinates foo)
        {
            return Map[(foo.Center.Y / 64, foo.Center.X / 64)];
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
