using System;
using Fantasy.Logic.Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.graphics.particles;
using Fantasy.Logic.Controllers;
using Fantasy.Logic.Engine.Screen.View;
using Fantasy.Logic.Engine.Physics;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.Screen
{
    public class Scene
    {
        public Camera _camera;
        public TileMap _tileMap;
        public Character _character;
        public Particle particle = new Particle(new Point(0, 0), Color.CornflowerBlue, 1, 10000);
        public Scene(TileMap _tileMap)
        {
            this._tileMap = _tileMap;
        }
        public void LoadScene()
        {
            _camera = new Camera(new Point(0, 0), true, false);
            _character = new Character("character", "character", Global._content.Load<Texture2D>(@"character-sets\character_three_spritesheet"), 1, new Point(64, 128),
                new Entitybox(new Point(0, 0), new Rectangle[] { new Rectangle(16, -104, 32, 24)}), new MoveSpeed(96, TimeUnits.seconds), Orientation.down);
            CameraHandler.AssignFollowingTask(_character, false);
        }
        public void UpdateScene()
        {
            _character.UpdateEntity();
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
            _tileMap.DrawHitboxes();
            _character.DrawHitbox();
            _character.DrawCharacter();
            particle.Draw();

            Global._spriteBatch.End();

            

            Global._spriteBatch.Begin();

            //Debug.DebugOverlay(this);
            MouseControlHandler.DrawMouse();

            Global._spriteBatch.End();
        }
        public void TransitionScene(string tileMapString)
        {
            _tileMap = new TileMap(tileMapString);
            _camera.SetBoundingBox(true);
        }
        public void DoEvent(SceneEvent sceneEvent)
        {
            System.Diagnostics.Debug.WriteLine("Event Recieved @: " + Global._gameTime.TotalGameTime.TotalMilliseconds);
            if (sceneEvent.transitionScene)
            {
                TransitionScene(sceneEvent.transitionTileMap);
                _character.SetCharacterPosition(sceneEvent.transitionStartLocation);
            }
        }
        public void ProcessInputs(CurrentActionsList actives)
        {
            CameraHandler.DoActions(actives.Get(ControlContexts.camera));

            _character.DoActions(actives.Get(ControlContexts.character));

        }
    }
}