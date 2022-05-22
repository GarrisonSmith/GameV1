using System;
using Microsoft.Xna.Framework;
using Fantasy.Content.Logic.utility;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Hitbox
    {
        public string reference;
        public Rectangle[] area;

        public Hitbox(string reference)
        {
            this.reference = reference;
        }
        public Hitbox(string reference, Rectangle[] area)
        {
            this.reference = reference;
            this.area = area;
        }
        public bool Collision(Rectangle foo)
        {
            foreach (Rectangle bar in area)
            {
                if (foo.Intersects(bar))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
