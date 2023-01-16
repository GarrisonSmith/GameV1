using Fantasy.Engine.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Fantasy.Engine.Mapping
{
    internal class MapLayer : IDrawable
    {
        private static MapLayer highest_layer;

        internal static MapLayer HIGHEST_LAYER
        {
            get => highest_layer;
        }

        private readonly int layer;
        private readonly Dictionary<Location, Tile> map;
        private MapLayer next;

        internal int Layer
        {
            get => layer;
        }

        internal Dictionary<Location, Tile> Map
        {
            get => map;
        }

        internal MapLayer Next
        {
            get => next;
        }

        public int DrawOrder => 1; //throw new NotImplementedException();

        public bool Visible => true; //throw new NotImplementedException();

        internal MapLayer(int layer)
        {
            this.layer = layer;

            MapLayer cur = highest_layer;
            while (cur.Next.Layer > layer && cur.next != null)
            {
                cur = cur.Next;
            }
            next = cur.next;
            cur.next = this;

            map = Tile.GetLayerDictionary(layer);
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

        public void Draw(GameTime gameTime)
        {
            foreach (Tile tile in Map.Values)
            {
                tile.LayerCoordinates.TryGetValue(Layer, out HashSet<Coordinates> locations);
                foreach (Coordinates cord in locations)
                {
                    //Game._spriteBatch.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
                }
            }
        }


    }
}
