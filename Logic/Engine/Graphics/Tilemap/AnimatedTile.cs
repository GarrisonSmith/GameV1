using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;

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
        public AnimatedTile(string tileID, int column, int row, int frameAmount, int minFrameDuration, int maxFrameDuration, bool hasHitbox)
        {
            tileMapCoordinate = new Point(column, row);
            this.hasHitbox = hasHitbox;
            if (tileID == "BLACK")
            {
                ContentHandler.tileTextures.TryGetValue(tileID, out tileSet);
                tileSetCoordinate = new Point(0, 0);
                color = Color.Black;
            }
            else
            {
                ContentHandler.tileTextures.TryGetValue(tileID.Substring(0, tileID.IndexOf('{')), out tileSet);
                tileSetCoordinate = Util.PointFromString(tileID);
                color = Color.White;
            }
            frames = new Animation(minFrameDuration, maxFrameDuration, 0, frameAmount - 1, tileSetCoordinate.Y / 64, tileSetCoordinate.X / 64, 64, 64, AnimationState.cycling);
        }
        /// <summary>
        /// Draws the animated tile.
        /// </summary>
        new public void DrawTile()
        {
            frames.DrawAnimation(new Point(tileMapCoordinate.X * 64, (tileMapCoordinate.Y + 1) * 64), tileSet, color);
        }
    }
}
