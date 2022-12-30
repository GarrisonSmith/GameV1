using Fantasy.Engine;
using Fantasy.Engine.Logic.Drawing;
using Fantasy.Engine.Logic.Mapping;
using Fantasy.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

//System.Diagnostics.Debug.WriteLine(); <--GREATEST DEBUG
namespace Fantasy
{
    public class Game1 : Game
    {
        internal static GraphicsDeviceManager _graphics;
        internal static SpriteBatch _spriteBatch;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameMap map = new GameMap(this);
            this.Components.Add(map);

            base.Initialize(); //calls LoadContent()
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here



            base.Draw(gameTime);
        }
    }
}