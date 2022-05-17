using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Content.Logic.entities
{
    class Entity
    {
        public string id;
        public Texture2D spritesheet;
        public int layer;
        public Rectangle positionBox;
        public Rectangle hitBox;

        public Entity() { }

        public Entity(string id, Texture2D spritesheet, int layer, Rectangle positionBox)
        {
            this.id = id;
            this.spritesheet = spritesheet;
            this.layer = layer;
            this.positionBox = positionBox;
        }
    }
}