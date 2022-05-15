using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.graphics
{
    /// <summary>
    /// Describes layers of tile maps from a given string description. Tiles are 64 by 64 pixels large.
    /// </summary>
    class TileMap
    {
        /// <summary>
        /// List containing the TileMapLayers of this TileMap.
        /// </summary>
        public List<TileMapLayer> map;
        /// <summary>
        /// List containing the textures of the tiles in this TileMap.
        /// </summary>
        public List<Texture2D> tileTextures;

        /// <summary>
        /// Constructs a TileMapLayer with the given properties.
        /// <param name="initialized"> is parsed to describe the tiles in the map. </param>
        /// </summary>>
        public TileMap(String initialize)
        {
            this.map = new List<TileMapLayer>();
            this.tileTextures = new List<Texture2D>();
            string[] layerTemp = initialize.Split("<");
            foreach (string i in layerTemp)
            {
                if (i != "")
                {
                    AddLayer(new TileMapLayer(int.Parse(i.Substring(0, i.IndexOf('>'))), i.Substring(i.IndexOf('>') + 1)));
                }
            }
        }
        /// <summary>
        /// Adds the given <c>TileMapLayer</c> to the TileMap.
        /// If the given <c>TileMapLayer</c> is assigned to a layer already present in TileMap then the present TileMapLayer is replaced. 
        /// </summary>
        public void AddLayer(TileMapLayer mapLayer)
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
        public List<TileMapLayer> GetLayers(int[] layers)
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
        public TileMapLayer GetLayer(int layer)
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
        public void LoadTileTextures(Texture2D[] tileSets, GraphicsDeviceManager _graphics)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    foreach (Texture2D k in tileSets)
                    {
                        if (j.tileSetName == k.Name)
                        {
                            Texture2D tile = new Texture2D(_graphics.GraphicsDevice, 64, 64);
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
        public void UnloadTileTextures()
        {
            tileTextures = null;
        }
        public void DrawToMap(Texture2D texture, Color color, Vector2 stretch, SpriteBatch _spriteBatch, int row, int column, int horizontalOffSet, int verticalalOffSet)
        {
            _spriteBatch.Draw(texture,
                            new Vector2(((row * 64)+horizontalOffSet) * stretch.X, ((-(column + 1) * 64) - verticalalOffSet )* stretch.Y),
                            new Rectangle(0, 0, 64, 64), color, 0, new Vector2(0, 0),
                            stretch, new SpriteEffects(), 0);
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap.
        /// </summary>
        public void DrawLayers(Vector2 stretch, SpriteBatch _spriteBatch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layers.
        /// </summary>
        public void DrawLayers(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (0 == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer.
        /// </summary>
        public void DrawLayer(Vector2 stretch, SpriteBatch _spriteBatch, int layer)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows.
        /// </summary>
        public void DrawRows(Vector2 stretch, SpriteBatch _spriteBatch, int[] rows)
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
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layers.
        /// </summary>
        public void DrawRows(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers, int[] rows)
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
                                    DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
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
        public void DrawRows(Vector2 stretch, SpriteBatch _spriteBatch, int layer, int[] rows)
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
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row.
        /// </summary>
        public void DrawRow(Vector2 stretch, SpriteBatch _spriteBatch, int row)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == row)
                    {
                        DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layers.
        /// </summary>
        public void DrawRow(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers, int row)
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
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layer.
        /// </summary>
        public void DrawRows(Vector2 stretch, SpriteBatch _spriteBatch, int layer, int row)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == row)
                        {
                            DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns.
        /// </summary>
        public void DrawColumns(Vector2 stretch, SpriteBatch _spriteBatch, int[] columns)
        {
            foreach (int c in columns)
            {
                foreach (TileMapLayer i in map)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == c)
                        {
                            DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layers.
        /// </summary>
        public void DrawColumns(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers, int[] columns)
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
                                    DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
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
        public void DrawColumns(Vector2 stretch, SpriteBatch _spriteBatch, int layer, int[] columns)
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
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column.
        /// </summary>
        public void DrawColumn(Vector2 stretch, SpriteBatch _spriteBatch, int column)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == column)
                    {
                        DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layers.
        /// </summary>
        public void DrawColumn(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers, int column)
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
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layer.
        /// </summary>
        public void DrawColumns(Vector2 stretch, SpriteBatch _spriteBatch, int layer, int column)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == column)
                        {
                            DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy within the provided rectangle <c>drawArea</c>.
        /// </summary>
        public void DrawArea(Vector2 stretch, SpriteBatch _spriteBatch, Rectangle drawArea)
        {
            drawArea.Y = -drawArea.Y;

            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    Rectangle tileArea = new Rectangle((int)(j.tileMapCoordinate.X * 64 * stretch.X), (int)(-j.tileMapCoordinate.Y * 64 * stretch.Y),
                        (int)(64 * stretch.X), (int)(64 * stretch.Y));
                    if (tileArea.Intersects(drawArea))
                    {
                        DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy within the provided layers and rectangle <c>drawArea</c>.
        /// </summary>
        public void DrawArea(Vector2 stretch, SpriteBatch _spriteBatch, int[] layers, Rectangle drawArea)
        {
            drawArea.Y = -drawArea.Y;

            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            Rectangle tileArea = new Rectangle(
                                (int)(j.tileMapCoordinate.X * 64),
                                (int)(-j.tileMapCoordinate.Y * 64),
                                (int)(64),
                                (int)(64));

                            if (tileArea.Intersects(drawArea))
                            {
                                DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy within the provided layer and rectangle <c>drawArea</c>.
        /// </summary>
        public void DrawArea(Vector2 stretch, SpriteBatch _spriteBatch, int layer, Rectangle drawArea)
        {
            drawArea.Y = -drawArea.Y;

            foreach (TileMapLayer i in map)
            {
                if (0 == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        Rectangle tileArea = new Rectangle(
                                (int)(j.tileMapCoordinate.X * 64),
                                (int)(-j.tileMapCoordinate.Y * 64),
                                (int)(64),
                                (int)(64));

                        if (tileArea.Intersects(drawArea))
                        {
                            DrawToMap(tileTextures[j.graphicsIndex], j.color, stretch, _spriteBatch, j.tileMapCoordinate.X, j.tileMapCoordinate.Y, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Returns the number of TileMapLayers present in the TileMap.
        /// </summary>
        public int GetNumberOfLayer()
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
        /// Returns a rectangle that is the size and location of the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Rectangle GetTileMapBounding(Vector2 stretch)
        {
            int widthLargest = 1, widthSmallest = 1;
            int heightLargest = 1, heightSmallest = 1;

            foreach (TileMapLayer i in map)
            {
                if (i.width > widthLargest)
                {
                    widthLargest = i.width;
                }
                else if (i.width < widthSmallest)
                {
                    widthSmallest = i.width;
                }

                if (i.height > heightLargest)
                {
                    heightLargest = i.height;
                }
                else if (i.height < heightSmallest)
                {
                    heightSmallest = i.height;
                }
            }

            return new Rectangle(
                (int)Math.Round((widthSmallest * 64 * stretch.X)),
                (int)Math.Round(((heightLargest+1) * 64 * stretch.X)),
                (int)Math.Round(((widthLargest - widthSmallest + 1) * 64 * stretch.X)),
                (int)Math.Round(((heightLargest - heightSmallest + 1) * 64 * stretch.Y)));
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided <c>layers</c> in the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Rectangle GetTileMapBounding(Vector2 stretch, int[] layers)
        {
            int widthLargest = 2, widthSmallest = 2;
            int heightLargest = 2, heightSmallest = 2;

            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        if (i.width > widthLargest)
                        {
                            widthLargest = i.width;
                        }
                        else if (i.width < widthSmallest)
                        {
                            widthSmallest = i.width;
                        }

                        if (i.height > heightLargest)
                        {
                            heightLargest = i.height;
                        }
                        else if (i.height < heightSmallest)
                        {
                            heightSmallest = i.height;
                        }
                    }
                }
            }

            return new Rectangle(
                (int)Math.Round(((widthSmallest - 1) * 64 * stretch.X)),
                (int)Math.Round(((heightSmallest - 1) * 64 * stretch.X)),
                (int)Math.Round(((widthLargest - (widthSmallest - 1)) * 64 * stretch.X)),
                (int)Math.Round(((heightLargest - (heightSmallest - 1)) * 64 * stretch.Y)));
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided <c>layer</c> in the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Rectangle GetTileMapBounding(Vector2 stretch, int layer)
        {
            int widthLargest = 2, widthSmallest = 2;
            int heightLargest = 2, heightSmallest = 2;

            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    if (i.width > widthLargest)
                    {
                        widthLargest = i.width;
                    }
                    else if (i.width < widthSmallest)
                    {
                        widthSmallest = i.width;
                    }

                    if (i.height > heightLargest)
                    {
                        heightLargest = i.height;
                    }
                    else if (i.height < heightSmallest)
                    {
                        heightSmallest = i.height;
                    }
                }
            }

            return new Rectangle(
                (int)Math.Round(((widthSmallest - 1) * 64 * stretch.X)),
                (int)Math.Round(((heightSmallest - 1) * 64 * stretch.X)),
                (int)Math.Round(((widthLargest - (widthSmallest - 1)) * 64 * stretch.X)),
                (int)Math.Round(((heightLargest - (heightSmallest - 1)) * 64 * stretch.Y)));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Point GetTileMapCenter(Vector2 stretch)
        {
            int widthLargest = 2, widthSmallest = 2;
            int heightLargest = 2, heightSmallest = 2;

            foreach (TileMapLayer i in map)
            {
                if (i.width > widthLargest)
                {
                    widthLargest = i.width;
                }
                else if (i.width < widthSmallest)
                {
                    widthSmallest = i.width;
                }

                if (i.height > heightLargest)
                {
                    heightLargest = i.height;
                }
                else if (i.height < heightSmallest)
                {
                    heightSmallest = i.height;
                }
            }

            return new Point(
                (int)Math.Round((((widthSmallest - 1) * 64) + ((widthLargest - (widthSmallest - 1)) * 32)) * stretch.X),
                (int)Math.Round((((heightSmallest - 1) * 64) + ((heightLargest - (heightSmallest - 1)) * 32)) * stretch.Y));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided <c>layers</c> in the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Point GetTileMapCenter(Vector2 stretch, int[] layers)
        {
            int widthLargest = 2, widthSmallest = 2;
            int heightLargest = 2, heightSmallest = 2;

            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        if (i.width > widthLargest)
                        {
                            widthLargest = i.width;
                        }
                        else if (i.width < widthSmallest)
                        {
                            widthSmallest = i.width;
                        }

                        if (i.height > heightLargest)
                        {
                            heightLargest = i.height;
                        }
                        else if (i.height < heightSmallest)
                        {
                            heightSmallest = i.height;
                        }
                    }
                }
            }

            return new Point(
                (int)Math.Round((((widthSmallest - 1) * 64) + ((widthLargest - (widthSmallest - 1)) * 32)) * stretch.X),
                (int)Math.Round((((heightSmallest - 1) * 64) + ((heightLargest - (heightSmallest - 1)) * 32)) * stretch.Y));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided <c>layer</c> in the TileMap with the provided <c>stretch</c>.
        /// </summary>
        public Point GetTileMapCenter(Vector2 stretch, int layer)
        {
            int widthLargest = 2, widthSmallest = 2;
            int heightLargest = 2, heightSmallest = 2;

            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    if (i.width > widthLargest)
                    {
                        widthLargest = i.width;
                    }
                    else if (i.width < widthSmallest)
                    {
                        widthSmallest = i.width;
                    }

                    if (i.height > heightLargest)
                    {
                        heightLargest = i.height;
                    }
                    else if (i.height < heightSmallest)
                    {
                        heightSmallest = i.height;
                    }
                }
            }

            return new Point(
                (int)Math.Round((((widthSmallest - 1) * 64) + ((widthLargest - (widthSmallest - 1)) * 32)) * stretch.X),
                (int)Math.Round((((heightSmallest - 1) * 64) + ((heightLargest - (heightSmallest - 1)) * 32)) * stretch.Y));
        }
    }
}