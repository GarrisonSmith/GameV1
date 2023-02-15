using Fantasy.Engine.Drawing;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Fantasy.Engine.Mapping.Tiling
{
	/// <summary>
	/// Represents a collection of tiles on a specific map layer.
	/// </summary>
	internal struct TileCollection : ISubDrawable
	{
		private bool isVisible;
		private bool useCombinedTexture;
		private Texture2D combinedTexture;
		private readonly MapLayer map;
		private readonly Dictionary<Location, Tile> tiles;
		private readonly Coordinates coordinates;

		/// <summary>
		/// Indicates whether the TileCollection is visible or not.
		/// </summary>
		public bool IsVisible
		{
			get => isVisible;
			set => isVisible = value;
		}
		/// <summary>
		/// Indicates whether the TileCollection uses a combined texture or not.
		/// </summary>
		internal bool UseCombinedTexture
		{
			get => useCombinedTexture;
			set => useCombinedTexture = value;
		}
		/// <summary>
		/// The combined texture of the TileCollection.
		/// </summary>
		internal Texture2D CombinedTexture
		{
			get => combinedTexture;
		}
		/// <summary>
		/// The MapLayer of the TileCollection.
		/// </summary>
		internal MapLayer Map
		{
			get => map;
		}
		/// <summary>
		/// The tiles in the TileCollection, with the keys being a Location struct describing the row and column of the coordinates of the layer 
		/// and the values being the tiles themselves.
		/// </summary>
		internal Dictionary<Location, Tile> Tiles
		{
			get => tiles;
		}
		/// <summary>
		/// The coordinates of the TileCollection.
		/// </summary>
		internal Coordinates Coordinates
		{
			get => coordinates;
		}

		/// <summary>
		/// Creates a new TileCollection for the specified MapLayer.
		/// </summary>
		/// <param name="map">The MapLayer to create the TileCollection for.</param>
		/// <param name="isVisible">Indicates whether the TileCollection is visible or not.</param>
		/// <param name="useCombinedTexture">Indicates whether the TileCollection uses a combined texture or not.</param>
		public TileCollection(MapLayer map, bool isVisible = true, bool useCombinedTexture = false)
		{
			this.isVisible = isVisible;
			this.useCombinedTexture = useCombinedTexture;
			combinedTexture = null;
			this.map = map;
			tiles = Tile.GetLayerDictionary(map.Layer, out coordinates);
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
		/// Creates a combined texture of all the tiles in the TileCollection.
		/// </summary>
		/// <param name="useCombinedTexture">Indicates whether the TileCollection uses a combined texture or not.</param>
		internal void CreateCombinedTexture(bool useCombinedTexture = false)
		{
			UseCombinedTexture = useCombinedTexture;
			//RenderTarget2D foo = new(SpriteBatchHandler.SpriteBatch.GraphicsDevice, coordinates.Width, coordinates.Height);
			RenderTarget2D foo = new RenderTarget2D(
				SpriteBatchHandler.SpriteBatch.GraphicsDevice,
				coordinates.Width, coordinates.Height,
				false, SurfaceFormat.Color, DepthFormat.None, 0,
				RenderTargetUsage.PreserveContents
			);
			SpriteBatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(foo);
			SpriteBatchHandler.Begin();
			foreach (Tile tile in Tiles.Values)
			{
				tile.DrawCoordinates.TryGetValue(Map.Layer, out HashSet<Coordinates> layerDrawCoordinates);
				foreach (Coordinates cord in layerDrawCoordinates)
				{
					SpriteBatchHandler.Draw(tile.Spritesheet, cord.TopLeft - Coordinates.TopLeft, tile.SheetBox, Color.White);
				}
			}
			SpriteBatchHandler.End();
			SpriteBatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
			combinedTexture = foo;
		}
		/// <summary>
		/// Draws the tile collection on the screen.
		/// </summary>
		/// <param name="gameTime">The current game time</param>
		public void Draw(GameTime gameTime)
		{
			if (!IsVisible)
			{
				return;
			}

			if (useCombinedTexture)
			{
				SpriteBatchHandler.Draw(combinedTexture, coordinates.Rectangle, Color.White);
				return;
			}

			foreach (Tile tile in Tiles.Values)
			{
				tile.DrawCoordinates.TryGetValue(Map.Layer, out HashSet<Coordinates> layerDrawCoordinates);
				foreach (Coordinates cord in layerDrawCoordinates)
				{
					SpriteBatchHandler.Draw(tile.Spritesheet, cord.TopLeft, tile.SheetBox, Color.White);
				}
			}
		}
	}
}
