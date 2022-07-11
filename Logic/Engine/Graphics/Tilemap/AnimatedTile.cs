using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Hitboxes;

namespace Fantasy.Logic.Engine.graphics.tilemap
{
    /// <summary>
    /// Describes a AnimatedTile in a given TileMapLayer.
    /// </summary>
    public class AnimatedTile : Tile
    {
        /// <summary>
        /// Manages the tiles animation.
        /// </summary>
        public Animation animation;

        /// <summary>
        /// Creates a Tile with the provided perameters.
        /// </summary>
        /// <param name="tileSet">The tileSet this Tile will use.</param>
        /// <param name="tileSetCoordinate">The coordinate of the tileSet this tiles graphic is located at.</param>
        /// <param name="tileMapCoordinate">Point containing the column (x value) and row (y value) this Tile occupies on its TileMapLayer.</param>
        /// <param name="positionBox">Describes where the tile is located (top right position) and the area this tile occupies.</param>
        /// <param name="hitboxes">Describes the hitboxes of this Tile.</param>
        /// <param name="animation">The Animation this AnimatedTile will use.</param>
        public AnimatedTile(Texture2D tileSet, Point tileSetCoordinate, Point tileMapCoordinate, Rectangle positionBox, Tilebox[] hitboxes, Animation animation)
        {
            this.tileSet = tileSet;
            this.tileSetCoordinate = tileSetCoordinate;
            this.tileMapCoordinate = tileMapCoordinate;
            this.positionBox = positionBox;
            this.hitboxes = hitboxes;
            this.animation = animation;
        }
        
        /// <summary>
        /// Draws the animated tile.
        /// </summary>
        new public void DrawTile()
        {
            animation.DrawAnimation(Util.GetTopLeftVector(positionBox, true), Color.White);
        }
    }
}
