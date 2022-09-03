using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Graphics.Lighting;

namespace Fantasy.Logic.Engine.Graphics.tilemap
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
        /// Creates a AnimatedTile with the provided perameters.
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
            lightboxes = new Lightbox[] { new Lightbox(Util.GetTopLeftPoint(positionBox), new Rectangle[] { new Rectangle(0, 0, 64, 64) }, new bool[,] { { true, true, true, true }, { true, true, true, true } }) };
        }

        /// <summary>
        /// Draws the AnimatedTile.
        /// </summary>
        new public void Draw()
        {
            animation.Draw(Util.GetTopLeftVector(positionBox, true), Color.White);
        }

        /// <summary>
        /// Creates a string describing this AnimatedTile.
        /// </summary>
        /// <returns>A string describing this AnimatedTile.</returns>
        new public string ToString()
        {
            StringBuilder text = new StringBuilder("TileName: " + tileSet.Name + tileSetCoordinate + Environment.NewLine
                + "Row&Column: " + tileMapCoordinate + " Location:" + Util.GetTopLeftPoint(positionBox));

            if (hitboxes != null)
            {
                int i = 0;
                foreach (Tilebox box in hitboxes)
                {
                    text.Append(Environment.NewLine + "__Hitbox " + i + "__" + Environment.NewLine + box.ToString());
                    i++;
                }
            }

            text.Append(Environment.NewLine + animation.ToString());

            return text.ToString();
        }
    }
}
