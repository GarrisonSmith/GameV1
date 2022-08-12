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
using Fantasy.Logic.Engine.Graphics.Drawing;

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
            //drawing with movement matrix and effects applied.
            Global._spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                null,
                null,
                null,
                _camera.GetTransformation());

            _tileMap.DrawArea(_camera.cameraPosition);
            Debug.DebugScene(this);
            _character.DrawHitbox(Color.White);
            _character.DrawCharacter();
            particle.Draw();

            Global._spriteBatch.End();

            //drawing with movement matrix and effects applied. Used for lighting system.
            Global._spriteBatch.Begin(SpriteSortMode.Immediate, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                null,
                null,
                null,
                _camera.GetTransformation());

            //Debug.DrawRectangle(new Rectangle(128, 192, 64, 64), Color.Yellow, true);
            Debug.DrawRectangle(new Rectangle(128, 192, 128, 128), new Color (255, 255, 255) * 0.5f, true);
            Debug.DrawRectangle(new Rectangle(64, 192, 128, 64), new Color(0 , 0, 0), true);
            Debug.DrawRectangle(new Rectangle(64, 128, 128, 64), new Color(100, 100, 1) * 0.5f, true);

            Global._spriteBatch.End();

            //drawing with just movement effects. Used for some debug functions.
            Global._spriteBatch.Begin(SpriteSortMode.Deferred, //first things drawn on bottom, last things on top
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                null,
                null,
                null,
                _camera.GetTransformation());

            Debug.DrawTileHitboxesUnderMouse(this, MouseControlHandler.mousePosition);
            Debug.DrawEventboxUnderMouse(this, MouseControlHandler.mousePosition);
            
            Global._spriteBatch.End();

            //static drawing.
            Global._spriteBatch.Begin();

            //Debug.DebugOverlay(this);
            MouseControlHandler.DrawMouse();

            Debug.DebugMouse(this, MouseControlHandler.mousePosition);

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