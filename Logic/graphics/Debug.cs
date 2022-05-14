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
        public static void DebugAll(Scene _scene) {
            _scene._spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _scene._camera.GetTransformation(_scene._graphics.GraphicsDevice));

            DrawAxis(_scene);
            DrawRectangle(_scene, _scene._tileMap.GetTileMapBounding(_scene._camera.zoom));

            _scene._spriteBatch.End();
        }
       
        public static void DrawAxis(Scene _scene)
        {
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding(_scene._camera.zoom).Width + (64 * _scene._camera.zoom.X); i++)
            {
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
                        new Vector2(i, 0),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            for (int i = 0; i <= _scene._tileMap.GetTileMapBounding(_scene._camera.zoom).Height + +(64 * _scene._camera.zoom.Y); i++)
            {
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
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
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
                        new Vector2(i, -foo.Y),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws left line
            for (int i = foo.Y; i < foo.Y + foo.Height; i++)
            {
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
                        new Vector2(foo.X, -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws top line
            for (int i = foo.X; i < foo.X + foo.Width; i++)
            {
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
                        new Vector2(i, -foo.Height - (64 * _scene._camera.zoom.X)),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
            //draws right line
            for (int i = foo.Y; i < foo.Y + foo.Height; i++)
            {
                _scene._spriteBatch.Draw(_scene._tileTextures[0],
                        new Vector2(foo.Width + (64 * _scene._camera.zoom.Y), -i),
                        new Rectangle(0, 0, 1, 1), Color.White, 0, new Vector2(0, 0),
                        new Vector2(1, 1), new SpriteEffects(), 1);
            }
        }
    }
}
