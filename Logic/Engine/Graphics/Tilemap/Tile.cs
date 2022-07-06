using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Hitboxes;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Describes A tile in a given TileMapLayer.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Reference graphical tile set used by this tile.
        /// </summary>
        public Texture2D tileSet;
        /// <summary>
        /// Top left coordinate of the tile collisionArea this tile describes inside of the given tile set.
        /// </summary>
        public Point tileSetCoordinate;
        /// <summary>
        /// Point this tile occupies in its TileMapLayer. The X value is the horizontal tile column and the Y value is the vertical tile row.
        /// </summary>
        public Point tileMapCoordinate;
        /// <summary>
        /// Color this tile loads when drawn.
        /// </summary>
        public Color color;
        /// <summary>
        /// Determines if this Tile has a corresponding Hitbox or not.
        /// </summary>
        public Tilebox hitbox;

        /// <summary>
        /// Generic inherited constructor.
        /// </summary>
        public Tile() { }
        /// <summary>
        /// Constructs a tile with the given properties.
        /// <param name="tileID">is parsed to get the tiles tileSetName and tiles x and y values.</param>
        /// <param name="column">the column this tile occupies on its TileMapLayer.</param>
        /// <param name="row">the row this tile occupies on its TileMapLayer.</param>
        /// </summary>
        public Tile(string tileID, int column, int row, bool hasHitbox)
        {
            tileMapCoordinate = new Point(column, row);
            this.hasHitbox = hasHitbox;
            if (tileID == "BLACK")
            {
                ContentHandler.tileTextures.TryGetValue(tileID, out tileSet);
                tileSetCoordinate = new Point(0, 0);
                color = Color.Black;
            }
            else
            {
                ContentHandler.tileTextures.TryGetValue(tileID.Substring(0, tileID.IndexOf('{')), out tileSet);
                tileSetCoordinate = Util.PointFromString(tileID);
                color = Color.White;
            }
        }
        /// <summary>
        /// Draws the tile.
        /// </summary>
        public void DrawTile()
        {
            Global._spriteBatch.Draw(tileSet, new Vector2(tileMapCoordinate.X * 64, -(tileMapCoordinate.Y+1) * 64),
                new Rectangle(tileSetCoordinate.X, tileSetCoordinate.Y, 64, 64),
                color, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), 0);
        }
    }
}