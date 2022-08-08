using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Screen;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.graphics
{
    static class Debug
    {
        public static Texture2D debug = Global._content.Load<Texture2D>("tile-sets/DEBUG");

        //For drawing things that move in the scene
        public static void DebugOnScene(Scene _scene)
        {
            DrawAxis(_scene, Color.White);
            DrawRectangle(_scene._tileMap.GetTileMapBounding(), Color.White);
            DrawPoint(_scene._tileMap.GetTileMapCenter(), false, Color.White);
            DrawPoint(_scene._camera.cameraPosition.Location, false, Color.White);
            DrawPoint(_scene._camera.cameraCenter, false, Color.White);
        }

        public static void DrawAxis(Scene _scene, Color color)
        {
            //draws x axis
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding().Width + (64); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws y axis
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding().Height + +(64); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(0, -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1), new SpriteEffects(), 0);
            }
        }

        public static void DrawRectangle(Rectangle foo, Color color)
        {
            Global._spriteBatch.Draw(debug,
                       Util.GetTopLeftVector(foo),
                       new Rectangle(0, 0, 1, 1), Color.Red, 0, new Vector2(0, 0),
                       new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);

            //draws bottom line
            for (int i = foo.X; i <= foo.X + foo.Width - 1; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -(foo.Y - foo.Height  + (1 / Global._currentStretch))),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws left line
            for (int i = foo.Y - foo.Height + 1; i <= foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(foo.X, -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws top line
            for (int i = foo.X; i <= foo.X + foo.Width - 1; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -foo.Y),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            //draws right line
            for (int i = foo.Y - foo.Height + 1; i <= foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2((foo.X + foo.Width - (1 / Global._currentStretch)), -i),
                        new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                        new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
        }

        public static void DrawPoint(Point foo, bool stretchPosition, Color color)
        {
            if (stretchPosition)
            {
                Global._spriteBatch.Draw(debug,
                    new Vector2(foo.X, -foo.Y),
                    new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                    new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
            else
            {
                Global._spriteBatch.Draw(debug,
                    new Vector2(foo.X , -foo.Y),
                    new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0, 0),
                    new Vector2(1 / Global._currentStretch, 1 / Global._currentStretch), new SpriteEffects(), 0);
            }
        }

        //For drawing things that stay static in the view.
        public static void DebugOverlay(Scene _scene)
        {
            DrawCameraCenterAxis(_scene, Color.White);
        }

        public static void DrawCameraCenterAxis(Scene _scene, Color color)
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
    }
}
