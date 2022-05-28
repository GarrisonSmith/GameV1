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
        public Rectangle positionBox;
        public Entitybox hitBox;
        public Texture2D spriteSheet;

        public Entity() { }

        public Entity(string id, string type, string spriteSheetName, int layer, Rectangle positionBox)
        {
            this.id = id;
            this.type = type;
            this.spriteSheetName = spriteSheetName;
            this.layer = layer;
            this.positionBox = positionBox;

            spriteSheet = Global._content.Load<Texture2D>(spriteSheetName);
        }

        public void DrawHitbox()
        {
            hitBox.DrawHitbox(positionBox.Location);
        }
    }
}