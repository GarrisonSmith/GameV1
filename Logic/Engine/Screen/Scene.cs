using System;
using System.Collections.Generic;
using Fantasy.Logic.Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Graphics.particles;
using Fantasy.Logic.Controllers;
using Fantasy.Logic.Engine.Screen.View;
using Fantasy.Logic.Engine.Physics;
using Fantasy.Logic.Engine.Graphics;
using Fantasy.Logic.Engine.Graphics.Drawing;
using Fantasy.Logic.Engine.Graphics.Lighting;

namespace Fantasy.Logic.Engine.Screen
{
    public class Scene
    {
        public SceneContentManager _spriteManager;

        public Camera _camera;

        public TileMap _tileMap;

        public EntitySet _entitySet;

        public Particle particle = new Particle(new Point(0, 0), Color.CornflowerBlue, 1, 10000);

        LightSource light;

        public List<Tuple<Point, Point>> fbao = new List<Tuple<Point, Point>>(); 

        public Scene()
        {
            _tileMap = new TileMap("large_light_test_map");
            _entitySet = new EntitySet();
            _entitySet.AddEntity(new Character("character", "character", Global._content.Load<Texture2D>(@"character-sets\character_three_spritesheet"), 2, new Point(64, 128),
                new Entitybox(new Point(-350, 516), new Rectangle[] { new Rectangle(16, -104, 32, 24) }), new MoveSpeed(96, TimeUnits.seconds), Orientation.down), true);

            _spriteManager = new SceneContentManager(_tileMap, _entitySet);
        }
        public void LoadScene()
        {
            _camera = new Camera(new Point(0, 0), true, false);
            CameraHandler.AssignFollowingTask(_entitySet.player, true);
            light = new LightSource(2, new Point(349, 2846), new int[] { 300 }, new float[] { .5f }, new Color[] { Color.White });
        }
        public void UpdateScene()
        {
            _entitySet.UpdateEntitySet();
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

            Debug.DebugScene(this);
            _spriteManager.DrawArea(_camera.cameraPosition);
            //Debug.DrawRectangle(_tileMap.GetTileMapBounding(), Color.Black * .9f, true);
            particle.Draw();

            light.Draw();

            foreach (Tuple<Point, Point> a in fbao)
            {
                Lines.DrawLinearAxisLineSegment(a);
            }

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

            MouseControlHandler.DrawMouse();
            Debug.DebugMouse(this, MouseControlHandler.mousePosition);

            Global._spriteBatch.End();
        }
        public void TransitionScene(string tileMapString)
        {
            _tileMap = new TileMap(tileMapString);
            _spriteManager._tileMap = _tileMap;
            _camera.SetBoundingBox(true);
        }
        public void DoEvent(SceneEvent sceneEvent)
        {
            System.Diagnostics.Debug.WriteLine("Event Recieved @: " + Global._gameTime.TotalGameTime.TotalMilliseconds);
            if (sceneEvent.transitionScene)
            {
                TransitionScene(sceneEvent.transitionTileMap);
                _entitySet.GetEntity("character").SetCharacterPosition(sceneEvent.transitionStartLocation);
            }
        }
        public void ProcessInputs(CurrentActionsList actives)
        {
            CameraHandler.DoActions(actives.Get(ControlContexts.camera));

            _entitySet.GetEntity("character").DoActions(actives.Get(ControlContexts.character));

        }
    }
}