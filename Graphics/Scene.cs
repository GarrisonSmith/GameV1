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
            this._camera.SetBoundingBox(this._tileMap);
            this._camera._scene = this;
        }
        public void drawScene(Texture2D debug)
        {
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(_camera.cameraPosition);
            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
        }
        public void clearAndRedraw()
        {
            _graphics.BeginDraw();
            _graphics.GraphicsDevice.Reset();
            _graphics.GraphicsDevice.Clear(Color.Gray);
            SpriteBatch _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(_camera.cameraPosition);
            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
            _graphics.EndDraw();
            this._spriteBatch = _spriteBatch;
        }
        public void drawMapBorder(Texture2D debug)
        {
            _spriteBatch.Begin();
            for (int i = _camera.boundingBox.X; i <= _camera.boundingBox.X + _camera.boundingBox.Width; i++)
            {
                _spriteBatch.Draw(debug, new Vector2(i, 0), Color.White);
            }
            _spriteBatch.End();
        }
    }
}
