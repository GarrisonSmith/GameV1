using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.screen;

namespace Fantasy.Content.Logic.graphics
{
    static class Debug
    {
        public static Texture2D debug = Global._content.Load<Texture2D>("tile-sets/DEBUG");

        //For drawing things that move in the scene
        public static void DebugOnScene(Scene _scene)
        {
            DrawAxis(_scene);
            DrawRectangle(_scene, _scene._tileMap.GetTileMapBounding(_scene._camera._stretch));
            DrawPoint(_scene, _scene._tileMap.GetTileMapCenter(_scene._camera._stretch), false);
            DrawPoint(_scene, new Point(640, 640), true);
        }

        public static void DrawAxis(Scene _scene)
        {
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding(_scene._camera._stretch).Width + (64 * _scene._camera._stretch.X); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding(_scene._camera._stretch).Height + +(64 * _scene._camera._stretch.Y); i++)
            {
                Global._spriteBatch.Draw(debug,
                        new Vector2(0, -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }

        public static void DrawRectangle(Scene _scene, Rectangle foo)
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

        public static void DrawPoint(Scene _scene, Point foo, bool stretchPosition)
        {
            if (stretchPosition)
            {
                Global._spriteBatch.Draw(debug,
                            new Vector2(foo.X * _scene._camera._stretch.X, -foo.Y * _scene._camera._stretch.Y),
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
