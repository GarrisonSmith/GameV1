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
        /// Constructs a TileMapLayer from the provided string.
        /// </summary>
        /// <param name="layer">The layer this TileMapLayer occupies in its TileMap</param>
        /// <param name="initialize">string to be parsed to describe the Tiles in the TileMapLayer.</param>
        public TileMapLayer(int layer, String initialize)
        {
            this.map = new List<Tile>();
            this.layer = layer;
            string[] columnTemp;
            string[] rowTemp;
            int row = 1;

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
                            if (column > this.width)
                            {
                                this.width = column;
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
        /// <param name="index">The index of the tile to be returned.</param>
        /// <returns>The tile with the corresponding index. If not tile with that index exists then null.</returns>
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
        /// Returns the layer dimensions in as a point containing width and height.
        /// </summary>
        /// <returns>Point where the X value is the TileMapLayer width and the Y is the TileMapLayer height.</returns>
        public Point GetLayerDimensions()
        {
            return new Point(this.width, this.height);
        }
    }
}