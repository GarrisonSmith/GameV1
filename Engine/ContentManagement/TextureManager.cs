using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Engine.ContentManagement
{
    //tileSets.Add("NAME", Global._content.Load<Texture2D>(@"spritesheets\NAME"));
    internal static class TextureManager
    {

        private static Dictionary<string, Texture2D> Spritesheets { get; set; }

        internal static void LoadTextures(Game1 game)
        {
            LoadSpritesheets(game);
        }

        internal static void LoadSpritesheets(Game1 game)
        {
            Spritesheets = new Dictionary<string, Texture2D>
            {
                { "DEBUG", game.Content.Load<Texture2D>(@"spritesheets\DEBUG") },
                { "EMPTY", game.Content.Load<Texture2D>(@"spritesheets\EMPTY") },
                { "brickwall_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\brickwall_spritesheet") },
                { "grass_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\grass_spritesheet")},
                { "woodfloor_spritesheet", game.Content.Load<Texture2D>(@"spritesheets\woodfloor_spritesheet")}
            };
        }

        internal static Texture2D GetSpritesheet(string spritesheetName)
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
