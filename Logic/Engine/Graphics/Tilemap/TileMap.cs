using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Hitboxes;
using System.Xml;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Contains a tile maps from a given string description. Tiles are 64 by 64 pixels in size.
    /// </summary>
    public class TileMap
    {
        /// <summary>
        /// List containing the TileMapLayers of this TileMap.
        /// </summary>
        public List<TileMapLayer> map;

        /// <summary>
        /// Constructs the TileMap from the provided tileMap files name.
        /// </summary>
        /// <param name="tileMapName">The file name containing the string that desribes the tileMap.</param>
        public TileMap(string tileMapName)
        {
            map = new List<TileMapLayer>();
            XmlDocument tilemap = new XmlDocument();
            tilemap.Load(@"Content\tile-maps\"+tileMapName+".xml");
            foreach (XmlElement foo in tilemap.GetElementsByTagName("layer"))
            {
                map.Add(new TileMapLayer(int.Parse(foo.GetAttribute("name")), foo));   
            }
        }

        /// <summary>
        /// Checks if the provided Hitbox with the provided position collide with any tiles in the provided layer that have hitboxes in this TileMap.
        /// </summary>
        /// <param name="layer">The layer of the tiles to be checked.</param>
        /// <param name="box">The Hitbox to be checked.</param>
        /// <returns>True if a collision exists between the TileMapLayer and provided Hitbox and position, False if not.</returns>
        public bool Collision(int layer, Entitybox box)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    if (i.CheckLayerCollision(box))
                    {
                        return true;
                    }
                }
            }
            return false;
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
        /// Finds a TileMapLayer with the corrasponding layer value from this TileMap.
        /// </summary>
        /// <param name="layer">Integer containing the layer to be added.</param>
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
        /// Returns a rectangle that is the size and location of the TileMap with the provided stretching.
        /// </summary>
        /// <returns>Rectangle whose collisionArea contains the TileMap drawing collisionArea.</returns>
        public Rectangle GetTileMapBounding()
        {
            int widthLargest = 1, widthSmallest = 0;
            int heightLargest = 1, heightSmallest = 0;

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
                (widthSmallest + 1) * 64,
                (heightLargest + 1) * 64,
                (widthLargest - widthSmallest) * 64,
                (heightLargest - heightSmallest) * 64);
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided layers in the TileMap with the provided stretching.
        /// </summary>
        /// <param name="layers">Array containing the layers to calculate the TileMapBounding from.</param>
        /// <returns>Rectangle whose collisionArea contains the corrasponding layers of the TileMap drawing collisionArea.</returns>
        public Rectangle GetTileMapBounding(int[] layers)
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
                (widthSmallest + 1) * 64,
                (heightLargest + 1) * 64,
                (widthLargest - widthSmallest) * 64,
                (heightLargest - heightSmallest) * 64);
        }
        /// <summary>
        /// Returns a rectangle that is the size and location of the provided layer in the TileMap with the provided Global._baseStretch.
        /// </summary>
        /// <param name="layer">Integer containing the layer to calculate the TileMapBounding from.</param>
        /// <returns>Rectangle whose collisionArea contains the corrasponding layer of the TileMap drawing collisionArea.</returns>
        public Rectangle GetTileMapBounding(int layer)
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
                (widthSmallest + 1) * 64,
                (heightLargest + 1) * 64,
                (widthLargest - widthSmallest) * 64,
                (heightLargest - heightSmallest) * 64);
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap with the provided stretching.
        /// </summary>
        /// <returns>Point containing the center of the TileMap drawing collisionArea.</returns>
        public Point GetTileMapCenter()
        {
            return Util.GetCenter(GetTileMapBounding());

        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided layers in the TileMap with the provided stretching.
        /// </summary>
        /// <param name="layers">Array containing the layers to calculate the TileMapCenter from.</param>
        /// <returns>Point containing the center of the corrasponding layers of theTileMap drawing collisionArea.</returns>
        public Point GetTileMapCenter(int[] layers)
        {
            return Util.GetCenter(GetTileMapBounding(layers));
        }
        /// <summary>
        /// Returns a point that is the center of the TileMap of the provided layer in the TileMap with the provided Global._baseStretch.
        /// </summary>
        /// <param name="layer">Array containing the layer to calculate the TileMapCenter from.</param>
        /// <returns>Point containing the center of the corrasponding layer of theTileMap drawing collisionArea.</returns>
        public Point GetTileMapCenter(int layer)
        {
            return Util.GetCenter(GetTileMapBounding(layer));
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
        /// Draws a highlight around all the Hitboxes in all of the layers of the TileMap.
        /// </summary>
        public void DrawHitboxes()
        {
            foreach (TileMapLayer i in map)
            {
                    i.DrawLayerHitboxes();
            }
        }
        /// <summary>
        /// Draws a highlight around all the Hitboxes in the provide layer of the TileMap.
        /// </summary>
        /// <param name="layer">The layer of the tiles to be drawn.</param>
        public void DrawHitboxes(int layer)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawLayerHitboxes();
                }
            }
        }
        /// <summary>
        /// Draws a highlight around all the Hitboxes in the provide layers of the TileMap.
        /// </summary>
        /// <param name="layers">The layer of the tiles to be drawn.</param>
        public void DrawHitboxes(int[] layers)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawLayerHitboxes();
                    }
                }
            }
        }
        /// <summary>
        /// Draws the provided Texture2D onto the TileMap with the provided location and stretching.
        /// </summary>
        /// <param name="texture">the texture to be drawn.</param>
        /// <param name="drawArea">describes the collisionArea of the provided texture to be drawn.</param>
        /// <param name="color">the shading color of the texture.</param>
        /// <param name="column">the column of this Tilemap that the texture to be drawn.</param>
        /// <param name="row">the row of this Tilemap that the texture to be drawn.</param>
        /// <param name="horizontalOffSet">the number of pixel the texture will be horizontally offset by.</param>
        /// <param name="verticalalOffSet">the number of pixel the texture will be vertically offset by.</param>
        public void DrawToMap(Texture2D texture, Rectangle drawArea, Color color, int column, int row, int horizontalOffSet, int verticalalOffSet)
        {
            Global._spriteBatch.Draw(texture,
                new Vector2((column * 64) + horizontalOffSet, (-(row + 1) * 64) - verticalalOffSet),
                drawArea, color, 0, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), 0);
        }
        /// <summary>
        /// Draws all TileMapLayers in the TileMap.
        /// </summary>
        public void DrawLayers()
        {
            foreach (TileMapLayer i in map)
            {
                foreach (Tile j in i.map)
                {
                    if (j is AnimatedTile)
                    {
                        ((AnimatedTile)j).DrawTile();
                    }
                    else
                    {
                        j.DrawTile();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        public void DrawLayer(int layer)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayer();
                }
            }
        }
        /// <summary>
        /// Draws all TileMapLayers in the TileMap which occupy the provided layers.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        public void DrawLayers(int[] layers)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayer();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row.
        /// </summary>
        /// <param name="row">The row of the TileMapLayer to be drawn.</param>
        public void DrawRow(int row)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerRow(row);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layers.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="row">The row of the TileMapLayer to be drawn.</param>
        public void DrawRow(int[] layers, int row)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayerRow(row);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided row and layer.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="row">The row of the TileMapLayer to be drawn.</param>
        public void DrawRow(int layer, int row)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerRow(row);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows.
        /// </summary>
        /// <param name="rows">Array containing the rows to be drawn.</param>
        public void DrawRows(int[] rows)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerRows(rows);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layers.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="rows">The rows of the TileMapLayer to be drawn.</param>
        public void DrawRows(int[] layers, int[] rows)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayerRows(rows);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rows and layer.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="rows">The rows of the TileMapLayer to be drawn.</param>
        public void DrawRows(int layer, int[] rows)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerRows(rows);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column.
        /// </summary>
        /// <param name="column">The column of the TileMapLayer to be drawn.</param>
        public void DrawColumn(int column)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerColumn(column);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layers.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="column">The column of the TileMapLayer to be drawn.</param>
        public void DrawColumn(int[] layers, int column)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayerColumn(column);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided column and layer.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="column">The column of the TileMapLayer to be drawn.</param>
        public void DrawColumn(int layer, int column)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerColumn(column);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns.
        /// </summary>
        /// <param name="columns">The columns of the TileMapLayer to be drawn.</param>
        public void DrawColumns(int[] columns)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerColumns(columns);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layers.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="columns">The columns of the TileMapLayer to be drawn.</param>
        public void DrawColumns(int[] layers, int[] columns)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayerColumns(columns);
                    }
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided columns and layer.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="columns">The columns of the TileMapLayer to be drawn.</param>
        public void DrawColumns(int layer, int[] columns)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerColumns(columns);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided rectangle drawArea.
        /// </summary>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(Rectangle drawArea)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerArea(drawArea);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layers and rectangle drawArea.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(int[] layers, Rectangle drawArea)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    i.DrawTileMapLayerArea(drawArea);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer and rectangle drawArea.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="drawArea">Rectangle describing the area to be drawn.</param>
        public void DrawArea(int layer, Rectangle drawArea)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerArea(drawArea);
                }
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer and rectangles drawAreas.
        /// </summary>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(Rectangle[] drawAreas)
        {
            foreach (TileMapLayer i in map)
            {
                i.DrawTileMapLayerAreas(drawAreas);
            }
        }
        /// <summary>
        /// Draws all tiles inside of the TileMap which occupy the provided layer and rectangles drawAreas.
        /// </summary>
        /// <param name="layers">The layers to be drawn.</param>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(int[] layers, Rectangle[] drawAreas)
        {
            foreach (int l in layers)
            {
                foreach (TileMapLayer i in map)
                {
                    if (i.layer == l)
                    {
                        i.DrawTileMapLayerAreas(drawAreas);
                    }
                }
            }
        }
        /// <summary>
        ///  Draws all tiles inside of the TileMap which occupy the provided layer and rectangles drawAreas.
        /// </summary>
        /// <param name="layer">The layer to be drawn.</param>
        /// <param name="drawAreas">Rectangle describing the area to be drawn.</param>
        public void DrawAreas(int layer, Rectangle[] drawAreas)
        {
            foreach (TileMapLayer i in map)
            {
                if (i.layer == layer)
                {
                    i.DrawTileMapLayerAreas(drawAreas);
                }
            }
        }
    }
}