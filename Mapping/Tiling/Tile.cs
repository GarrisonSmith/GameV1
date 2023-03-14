using Fantasy.Engine.ContentManagement;
using Fantasy.Engine.Drawing;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Fantasy.Engine.Mapping.Tiling
{
	/// <summary>
	/// Represents a tile in a MapLayer.
	/// </summary>
	public class Tile : ISubDrawable
	{
		private static readonly int TileWidth = 64;
		private static readonly int TileHeight = 64;
		private static readonly Dictionary<string, Tile> UniqueTiles = new();

		/// <summary>
		/// The width of a tile, in pixels.
		/// </summary>
		public static int TILE_WIDTH
		{
			get => TileWidth;
		}
		/// <summary>
		/// The height of a tile, in pixels.
		/// </summary>
		public static int TILE_HEIGHT
		{
			get => TileHeight;
		}
		/// <summary>
		/// A dictionary that stores unique tiles by their ID.
		/// </summary>
		protected static Dictionary<string, Tile> UNIQUE_TILES
		{
			get => UniqueTiles;
		}

		/// <summary>
		/// Attempts to create a Tile from the provided XmlElement, if the Tile has already been created then no Tile is created.
		/// </summary>
		/// <param name="tileElement">The XML element that defines the tile.</param>
		/// <returns>A bool indicating whether or not a new Tile has been created or if it already exists.</returns>
		public static bool CreateTile(XmlElement tileElement)
		{
			if (!UNIQUE_TILES.ContainsKey(tileElement.GetAttribute("id")))
			{
				Tile tile = new(tileElement);
				UNIQUE_TILES.Add(tileElement.GetAttribute("id"), tile);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Gets a tile with the specified ID.
		/// </summary>
		/// <param name="tileId">The ID of the tile to get.</param>
		/// <returns>The tile with the specified ID.</returns>
		public static Tile GetTile(string tileId)
		{ 
			return UniqueTiles[tileId];
		}
		/// <summary>
		/// Gets a dictionary of tiles that exist on the specified layer.
		/// </summary>
		/// <param name="layer">The number of the map layer to get tiles for.</param>
		/// <returns>A dictionary containing the tiles on the specified layer, with the keys being a Location struct describing the row and column of the coordinates of the layer 
		/// and the values being the tiles themselves.</returns>
		public static Dictionary<Location, Tile> GetLayerDictionary(int layer)
		{
			Dictionary<Location, Tile> foo = new();
			foreach (Tile tile in UNIQUE_TILES.Values)
			{
				tile.GetLocationDictionary(layer, foo);
			}

			return foo;
		}
		/// <summary>
		/// Gets a dictionary of tiles that exist on the specified layer.
		/// </summary>
		/// <param name="layer">The number of the map layer to get tiles for. The layer number should be an integer.</param>
		/// <param name="coordinates">An out parameter that will contain the coordinates of the top left and bottom left of the tiles on the specified layer</param>
		/// <returns>A dictionary containing the tiles on the specified layer, with the keys being a Location struct describing the row and column of the coordinates of the layer
		/// and the values being the tiles themselves.</returns>
		public static Dictionary<Location, Tile> GetLayerDictionary(int layer, out Coordinates coordinates)
		{
			Dictionary<Location, Tile> foo = new();
			foreach (Tile tile in UNIQUE_TILES.Values)
			{
				tile.GetLocationDictionary(layer, foo);
			}

			Vector2 topLeft = new(foo.Keys.ToArray()[0].Col, foo.Keys.ToArray()[0].Row);
			Vector2 bottomLeft = topLeft;
			foreach (Location location in foo.Keys)
			{
				if (location.Col < topLeft.X)
				{
					topLeft.X = location.Col;
				}
				else if (location.Col > bottomLeft.X)
				{
					bottomLeft.X = location.Col;
				}

				if (location.Row < topLeft.Y)
				{
					topLeft.Y = location.Row;
				}
				else if (location.Row > bottomLeft.Y)
				{
					bottomLeft.Y = location.Row;
				}
			}
			bottomLeft.X += 1; bottomLeft.Y += 1; bottomLeft.X *= TILE_WIDTH; bottomLeft.Y *= TILE_HEIGHT;
			topLeft.X *= TILE_WIDTH; topLeft.Y *= TILE_HEIGHT;
			coordinates = new Coordinates(topLeft, new Vector2((topLeft.X + (bottomLeft.X - topLeft.X) / 2), ((topLeft.Y + (bottomLeft.Y - topLeft.Y) / 2)) + .5f));

			return foo;
		}
		/// <summary>
		/// Gets a list of tiles that exist on the specified map layer.
		/// </summary>
		/// <param name="layer">The number of the map layer to get tiles for.</param>
		/// <returns>A list of tiles that exist on the specified map layer.</returns>
		public static List<Tile> GetLayerTiles(int layer)
		{
			List<Tile> foo = new();
			foreach (Tile tile in UNIQUE_TILES.Values)
			{
				if (tile.IsInLayer(layer))
				{
					foo.Add(tile);
				}
			}
			return foo;
		}
		/// <summary>
		/// Update the drawing coordinates of tiles in the active game map.
		/// </summary>
		/// <exception cref="Exception">Thrown if the ActiveGameMap has no highest MapLayer.</exception>
		public static void UpdateTileDrawLocations()
		{
			if (ActiveGameMap.HIGHEST_LAYER == null)
			{
				throw new Exception("The ActiveGameMap has no highest MapLayer.");
			}

			MapLayer map = ActiveGameMap.HIGHEST_LAYER;
			while (map != null)
			{
				foreach (Tile tile in UNIQUE_TILES.Values)
				{
					tile.UpdateDrawCoordinates(map.Layer);
				}
				map = map.Next;
			}
		}
		/// <summary>
		/// Update the drawing coordinates of tiles in the active game map, starting from the specified layer.
		/// </summary>
		/// <param name="startLayer">The layer number to start updating the drawing coordinates from.</param>
		/// <exception cref="Exception">Thrown if the ActiveGameMap has no highest MapLayer.</exception>
		public static void UpdateTileDrawLocations(int startLayer)
		{
			if (ActiveGameMap.MapLayers[startLayer] == null)
			{
				throw new Exception("The ActiveGameMap does not contain the provided startLayer: " + startLayer + ".");
			}

			MapLayer map = ActiveGameMap.MapLayers[startLayer];
			while (map != null)
			{
				foreach (Tile tile in UNIQUE_TILES.Values)
				{
					tile.UpdateDrawCoordinates(map.Layer);
				}
				map = map.Next;
			}
		}

		private bool isVisible = true; //TODO implement
		private Rectangle sheetBox;
		private readonly Texture2D spritesheet;
		private readonly Dictionary<int, HashSet<Coordinates>> layerCoordinates;
		private readonly Dictionary<int, HashSet<Coordinates>> drawCoordinates;
		private readonly string id;

		/// <summary>
		/// Describes if the tile completely obstructs the view of any tiles beneath it.
		/// </summary>
		public bool IsVisible
		{
			get => isVisible;
			set => isVisible = value;
		}
		/// <summary>
		/// The top left point of this tile's starting texture on the spritesheet.
		/// </summary>
		public Point TextureSourceTopLeft 
		{
			get => sheetBox.Location;
		}
		/// <summary>
		/// The area of the spritesheet from which the tile's texture is taken.
		/// </summary>
		public Rectangle SheetBox
		{
			get => sheetBox;
		}
		/// <summary>
		/// The spritesheet that the tile's texture is taken from.
		/// </summary>
		public Texture2D Spritesheet
		{
			get => spritesheet;
		}
		/// <summary>
		/// A dictionary that maps layer numbers to HashSets of coordinates for the tile on the current map.
		/// </summary>
		public Dictionary<int, HashSet<Coordinates>> LayerCoordinates
		{
			get => layerCoordinates;
		}
		/// <summary>
		/// A dictionary that maps layer numbers to HashSets of coordinates that describe draw locations on the current map.
		/// </summary>
		public Dictionary<int, HashSet<Coordinates>> DrawCoordinates
		{
			get => drawCoordinates;
		}
		/// <summary>
		/// The ID of the tile.
		/// </summary>
		public string Id
		{
			get => id;
		}

		/// <summary>
		/// Creates a new tile from the specified XML element.
		/// </summary>
		/// <param name="tileElement">The XML element that defines the tile.</param>
		/// <exception cref="ArgumentException">Throws an exception if the XML element is invalid or if the spritesheet, id, or locations elements are missing.</exception>
		protected Tile(XmlElement tileElement)
		{
			id = tileElement.GetAttribute("id");
			layerCoordinates = new Dictionary<int, HashSet<Coordinates>>();
			drawCoordinates = new Dictionary<int, HashSet<Coordinates>>();
			foreach (XmlElement foo in tileElement)
			{
				if (foo.Name.Equals("spritesheet"))
				{
					spritesheet = TextureManager.GetSpritesheet(foo.InnerText);
					continue;
				}
				if (foo.Name.Equals("sheet-coordinates"))
				{
					int col = int.Parse(foo.GetAttribute("col"));
					int row = int.Parse(foo.GetAttribute("row"));
					sheetBox = new Rectangle(col * TILE_WIDTH, row * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
					continue;
				}
				if (foo.Name.Equals("locations"))
				{
					int layer = int.Parse(foo.GetAttribute("layer"));
					if (layerCoordinates.ContainsKey(layer))
					{
						throw new Exception("Tile XmlElement contains duplicate layer: " + layer + " " + tileElement);
					}

					HashSet<Coordinates> layerSet = new();
					HashSet<Coordinates> drawSet = new();
					foreach (XmlElement location in foo)
					{
						float x = float.Parse(location.GetAttribute("x"));
						float y = float.Parse(location.GetAttribute("y"));
						Coordinates cord = new(x, y, x + TILE_WIDTH / 2 + .5f, y + TILE_HEIGHT / 2 + .5f);
						layerSet.Add(cord);
						drawSet.Add(cord);
					}
					LayerCoordinates.Add(layer, layerSet);
					DrawCoordinates.Add(layer, drawSet);
					continue;
				}
			}
			if (Spritesheet == null || Id == null || LayerCoordinates == null)
			{
				throw new Exception("Invalid Tile XmlElement: " + tileElement);
			}
		}

		/// <summary>
		/// Adds the locations of the tile in the specified layer to the provided dictionary.
		/// </summary>
		/// <param name="layer">The layer number to get the locations of the tile from.</param>
		/// <param name="foo">The dictionary to add the tile's locations to. 
		/// The key is a Location struct describing the rows and columns the tile occupies and value is the tile itself.</param>
		/// <returns>A bool value indicating whether or not the tile is present on the specified layer.</returns>
		public bool GetLocationDictionary(int layer, Dictionary<Location, Tile> foo)
		{
			if (!LayerCoordinates.TryGetValue(layer, out HashSet<Coordinates> set))
			{
				return false;
			}

			foreach (Coordinates cord in set)
			{
				foo.Add(new Location(cord), this);
			}
			return true;
		}
		/// <summary>
		/// Checks if this tile is in the specified layer.
		/// </summary>
		/// <param name="layer">The layer number to check if the tile occupies.</param>
		/// <returns>A bool indicating if the tile is found on the provided layer.</returns>
		public bool IsInLayer(int layer)
		{
			return LayerCoordinates.ContainsKey(layer);
		}
		/// <summary>
		/// Removes the draw location of the tile from the specified layer.
		/// </summary>
		/// <param name="layer">The layer number to remove the draw location from.</param>
		public void RemoveDrawLocation(int layer, Coordinates cord)
		{
			if (!IsInLayer(layer))
			{
				return;
			}
			DrawCoordinates[layer].RemoveWhere(drawCord => cord.Equals(drawCord)); //TODO could be optimized. probably 
		}
		/// <summary>
		/// Update the drawing coordinates of the tile for the specified layer.
		/// </summary>
		/// <exception cref="Exception">Thrown if the specified layer is not found.</exception>
		public void UpdateDrawCoordinates(int startLayer)
		{
			if (!ActiveGameMap.MapLayers.ContainsKey(startLayer))
			{
				throw new Exception("The ActiveGameMap does contain the provided layer.");
			}

			MapLayer map = ActiveGameMap.MapLayers[startLayer].Next;
			while (map != null)
			{
				if (!IsInLayer(startLayer))
				{
					map = map.Next;
					continue;
				}

				foreach (Coordinates cord in DrawCoordinates[startLayer])
				{
					map.TileLayer.LookUpTile(cord)?.RemoveDrawLocation(map.Layer, cord);
				}
				map = map.Next;
			}
		}
		/// <summary>
		/// Draws the tile to all draw location on the provided layer. 
		/// </summary>
		/// <param name="gameTime">The current game time.</param>
		/// <param name="layer">The layer to be drawn.</param>
		public void Draw(GameTime gameTime, int layer)
		{
			DrawCoordinates.TryGetValue(layer, out HashSet<Coordinates> layerDrawCoordinates);
			foreach (Coordinates cord in layerDrawCoordinates)
			{
				SpriteBatchHandler.Draw(Spritesheet, cord.TopLeft, SheetBox, Color.White);
			}
		}
	}
}