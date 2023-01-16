using Fantasy.Engine.ContentManagement;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Fantasy.Engine.Mapping.Tiling
{
    /// <summary>
    /// Represents a tile in a MapLayer.
    /// </summary>
    internal class Tile
    {
        /// <summary>
        /// The width of a tile, in pixels.
        /// </summary>
        internal static readonly int TILE_WIDTH = 64;
        /// <summary>
        /// The height of a tile, in pixels.
        /// </summary>
        internal static readonly int TILE_HEIGHT = 64;
        /// <summary>
        /// A dictionary that stores unique tiles by their ID.
        /// </summary>
        private static readonly Dictionary<string, Tile> UNIQUE_TILES = new();

        /// <summary>
        /// Attempts to create a Tile from the provided XmlElement, if the Tile has already been created then no Tile is created.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        /// <returns>A bool indicating whether or not a new Tile has been created or if it already exists.</returns>
        internal static bool CreateTile(XmlElement tileElement)
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
        /// Gets a dictionary of tiles that exist on the specified map layer.
        /// </summary>
        /// <param name="layer">The number of the map layer to get tiles for.</param>
        /// <returns>A dictionary containing the tiles on the specified layer, with the keys being a Location struct describing the row and column of the coordinates of the layer 
        /// and the values being the tiles themselves.</returns>
        internal static Dictionary<Location, Tile> GetLayerDictionary(int layer)
        {
            Dictionary<Location, Tile> foo = new();
            foreach (Tile tile in UNIQUE_TILES.Values)
            {
                tile.GetLocationDictionary(layer, foo);
            }
            return foo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        internal static List<Tile> GetLayerTiles(int layer)
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

        private readonly Texture2D spritesheet;
        private readonly Rectangle sheetBox;
        private readonly Dictionary<int, HashSet<Coordinates>> layerCoordinates;
        private readonly string id;

        /// <summary>
        /// The spritesheet that the tile's image is taken from.
        /// </summary>
        internal Texture2D Spritesheet
        {
            get => spritesheet;
        }
        /// <summary>
        /// The area of the spritesheet from which the tile's image is taken.
        /// </summary>
        internal Rectangle SheetBox
        {
            get => sheetBox;
        }
        /// <summary>
        /// A dictionary that maps layer numbers to HashSets of coordinates for the tile on the current map.
        /// </summary>
        internal Dictionary<int, HashSet<Coordinates>> LayerCoordinates
        {
            get => layerCoordinates;
        }
        /// <summary>
        /// The ID of the tile.
        /// </summary>
        internal string Id
        {
            get => id;
        }

        /// <summary>
        /// Creates a new tile from the specified XML element.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        /// <exception cref="ArgumentException">Throws an exception if the XML element is invalid or if the spritesheet, id, or locations elements are missing.</exception>
        private Tile(XmlElement tileElement)
        {
            id = tileElement.GetAttribute("id");
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
                if (layerCoordinates == null && foo.Name.Equals("locations"))
                {
                    layerCoordinates = new Dictionary<int, HashSet<Coordinates>>();
                }
                if (foo.Name.Equals("locations"))
                {
                    int layer = int.Parse(foo.GetAttribute("layer"));
                    if (layerCoordinates.ContainsKey(layer))
                    {
                        throw new Exception("Tile XmlElement contains duplicate layer: " + layer + " " + tileElement);
                    }

                    HashSet<Coordinates> layerSet = new();
                    foreach (XmlElement location in foo)
                    {
                        float x = float.Parse(location.GetAttribute("x"));
                        float y = float.Parse(location.GetAttribute("y"));
                        layerSet.Add(new Coordinates(x, y, x + TILE_WIDTH / 2 + .5f, y + TILE_HEIGHT / 2 + .5f));
                    }
                    layerCoordinates.Add(layer, layerSet);
                }
            }
            if (spritesheet == null || id == null || layerCoordinates == null)
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
        internal bool GetLocationDictionary(int layer, Dictionary<Location, Tile> foo)
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
        internal bool IsInLayer(int layer)
        {
            return LayerCoordinates.ContainsKey(layer);
        }

        internal HashSet<Coordinates> GetLayerDrawLocations(int layer)
        {
            if (layer == MapLayer.HIGHEST_LAYER.Layer)
            {
                return LayerCoordinates[layer];
            }

            HashSet<Coordinates> foo = new();
            foreach (Coordinates cord in LayerCoordinates[layer])
            {

            }

            return foo;
        }
    }
}