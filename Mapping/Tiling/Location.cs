using Fantasy.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.Mapping.Tiling
{
    /// <summary>
    /// Represents a location on a grid with a column and row.
    /// </summary>
    internal readonly struct Location
    {
        private readonly int col;
        private readonly int row;

        /// <summary>
        /// The column of the location.
        /// </summary>
        public int Col
        {
            get => col;
        }
        /// <summary>
        /// The row of the location.
        /// </summary>
        public int Row
        {
            get => row;
        }

        /// <summary>
        /// Creates a new instance of the Location struct with the specified column and row.
        /// Column and row values less than 0 will be set to 0.
        /// </summary>
        /// <param name="col">The column of the location.</param>
        /// <param name="row">The row of the location.</param>
        public Location(int col, int row)
        {
            if (col < 0)
            {
                this.col = 0;
            }
            else
            {
                this.col = col;
            }

            if (row < 0)
            {
                this.row = 0;
            }
            else
            {
                this.row = row;
            }
        }
        /// <summary>
        /// Creates a new instance of the Location struct based on the specified Coordinates.
        /// Column and row values less than 0 will be set to 0.
        /// </summary>
        /// <param name="cord">The Coordinates to base the location on.</param>
        public Location(Coordinates cord)
        {
            if (cord.TopLeft.X < 0)
            {
                col = 0;
            }
            else
            {
                col = (int)(cord.TopLeft.X / Tile.TILE_WIDTH);
            }

            if (cord.TopLeft.Y < 0)
            {
                row = 0;
            }
            else
            {
                row = (int)(cord.TopLeft.Y / Tile.TILE_HEIGHT);
            }
        }
    }
}
