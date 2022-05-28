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
        public Tilebox(string reference, Rectangle[] area) : this(reference)
        {
            this.area = area;
        }
        public bool Collision(Point inRef, Point thisRef, Rectangle foo)
        {
            Rectangle doo = new Rectangle(
                (int)((inRef.X + foo.X) * Global._baseStretch.X),
                (int)((inRef.Y - foo.Y - foo.Height) * Global._baseStretch.Y),
                (int)(foo.Width * Global._baseStretch.X),
                (int)(foo.Height * Global._baseStretch.Y));

            foreach (Rectangle bar in area)
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

        public bool Collision(Point inRef, Point thisRef, Rectangle[] foofoo)
        {
            foreach (Rectangle foo in foofoo)
            {
                if (Collision(inRef, thisRef, foo))
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawHitbox(Point thisRef)
        {
            foreach (Rectangle foo in area)
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
