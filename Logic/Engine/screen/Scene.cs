using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.graphics.particles;

namespace Fantasy.Logic.Engine.screen
{
    class Scene
    {
        public Camera _camera;
        public TileMap _tileMap;
        public Character _character;
        public Particle particle = new Particle(new Point(0,0), Color.CornflowerBlue, 1, 10000);
        public Scene(TileMap _tileMap)
        {
            this._tileMap = _tileMap;
            this._character = new Character("character_two", "character", "character_two_spritesheet", 1, new Entitybox("character", new Rectangle(0, 0, 64, 128)), 3, Orientation.up);

            _camera = new Camera(this, new Point(640, 640), true, false);
        }
        public void LoadScene()
        {
            _tileMap.LoadTileTextures();
            _tileMap.LoadTileHitboxes();
        }
        public void DrawScene()
        {
            Global._spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                _camera.GetTransformation());

            _tileMap.DrawArea(1, _camera.cameraPosition);
            Debug.DebugOnScene(this);
            _tileMap.DrawHitboxes(1);
            _character.DrawHitbox();
            _character.DrawCharacter();
            particle.Draw();
            //System.Diagnostics.Debug.WriteLine(_tileMap.Collision(1, _character.positionBox.Location, _character.hitBox));

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
        public void TransitionScene(string tileMapString)
        {
            _tileMap.UnloadTileTextures();
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures();
            _tileMap.LoadTileHitboxes();
            _camera.SetBoundingBox(true);
            ClearAndRedraw();
        }
        public void DoEffects(SceneEvent sceneEvent)
        {
            
        }
    }
}