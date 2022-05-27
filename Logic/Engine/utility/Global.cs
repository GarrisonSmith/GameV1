using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine
{
    class Global
    {
        public static GameTime _gameTime;
        public static ContentManager _content;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static Scene _currentScene;
        public static Vector2 _baseStretch;
    }
}
