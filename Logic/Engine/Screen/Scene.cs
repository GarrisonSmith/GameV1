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
        public SpriteManager _spriteManager;

        public Camera _camera;

        public TileMap _tileMap;

        public EntitySet _entitySet;

        public Particle particle = new Particle(new Point(0, 0), Color.CornflowerBlue, 1, 10000);

        public Circle foo;

        public Scene()
        {
            _tileMap = new TileMap("water_grass_map");
            _entitySet = new EntitySet();
            _entitySet.AddEntity(new Character("character", "character", Global._content.Load<Texture2D>(@"character-sets\character_three_spritesheet"), 1, new Point(64, 128),
                new Entitybox(new Point(0, 0), new Rectangle[] { new Rectangle(16, -104, 32, 24) }), new MoveSpeed(96, TimeUnits.seconds), Orientation.down), true);

            _spriteManager = new SpriteManager(_tileMap, _entitySet);
            foo = new Circle(100, new Point(0, -101));
        }
        public void LoadScene()
        {
            _camera = new Camera(new Point(0, 0), true, false);
            CameraHandler.AssignFollowingTask(_entitySet.player, true);
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

            //_tileMap.DrawArea(_camera.cameraPosition);
            Debug.DebugScene(this);
            _spriteManager.DrawArea(_camera.cameraPosition);
            Debug.DrawRectangle(_tileMap.GetTileMapBounding(), Color.Black * .1f, true);
            Debug.DrawRectangle(_tileMap.GetTileMapBounding(), new Color(255, 255, 255) * 0.5f, true);
            particle.Draw();

            Global._spriteBatch.Draw(foo.texture, foo.texture.Bounds, Color.Black) ;

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