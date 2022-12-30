using Fantasy.Engine.ContentManagement;
using Fantasy.Engine.Logic.Drawing;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <returns>The tile that was looked up or created.</returns>
        internal static Tile LookUpTile(XmlElement tileElement, (int row, int col) mapKey)
        {
            if (!UNIQUE_TILES.TryGetValue(tileElement.GetAttribute("id"), out Tile foo))
            {
                foo = new Tile(tileElement);
                UNIQUE_TILES.Add(tileElement.GetAttribute("id"), foo);
            }
            foo.Locations.Add(new Vector2(mapKey.col * 64, mapKey.row * 64));

            return foo;
        }

        /// <summary>
        /// The spritesheet that the tile's image is taken from.
        /// </summary>
        internal Texture2D Spritesheet { get; set; }
        /// <summary>
        /// The coordinates of the tile's image on the spritesheet, as a tuple of (row, col).
        /// </summary>
        internal (int row, int col) SheetCoordinates { get; set; }
        /// <summary>
        /// The locations of the tile on the map.
        /// </summary>
        internal HashSet<Vector2> Locations { get; set; }
        /// <summary>
        /// The ID of the tile.
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// Creates a new tile from the specified XML element.
        /// </summary>
        /// <param name="tileElement">The XML element that defines the tile.</param>
        private Tile(XmlElement tileElement)
        {
            Locations = new HashSet<Vector2>();
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
                    SheetCoordinates = (row, col);
                }
            }
            if (Spritesheet == null || SheetCoordinates == (null, null) || Id == null)
            {
                throw new Exception("Invalid Tile XmlElement " + tileElement);
            }
        }
    }
}
