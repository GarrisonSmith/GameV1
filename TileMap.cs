using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    class TileMap
    {
        private List<Tile> map = new List<Tile>(); //contains the list of tile for the given tile map
        private string initialize; //area definition string
        int topLayer=1;
        int bottomLayer=1;

        public TileMap( String initialize)
        {
            this.initialize = initialize.Replace(" ", "");
            string[] columnTemp;
            string[] rowTemp;
            int row = 0;
            int column = 0;
            int layer = 0;

            columnTemp = initialize.Split(";");
            for (int i = 0; i < columnTemp.Length; i++)
            {
                if (columnTemp[i] != "" && columnTemp[i].Substring(0,1) == "<")
                {
                    column = 0;
                    row = 0;
                    layer = int.Parse(columnTemp[i].Substring(columnTemp[i].IndexOf('<')+1, columnTemp[i].IndexOf('>')-1));
                    if (layer > topLayer)
                    {
                        topLayer = layer;
                    }
                    else if (layer < bottomLayer)
                    {
                        bottomLayer = layer;
                    }
                    columnTemp[i] = columnTemp[i].Substring(columnTemp[i].IndexOf('>')+1);
                }
                column = 0;
                rowTemp = columnTemp[i].Split(":");
                foreach (string j in rowTemp)
                {
                    if (j == "BLANK") 
                    {
                        //does nothing
                    }
                    else if (j != "")
                    {
                        createTile(j, column, row, layer);
                    }
                    column++;
                }
                row++;
            }
        }
        private void createTile(String tileID, int row, int column, int layer)
        {
            map.Add(new Tile(tileID, row, column, layer));
        }
        public void loadTiles(Texture2D[] tileSets, GraphicsDevice device)
        {
            foreach (Tile i in map)
            {
                i.loadTile(tileSets, device);
            }
        }
        public void drawTile(int index, SpriteBatch _spriteBatch)
        {
            map[index].drawTile(_spriteBatch);
        }
        public void drawTiles(SpriteBatch _spriteBatch) 
        {
            for (int i = bottomLayer; i <= topLayer; i++)
            {
                foreach (Tile j in map)
                {
                    if (j.layer == i)
                    {
                        j.drawTile(_spriteBatch);
                    }
                }
            }
        }
        public void drawLayer(int layer, SpriteBatch _spriteBatch)
        {
            foreach (Tile j in map)
            {
                if (j.layer == layer)
                {
                    System.Diagnostics.Debug.WriteLine(j.tileSetName);
                    System.Diagnostics.Debug.WriteLine(j.row);
                    System.Diagnostics.Debug.WriteLine(j.column);
                    j.drawTile(_spriteBatch);
                }
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
