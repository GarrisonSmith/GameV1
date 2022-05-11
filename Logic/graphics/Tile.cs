using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.Graphics
{
    /// <summary>
    /// Describes A tile in a given <c>TileMapLayer</c>.
    /// </summary>
    class Tile
    {
        /// <summary>
        /// Name of the tile set this tile describes.
        /// </summary>
        public string tileSetName;
        /// <summary>
        /// Top left coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public Point tileSetCoordinate;
        /// <summary>
        /// Point this tile occupies in its <c>TileMapLayer<c>.
        /// </summary>
        public Point tileMapCoordinate;
        /// <summary>
        /// Color this tile loads when drawn.
        /// </summary>
        public Color color;
        /// <summary>
        /// The index of the graphic in the TileMap for this tile.
        /// </summary>
        public int graphicsIndex;

        /// <summary>
        /// Constructs a tile with the given properties.
        /// <param name="tileID"> is parsed to get the tiles <c>tileSetName</c> and tiles <c>x</c> and <c>y</c> values. </param>
        /// </summary>
        public Tile(string tileID, int row, int column)
        {
            tileMapCoordinate = new Point(row, column);
            if (tileID == "BLACK")
            {
                this.tileSetName = tileID;
                tileSetCoordinate = new Point(0, 0);
                color = Color.Black;
            }
            else
            {
                this.tileSetName = tileID.Substring(0, tileID.IndexOf('('));
                tileSetCoordinate = new Point
                    (
                    int.Parse(tileID.Substring(tileID.IndexOf('(') + 1, tileID.IndexOf(',') - (tileID.IndexOf('(') + 1))),
                    int.Parse(tileID.Substring(tileID.IndexOf(',') + 1, tileID.IndexOf(')') - (tileID.IndexOf(',') + 1)))
                    );
                color = Color.White;
            }
        }
    }
}