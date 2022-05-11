using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.Graphics;

namespace Fantasy.Content.Logic.Screen
{
    class Scene
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public TileMap _tileMap;
        public Texture2D[] _tileTextures;
        public Camera _camera;
        public Scene(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, TileMap _tileMap, Texture2D[] _tileTextures)
        {
            this._graphics = _graphics;
            this._spriteBatch = _spriteBatch;
            this._tileMap = _tileMap;
            this._tileTextures = _tileTextures;
            this._camera = new Camera(this, new Point(-500, -500));
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures(_tileTextures, _graphics);
        }
        public void DrawScene()
        {
            _spriteBatch.GraphicsDevice.Viewport = _camera.GetViewport();
            _graphics.GraphicsDevice.Viewport = _camera.GetViewport();
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
        }
        public void ClearAndRedraw()
        {
            _graphics.BeginDraw();
            SpriteBatch _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _spriteBatch.GraphicsDevice.Viewport = _camera.GetViewport();
            _graphics.GraphicsDevice.Viewport = _camera.GetViewport();
            _graphics.GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
            _graphics.EndDraw();
            this._spriteBatch = _spriteBatch;
        }
        public void TransitionScene(String tileMapString, Texture2D[] tileSets)
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures(tileSets, _graphics);
            _camera.SetBoundingBox(true);
            ClearAndRedraw();
        }
        public void bufferTest()
        {
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
        }
    }
}
