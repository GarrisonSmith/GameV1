using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.graphics;
using Fantasy.Content.Logic.entities;

namespace Fantasy.Content.Logic.screen
{
    class Scene
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public CameraNEw _camera;
        public TileMap _tileMap;
        public Texture2D[] _tileTextures;
        public List<Character> _characters;
        public Scene(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, TileMap _tileMap, Texture2D[] _tileTextures)
        {
            this._graphics = _graphics;
            this._spriteBatch = _spriteBatch;
            this._tileMap = _tileMap;
            this._tileTextures = _tileTextures;
            this._characters = new List<Character>();
            _characters.Add(new Character(_tileTextures[5], new Point(200, -700), "character one", 3, Orientation.forward));

            this._camera = new CameraNEw(this, new Point(0, 1500), true);
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures(_tileTextures, _graphics);
        }
        public void DrawScene()
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, //1.0 on top, 0.0 on bottom 
                null,
                null,
                null, 
                null, 
                null,
                _camera.GetTransformation(_graphics.GraphicsDevice));
            _characters[0].DrawCharacter(_spriteBatch, 1);
            _tileMap.DrawLayers(_camera.zoom, _spriteBatch);
            _spriteBatch.End();

            Debug.DebugAll(this);
        }
        public void ClearAndRedraw()
        {
            _graphics.GraphicsDevice.Clear(Color.Gray);
            _graphics.BeginDraw();
            DrawScene();
            _graphics.EndDraw();
        }
        public void TransitionScene(String tileMapString, Texture2D[] tileSets)
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures(tileSets, _graphics);
            _camera.SetBoundingBox(true);
            ClearAndRedraw();
        }
        public void BufferTest()
        {
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this._camera = new CameraNEw(this, this._camera.cameraCenter, true);
        }
    }
}
