using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    class Tile
    {
        //tile is 16x16 pixels large
        string tileID; //Id for which tile this represents
        string tileSetName;
        int x;
        int y;
        Texture2D tile;
        Color color;

        public Tile(string tileID)
        {
            //System.Diagnostics.Debug.WriteLine(tileID);
            this.tileID = tileID;
            char[] tempChar = tileID.ToCharArray();
            int index = 0;
            string tileSetName = "";
            string[] coordinates = { "0","0" };
            foreach (char i in tempChar)
            {
                if (i == '(')
                {
                    tileSetName = tileID.Substring(0, index);
                    coordinates = tileID.Substring(index).Replace("(","").Replace(")","").Split(",");
                    break;
                }
                index++;
            }
            this.tileSetName = tileSetName;
            x = int.Parse(coordinates[0]);
            y = int.Parse(coordinates[1]);
            color = Color.White;
        }
        public string getTileId()
        {
            return tileID;
        }
        public string getTileSetName()
        {
            return tileSetName;
        }
        public Vector2 getCoordinate()
        {
            return new Vector2(x, y);
        }
        public void loadTile(Texture2D[] tileSets, GraphicsDevice device) 
        {
            foreach (Texture2D i in tileSets)
            {
                if (tileSetName == i.Name)
                {
                    System.Diagnostics.Debug.WriteLine(tileID);
                    tile = new Texture2D(device, 64, 64);
                    Color[] newColor = new Color[64 * 64];
                    System.Diagnostics.Debug.WriteLine(x);
                    System.Diagnostics.Debug.WriteLine(y);
                    Rectangle selectionArea = new Rectangle(y, x, 64, 64);

                    System.Diagnostics.Debug.WriteLine(selectionArea.Width);
                    System.Diagnostics.Debug.WriteLine(selectionArea.Height);
                    i.GetData(0, selectionArea, newColor, 0, newColor.Length);


                    tile.SetData(newColor);
                }
            }
        }
        public void loadTile(Texture2D tile)
        {
            this.tile = tile;
        }
        public void drawTile(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(tile, new Vector2(x, y), Color.White);
        }
        public Texture2D getContent()
        {
            return tile;
        }
        public Color getColor()
        {
            return color;
        }
    }
}
