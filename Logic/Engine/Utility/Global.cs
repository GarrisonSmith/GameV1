using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Fantasy.Logic.Engine.Screen;

namespace Fantasy.Logic.Engine.Utility
{
    /// <summary>
    /// Static class containg members used and referenced throughout the project.
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Tracks and manages the gametime of the current run time.
        /// </summary>
        public static GameTime _gameTime;
        
        /// <summary>
        /// Manages the content of the game.
        /// </summary>
        public static ContentManager _content;
        
        /// <summary>
        /// Manages the graphics of the game.
        /// </summary>
        public static GraphicsDeviceManager _graphics;
        
        /// <summary>
        /// Draws the frames of the game.
        /// </summary>
        public static SpriteBatch _spriteBatch;

        /// <summary>
        /// The stretch being applied to the final spritebatch.begin draw.
        /// </summary>
        public static float _currentStretch;

        /// <summary>
        /// The current scene of the game. Manages the loaded TileMap, Camera, and Entites.
        /// </summary>
        public static Scene _currentScene;

        /// <summary>
        /// The Game1 class defined by Monogame.
        /// </summary>
        public static Game1 _game1;
    }
}