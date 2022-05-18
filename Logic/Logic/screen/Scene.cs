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
        public Camera _camera;
        public TileMap _tileMap;
        public Character _character;
        public Scene(TileMap _tileMap)
        {
            this._tileMap = _tileMap;
            this._character = new Character("character_two", "character_two_spritesheet", 1, new Rectangle(0,0,64,128), 3, Orientation.up);

            _camera = new Camera(this, new Point(640, 640), true, false);
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures();
        }
        public void DrawScene()
        {
            Global._spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _camera.GetTransformation());

            _tileMap.DrawArea(_camera._stretch, 1, _camera.cameraPosition);
            Debug.DebugOnScene(this);
            _character.DrawCharacter(_camera._stretch, Global._spriteBatch);

            Global._spriteBatch.End();

            Global._spriteBatch.Begin();

            Debug.DebugOverlay(this);

            Global._spriteBatch.End();
        }
        public void ClearAndRedraw()
        {
            Global._graphics.GraphicsDevice.Clear(Color.Gray);
            Global._graphics.BeginDraw();
            DrawScene();
            Global._graphics.EndDraw();
        }
        public void TransitionScene(String tileMapString)
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures();
            _camera.SetBoundingBox(true);
            ClearAndRedraw();
        }
        public void BufferTest()
        {
            Global._graphics.PreferredBackBufferWidth = 500;
            Global._graphics.PreferredBackBufferHeight = 500;
            Global._graphics.ApplyChanges();
            this._camera = new Camera(this, this._camera.cameraCenter, true, true);
        }
    }
}
