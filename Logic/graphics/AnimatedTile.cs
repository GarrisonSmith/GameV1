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
            frames = new Animation(400, 0, frameAmount-1, -row*64, 64, 64, AnimationState.cycling);
        }

        public void DrawTile(Texture2D tileSet, Vector2 _stretch)
        {
            frames.DrawAnimation(tileMapCoordinate, tileSet, color, _stretch);
        }
    }
}
