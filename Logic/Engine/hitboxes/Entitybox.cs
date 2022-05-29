using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Entitybox : Hitbox
    {
        public string reference;
        public Rectangle characterArea;

        public Entitybox(string reference, Rectangle characterArea)
        {
            this.reference = reference;
            this.characterArea = characterArea;
        }

        public Entitybox(string reference, Rectangle characterArea, Rectangle[] collisionArea) : this(reference, characterArea)
        {
            this.collisionArea = collisionArea;
        }

        public bool AttemptMovement(int layer, Rectangle newCharacterArea)
        {
            Rectangle temp = characterArea;
            characterArea = newCharacterArea;
            if (Global._currentScene._tileMap.Collision(layer, this))
            {
                characterArea = temp;
                return false;
            }
            return true;
        }

        new public void DrawHitbox()
        {
            foreach (Rectangle foo in collisionArea)
            {
                Rectangle bar = new Rectangle(
                 (int)((characterArea.X + foo.X) * Global._baseStretch.X),
                 (int)((characterArea.Y - foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}
