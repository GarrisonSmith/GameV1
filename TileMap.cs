using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Graphics
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
        /// List containing the layer index of all loaded tiles in this TileMap.
        /// </summary>
        public List<int> loadedLayers = new List<int>();
        /// <summary>
        /// Describes the bounding box of this TileMap.
        /// </summary>
        public Rectangle tileMapBounding;

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
            for (int i = 0; i < map.Count; i++)
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
                    if (!loadedLayers.Contains(i.layer))
                    {
                        loadedLayers.Add(i.layer);
                    }
                }
            }
        }
        /// <summary>
        /// Loads all tiles in the given layers with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadLayers(int[] layers, Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (int j in layers)
                {
                    if (i != null && j == i.layer)
                    {
                        i.loadTiles(tileSets, device);
                        if (!loadedLayers.Contains(i.layer))
                        {
                            loadedLayers.Add(i.layer);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Loads all tiles in the given layer with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadLayer(int layer, Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (TileMapLayer i in map)
            {
                if (i != null && i.layer == layer)
                {
                    i.loadTiles(tileSets, device);
                    if (!loadedLayers.Contains(i.layer))
                    {
                        loadedLayers.Add(i.layer);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles in the TileMap.
        /// </summary>
        public void drawLayers(Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                i.drawTiles(stretch, _spriteBatch);
            }
        }
        /// <summary>
        /// Draws the given layers in the TileMap.
        /// </summary>
        public void drawLayers(int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (int j in layers)
                {
                    if (j == i.layer)
                    {
                        i.drawTiles(stretch, _spriteBatch);
                    }
                }
            }
        }
        /// <summary>
        /// Draws the given layer in the TileMap.
        /// </summary>
        public void drawLayer(int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.drawTiles(stretch, _spriteBatch);
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
        /// <summary>
        /// Returns the bounding box of the TileMap.
        /// </summary>
        public Rectangle getTileMapBounding(int[] layers, float stretch)
        {
            return tileMapBounding;
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
        /// The width of this TileMapLayer.
        /// </summary>
        public int width;
        /// <summary>
        /// The height of this TileMapLayer.
        /// </summary>
        public int height;

        /// <summary>
        /// Constructs a TileMapLayer with the given properties.
        /// <param name="initialized"> is parsed to describe the tiles in the map. </param>
        /// </summary>>
        public TileMapLayer(int layer, String initialize)
        {
            this.layer = layer;
            string[] columnTemp;
            string[] rowTemp;
            int row = 0;
            int column = 0;

            columnTemp = initialize.Split(";");
            for (int i = 0; i < columnTemp.Length; i++)
            {
                row = 0;
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
            this.width = row+1;
            this.height = column+1;
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
        /// Loads all tiles inside <c>indexes</c> with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadTiles(int[] indexes, Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (int i in indexes)
            {
                map[i].loadTile(tileSets, device);
            }
        }
        /// <summary>
        /// Loads all tiles within the <c>row</c> with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadRow(int row, Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.X == row)
                {
                    i.loadTile(tileSets, device);
                }
            }
        }
        /// <summary>
        /// Loads all tiles within the <c>column</c> with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadColumn(int column, Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.Y== column)
                {
                    i.loadTile(tileSets, device);
                }
            }
        }
        /// <summary>
        /// Loads the tile at <c>index</c> with the correct graphics in <c>tileSets</c> if the correct graphic is present. 
        /// </summary>
        public void loadTile(int index, Texture2D[] tileSets, GraphicsDevice device)
        {
            map[index].loadTile(tileSets, device);
        }
        /// <summary>
        /// Unloads all tiles in <c>tileSets</c>. 
        /// </summary>
        public void unloadTiles()
        {
            foreach (Tile i in map)
            {
                i.unloadTile();
            }
        }
        /// <summary>
        /// Unloads all tiles inside <c>indexes</c>. 
        /// </summary>
        public void unloadTiles(int[] indexes)
        {
            foreach (int i in indexes)
            {
                map[i].unloadTile();
            }
        }
        /// <summary>
        /// Unloads all tiles within the <c>row</c>. 
        /// </summary>
        public void unloadRow(int row)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.X == row)
                {
                    i.unloadTile();
                }
            }
        }
        /// <summary>
        /// Unloads all tiles within the <c>column</c>. 
        /// </summary>
        public void unloadColumn(int column)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.Y == column)
                {
                    i.unloadTile();
                }
            }
        }
        /// <summary>
        /// Unloads the tile at <c>index</c>. 
        /// </summary>
        public void unloadTile(int index)
        {
            map[index].unloadTile();
        }
        /// <summary>
        /// Draws all tiles in the TileMapLayer.
        /// </summary>
        public void drawTiles(Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (Tile i in map)
            {
                i.drawTile(stretch, _spriteBatch);
            }
        }
        /// <summary>
        /// Draws all tiles inside <c>indexes</c>> in the TileMapLayer.
        /// </summary>
        public void drawTiles(int[] indexes, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int i in indexes)
            {
                map[i].drawTile(stretch, _spriteBatch);
            }
        }
        /// <summary>
        /// Draws the tile with the corrasponding index in the TileMapLayer.
        /// </summary>
        public void drawTile(int index, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            map[index].drawTile(stretch, _spriteBatch);
        }
        /// <summary>
        /// Draws all tiles within <c>row</c>> in the TileMapLayer.
        /// </summary>
        public void drawRow(int row, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.X == row)
                {
                    i.drawTile(stretch, _spriteBatch);
                }
            }
        }
        /// <summary>
        /// Draws all tiles within <c>column</c>> in the TileMapLayer.
        /// </summary>
        public void drawColumn(int column, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.Y == column)
                {
                    i.drawTile(stretch, _spriteBatch);
                }
            }
        }
        /// <summary>
        /// Returns the tile with the given index form the TileMaplayer.
        /// </summary>
        public Tile getTile(int index)
        {
            return map[index];
        }
        /// <summary>
        /// Returns the layer dimensions in as a point containing width and height.
        /// </summary>
        public Point getLayerDimension()
        {
            return new Point(this.width, this.height);
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
        /// Top left coordinate of the tile area this tile describes inside of the given tile set.
        /// </summary>
        public Point tileSetCoordinate;
        /// <summary>
        /// Point this tile occupies in its <c>TileMapLayer<c>.
        /// </summary>
        public Point tileMapCoordinate;
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
                    Rectangle selectionArea = new Rectangle(tileSetCoordinate.X, tileSetCoordinate.Y, 64, 64);

                    i.GetData(0, selectionArea, newColor, 0, newColor.Length);

                    tile.SetData(newColor);
                }
            }
        }
        /// <summary>
        /// Loads the provided graphic into this tile.
        /// </summary>
        public void loadTile(Texture2D tile)
        {
            this.tile = tile;
        }
        /// <summary>
        /// Unloads the graphic from this tile.
        /// </summary>
        public void unloadTile()
        {
            tile = null;
        }
        /// <summary>
        /// Draws the tiles texture with the provided stretch.
        /// </summary>
        public void drawTile(Vector2 stretch, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile,
                new Vector2(tileMapCoordinate.X * 64 * stretch.X, tileMapCoordinate.Y * 64 * stretch.Y),
                new Rectangle(0, 0, 64, 64), color, 0, new Vector2(0, 0),
                stretch, new SpriteEffects(), 0);
        }

    }
}
