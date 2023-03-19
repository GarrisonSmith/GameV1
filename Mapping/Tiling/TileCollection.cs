using Fantasy.Engine.Drawing;
using Fantasy.Engine.Drawing.Interfaces;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Fantasy.Engine.Mapping.Tiling
{
    /// <summary>
    /// Represents a collection of tiles on a specific map layer.
    /// </summary>
    public struct TileCollection : ILocatable, ISubDrawableCollection<Location, Tile>
	{
		private bool isVisible;
		private bool useCombinedTexture;
		private Texture2D combinedTexture;
		private readonly MapLayer map;
		private readonly Dictionary<Location, Tile> tiles;
		private readonly BoundingBox2 boundingBox2;

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
		public bool UseCombinedTexture
		{
			get => useCombinedTexture;
			set => useCombinedTexture = value;
		}
		/// <summary>
		/// The combined texture of the TileCollection.
		/// </summary>
		public Texture2D CombinedTexture
		{
			get => combinedTexture;
		}
		/// <summary>
		/// The MapLayer of the TileCollection.
		/// </summary>
		public MapLayer Map
		{
			get => map;
		}
		/// <summary>
		/// The tiles in the TileCollection, with the keys being a Location struct describing the row and column of the BoundingBox2 of the layer 
		/// and the values being the tiles themselves.
		/// </summary>
		public Dictionary<Location, Tile> Tiles
		{
			get => tiles;
		}
		/// <summary>
		/// The BoundingBox2 of the TileCollection.
		/// </summary>
		public BoundingBox2 BoundingBox2
		{
			get => boundingBox2;
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
			tiles = Tile.GetLayerDictionary(map.Layer, out boundingBox2);
		}
		/// <summary>
		/// Looks up a tile at a specified location.
		/// </summary>
		/// <param name="foo">The location of the tile to look up.</param>
		/// <returns>The tile at the specified location.</returns>
		public Tile LookUpTile(Location foo)
		{
			if (Tiles.ContainsKey(foo))
			{
				return Tiles[foo];
			}
			return null;
		}
		/// <summary>
		/// Looks up a tile at a specified BoundingBox2.
		/// </summary>
		/// <param name="foo">The BoundingBox2 of the tile to look up.</param>
		/// <returns>The tile at the specified BoundingBox2.</returns>
		public Tile LookUpTile(BoundingBox2 foo)
		{
			return LookUpTile(new Location(foo));
		}
		/// <summary>
		/// Creates a combined texture of all the tiles in the TileCollection.
		/// </summary>
		/// <param name="useCombinedTexture">Indicates whether the TileCollection uses a combined texture or not.</param>
		public void CreateCombinedTexture(bool useCombinedTexture = false)
		{
			UseCombinedTexture = useCombinedTexture;
			RenderTarget2D foo = new RenderTarget2D(
				SpriteBatchHandler.SpriteBatch.GraphicsDevice,
				(int)BoundingBox2.Width, (int)BoundingBox2.Height,
				false, SurfaceFormat.Color, DepthFormat.None, 0,
				RenderTargetUsage.PreserveContents
			);
			SpriteBatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(foo);
			SpriteBatchHandler.Begin();
			foreach (Tile tile in Tiles.Values)
			{
				if (tile is not AnimatedTile)
				{
					tile.DrawBoundingBoxes.TryGetValue(Map.Layer, out HashSet<BoundingBox2> layerDrawBoundingBox2);
					foreach (BoundingBox2 boundBox in layerDrawBoundingBox2)
					{
                        SpriteBatchHandler.Draw(tile.Spritesheet, boundBox.TopLeft - BoundingBox2.TopLeft, tile.SheetBox, Color.White);
					}
				}
			}
			SpriteBatchHandler.End();
			SpriteBatchHandler.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
			combinedTexture = foo;
		}

		/// <summary>
		/// Draws the tile collection on the screen.
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		public void Draw(GameTime gameTime)
		{
			if (!IsVisible)
			{
				return;
			}

			if (useCombinedTexture)
			{
				SpriteBatchHandler.Draw(combinedTexture, BoundingBox2.Rectangle, Color.White);
                foreach (Tile tile in Tiles.Values)
                {
					if (tile is AnimatedTile animatedTile)
					{
                        animatedTile.Draw(gameTime, map.Layer);
					}
                }
                return;
			}

			foreach (Tile tile in Tiles.Values)
			{
				if (tile is AnimatedTile animatedTile)
				{
                    animatedTile.Draw(gameTime, map.Layer);
				}
				else
				{
					tile.Draw(gameTime, map.Layer);
				}
			}
		}

        public bool Intersects(ILocatable foo)
        {
            throw new System.NotImplementedException();
        }

		public bool Contains(ILocatable foo)
		{
            throw new System.NotImplementedException();
        }

        public float Distance(ILocatable foo)
        {
            throw new System.NotImplementedException();
        }
    }
}