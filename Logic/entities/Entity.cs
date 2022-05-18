using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.entities
{
    class Entity
    {
        public string id;
        public string spriteSheetName;
        public int layer;
        public Rectangle positionBox;
        public Rectangle hitBox;
        public Texture2D spriteSheet;

        public Entity() { }

        public Entity(string id, string spriteSheetName, int layer, Rectangle positionBox)
        {
            this.id = id;
            this.spriteSheetName = spriteSheetName;
            this.layer = layer;
            this.positionBox = positionBox;

            spriteSheet = Global._content.Load<Texture2D>(spriteSheetName);
        }
    }
}