using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Fantasy.Content.Logic.graphics;
using Fantasy.Content.Logic.screen;
using Fantasy.Content.Logic.entities;

namespace Fantasy
{
    public class Game1 : Game
    {
        //System.Diagnostics.Debug.WriteLine();
        public static GameTime _gameTime;
        public static ContentManager _contant;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        Texture2D[] _tileTextures;
        Scene test;

        string large_test_map_text;
        string test_map_text;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            large_test_map_text = System.IO.File.ReadAllText(@"Content\tile-maps\large_test_map.txt");
            test_map_text = System.IO.File.ReadAllText(@"Content\tile-maps\test_map.txt");
            _tileTextures = new Texture2D[6];
            base.Initialize(); //calls loadContent
        }

        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
            //_tileTextures[0] = Content.Load<Texture2D>("DEBUG");
            //_tileTextures[1] = Content.Load<Texture2D>("EMPTY");
            //_tileTextures[2] = Content.Load<Texture2D>("BLACK");
            //_tileTextures[3] = Content.Load<Texture2D>("brickwall_tile_set");
            //_tileTextures[4] = Content.Load<Texture2D>("woodfloor_tile_set");
            //_tileTextures[5] = Content.Load<Texture2D>("character_two_spritesheet");
            Content.Load<Texture2D>("DEBUG");
            Content.Load<Texture2D>("EMPTY");
            Content.Load<Texture2D>("BLACK");
            Content.Load<Texture2D>("brickwall_tile_set");
            Content.Load<Texture2D>("woodfloor_tile_set");
            Content.Load<Texture2D>("character_two_spritesheet");
            _contant = Content;
            test = new Scene(_graphics, _spriteBatch, new TileMap(test_map_text), _tileTextures);
            test.LoadScene();
        }

        protected override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                test._character.MoveCharacter(Orientation.up);
                //test._camera.ForceMoveVertical(true, 10, true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                test._character.MoveCharacter(Orientation.down);
                //test._camera.ForceMoveVertical(false, 10, true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                test._character.MoveCharacter(Orientation.left);
                //test._camera.ForceMoveHorizontal(false, 10, true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                test._character.MoveCharacter(Orientation.right);
                //test._camera.ForceMoveHorizontal(true, 10, true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                test._camera.ForcePanWithStretch(new Point(640, 640), 10, true);
                //test.bufferTest();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                test._camera.Stretch(new Vector2(2f, 2f), true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                test._camera.Stretch(new Vector2(1f, 1f), true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                test._camera.Stretch(new Vector2(.7f, .7f), true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                test.TransitionScene(test_map_text, _tileTextures);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                test.TransitionScene(large_test_map_text, _tileTextures);
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D))
            {
                test._character.StopCharacter();
            }
            test._camera.ForceSetVertical(test._character.positionBox.Y, true, false);
            test._camera.ForceSetHorizontal(test._character.positionBox.X, true, false);
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameTime = gameTime;

            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            test.DrawScene();

            base.Draw(gameTime);
        }
    }
}
