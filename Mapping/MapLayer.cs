using Fantasy.Engine.Mapping.Tiling;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Fantasy.Engine.Drawing;

namespace Fantasy.Engine.Mapping
{
	/// <summary>
	/// Represents a layer of tiles in a game map.
	/// </summary>
	internal class MapLayer : DrawableGameComponent
    {
        private readonly int layer;
        private readonly Dictionary<Location, Tile> map;
        private MapLayer next;

		/// <summary>
		/// The layer number.
		/// </summary>
		internal int Layer
        {
            get => layer;
        }
		/// <summary>
		/// The collection of tiles in the layer.
		/// </summary>
		internal Dictionary<Location, Tile> Map
        {
            get => map;
        }
		/// <summary>
		/// The next layer in the map. This will be the layer with the next lowest layer value or null if no such layer exists.
		/// </summary>
		internal MapLayer Next
        {
            get => next;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="MapLayer"/> class using a specified game object and layer number.
		/// </summary>
		/// <param name="game">The game object associated with the layer.</param>
		/// <param name="layer">The layer number.</param>
		internal MapLayer(Game game, int layer) : base(game)
        {
            this.layer = layer;
            if (ActiveGameMap.HIGHEST_LAYER == null)
            {
				ActiveGameMap.HIGHEST_LAYER = this;
                map = Tile.GetLayerDictionary(layer);
                next = null;
            }
            else
            {
                MapLayer cur = ActiveGameMap.HIGHEST_LAYER;
                while (cur.next != null && cur.next.layer > layer)
                {
                    cur = cur.Next;
                }
                next = cur.next;
                cur.next = this;
            }
            DrawOrder = layer;
            UpdateOrder = layer;
            map = Tile.GetLayerDictionary(layer);
        }
		/// <summary>
		/// Looks up a tile at a specified location.
		/// </summary>
		/// <param name="foo">The location of the tile to look up.</param>
		/// <returns>The tile at the specified location.</returns>
		internal Tile LookUpTile(Location foo)
        {
            if (Map.ContainsKey(foo))
            {
                return map[foo];
            }
            return null;
        }
		/// <summary>
		/// Looks up a tile at a specified coordinates.
		/// </summary>
		/// <param name="foo">The coordinates of the tile to look up.</param>
		/// <returns>The tile at the specified coordinates.</returns>
		internal Tile LookUpTile(Coordinates foo)
        {
            return LookUpTile(new Location(foo));
        }
		/// <summary>
		/// Draws the layer.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		public override void Draw(GameTime gameTime)
        {
            foreach (Tile tile in Map.Values)
            {
                tile.LayerCoordinates.TryGetValue(Layer, out HashSet<Coordinates> locations);
                foreach (Coordinates cord in locations)
                {
                    SpritebatchHandler.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
                }
            }
        }
    }
}
