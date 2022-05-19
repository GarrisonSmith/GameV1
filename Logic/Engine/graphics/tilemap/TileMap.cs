using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.utility;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Contains a tile maps from a given string description. Tiles are 64 by 64 pixels large.
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
        /// Constructs a TileMapLayer from the provided string.
        /// </summary>
        /// <param name="initialized">string to be parsed to describe the Tiles in the TileMap.</param>
        public TileMap(string initialize)
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
        /// Adds the given TileMapLayer to the TileMap.
        /// If the given TileMapLayer is assigned to a layer already present in TileMap then the present TileMapLayer is replaced. 
        /// </summary>
        /// <param name="mapLayer">The TileMapLayer to be added to this TileMap</param>
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
        /// Creates a list of TileMapLayers with the corrasponding layers values from this TileMap.
        /// </summary>
        /// <param name="layers">Array containing the layers to be added.</param>
        /// <returns>List containing the TileMapLayers with the corrasponding layers.</returns>         
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
        /// Finds a TileMapLayer with the corrasponding layer value from this TileMap.
        /// </summary>
        /// <param name="layers">Integer containing the layer to be added.</param>
        /// <returns>TileMapLayer with the corrasponding layer.</returns>  
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
        /// Loads all textures being being used by this TileMap from all layers into tileTextures.
        /// </summary>
        public void LoadTileTextures()
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    Texture2D tileSet = Global._content.Load<Texture2D>("tile-sets/"+j.tileSetName);
                    if (!tileTextures.Contains(tileSet))
                    {
                        tileTextures.Add(tileSet);
                    }
                    j.graphicsIndex = tileTextures.IndexOf(tileSet);
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
        /// <summary>
        /// Draws the provided Texture2D onto the TileMap with the provided location and stretching.
        /// </summary>
        /// <param name="texture">the texture to be drawn.</param>
        /// <param name="drawArea">describes the area of the provided texture to be drawn.</param>
        /// <param name="color">the shading color of the texture.</param>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="column">the column of this Tilemap that the texture to be drawn.</param>
        /// <param name="row">the row of this Tilemap that the texture to be drawn.</param>
        /// <param name="horizontalOffSet">the number of pixel the texture will be horizontally offset by.</param>
        /// <param name="verticalalOffSet">the number of pixel the texture will be vertically offset by.</param>
        public void DrawToMap(Texture2D texture, Rectangle drawArea, Color color, Vector2 _stretch, int column, int row, int horizontalOffSet, int verticalalOffSet)
        {
            Global._spriteBatch.Draw(texture,
                new Vector2(((column * 64) + horizontalOffSet) * _stretch.X, ((-(row + 1) * 64) - verticalalOffSet) * _stretch.Y),
                drawArea, color, 0, new Vector2(0, 0), _stretch, new SpriteEffects(), 0);
        }
        /// <summary>
        /// Draws all TileMapLayers in the TileMap.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        public void DrawLayers(Vector2 _stretch)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j is AnimatedTile)
                    {
                        ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                    }
                    else
                    {
                        j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all TileMapLayers in the TileMap which occupy the provided layers.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        public void DrawLayers(Vector2 _stretch, int[] layers)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (0 == l)
                    {
                        foreach (Tile j in i.map)
                        {
                            if (j is AnimatedTile)
                            {
                                ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                            else
                            {
                                j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        public void DrawLayer(Vector2 _stretch, int layer)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j is AnimatedTile)
                        {
                            ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                        else
                        {
                            j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="rows">Array containing the rows to be drawn.</param>
        public void DrawRows(Vector2 _stretch, int[] rows)
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
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layers.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        /// <param name="rows">Array containing the rows to be drawn.</param>
        public void DrawRows(Vector2 _stretch, int[] layers, int[] rows)
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
                                    if (j is AnimatedTile)
                                    {
                                        ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                    }
                                    else
                                    {
                                        j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>   
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        /// <param name="rows">Array containing the rows to be drawn.</param>
        public void DrawRows(Vector2 _stretch, int layer, int[] rows)
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
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="row">Integer containing the row to be drawn.</param>
        public void DrawRow(Vector2 _stretch, int row)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == row)
                    {
                        if (j is AnimatedTile)
                        {
                            ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                        else
                        {
                            j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layers.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        /// <param name="row">Integer containing the row to be drawn.</param>
        public void DrawRow(Vector2 _stretch, int[] layers, int row)
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
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        /// <param name="row">Integer containing the row to be drawn.</param>
        public void DrawRow(Vector2 _stretch, int layer, int row)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == row)
                        {
                            if (j is AnimatedTile)
                            {
                                ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                            else
                            {
                                j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="columns">Array containing the columns to be drawn.</param>
        public void DrawColumns(Vector2 _stretch, int[] columns)
        {
            foreach (int c in columns)
            {
                foreach (TileMapLayer i in map)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == c)
                        {
                            if (j is AnimatedTile)
                            {
                                ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                            else
                            {
                                j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layers.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        /// <param name="columns">Array containing the columns to be drawn.</param>
        public void DrawColumns(Vector2 _stretch, int[] layers, int[] columns)
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
                                    if (j is AnimatedTile)
                                    {
                                        ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                    }
                                    else
                                    {
                                        j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        /// <param name="columns">Array containing the columns to be drawn.</param>
        public void DrawColumns(Vector2 _stretch, int layer, int[] columns)
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
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="column">Integer containing the column to be drawn.</param>
        public void DrawColumn(Vector2 _stretch, int column)
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j.tileMapCoordinate.X == column)
                    {
                        if (j is AnimatedTile)
                        {
                            ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                        else
                        {
                            j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layers.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        /// <param name="column">Integer containing the column to be drawn.</param>
        public void DrawColumn(Vector2 _stretch, int[] layers, int column)
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
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
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
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        /// <param name="column">Integer containing the column to be drawn.</param>
        public void DrawColumn(Vector2 _stretch, int layer, int column)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        if (j.tileMapCoordinate.X == column)
                        {
                            if (j is AnimatedTile)
                            {
                                ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                            else
                            {
                                j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rectangle drawArea.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(Vector2 _stretch, Rectangle drawArea)
        {
            drawArea.Y = -drawArea.Y;

            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    Rectangle tileArea = new Rectangle(
                        (int)(j.tileMapCoordinate.X * 64 * _stretch.X),
                        (int)(-(j.tileMapCoordinate.Y + 1) * 64 * _stretch.Y),
                        (int)(64 * _stretch.X),
                        (int)(64 * _stretch.Y));

                    if (tileArea.Intersects(drawArea))
                    {
                        if (j is AnimatedTile)
                        {
                            ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                        else
                        {
                            j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layers and rectangle drawArea.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layers">Array containing the layers to be drawn.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(Vector2 _stretch, int[] layers, Rectangle drawArea)
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
                                (int)(j.tileMapCoordinate.X * 64 * _stretch.X),
                                (int)(-(j.tileMapCoordinate.Y + 1) * 64 * _stretch.Y),
                                (int)(64 * _stretch.X),
                                (int)(64 * _stretch.Y));

                            if (tileArea.Intersects(drawArea))
                            {
                                if (j is AnimatedTile)
                                {
                                    ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                                else
                                {
                                    j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer and rectangle drawArea.
        /// </summary>
        /// <param name="_stretch">the stretching of the texture.</param>
        /// <param name="layer">Integer containing the layer to be drawn.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(Vector2 _stretch, int layer, Rectangle drawArea)
        {
            drawArea.Y = -drawArea.Y;

            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    foreach (Tile j in i.map)
                    {
                        Rectangle tileArea = new Rectangle(
                            (int)(j.tileMapCoordinate.X * 64 * _stretch.X),
                            (int)(-(j.tileMapCoordinate.Y + 1) * 64 * _stretch.Y),
                            (int)(64 * _stretch.X),
                            (int)(64 * _stretch.Y));

                        if (tileArea.Intersects(drawArea))
                        {
                            if (j is AnimatedTile)
                            {
                                ((AnimatedTile)j).DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                            else
                            {
                                j.DrawTile(tileTextures[j.graphicsIndex], _stretch);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gets the number of TileMapLayers present in the TileMap.
        /// </summary>
        /// <returns>The number of TileMapLayers in the TileMap that are not null.</returns>
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
        /// Returns a rectangle that is the size and location of the TileMap with the provided stretching.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapBounding components.</param>
        /// <returns>Rectangle whose area contains the TileMap drawing area.</returns>
        public Rectangle GetTileMapBounding(Vector2 _stretch)
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
                (int)Math.Round((widthSmallest * 64 * _stretch.X)),
                (int)Math.Round(((heightLargest + 1) * 64 * _stretch.Y)),
                (int)Math.Round(((widthLargest - widthSmallest + 1) * 64 * _stretch.X)),
                (int)Math.Round(((heightLargest - heightSmallest + 1) * 64 * _stretch.Y)));
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided layers in the TileMap with the provided stretching.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapBounding components.</param>
        /// <param name="layers">Array containing the layers to calculate the TileMapBounding from.</param>
        /// <returns>Rectangle whose area contains the corrasponding layers of the TileMap drawing area.</returns>
        public Rectangle GetTileMapBounding(Vector2 _stretch, int[] layers)
        {
            int widthLargest = 1, widthSmallest = 1;
            int heightLargest = 1, heightSmallest = 1;

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
                 (int)Math.Round((widthSmallest * 64 * _stretch.X)),
                 (int)Math.Round(((heightLargest + 1) * 64 * _stretch.Y)),
                 (int)Math.Round(((widthLargest - widthSmallest + 1) * 64 * _stretch.X)),
                 (int)Math.Round(((heightLargest - heightSmallest + 1) * 64 * _stretch.Y)));
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided layer in the TileMap with the provided _stretch.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapBounding components.</param>
        /// <param name="layer">Integer containing the layer to calculate the TileMapBounding from.</param>
        /// <returns>Rectangle whose area contains the corrasponding layer of the TileMap drawing area.</returns>
        public Rectangle GetTileMapBounding(Vector2 _stretch, int layer)
        {
            int widthLargest = 1, widthSmallest = 1;
            int heightLargest = 1, heightSmallest = 1;

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
                 (int)Math.Round((widthSmallest * 64 * _stretch.X)),
                 (int)Math.Round(((heightLargest + 1) * 64 * _stretch.Y)),
                 (int)Math.Round(((widthLargest - widthSmallest + 1) * 64 * _stretch.X)),
                 (int)Math.Round(((heightLargest - heightSmallest + 1) * 64 * _stretch.Y)));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap with the provided stretching.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapCenter components.</param>
        /// <returns>Point containing the center of the TileMap drawing area.</returns>
        public Point GetTileMapCenter(Vector2 _stretch)
        {
            return Util.GetRectangleCenter(GetTileMapBounding(_stretch));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided layers in the TileMap with the provided stretching.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapCenter components.</param>
        /// <param name="layers">Array containing the layers to calculate the TileMapCenter from.</param>
        /// <returns>Point containing the center of the corrasponding layers of theTileMap drawing area.</returns>
        public Point GetTileMapCenter(Vector2 _stretch, int[] layers)
        {
            return Util.GetRectangleCenter(GetTileMapBounding(_stretch, layers));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided layer in the TileMap with the provided _stretch.
        /// </summary>
        /// <param name="_stretch">the stretching of the TileMapCenter components.</param>
        /// <param name="layer">Array containing the layer to calculate the TileMapCenter from.</param>
        /// <returns>Point containing the center of the corrasponding layer of theTileMap drawing area.</returns>
        public Point GetTileMapCenter(Vector2 _stretch, int layer)
        {
            return Util.GetRectangleCenter(GetTileMapBounding(_stretch, layer));
        }
    }
}