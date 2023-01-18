using Fantasy.Engine.Drawing;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Fantasy.Engine.Mapping.Tiling
{
	internal struct TileCollection : ISubDrawable
	{
		private bool isVisible;
		private bool useCombinedTexture;
		private Texture2D combinedTexture;
		private readonly MapLayer map;
		private readonly Dictionary<Location, Tile> tiles;

		/// <summary>
		/// 
		/// </summary>
		public bool IsVisible
		{
			get => isVisible;
			set => isVisible = value;
		}
		/// <summary>
		/// 
		/// </summary>
		internal bool UseCombinedTexture
		{
			get => useCombinedTexture;
			set => useCombinedTexture = value;
		}
		/// <summary>
		/// 
		/// </summary>
		internal Texture2D CombinedTexture
		{
			get => combinedTexture;
			set => combinedTexture = value;
		}
		/// <summary>
		/// 
		/// </summary>
		internal MapLayer Map
		{
			get => map;
		}
		/// <summary>
		/// 
		/// </summary>
		internal Dictionary<Location, Tile> Tiles
		{
			get => tiles;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="map"></param>
		/// <param name="isVisible"></param>
		/// <param name="useCombinedTexture"></param>
		public TileCollection(MapLayer map, bool isVisible = true, bool useCombinedTexture = false)
		{
			this.isVisible = isVisible;
			this.useCombinedTexture = useCombinedTexture;
			combinedTexture = null;
			this.map = map;
			tiles = Tile.GetLayerDictionary(map.Layer);
		}
		/// <summary>
		/// Looks up a tile at a specified location.
		/// </summary>
		/// <param name="foo">The location of the tile to look up.</param>
		/// <returns>The tile at the specified location.</returns>
		internal Tile LookUpTile(Location foo)
		{
			if (Tiles.ContainsKey(foo))
			{
				return Tiles[foo];
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
		/// 
		/// </summary>
		/// <param name="useCombinedTexture"></param>
		internal void CreateCombinedTexture(bool useCombinedTexture = true)
		{ 
			UseCombinedTexture = useCombinedTexture;
			RenderTarget2D foo = new(SpritebatchHandler.SpriteBatch.GraphicsDevice, 100, 100);
			SpritebatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(foo);
			SpritebatchHandler.Begin();
			foreach (Tile tile in Tiles.Values)
			{
				tile.DrawCoordinates.TryGetValue(Map.Layer, out HashSet<Coordinates> layerDrawCoordinates);
				foreach (Coordinates cord in layerDrawCoordinates)
				{
					SpritebatchHandler.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
				}
			}
			SpritebatchHandler.End();
			SpritebatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public void Draw(GameTime gameTime)
		{
			if (!IsVisible)
			{
				return;
			}

			foreach (Tile tile in Tiles.Values)
			{
				tile.DrawCoordinates.TryGetValue(Map.Layer, out HashSet<Coordinates> layerDrawCoordinates);
				foreach (Coordinates cord in layerDrawCoordinates)
				{
					SpritebatchHandler.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
				}
			}
		}
	}
}
