using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.graphics
{
    static class Debug
    {
        public static Texture2D debug = Global._content.Load<Texture2D>("tile-sets/DEBUG");

        //For drawing things that move in the scene
        public static void DebugOnScene(Scene _scene)
        {
            DrawAxis(_scene);
            DrawRectangle(_scene._tileMap.GetTileMapBounding());
            DrawPoint(_scene._tileMap.GetTileMapCenter(), false);
            DrawPoint(new Point(640, 640), true);
        }

        public static void DrawAxis(Scene _scene)
        {
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding().Width + (64 * Global._baseStretch.X); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding().Height + +(64 * Global._baseStretch.Y); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(0, -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }

        public static void DrawRectangle(Rectangle foo)
        {
            //draws bottom line
            for (int i = foo.X; i < foo.X + foo.Width; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -(foo.Y - foo.Height)),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws left line
            for (int i = foo.Y - foo.Height; i < foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(foo.X, -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws top line
            for (int i = foo.X; i < foo.X + foo.Width; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, -foo.Y),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws right line
            for (int i = foo.Y - foo.Height; i < foo.Y; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2((foo.X + foo.Width), -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }

        public static void DrawPoint(Point foo, bool stretchPosition)
        {
            if (stretchPosition)
            {
                Global._spriteBatch.Draw(debug,
                            new Vector2(foo.X * Global._baseStretch.X, -foo.Y * Global._baseStretch.Y),
                            new Rectangle(0, 0, 2, 2), Color.White, 0, new Vector2(0, 0),
                           new Vector2(1, 1), new SpriteEffects(), 1);
            }
            else
            {
                Global._spriteBatch.Draw(debug,
                            new Vector2(foo.X, -foo.Y),
                            new Rectangle(0, 0, 2, 2), Color.White, 0, new Vector2(0, 0),
                            new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }

        //For drawing things that stay static in the view.
        public static void DebugOverlay(Scene _scene)
        {
            DrawCameraCenterAxis(_scene);
        }

        public static void DrawCameraCenterAxis(Scene _scene)
        {
            //draws horizontal line
            for (int i = 64; i <= _scene._camera.cameraPosition.Width - 64; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, (_scene._camera.cameraPosition.Height / 2)),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }

            //draws vertical line
            for (int i = 64; i <= _scene._camera.cameraPosition.Height - 64; i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2((_scene._camera.cameraPosition.Width / 2), i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }
    }
}
