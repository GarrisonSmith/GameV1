using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.graphics
{
    /// <summary>
    /// Desribes a layer of tiles in a given <c>TileMap</c>.
    /// </summary>
    class TileMapLayer
    {
        /// <summary>
        /// List containing this layers tiles.
        /// </summary>
        public List<Tile> map;
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
            this.map = new List<Tile>();
            this.layer = layer;
            string[] columnTemp;
            string[] rowTemp;
            int row = 2;

            rowTemp = initialize.Split(";");
            Array.Reverse(rowTemp);
            for (int i = 0; i < rowTemp.Length; i++)
            {
                int column = 1;
                columnTemp = rowTemp[i].Split(":");
                if (rowTemp[i] != "")
                {
                    foreach (string j in columnTemp)
                    {
                        if (j != "")
                        {
                            map.Add(new Tile(j, column, row));
                            if (column+1 > this.width)
                            {
                                this.width = column+1;
                            }
                            if (row > this.height)
                            {
                                this.height = row;
                            }
                        }
                        column++;
                    }
                    
                    row++;
                }
            }
        }
        /// <summary>
        /// Returns the tile with the given index from the TileMaplayer. If the index is invalid returns null.
        /// </summary>
        public Tile GetTile(int index)
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
        /// Returns the layer dimensions in as a point containing width and height.
        /// </summary>
        public Point GetLayerDimension()
        {
            return new Point(this.width, this.height);
        }
    }
}