using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Logic.Engine.Utility
{
    static class ContentHandler
    {

        public static Dictionary<string, Texture2D> tileTextures = new Dictionary<string, Texture2D>();

        public static Dictionary<string, Texture2D> characterSpritesheets = new Dictionary<string, Texture2D>();

        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void LoadTextures()
        {
            //tile textures
            //tileTextures.Add( , Global._content.Load<Texture2D>());
            tileTextures.Add("BLACK", Global._content.Load<Texture2D>(@"tile-sets\BLACK"));
            tileTextures.Add("DEBUG", Global._content.Load<Texture2D>(@"tile-sets\DEBUG"));
            tileTextures.Add("EMPTY", Global._content.Load<Texture2D>(@"tile-sets\EMPTY"));
            tileTextures.Add("brickwall_tile_set", Global._content.Load<Texture2D>(@"tile-sets\brickwall_tile_set"));
            tileTextures.Add("woodfloor_tile_set", Global._content.Load<Texture2D>(@"tile-sets\woodfloor_tile_set"));
            tileTextures.Add("grass_tile_set", Global._content.Load<Texture2D>(@"tile-sets\grass_tile_set"));

            //character spritesheets
            //characterSpritesheet.Add( , Global._content.Load<Texture2D>());
            characterSpritesheets.Add("character_one_spritesheet" , Global._content.Load<Texture2D>(@"character-sets\character_one_spritesheet"));
            characterSpritesheets.Add("character_two_spritesheet", Global._content.Load<Texture2D>(@"character-sets\character_two_spritesheet"));
        }
    }
}
