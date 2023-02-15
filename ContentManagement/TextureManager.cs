using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.ContentManagement
{
    public static class TextureManager
    {

        private static Dictionary<string, Texture2D> Spritesheets { get; set; }

        public static void LoadTextures(Game game)
        {
            LoadSpritesheets(game);
        }

        public static void LoadSpritesheets(Game game)
        {
			//tileSets.Add("NAME", Global._content.Load<Texture2D>(@"spritesheets\NAME"));
			Spritesheets = new Dictionary<string, Texture2D>
            {
                { "DEBUG", game.Content.Load<Texture2D>(@"spritesheets\DEBUG") },
                { "EMPTY", game.Content.Load<Texture2D>(@"spritesheets\EMPTY") },
                { "brickwall_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\brickwall_spritesheet") },
                { "grass_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\grass_spritesheet")},
                { "woodfloor_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\woodfloor_spritesheet")}
            };
        }

        public static Texture2D GetSpritesheet(string spritesheetName)
        {
            Texture2D foo;
            if (Spritesheets.TryGetValue(spritesheetName, out foo))
            {
                return foo;
            }
            throw new Exception("Spritesheet with name " + spritesheetName + " was not found.");
        }

    }
}
