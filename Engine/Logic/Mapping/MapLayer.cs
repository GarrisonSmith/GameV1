using Fantasy.Engine.Logic.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
                (int row, int col) mapKey = (int.Parse(tileElement.GetAttribute("mapRow")), int.Parse(tileElement.GetAttribute("mapCol")));
                Map.Add(mapKey, Tile.GetTile(tileElement, mapKey, Layer));
            }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        internal Tile LookUpTile((int row, int col) foo)
        {
            return Map[foo];
        }

        internal Tile LookUpTile(Coordinates foo)
        {
            return Map[((int)foo.Center.Y / Tile.TILE_HEIGHT, (int)foo.Center.X / Tile.TILE_WIDTH)];
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Tile tile in Map.Values) 
            {
                tile.Locations.TryGetValue(Layer, out HashSet<Coordinates> locations);
                foreach (Coordinates cord in locations)
                {
                    Game1._spriteBatch.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
                }
            }
        }


    }
}
