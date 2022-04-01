using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fantasy.Content.Logic.Graphics
{
    class Scene
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public TileMap _tileMap;
        public Camera _camera;

        public Scene(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, TileMap _tileMap, Camera _camera)
        {
            this._graphics = _graphics;
            this._spriteBatch = _spriteBatch;
            this._tileMap = _tileMap;
            this._camera = _camera;
            this._camera._scene = this;
        }
        public void drawScene() {
            _spriteBatch.Begin();
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(new Rectangle(0,0, 1000, 1000));
            _graphics.GraphicsDevice.Viewport = new Viewport(_camera.cameraPosition);
            _tileMap.DrawArea(_camera.zoom, _spriteBatch,1 ,_camera.cameraPosition);
            _spriteBatch.End();
        }
        public void clearAndRedraw() 
        {
            _graphics.BeginDraw();
            _graphics.GraphicsDevice.Reset();
            _graphics.GraphicsDevice.Clear(Color.Black);
            _graphics.GraphicsDevice.Viewport = new Viewport(_camera.cameraPosition);
            SpriteBatch _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, 1, _camera.cameraPosition);
            _spriteBatch.End();
            _graphics.EndDraw();
            this._spriteBatch = _spriteBatch;
        }
    }
}
