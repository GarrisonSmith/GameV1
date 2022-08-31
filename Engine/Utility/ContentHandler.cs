using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Logic.Engine.Utility
{
    /// <summary>
    /// Static class used to handling the content used thoughout the game.
    /// </summary>
    static class ContentHandler
    {
        /// <summary>
        /// Dictionary containing all tileSets that can be used. The key for each tileSet is the tileSets name.
        /// </summary>
        public static Dictionary<string, Texture2D> tileSets = new Dictionary<string, Texture2D>();
        /// <summary>
        /// Dictionary containing all characterSpritesheets that can be used. The key for each characterSpritesheet is the characterSpritesheets name.
        /// </summary>
        public static Dictionary<string, Texture2D> characterSpritesheets = new Dictionary<string, Texture2D>();
        /// <summary>
        /// Dictionary containing all spritefonts that can be used. The key for each spritefont is the spritefonts name.
        /// </summary>
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        /// <summary>
        /// Loads all textures and spritefonts that can be used.
        /// </summary>
        public static void LoadContent()
        {
            //tile textures
            //tileTextures.Add( , Global._content.Load<Texture2D>());
            tileSets.Add("BLACK", Global._content.Load<Texture2D>(@"tile-sets\BLACK"));
            tileSets.Add("DEBUG", Global._content.Load<Texture2D>(@"tile-sets\DEBUG"));
            tileSets.Add("EMPTY", Global._content.Load<Texture2D>(@"tile-sets\EMPTY"));
            tileSets.Add("brickwall_tile_set", Global._content.Load<Texture2D>(@"tile-sets\brickwall_tile_set"));
            tileSets.Add("woodfloor_tile_set", Global._content.Load<Texture2D>(@"tile-sets\woodfloor_tile_set"));
            tileSets.Add("grass_tile_set", Global._content.Load<Texture2D>(@"tile-sets\grass_tile_set"));

            //character spritesheets
            //characterSpritesheet.Add( , Global._content.Load<Texture2D>());
            characterSpritesheets.Add("character_one_spritesheet" , Global._content.Load<Texture2D>(@"character-sets\character_one_spritesheet"));
            characterSpritesheets.Add("character_two_spritesheet", Global._content.Load<Texture2D>(@"character-sets\character_two_spritesheet"));
        }

        /// <summary>
        /// Attempts to get the Texture2D that corresponds to the provided key in either the dictionary tileSets or characterSpritesheets.
        /// Will attempt to get a Texture2D from tileSets before characterSpritesheets.
        /// </summary>
        /// <param name="key">The key to be used.</param>
        /// <param name="graphic">The graphic to be loaded with the corresponding key.</param>
        /// <returns>True if the key corresponds with the Texture2D in either tileSets or characterSpritesheets, False if not.</returns>
        public static bool TryGetTexture(string key, out Texture2D graphic)
        {
            if (tileSets.TryGetValue(key, out graphic))
            {
                return true;
            }
            else if (characterSpritesheets.TryGetValue(key, out graphic))
            {
                return true;
            }
            return false;
        }
    }
}
