using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Xml;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Desribes a layer of tiles in a given TileMap.
    /// </summary>
    class TileMapLayer
    {
        /// <summary>
        /// List containing this layers tiles.
        /// </summary>
        public List<Tile> map;
        /// <summary>
        /// List containing the Eventboxes used by this TileMapLayer.
        /// </summary>
        public List<Eventbox> layerEventboxes;
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
        /// <param name="initialize">string to be parsed to describe the Tiles in the TileMapLayer.</param>
        public TileMapLayer(int layer, string initialize)
        {
            map = new List<Tile>();
            layerEventboxes = new List<Eventbox>();
            this.layer = layer;

            XmlDocument animatedList = new XmlDocument();
            XmlDocument hitboxList = new XmlDocument();
            animatedList.Load(@"Content\tile-sets\animated_tiles_config.xml");
            hitboxList.Load(@"Content\tile-sets\tile_hitboxes_config.xml");

            string[] columnTemp;
            string[] rowTemp;
            int row = 1;

            rowTemp = initialize.Split(";");
            Array.Reverse(rowTemp);
            for (int i = 0; i < rowTemp.Length; i++)
            {
                int column = 1;
                columnTemp = rowTemp[i].Split("|");
                if (rowTemp[i] != "")
                {
                    foreach (string j in columnTemp)
                    {
                        if (j != "")
                        {
                            bool animated = false;
                            int frames = 1;
                            int minDuration = 0;
                            int maxDuration = 0;
                            foreach (XmlElement foo in animatedList.GetElementsByTagName("tileSet"))
                            {
                                foreach (XmlElement bar in foo)
                                {
                                    if (j == foo.GetAttribute("name") + bar.GetAttribute("name"))
                                    {
                                        animated = true;
                                        frames = int.Parse(bar.ChildNodes[0].InnerText);
                                        minDuration = int.Parse(bar.ChildNodes[1].InnerText);
                                        maxDuration = int.Parse(bar.ChildNodes[2].InnerText);
                                        break;
                                    }
                                }
                            }

                            bool hitbox = false;
                            foreach (XmlElement foo in hitboxList.GetElementsByTagName("tileSet"))
                            {
                                foreach (XmlElement bar in foo)
                                {
                                    if (j == foo.GetAttribute("name") + bar.GetAttribute("name"))
                                    {
                                        hitbox = true;
                                    }
                                }
                            }

                            if (animated)
                            {
                                map.Add(new AnimatedTile(j, column, row, frames, minDuration, maxDuration, hitbox));
                            }
                            else
                            {
                                map.Add(new Tile(j, column, row, hitbox));
                            }


                            if (column > width)
                            {
                                width = column;
                            }
                            if (row > height)
                            {
                                height = row;
                            }
                        }
                        column++;
                    }

                    row++;
                }
            }
        }
        /// <summary>
        /// Loads all Eventboxes being used by ths TileMapLayer into layerEventboxes.
        /// </summary>
        public void LoadEventboxes(XmlElement layerTag)
        {
            foreach (XmlElement foo in layerTag.GetElementsByTagName("eventBox"))
            {
                Point tempPoint = Util.PointFromString(foo.GetAttribute("name"));
                Eventbox temp = new Eventbox(new Point(tempPoint.X * 64, tempPoint.Y * 64));

                foreach (XmlElement bar in foo)
                {

                    if (foo.ChildNodes[0].InnerText == "FULL")
                    {
                        temp.collisionArea = new Rectangle[] { new Rectangle(0, 0, 64, 64) };
                        break;
                    }
                    else
                    {
                        temp.collisionArea = new Rectangle[foo.ChildNodes.Count - 1];
                        for (int index = 1; index < bar.ChildNodes.Count; index++)
                        {
                            Rectangle hitArea = Util.RectangleFromString(bar.ChildNodes[index].InnerText);
                            temp.collisionArea[index - 1] = hitArea;
                        }
                        break;
                    }
                }

                temp.sceneEvent = new SceneEvent(foo);
                layerEventboxes.Add(temp);

            }
        }
        /// <summary>
        /// Returns the tile with the given coordinate from the TileMaplayer. If the coordiante is invalid returns null.
        /// </summary>
        /// <param name="coordinate">The coordinate of the tile to be returned. coordinate.X is the column and coordinate.Y is the row the tile occupies in the TileMapLayer.</param>
        /// <returns></returns>
        public Tile GetTile(Point coordinate)
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
        /// Checks if the provided Hitbox with the provided position collide with any tiles that have hitboxes in this TileMapLayer.
        /// </summary>
        /// <param name="box">The Hitbox to be checked.</param>
        /// <param name="tileHitboxes">The reference Hitboxes for the tiles in this TileMapLayer.</param>
        /// <returns>True if a collision exists between the TileMapLayer and provided Hitbox and position, False if not.</returns>
        public bool CheckLayerCollision(Entitybox box, List<Tilebox> tileHitboxes)
        {
            foreach (Tile j in map)
            {
                foreach (Tilebox k in tileHitboxes)
                {
                    if (j.tileSet.Name + j.tileSetCoordinate == k.reference)
                    {
                        if (k.Collision(new Point(j.tileMapCoordinate.X * 64, (j.tileMapCoordinate.Y + 1) * 64), box))
                        {
                            return true;
                        }
                    }
                }
            }

            foreach (Eventbox foo in layerEventboxes)
            {
                foo.Collision(box);
            }

            return false;
        }
        /// <summary>
        /// Draws all the Hitboxes in the TileMapLayer.
        /// </summary>
        /// <param name="tileHitboxes">The reference Hitboxes for the tiles in this TileMapLayer.</param>
        public void DrawLayerHitboxes(List<Tilebox> tileHitboxes)
        {
            foreach (Tile j in map)
            {
                foreach (Tilebox k in tileHitboxes)
                {
                    if (j.tileSet.Name + j.tileSetCoordinate == k.reference)
                    {
                        k.DrawHitbox(new Point(j.tileMapCoordinate.X * 64, (j.tileMapCoordinate.Y + 1) * 64));
                    }
                }
            }

            foreach (Eventbox foo in layerEventboxes)
            {
                foo.DrawHitbox();
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
                    ((AnimatedTile)i).DrawTile();
                }
                else
                {
                    i.DrawTile();
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
                        ((AnimatedTile)i).DrawTile();
                    }
                    else
                    {
                        i.DrawTile();
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
                        ((AnimatedTile)i).DrawTile();
                    }
                    else
                    {
                        i.DrawTile();
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
            drawArea.Y = -drawArea.Y;
            foreach (Tile j in map)
            {
                Rectangle tileArea = new Rectangle(
                    j.tileMapCoordinate.X * 64,
                    -(j.tileMapCoordinate.Y + 1) * 64,
                    64,
                    64);

                if (tileArea.Intersects(drawArea))
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
    }
}