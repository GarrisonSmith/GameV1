using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    class TileMap
    {
        private List<Tile> map = new List<Tile>(); //contains the list of tile for the given tile map
        private string initialize; //area definition string

        public TileMap( String initialize)
        {
            this.initialize = initialize.Replace(" ", "");
            string[] columnTemp;
            string[] rowTemp;
            int row = 0;
            int column = 0;
            rowTemp = initialize.Split(";");
            foreach (string i in rowTemp)
            {
                row = 0;
                columnTemp = i.Split(":");
                foreach (string j in columnTemp)
                {
                    if (j == "BLANK") 
                    {
                        //does nothing
                    }
                    else if (j != "")
                    {
                        createTile(j, row,column);
                    }
                    row++;
                }
                column++;
            }
        }
        private void createTile(String tileID, int row, int column)
        {
            //System.Diagnostics.Debug.WriteLine(map.Count);
            map.Add(new Tile(tileID, row, column));
            //System.Diagnostics.Debug.WriteLine(tileID);
        }
        public void loadTiles(Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Tile i in map)
            {
                i.loadTile(tileSets, device);
            }
        }
        public void drawTile(int index, SpriteBatch spriteBatch)
        {
            map[index].drawTile(spriteBatch);
        }
        public void drawTiles(SpriteBatch _spriteBatch) 
        { 
            foreach (Tile i in map)
            {
                i.drawTile(_spriteBatch);
            }
        }
        public string getInitialize() 
        {
            return initialize;
        }
        public int getMapSize()
        {
            return map.Count;
        }
        public Tile getTile(int index)
        {
            return map[index];
        }
    }
}
