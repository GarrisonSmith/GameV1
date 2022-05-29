using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Tilebox : Hitbox
    {
        public string reference;

        public Tilebox() { }
        public Tilebox(string reference)
        {
            this.reference = reference;
        }
        public Tilebox(string reference, Rectangle[] collisionArea) : this(reference)
        {
            this.collisionArea = collisionArea;
        }
        public bool Collision(Point inRef, Point thisRef, Rectangle foo)
        {
            Rectangle doo = new Rectangle(
                (int)((inRef.X + foo.X) * Global._baseStretch.X),
                (int)((inRef.Y - foo.Y - foo.Height) * Global._baseStretch.Y),
                (int)(foo.Width * Global._baseStretch.X),
                (int)(foo.Height * Global._baseStretch.Y));

            foreach (Rectangle bar in collisionArea)
            {
                Rectangle baz = new Rectangle(
                (int)((thisRef.X + bar.X) * Global._baseStretch.X),
                (int)((thisRef.Y - bar.Y - bar.Height) * Global._baseStretch.Y),
                (int)(bar.Width * Global._baseStretch.X),
                (int)(bar.Height * Global._baseStretch.Y));
                if (baz.Intersects(doo))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Collision(Point thisRef, Entitybox entityBox)
        {
            Rectangle[] foofoo = entityBox.collisionArea;
            
            foreach (Rectangle foo in foofoo)
            {
                if (Collision(entityBox.characterArea.Location, thisRef, foo))
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawHitbox(Point thisRef)
        {
            foreach (Rectangle foo in collisionArea)
            {
                Rectangle bar = new Rectangle(
                 (int)((thisRef.X + foo.X) * Global._baseStretch.X),
                 (int)((thisRef.Y - foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}
