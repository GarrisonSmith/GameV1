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
            _characters.Add(new Character(_tileTextures[5], new Point(30, 0), "character one", 3, Orientation.forward));

            this._camera = new CameraNEw(this, new Point(0, 0), true);
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures(_tileTextures, _graphics);
        }
        public void DrawScene()
        {
            //SpriteBatch _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _camera.GetTransformation(_graphics.GraphicsDevice));
            _tileMap.DrawLayers(_spriteBatch);
            _characters[0].DrawCharacter(_spriteBatch, 0);
            _spriteBatch.End();
            DrawDebug();
        }
        public void ClearAndRedraw()
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
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
        public void DrawDebug() {
            _spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _camera.GetTransformation(_graphics.GraphicsDevice));
            for (int i = 0; i <= _graphics.PreferredBackBufferWidth / 2; i++)
            {
                _spriteBatch.Draw(_tileTextures[0],
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 2, 1), Color.White, 0, new Vector2(0,0),
                        new Vector2(1, 1), new SpriteEffects(), 0);
            }
            for (int i = 0; i <= _graphics.PreferredBackBufferHeight / 2; i++)
            {
                _spriteBatch.Draw(_tileTextures[0],
                        new Vector2(0, -i-_camera.zoom.Y),
                        new Rectangle(0, 0, 1, 2), Color.White, 0, new Vector2(0,0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            _spriteBatch.End();
        }
    }
}
