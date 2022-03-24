using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.Drawing
{
    class Tilemap
    {
        private List<Tile> map = new List<Tile>(); //contains the list of tile for the given tile map
        private string initialize; //area definition string

        public Tilemap(String initialize)
        {
            this.initialize = initialize.Replace(" ", "");
            this.initialize = this.initialize.Replace(":;", ";");
            string[] columnTemp;
            string[] rowTemp;
            rowTemp = initialize.Split(";");
            foreach (string i in rowTemp)
            {
                columnTemp = i.Split(":");
                foreach (string j in columnTemp)
                {
                    if (j != "BLACK" && j != "")
                    {
                        createTile(j);
                    }
                }
            }
        }

        private void createTile(String tileID)
        {
            map.Add(new Tile(tileID));
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
