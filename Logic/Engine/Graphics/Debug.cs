using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Screen;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.graphics.tilemap;
using System;

namespace Fantasy.Logic.Engine.graphics
{
    static class Debug
    {
        public static Texture2D debug = Global._content.Load<Texture2D>("tile-sets/DEBUG");

        public static SpriteFont basic_font = Global._content.Load<SpriteFont>("Fonts/ConsolaMono");

        //For drawing things that move in the scene
        public static void DebugScene(Scene _scene)
        {
            Rectangle foo = _scene._tileMap.GetTileMapBounding();
            DrawAxis(foo, Color.White);
            DrawRectangle(foo, Color.White);
            DrawPoint(_scene._tileMap.GetTileMapCenter(), Color.White);
        }

        public static void DrawAxis(Rectangle foo, Color color)
        {
            //draws x axises
            for (int i = 0; i <= foo.Width + (64); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws y axises
            for (int i = 0; i <= foo.Height + +(64); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(0, -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1), new SpriteEffects(), 0);
            }
        }

        public static void DrawRectangle(Rectangle foo, Color color)
        {
            //draws bottom line
            for (int i = foo.X; i <= foo.X + foo.Width - 1; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -(foo.Y - foo.Height + (1 / Global._currentStretch))),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws left line
            for (int i = foo.Y - foo.Height + 1; i <= foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(foo.X, -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1), new SpriteEffects(), 0);
            }
            //draws top line
            for (int i = foo.X; i <= foo.X + foo.Width - 1; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -foo.Y),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws right line
            for (int i = foo.Y - foo.Height + 1; i <= foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2((foo.X + foo.Width - (1 / Global._currentStretch)), -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1), new SpriteEffects(), 0);
            }

            Global._spriteBatch.Draw(debug,
                       Util.GetTopLeftVector(foo, true),
                       new Rectangle(0, 0, 1, 1), Color.Red, 0, new Vector2(0, 0),
                       new Vector2(1, 1), new SpriteEffects(), 0);
        }

        public static void DrawPoint(Point foo, Color color)
        {
            Global._spriteBatch.Draw(debug,
                new Vector2(foo.X, -foo.Y),
                new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                new Vector2(1, 1), new SpriteEffects(), 0);
        }

        //For drawing things that stay static in the view.
        public static void DebugOverlay(Scene _scene)
        {
            DrawCameraCenterAxis(Color.White);
        }

        public static void DrawCameraCenterAxis(Color color)
        {
            //draws horizontal line
            for (int i = 64; i <= Global._graphics.PreferredBackBufferWidth - 64; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, (Global._graphics.PreferredBackBufferHeight / 2)),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 0);
            }

            //draws vertical line
            for (int i = 64; i <= Global._graphics.PreferredBackBufferHeight - 64; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2((Global._graphics.PreferredBackBufferWidth / 2), i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 0);
            }
        }

        //For drawing this around the mouse.
        public static void DebugMouse(Scene _scene, Point mousePosition)
        {
            Point mouseRelativePosition = new Point(_scene._camera.cameraPosition.X + (int)(mousePosition.X * 1 / _scene._camera.stretch),
                _scene._camera.cameraPosition.Y - (int)(mousePosition.Y * 1 / _scene._camera.stretch));

            DrawMousePosition(_scene, mousePosition);
            InterrogateTileUnderMouse(_scene, mousePosition, mouseRelativePosition);
        }

        public static void DrawMousePosition(Scene _scene, Point mousePosition)
        {
            Global._spriteBatch.Draw(debug, new Vector2(mousePosition.X+15, mousePosition.Y - 16), new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(9 * mousePosition.ToString().Length, 16), new SpriteEffects(), 0);

            Global._spriteBatch.DrawString(basic_font, mousePosition.ToString(), new Vector2(mousePosition.X+15, mousePosition.Y - 20), Color.Black);
        }

        public static void InterrogateTileUnderMouse(Scene _scene, Point mousePosition, Point mouseRelativePosition) 
        {
            Tile foo = _scene._tileMap.GetTile(1, mouseRelativePosition);

            if (foo != null)
            {
                Global._spriteBatch.DrawString(basic_font, foo.ToString(), new Vector2(mousePosition.X, mousePosition.Y+20), Color.Black);
                foo.DrawHitboxes();
            }
        }
    }
}
