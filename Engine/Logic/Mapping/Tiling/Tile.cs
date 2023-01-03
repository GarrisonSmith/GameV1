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
        /// The height of a tile, in pixels.
        /// </summary>
        internal static readonly int TILE_HEIGHT = 64;
        /// <summary>
        /// The width of a tile, in pixels.
        /// </summary>
        internal static readonly int TILE_WIDTH = 64;
        /// <summary>
        /// A dictionary that stores unique tiles by their ID.
        /// </summary>
        private static readonly Dictionary<string, Tile> UNIQUE_TILES = new();

        /// <summary>
        /// Looks up a tile by its ID and adds it to the map at the specified location.
        /// If the tile does not already exist, it is created.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        /// <param name="mapKey">The location of the tile in the map, as a tuple of (row, col).</param>
        /// <param name="layer">The layer of the tile.</param>
        /// <returns>The tile that was looked up or created.</returns>
        internal static Tile GetTile(XmlElement tileElement, (int row, int col) mapKey, int layer)
        {
            if (!UNIQUE_TILES.TryGetValue(tileElement.GetAttribute("id"), out Tile tile))
            {
                tile = new Tile(tileElement);
                UNIQUE_TILES.Add(tileElement.GetAttribute("id"), tile);
            }
            if (!tile.Locations.ContainsKey(layer))
            {
                tile.Locations[layer] = new HashSet<Coordinates>();
            }
            tile.Locations[layer].Add(new Coordinates(mapKey.col * TILE_WIDTH, mapKey.row * TILE_HEIGHT,
                (mapKey.col * TILE_WIDTH) + (TILE_WIDTH / 2) + .5f, (mapKey.row * TILE_HEIGHT) + (TILE_HEIGHT / 2) + .5f));

            return tile;
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
            Locations = new Dictionary<int, HashSet<Coordinates>>();
            Id = tileElement.GetAttribute("id");
            foreach (XmlElement foo in tileElement)
            {
                if (foo.Name.Equals("Spritesheet"))
                {
                    Spritesheet = TextureManager.GetSpritesheet(foo.GetAttribute("name"));
                }
                if (foo.Name.Equals("SheetCoordinates"))
                {
                    int row = int.Parse(foo.GetAttribute("row"));
                    int col = int.Parse(foo.GetAttribute("col"));
                    SheetBox = new Rectangle(col * TILE_WIDTH, row * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
                }
            }
            if (Spritesheet == null || Id == null)
            {
                throw new Exception("Invalid Tile XmlElement " + tileElement);
            }
        }
    }
}