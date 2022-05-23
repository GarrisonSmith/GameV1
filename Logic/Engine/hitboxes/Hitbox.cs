using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Hitbox
    {
        public string reference;
        public Rectangle[] area;

        public Hitbox() { }
        public Hitbox(string reference)
        {
            this.reference = reference;
        }
        public Hitbox(string reference, Rectangle[] area)
        {
            this.reference = reference;
            this.area = area;
        }
        public bool Collision(Point inRef, Point thisRef, Rectangle foo)
        {
            foo = new Rectangle(
                (int)((foo.X + inRef.X) * Global._baseStretch.X),
                (int)((foo.Y + inRef.Y) * Global._baseStretch.Y), 
                (int)(foo.Width* Global._baseStretch.X),
                (int)(foo.Height * Global._baseStretch.Y));

            Debug.DrawRectangle(foo);

            foreach (Rectangle bar in area)
            {
                Rectangle baz = new Rectangle(
                (int)((bar.X + thisRef.X) * Global._baseStretch.X),
                (int)((bar.Y + thisRef.Y) * Global._baseStretch.Y),
                (int)(bar.Width * Global._baseStretch.X),
                (int)(bar.Height * Global._baseStretch.Y));
                Debug.DrawRectangle(baz);
                if (foo.Intersects(baz))
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
    }
}
