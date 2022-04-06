using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Graphics
{
    class Scene
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public TileMap _tileMap;
        public Camera _camera;
        public Scene(ref GraphicsDeviceManager _graphics, ref SpriteBatch _spriteBatch, TileMap _tileMap)
        {
            this._graphics = _graphics;
            this._spriteBatch = _spriteBatch;
            this._tileMap = _tileMap;
            this._camera = new Camera(this, new Point(-500,-500), new Vector2(1-.02f, 1-.02f));
        }
        public void LoadScene()
        { 
            
        }
        public void DrawScene()
        {
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _spriteBatch.Begin();
            _tileMap.DrawArea(ref _camera.zoom, ref _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
        }
        public void ClearAndRedraw()
        {
            _graphics.BeginDraw();
            SpriteBatch _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Height, _camera.boundingBox.Height));
            _graphics.GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin();
            _tileMap.DrawArea(ref _camera.zoom, ref _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
            _graphics.EndDraw();
            this._spriteBatch = _spriteBatch;
        }
        public void TransitionScene(String tileMapString, Texture2D[] tileSets) 
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures(ref tileSets, ref _graphics);
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
