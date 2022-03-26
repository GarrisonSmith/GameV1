using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    /// <summary>
    /// Describes layers of tile maps from a given string description. Tiles are 64 by 64 pixels large.
    /// </summary>
    class TileMap
    {
        /// <summary>
        /// List containing the TileMapLayers of this TileMap.
        /// </summary>
        public List<TileMapLayer> map = new List<TileMapLayer>();

        /// <summary>
        /// Constructs a TileMapLayer with the given properties.
        /// <param name="initialized"> is parsed to describe the tiles in the map. </param>
        /// </summary>>
        public TileMap(String initialize)
        {
            string[] layerTemp = initialize.Split("<");
            foreach (string i in layerTemp)
            {
                if (i != "")
                {
                    addLayer(new TileMapLayer(int.Parse(i.Substring(0, i.IndexOf('>'))), i.Substring(i.IndexOf('>') + 1)));
                }
            }
        }
        /// <summary>
        /// Adds the given <c>TileMapLayer</c> to the TileMap.
        /// If the given <c>TileMapLayer</c> is assigned to a layer already present in TileMap then the present TileMapLayer is replaced. 
        /// </summary>
        public void addLayer(TileMapLayer mapLayer)
        {
            for (int i = 0; i<map.Count; i++)
            {
                if (map[i].layer == mapLayer.layer)
                {
                    map[i] = mapLayer;
                    return;
                }
            }
            map.Add(mapLayer);
        }
        /// <summary>
        /// Loads all tiles with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadLayers(Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (TileMapLayer i in map)
            {
                if (i != null)
                { 
                    i.loadTiles(tileSets, device); 
                }
            }
        }
        /// <summary>
        /// Describes layers of tile maps from a given string description.
        /// </summary>
        public void drawLayers(SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                i.drawTiles(_spriteBatch);
            }
        }
        /// <summary>
        /// Draws all tiles in the TileMap.
        /// </summary>
        public void drawLayer(int layer, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer j in map)
            {
                if (j.layer == layer)
                {
                    j.drawTiles(_spriteBatch);
                }
            }
        }
        /// <summary>
        /// Draws the TileMapLayer with the corrasponding layer value in the TileMap.
        /// </summary>
        public TileMapLayer getLayer(int index)
        {
            return map[index];
        }
        /// <summary>
        /// Returns the number of TileMapLayers present in the TileMap.
        /// </summary>
        public int getNumberOfLayer()
        {
            int count = 0;
            foreach (TileMapLayer i in map)
            {
                if (i != null)
                {
                    count++;
                }
            }
            return count;
        }
    }
    
    /// <summary>
    /// Desribes a layer of tiles in a given <c>TileMap</c>.
    /// </summary>
    class TileMapLayer
    {
        /// <summary>
        /// List containing this layers tiles.
        /// </summary>
        List<Tile> map = new List<Tile>();
        /// <summary>
        /// The layer this TileMapLayer will be drawn on.
        /// </summary>
        public int layer;

        /// <summary>
        /// Constructs a TileMapLayer with the given properties.
        /// <param name="initialized"> is parsed to describe the tiles in the map. </param>
        /// </summary>>
        public TileMapLayer(int layer, String initialize)
        {
            this.layer = layer;
            string[] columnTemp;
            string[] rowTemp;
            int column = 0;

            columnTemp = initialize.Split(";");
            for (int i = 0; i < columnTemp.Length; i++)
            {
                int row = 0;
                rowTemp = columnTemp[i].Split(":");
                foreach (string j in rowTemp)
                {
                    if (j == "BLANK")
                    {
                        //does nothing. Blank tiles are not added to the layer.
                    }
                    else if (j != "")
                    {
                        map.Add(new Tile(j, row, column));
                    }
                    row++;
                }
                column++;
            }
        }
        /// <summary>
        /// Add the given tile to the TileMapLayer.
        /// </summary>
        public void addTile(Tile tile)
        {
            map.Add(tile);
        }
        /// <summary>
        /// Loads all tiles with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadTiles(Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Tile i in map)
            {
                i.loadTile(tileSets, device);
            }
        }
        /// <summary>
        /// Draws all tiles in the TileMapLayer.
        /// </summary>
        public void drawTiles(SpriteBatch _spriteBatch)
        {
            foreach (Tile j in map)
            {
                j.drawTile(_spriteBatch);
            }
        }
        /// <summary>
        /// Draws the tile with the corrasponding index in the TileMapLayer.
        /// </summary>
        public void drawTile(int index, SpriteBatch _spriteBatch)
        {
            map[index].drawTile(_spriteBatch);
        }
        /// <summary>
        /// Returns the tile with the given index form the TileMaplayer.
        /// </summary>
        public Tile getTile(int index)
        {
            return map[index];
        }
    }

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
        /// Top left x coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public int x;
        /// <summary>
        /// Top left y coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public int y;
        /// <summary>
        /// Row this tile occupies in its <c>TileMap<c>.
        /// </summary>
        public int row;
        /// <summary>
        /// Column this tile occupies in its <c>TileMap<c>.
        /// </summary>
        public int column;
        /// <summary>
        /// Texture this tile loads to be drawn.
        /// </summary>
        public Texture2D tile;
        /// <summary>
        /// Color this tile loads when drawn.
        /// </summary>
        public Color color;

        /// <summary>
        /// Constructs a tile with the given properties.
        /// <param name="tileID"> is parsed to get the tiles <c>tileSetName</c> and tiles <c>x</c> and <c>y</c> values. </param>
        /// </summary>
        public Tile(string tileID, int row, int column)
        {
            this.row = row;
            this.column = column;
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
                x = int.Parse(tileID.Substring(tileID.IndexOf('(') + 1, tileID.IndexOf(',') - (tileID.IndexOf('(') + 1)));
                y = int.Parse(tileID.Substring(tileID.IndexOf(',') + 1, tileID.IndexOf(')') - (tileID.IndexOf(',') + 1)));
                color = Color.White;
            }
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
                    tile = new Texture2D(device, 64, 64);
                    Color[] newColor = new Color[64 * 64];
                    Rectangle selectionArea = new Rectangle(x, y, 64, 64);

                    i.GetData(0, selectionArea, newColor, 0, newColor.Length);

                    tile.SetData(newColor);
                }
            }
        }
        /// <summary>
        /// C
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
            spriteBatch.Draw(tile, new Vector2(row * 64, column * 64), color);
        }
    }
}
