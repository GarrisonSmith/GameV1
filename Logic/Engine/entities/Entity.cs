using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.hitboxes;
namespace Fantasy.Logic.Engine.entities
{
    class Entity
    {
        public string id;
        public string type;
        public string spriteSheetName;
        public int layer;
        public Entitybox hitbox;
        public Texture2D spriteSheet;

        public Entity() { }

        public Entity(string id, string type, string spriteSheetName, int layer, Entitybox hitbox)
        {
            this.id = id;
            this.type = type;
            this.spriteSheetName = spriteSheetName;
            this.layer = layer;
            this.hitbox = hitbox;

            spriteSheet = Global._content.Load<Texture2D>(spriteSheetName);
        }

        public void DrawHitbox()
        {
            hitbox.DrawHitbox();
        }
    }
}