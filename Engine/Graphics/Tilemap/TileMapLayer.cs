using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Xml;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.XmlDigest;

namespace Fantasy.Logic.Engine.Graphics.tilemap
{
    /// <summary>
    /// Desribes a layer of tiles in a given TileMap.
    /// </summary>
    public class TileMapLayer
    {
        /// <summary>
        /// List containing this layers tiles.
        /// </summary>
        public List<Tile> map;
        /// <summary>
        /// List containing the Eventboxes used by this TileMapLayer.
        /// </summary>
        public List<Eventbox> eventboxes;
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
        /// Constructs a TileMapLayer from the provided string.
        /// </summary>
        /// <param name="layer">The layer this TileMapLayer occupies in its TileMap</param>
        /// <param name="layerTag">The layers Xml breakdown.</param>
        public TileMapLayer(int layer, XmlElement layerTag)
        {
            map = new List<Tile>();
            eventboxes = new List<Eventbox>();
            this.layer = layer;
            foreach (XmlElement tileTag in layerTag.GetElementsByTagName("Tile"))
            {
                map.Add(TileMapXmlDigest.GetTile(tileTag));
            }
            foreach (XmlElement animatedTag in layerTag.GetElementsByTagName("AnimatedTile"))
            {
                map.Add(TileMapXmlDigest.GetAnimatedTile(animatedTag));
            }
            foreach (XmlElement eventTag in layerTag.GetElementsByTagName("eventBox"))
            {
                eventboxes.Add(TileMapXmlDigest.GetEventbox(eventTag));
            }

            CalculateLayerWidth();
            CalculateLayerHeight();
        }
        
        /// <summary>
        /// Calculates and sets this TileMapLayers width value.
        /// </summary>
        private void CalculateLayerWidth()
        {
            width = 0;
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.X > width)
                {
                    width = i.tileMapCoordinate.X;
                }
            }
        }
        /// <summary>
        /// Calculates and sets this TileMapLayers height value.
        /// </summary>
        private void CalculateLayerHeight()
        {
            height = 0;
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.Y > height)
                {
                    height = i.tileMapCoordinate.Y;
                }
            }
        }
        /// <summary>
        /// Returns the Tile that contains the coordinate from the TileMaplayer. If no tile contains the coordinate then returns null.
        /// </summary>
        /// <param name="position">The coordiante the returned Tile will contain.</param>
        /// <returns>A Tile containing the provided coordinate. Null if no such Tile exists.</returns>
        public Tile GetTile(Point position)
        {
            foreach (Tile i in map)
            {
                if (Util.PointInsideRectangle(position, i.positionBox))
                {
                    return i;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns the Eventbox that contains the coordiante form the TileMapLayer. If no eventbox contains the coordinate then returns null.
        /// </summary>
        /// <param name="position">The coordiante the returned Eventbox will contain.</param>
        /// <returns>A Eventbox containing the provided coordinate. Null if no such Eventbox exists.</returns>
        public Eventbox GetEventbox(Point position)
        {
            foreach (Eventbox box in eventboxes)
            {
                if (box.geometry.PointInsideRectangleSet(position))
                {
                    return box;
                }
            }
            return null;
        }
        /// <summary>
        /// Checks if the provided Hitbox with the provided position collide with any tiles that have hitboxes in this TileMapLayer.
        /// </summary>
        /// <param name="entityBox">The Hitbox to be checked.</param>
        /// <returns>True if a collision exists between the TileMapLayer and provided Hitbox and position, False if not.</returns>
        public bool CheckLayerCollision(Entitybox entityBox)
        {
            foreach (Tile j in map)
            {
                if (j.TileCollision(entityBox))
                {
                    return true;
                }
            }

            foreach (Eventbox foo in eventboxes)
            {
                if (foo.Collision(entityBox))
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Draws all the Hitboxes in the TileMapLayer.
        /// </summary>
        public void DrawLayerHitboxes()
        {
            foreach (Tile j in map)
            {
                j.DrawHitboxes();
            }

            foreach (Eventbox foo in eventboxes)
            {
                foo.Draw(Color.Purple);
            }
        }
        /// <summary>
        /// Draws all Tiles in this TileMapLayer.
        /// </summary>
        public void DrawTileMapLayer()
        {
            foreach (Tile i in map)
            {
                if (i is AnimatedTile)
                {
                    ((AnimatedTile)i).Draw();
                }
                else
                {
                    i.Draw();
                }
            }
        }
        /// <summary>
        /// Draws all Tiles in the provided row of this TileMapLayer.
        /// </summary>
        /// <param name="row">The row of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerRow(int row)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.X == row)
                {
                    if (i is AnimatedTile)
                    {
                        ((AnimatedTile)i).Draw();
                    }
                    else
                    {
                        i.Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all Tiles in the provided rows of this TileMapLayer.
        /// </summary>
        /// <param name="row">The rows of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerRows(int[] row)
        {
            foreach (int r in row)
            {
                DrawTileMapLayerRow(r);
            }
        }
        /// <summary>
        /// Draws all Tiles in the provided row of this TileMapLayer.
        /// </summary>
        /// <param name="column">The column of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerColumn(int column)
        {
            foreach (Tile i in map)
            {
                if (i.tileMapCoordinate.Y == column)
                {
                    if (i is AnimatedTile)
                    {
                        ((AnimatedTile)i).Draw();
                    }
                    else
                    {
                        i.Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all Tiles in the provided rows of this TileMapLayer.
        /// </summary>
        /// <param name="column">The columns of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerColumns(int[] column)
        {
            foreach (int c in column)
            {
                DrawTileMapLayerColumn(c);
            }
        }
        /// <summary>
        /// Draws all Tile in the provided drawArea of this TileMapLayer
        /// </summary>
        /// <param name="drawArea">The area of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerArea(Rectangle drawArea)
        {
            foreach (Tile j in map)
            {
                if (Util.RectanglesIntersect(drawArea, j.positionBox))
                {
                    if (j is AnimatedTile)
                    {
                        ((AnimatedTile)j).Draw();
                    }
                    else
                    {
                        j.Draw();
                    }
                }
            }
        }
        /// <summary>
        /// Draws all Tile in the provided drawAreas of this TileMapLayer
        /// </summary>
        /// <param name="drawArea">The areas of the TileMapLayer to be drawn.</param>
        public void DrawTileMapLayerAreas(Rectangle[] drawArea)
        {
            foreach (Rectangle a in drawArea)
            {
                DrawTileMapLayerArea(a);
            }
        }

        /// <summary>
        /// Creates a string describing this TileMapLayer.
        /// </summary>
        /// <returns>A string describing this TileMapLayer.</returns>
        new public string ToString()
        {
            return "Layer: " + layer + Environment.NewLine 
                + "Tiles/Eventboxes: " + map.Count + '/' + eventboxes.Count + Environment.NewLine
                + "Dimension: (" + width + ',' + height + ')';
        }
    }
}