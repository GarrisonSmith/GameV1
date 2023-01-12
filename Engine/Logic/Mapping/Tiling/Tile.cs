using Fantasy.Engine.ContentManagement;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Fantasy.Engine.Logic.Mapping.Tiling
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
        /// Looks up a tile by its ID and adds it to the map at the specified location.
        /// If the tile does not already exist, it is created.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        /// <returns>The tile that was looked up or created.</returns>
        internal static Tile GetTile(XmlElement tileElement)
        {
            if (!UNIQUE_TILES.TryGetValue(tileElement.GetAttribute("id"), out Tile tile))
            {
                tile = new Tile(tileElement);
                UNIQUE_TILES.Add(tileElement.GetAttribute("id"), tile);
            }

            return tile;
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

        /// <summary>
        /// The spritesheet that the tile's image is taken from.
        /// </summary>
        internal Texture2D Spritesheet { get; set; }
        /// <summary>
        /// The area of the spritesheet from which the tile's image is taken.
        /// </summary>
        internal Rectangle SheetBox { get; set; }
        /// <summary>
        /// A dictionary that maps layer numbers to sets of locations (Vector2 objects) for the tile on the map.
        /// </summary>
        internal Dictionary<int, HashSet<Coordinates>> Locations { get; set; }
        /// <summary>
        /// The ID of the tile.
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// Creates a new tile from the specified XML element.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        /// <exception cref="ArgumentException">Throws an exception if the XML element is invalid or if the spritesheet or ID attributes are missing.</exception>
        private Tile(XmlElement tileElement)
        {
            Id = tileElement.GetAttribute("id");
            foreach (XmlElement foo in tileElement)
            {
                if (foo.Name.Equals("spritesheet"))
                {
                    Spritesheet = TextureManager.GetSpritesheet(foo.InnerText);
                    continue;
                }
                if (foo.Name.Equals("sheet-coordinates"))
                {
                    int col = int.Parse(foo.GetAttribute("col"));
                    int row = int.Parse(foo.GetAttribute("row"));
                    SheetBox = new Rectangle(col * TILE_WIDTH, row * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
                    continue;
                }
                if (Locations == null && foo.Name.Equals("locations"))
                {
                    Locations = new Dictionary<int, HashSet<Coordinates>>();
                }
                if (foo.Name.Equals("locations"))
                {
                    int layer = int.Parse(foo.GetAttribute("layer"));
                    if (Locations.ContainsKey(layer))
                    {
                        throw new Exception("Tile XmlElement contains duplicate layer: " + layer + " " + tileElement);
                    }

                    HashSet<Coordinates> layerSet = new();
                    foreach (XmlElement location in foo)
                    {
                        float x = float.Parse(location.GetAttribute("x"));
                        float y = float.Parse(location.GetAttribute("y"));
                        layerSet.Add(new Coordinates(x, y, x + (TILE_WIDTH / 2) + .5f, y + (TILE_HEIGHT / 2) + .5f));
                    }
                    Locations.Add(layer, layerSet);
                }
            }
            if (Spritesheet == null || Id == null || Locations == null)
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
            if (!Locations.TryGetValue(layer, out HashSet<Coordinates> set))
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
            return Locations.ContainsKey(layer);
        }
    }
}