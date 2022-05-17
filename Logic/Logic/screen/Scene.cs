﻿using System;
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
        public Camera _camera;
        public TileMap _tileMap;
        public Texture2D[] _tileTextures;
        public Character _character;
        public Scene(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, TileMap _tileMap, Texture2D[] _tileTextures)
        {
            this._graphics = _graphics;
            this._spriteBatch = _spriteBatch;
            this._tileMap = _tileMap;
            this._tileTextures = _tileTextures;
            this._character = new Character("character one", _tileTextures[5], 1, new Rectangle(0,0,64,128), 5, Orientation.up);

            _camera = new Camera(this, new Point(640, 640), true, false);
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures(_tileTextures, _graphics);
        }
        public void DrawScene()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _camera.GetTransformation());

            _tileMap.DrawArea(_camera._stretch, _spriteBatch, _camera.cameraPosition);
            Debug.DebugOnScene(this);
            _character.DrawCharacter(_camera._stretch, _spriteBatch);

            _spriteBatch.End();

            _spriteBatch.Begin();

            Debug.DebugOverlay(this);

            _spriteBatch.End();
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
            this._camera = new Camera(this, this._camera.cameraCenter, true, true);
        }
    }
}
