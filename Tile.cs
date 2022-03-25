using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    /// <summary>
    /// Describes A tile in a given <c>TileMap</c>.
    /// </summary>
    class Tile
    {
        /// <summary>
        /// Name of the tile set this tile describes.
        /// </summary>
        public string tileSetName;
        /// <summary>
        /// Top left x coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public int x;
        /// <summary>
        /// Top left y coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public int y;
        /// <summary>
        /// Width of the tile area this tile describes inside of the given tile set. Default is 64.
        /// </summary>
        public int tileWidth = 64;
        /// <summary>
        /// Height of the tile area this tile describes inside of the given tile set. Default is 64.
        /// </summary>
        public int tileHeight = 64;
        /// <summary>
        /// Row this tile occupies in its <c>TileMap<c>.
        /// </summary>
        public int row;
        /// <summary>
        /// Column this tile occupies in its <c>TileMap<c>.
        /// </summary>
        public int column;
        /// <summary>
        /// Layer this tile occupies in its <c>TileMap<c>.
        /// </summary>
        public int layer;
        /// <summary>
        /// Texture this tile loads to be drawn.
        /// </summary>
        public Texture2D tile;
        /// <summary>
        /// Color this tile loads when drawn.
        /// </summary>
        public Color color;

        /// <summary>
        /// Constructs a tile with the following properties.
        /// <param name="tileID"> is parsed to get the tiles <c>tileSetName</c> and tiles <c>x</c> and <c>y</c> values. </param>
        /// </summary>
        public Tile(string tileID, int row, int column, int layer)
        {
            this.row = row;
            this.column = column;
            this.layer = layer;
            if (tileID == "BLACK")
            {
                this.tileSetName = tileID;
                x = 0;
                y = 0;
                color = Color.Black;
            }
            else
            {
                this.tileSetName = tileID.Substring(0, tileID.IndexOf('('));
                System.Diagnostics.Debug.WriteLine(tileID.IndexOf('(') + ":" + tileID.IndexOf(',') + ":" + tileID.Length);
                x = int.Parse(tileID.Substring(tileID.IndexOf('(') + 1, tileID.IndexOf(',') - (tileID.IndexOf('(') + 1)));
                y = int.Parse(tileID.Substring(tileID.IndexOf(',') + 1, tileID.IndexOf(')') - (tileID.IndexOf(',') + 1)));
                color = Color.White;
            }
        }
        /// <summary>
        /// Constructs a tile with the following properties.
        /// <param name="tileID"> is parsed to get the tiles <c>tileSetName</c> and tiles <c>x</c> and <c>y</c> values. </param>
        /// </summary>
        public Tile(string tileID, int row, int column, int layer, int tileWidth, int tileHeight) : this(tileID, row, column, layer)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }
        /// <summary>
        /// If this tiles <c>tileSetName</c> matches a tile set inside of the provided <c>tileSets</c> then it will load the corresponding tile graphics from the matching tile set.
        /// </summary>
        public void loadTile(Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Texture2D i in tileSets)
            {
                if (tileSetName == i.Name)
                {
                    tile = new Texture2D(device, tileWidth, tileHeight);
                    Color[] newColor = new Color[tileWidth * tileHeight];
                    Rectangle selectionArea = new Rectangle(x, y, tileWidth, tileHeight);

                    i.GetData(0, selectionArea, newColor, 0, newColor.Length);

                    tile.SetData(newColor);
                }
            }
        }
        /// <summary>
        /// Loads the graphics from <c>tile</c> into this tiles graphics. <c>tile</c>> must already be loaded.
        /// </summary>
        public void loadTile(Texture2D tile)
        {
            this.tile = tile;
        }
        /// <summary>
        /// Draws the tiles texture.
        /// </summary>
        public void drawTile(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile, new Vector2(row * tileWidth, column * tileHeight), color);
        }
    }
}
