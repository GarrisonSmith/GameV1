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
        public void InitializeScene()
        { 
            
        }
        public void LoadScene()
        { 
            
        }
        public void DrawScene()
        {
            _spriteBatch.GraphicsDevice.Viewport = new Viewport(_camera.cameraPosition);
            _graphics.GraphicsDevice.Viewport = new Viewport(new Rectangle(_camera.cameraPosition.X, _camera.cameraPosition.Y, _camera.boundingBox.Width, _camera.boundingBox.Height));
            _spriteBatch.Begin();
            _tileMap.DrawArea(_camera.zoom, _spriteBatch, _camera.cameraPosition);
            _spriteBatch.End();
        }
        public void ClearAndRedraw()
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
        public void TransitionScene(String tileMapString, Texture2D[] tileSets, GraphicsDevice _graphicsdevice) 
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures(tileSets, _graphicsdevice);
            _camera.SetBoundingBox();
            ClearAndRedraw();
        }
    }
}
