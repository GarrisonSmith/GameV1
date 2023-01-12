using Fantasy.Engine.Logic.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Fantasy.Engine.Logic.Mapping
{
    internal class MapLayer : DrawableGameComponent
    {

        internal int Layer { get; set; }

        internal Dictionary<Location, Tile> Map { get; set; }

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        internal MapLayer(Game game, XmlElement layerElement) : base(game)
        {
            Layer = int.Parse(layerElement.GetAttribute("layer"));
            Map = new Dictionary<Location, Tile>();

        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        internal Tile LookUpTile(Location foo)
        {
            return Map[foo];
        }

        internal Tile LookUpTile(Coordinates foo)
        {
            return Map[new Location(foo)];
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Tile tile in Map.Values) 
            {
                tile.Locations.TryGetValue(Layer, out HashSet<Coordinates> locations);
                foreach (Coordinates cord in locations)
                {
                    //Game._spriteBatch.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
                }
            }
        }


    }
}
