using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.hitboxes;
using Fantasy.Logic.Engine.graphics.particles;
using Fantasy.Logic.Controls;
using Fantasy.Logic.Engine.screen.camera;
using Fantasy.Logic.Engine.physics;

namespace Fantasy.Logic.Engine.screen
{
    class Scene
    {
        public Camera _camera;
        public TileMap _tileMap;
        public Character _character;
        public ControlContexts _controlContexts = ControlContexts.character;
        public Particle particle = new Particle(new Point(0,0), Color.CornflowerBlue, 1, 10000);
        public Scene(TileMap _tileMap)
        {
            this._tileMap = _tileMap;
            this._character = new Character("character_two", "character", "character_two_spritesheet", 1, new Entitybox("character", new Rectangle(0, 0, 64, 128)), 3, Orientation.up);
        }
        public void LoadScene()
        {
            _camera = new Camera(new Point(640, 640), true, false);
            _tileMap.LoadTileTextures();
            _tileMap.LoadTileHitboxes();
        }
        public void DrawScene()
        {
            Global._spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                null,
                null,
                null,
                _camera.GetTransformation());

            _tileMap.DrawLayers();
            Debug.DebugOnScene(this);
            _tileMap.DrawHitboxes(1);
            _character.DrawHitbox();
            _character.DrawCharacter();
            particle.Draw();

            Global._spriteBatch.End();



            Global._spriteBatch.Begin();

            Debug.DebugOverlay(this);

            Global._spriteBatch.End();
        }
        public void ClearAndRedraw()
        {
            Global._graphics.GraphicsDevice.Clear(Color.Gray);
            DrawScene();
            Global._game1.RunOneFrame();
            //Global._graphics.BeginDraw();
            //DrawScene();
            //Global._graphics.EndDraw();
        }
        public void TransitionScene(string tileMapString)
        {
            _tileMap = new TileMap(tileMapString);
            _tileMap.LoadTileTextures();
            _tileMap.LoadTileHitboxes();
            _camera.SetBoundingBox(true);
            ClearAndRedraw();
        }
        public void DoEvent(SceneEvent sceneEvent)
        {
            System.Diagnostics.Debug.WriteLine("Event Recieved"+Global._gameTime.TotalGameTime.TotalMilliseconds);
            if (sceneEvent.transitionScene)
            {
                TransitionScene(sceneEvent.transitionTileMapName);
                _character.SetCharacterPosition(sceneEvent.transitionStartLocation);
            }
        }
        public void ProcessInputs(List<ActionControl> actives)
        {
            switch (_controlContexts)
            {
                case ControlContexts.camera:
                    CameraHandler.DoActions(actives);
                    //System.Diagnostics.Debug.WriteLine(actionControl.action.ToString());
                    break;
                case ControlContexts.character:

                    break;
                case ControlContexts.menu:

                    break;
            }
        }
    }

    enum ControlContexts
    {
        camera,
        character,
        menu
    }

}