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
        /// List containing the textures of the tiles in this TileMap.
        /// </summary>
        public List<Texture2D> tileTextures = new List<Texture2D>();

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
        /// Returns the TileMapLayer with the corrasponding layer values in the TileMap. Returns null if the layers is not present.
        /// </summary>
        public List<TileMapLayer> getLayers(int[] layers)
        {
            List<TileMapLayer> tempList = new List<TileMapLayer>();
            foreach (TileMapLayer i in map)
            {
                foreach (int l in layers)
                {
                    if (i.layer == l)
                    {
                        tempList.Add(i);
                    }
                }
            }
            return tempList;
        }
        /// <summary>
        /// Returns the TileMapLayer with the corrasponding layer value in the TileMap. Returns null if the layer is not present.
        /// </summary>
        public TileMapLayer getLayer(int layer)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    return i;
                }
            }
            return null;
        }
        /// <summary>
        /// Loads all textures being being used by this TileMap from all layers.
        /// </summary>
        public void loadTileTextures(Texture2D[] tileSets, GraphicsDevice _device)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    foreach (Texture2D k in tileSets)
                    {
                        if (j.tileSetName == k.Name)
                        {
                            Texture2D tile = new Texture2D(_device, 64, 64);
                            Color[] newColor = new Color[64 * 64];
                            Rectangle selectionArea = new Rectangle(j.tileSetCoordinate.X, j.tileSetCoordinate.Y, 64, 64);

                            k.GetData(0, selectionArea, newColor, 0, newColor.Length);
                            tile.SetData(newColor);
                            if (!tileTextures.Contains(tile))
                            {
                                tileTextures.Add(tile);
                            }
                            j.graphicsIndex = tileTextures.IndexOf(tile);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Unloads all textures being being used by this TileMap from all layers.
        /// </summary>
        public void unloadTileTextures()
        {
            tileTextures = null;
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap.
        /// </summary>
        public void drawLayers(Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    foreach (Texture2D k in tileTextures)
                    {
                        _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                            new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                            new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                            stretch, new SpriteEffects(), 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layers.
        /// </summary>
        public void drawLayers(int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            foreach (Texture2D k in tileTextures)
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer.
        /// </summary>
        public void drawLayer(int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        foreach (Texture2D k in tileTextures)
                        {
                            _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                stretch, new SpriteEffects(), 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows.
        /// </summary>
        public void drawRows(int[] rows, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int r in rows)
            {
                foreach (TileMapLayer i in map)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == r)
                        {
                            foreach (Texture2D k in tileTextures)
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layers.
        /// </summary>
        public void drawRows(int[] rows, int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int r in rows)
            {
                foreach (int l in layers)
                {
                    foreach (TileMapLayer i in map)
                    {
                        if (i.layer == l)
                        {
                            foreach (Tile j in i.map)
                            {
                                if (j.tileMapCoordinate.X == r)
                                {
                                    foreach (Texture2D k in tileTextures)
                                    {
                                        _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                            new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                            new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                            stretch, new SpriteEffects(), 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layer.
        /// </summary>
        public void drawRows(int[] rows, int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int r in rows)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == layer)
                    {
                        foreach (Tile j in i.map)
                        {
                            if (j.tileMapCoordinate.X == r)
                            {
                                foreach (Texture2D k in tileTextures)
                                {
                                    _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                        new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                        new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                        stretch, new SpriteEffects(), 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row.
        /// </summary>
        public void drawRow(int row, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == row)
                    {
                        foreach (Texture2D k in tileTextures)
                        {
                            _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                stretch, new SpriteEffects(), 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layers.
        /// </summary>
        public void drawRow(int row, int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            if (j.tileMapCoordinate.X == row)
                            {
                                foreach (Texture2D k in tileTextures)
                                {
                                    _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                        new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                        new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                        stretch, new SpriteEffects(), 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layer.
        /// </summary>
        public void drawRows(int row, int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == row)
                        {
                            foreach (Texture2D k in tileTextures)
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns.
        /// </summary>
        public void drawColumns(int[] columns, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int c in columns)
            {
                foreach (TileMapLayer i in map)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == c)
                        {
                            foreach (Texture2D k in tileTextures)
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layers.
        /// </summary>
        public void drawColumns(int[] columns, int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int c in columns)
            {
                foreach (int l in layers)
                {
                    foreach (TileMapLayer i in map)
                    {
                        if (i.layer == l)
                        {
                            foreach (Tile j in i.map)
                            {
                                if (j.tileMapCoordinate.X == c)
                                {
                                    foreach (Texture2D k in tileTextures)
                                    {
                                        _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                            new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                            new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                            stretch, new SpriteEffects(), 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layer.
        /// </summary>
        public void draweColumns(int[] columns, int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int c in columns)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == layer)
                    {
                        foreach (Tile j in i.map)
                        {
                            if (j.tileMapCoordinate.X == c)
                            {
                                foreach (Texture2D k in tileTextures)
                                {
                                    _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                        new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                        new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                        stretch, new SpriteEffects(), 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column.
        /// </summary>
        public void drawColumn(int column, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == column)
                    {
                        foreach (Texture2D k in tileTextures)
                        {
                            _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                stretch, new SpriteEffects(), 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layers.
        /// </summary>
        public void drawColumn(int column, int[] layers, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            if (j.tileMapCoordinate.X == column)
                            {
                                foreach (Texture2D k in tileTextures)
                                {
                                    _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                        new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                        new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                        stretch, new SpriteEffects(), 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layer.
        /// </summary>
        public void drawColumns(int column, int layer, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == column)
                        {
                            foreach (Texture2D k in tileTextures)
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy with in the provided rectangle <c>drawArea</c>.
        /// </summary>
        public void drawArea(Rectangle drawArea, Vector2 stretch, SpriteBatch _spriteBatch)
        {
            drawArea = new Rectangle((int)(drawArea.X * stretch.X), (int)(drawArea.Y * stretch.Y),
                (int)(drawArea.Width * stretch.X), (int)(drawArea.Height * stretch.Y));
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    Rectangle tileArea = new Rectangle((int)(j.tileMapCoordinate.X * 64 * stretch.X), (int)(j.tileMapCoordinate.Y * 64 * stretch.Y),
                        (int)(64 * stretch.X), (int)(64 * stretch.Y));
                    if (tileArea.Intersects(drawArea))
                    {
                        _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                    new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                    new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                    stretch, new SpriteEffects(), 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy with in the provided layers and rectangle <c>drawArea</c>.
        /// </summary>
        public void drawArea(Rectangle drawArea, Vector2 stretch, SpriteBatch _spriteBatch, int[] layers)
        {
            drawArea = new Rectangle((int)(drawArea.X * stretch.X), (int)(drawArea.Y * stretch.Y),
                (int)(drawArea.Width * stretch.X), (int)(drawArea.Height * stretch.Y));
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            Rectangle tileArea = new Rectangle((int)(j.tileMapCoordinate.X * 64 * stretch.X), (int)(j.tileMapCoordinate.Y * 64 * stretch.Y),
                                (int)(64 * stretch.X), (int)(64 * stretch.Y));
                            if (tileArea.Intersects(drawArea))
                            {
                                _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                            new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                            new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                            stretch, new SpriteEffects(), 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy with in the provided layer and rectangle <c>drawArea</c>.
        /// </summary>
        public void drawArea(Rectangle drawArea, Vector2 stretch, SpriteBatch _spriteBatch, int layer)
        {
            drawArea = new Rectangle((int)(drawArea.X * stretch.X), (int)(drawArea.Y * stretch.Y),
                (int)(drawArea.Width * stretch.X), (int)(drawArea.Height * stretch.Y));
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        Rectangle tileArea = new Rectangle((int)(j.tileMapCoordinate.X * 64 * stretch.X), (int)(j.tileMapCoordinate.Y * 64 * stretch.Y),
                            (int)(64 * stretch.X), (int)(64 * stretch.Y));
                        if (tileArea.Intersects(drawArea))
                        {
                            _spriteBatch.Draw(tileTextures[j.graphicsIndex],
                                        new Vector2(j.tileMapCoordinate.X * 64 * stretch.X, j.tileMapCoordinate.Y * 64 * stretch.Y),
                                        new Rectangle(0, 0, 64, 64), j.color, 0, new Vector2(0, 0),
                                        stretch, new SpriteEffects(), 0);
                        }
                    }
                }
            }
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
        public Rectangle getTileMapBounding(Vector2 stretch)
        {
            return new Rectangle(0, 0, 1, 1);
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
        public List<Tile> map = new List<Tile>();
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
            this.width = row + 1;
            this.height = column + 1;
        }
        /// <summary>
        /// Returns the tile with the given index from the TileMaplayer. If the index is invalid returns null.
        /// </summary>
        public Tile getTile(int index)
        {
            if (map.Count - 1 >= index)
            {
                return map[index];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Returns the tile with the given coordinate from the TileMaplayer. If the coordiante is invalid returns null.
        /// </summary>
        public Tile getTile(Point coordinate)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate == coordinate)
                {
                    return i;
                }
            }
            return null;
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
