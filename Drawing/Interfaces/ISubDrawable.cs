using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Engine.Drawing.Interfaces
{
    public interface ISubDrawable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the component should be drawn.
        /// </summary>
        /// <value><c>true</c> if the component should be drawn; otherwise, <c>false</c>.</value>
        bool IsVisible { get; set; }
        /// <summary>
        /// The top left point of this tile's starting texture on the spritesheet.
        /// </summary>
        Point TextureSourceTopLeft { get; }
        /// <summary>
        /// The area of the spritesheet from which the tile's texture is taken.
        /// </summary>
        Rectangle SheetBox { get; }
        /// <summary>
        /// The spritesheet that the tile's texture is taken from.
        /// </summary>
        Texture2D Spritesheet { get; }
        /// <summary>
        /// Draws the component using the specified game time.
        /// </summary>
        /// <param name="gameTime">The elapsed game time since the last update.</param>
        /// <param name="layer">The layer to be drawn to.</param>
        void Draw(GameTime gameTime, int layer);
    }
}
