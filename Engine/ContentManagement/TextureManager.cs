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

        internal static void LoadTextures(Game1 foo)
        {
            LoadSpritesheets(foo);
        }

        internal static void LoadSpritesheets(Game1 foo)
        {
            Spritesheets = new Dictionary<string, Texture2D>
            {
                { "DEBUG", foo.Content.Load<Texture2D>(@"spritesheets\DEBUG") },
                { "EMPTY", foo.Content.Load<Texture2D>(@"spritesheets\EMPTY") },
                { "brickwall_spritesheet", foo.Content.Load<Texture2D>(@"spritesheets\brickwall_spritesheet") },
                { "grass_spritesheet", foo.Content.Load<Texture2D>(@"spritesheets\grass_spritesheet")},
                { "woodfloor_spritesheet", foo.Content.Load<Texture2D>(@"spritesheets\woodfloor_spritesheet")}
            };
        }

        internal static Texture2D GetSpritesheet(string name)
        {
            Texture2D foo;
            if (Spritesheets.TryGetValue(name, out foo))
            {
                return foo;
            }
            throw new Exception("Spritesheet with name " + name + " was not found.");
        }

    }
}
