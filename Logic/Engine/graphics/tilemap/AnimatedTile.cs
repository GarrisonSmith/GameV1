using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    class AnimatedTile : Tile
    {
        /// <summary>
        /// Manages the tiles animation.
        /// </summary>
        public Animation frames;

        /// <summary>
        /// Constructs a animated tile with the given properties.
        /// <param name="tileID">is parsed to get the tiles tileSetName and tiles x and y values.</param>
        /// <param name="column">the column this tile occupies on its TileMapLayer.</param>
        /// <param name="row">the row this tile occupies on its TileMapLayer.</param>
        /// <param name="frameAmount">the number of frames this animated tile has.</param>
        /// <param name="minFrameDuration">the minimum number of milliseconds a frame will be drawn for.</param>
        /// <param name="maxFrameDuration">the maximum number of milliseconds a frame will be drawn for.</param>
        /// </summary>
        public AnimatedTile(string tileID, int column, int row, int frameAmount, int minFrameDuration, int maxFrameDuration)
        {
            tileMapCoordinate = new Point(column, row);
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
            frames = new Animation(minFrameDuration, maxFrameDuration, 0, frameAmount - 1, tileSetCoordinate.Y / 64, tileSetCoordinate.X / 64, 64, 64, AnimationState.cycling);
        }
        /// <summary>
        /// Draws the current tile frame with the provided stretch.
        /// </summary>
        /// <param name="tileSet">the reference tileSet this tiles graphic references.</param>
        /// <param name="_stretch">the stretching of the texture,</param>
        new public void DrawTile(Texture2D tileSet, Vector2 _stretch)
        {
            frames.DrawAnimation(new Point(tileMapCoordinate.X * 64, (tileMapCoordinate.Y + 1) * 64), tileSet, color, _stretch);
        }
    }
}
