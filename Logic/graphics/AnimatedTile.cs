using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.graphics
{
    class AnimatedTile : Tile
    {
        public Animation frames;
        public AnimatedTile(string tileID, int column, int row, int frameAmount)
        {
            tileMapCoordinate = new Point(column, row);
            if (tileID == "BLACK")
            {
                this.tileSetName = tileID;
                tileSetCoordinate = new Point(0, 0);
                color = Color.Black;
            }
            else
            {
                this.tileSetName = tileID.Substring(0, tileID.IndexOf('('));
                tileSetCoordinate = new Point
                    (
                    int.Parse(tileID.Substring(tileID.IndexOf('(') + 1, tileID.IndexOf(',') - (tileID.IndexOf('(') + 1))),
                    int.Parse(tileID.Substring(tileID.IndexOf(',') + 1, tileID.IndexOf(')') - (tileID.IndexOf(',') + 1)))
                    );
                color = Color.White;
            }
            frames = new Animation(400, 0, frameAmount - 1, tileSetCoordinate.Y / 64, tileSetCoordinate.X / 64, 64, 64, AnimationState.cycling);
        }

        new public void DrawTile(Texture2D tileSet, Vector2 _stretch)
        {
            frames.DrawAnimation(new Point(tileMapCoordinate.X * 64, (tileMapCoordinate.Y + 1) * 64), tileSet, color, _stretch);
        }
    }
}
