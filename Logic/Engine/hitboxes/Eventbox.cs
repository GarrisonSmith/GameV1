using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Eventbox : Hitbox
    {
        public SceneEvent sceneEvent;
        public Point location;
        public Eventbox(Point location)
        {
            this.location = location;
        }
        public Eventbox(Point location, SceneEvent sceneEvent, Rectangle[] collisionArea) : this(location)
        {
            this.sceneEvent = sceneEvent;
            this.collisionArea = collisionArea;
        }

        public bool Collision(Point inRef, Rectangle foo)
        {
            Rectangle doo = new Rectangle(
                (int)((inRef.X + foo.X) * Global._baseStretch.X),
                (int)((inRef.Y - foo.Y - foo.Height) * Global._baseStretch.Y),
                (int)(foo.Width * Global._baseStretch.X),
                (int)(foo.Height * Global._baseStretch.Y));

            foreach (Rectangle bar in collisionArea)
            {
                Rectangle baz = new Rectangle(
                (int)((location.X + bar.X) * Global._baseStretch.X),
                (int)((location.Y - bar.Y - bar.Height) * Global._baseStretch.Y),
                (int)(bar.Width * Global._baseStretch.X),
                (int)(bar.Height * Global._baseStretch.Y));
                if (baz.Intersects(doo))
                {
                    Global._currentScene.DoEvent(sceneEvent);
                    return true;
                }
            }
            return false;
        }

        public bool Collision(Entitybox entityBox)
        {
            Rectangle[] foofoo = entityBox.collisionArea;

            foreach (Rectangle foo in foofoo)
            {
                if (Collision(entityBox.characterArea.Location, foo))
                {
                    return true;
                }
            }
            return false;
        }

        new public void DrawHitbox()
        {
            foreach (Rectangle foo in collisionArea)
            {
                Rectangle bar = new Rectangle(
                 (int)((location.X + foo.X) * Global._baseStretch.X),
                 (int)((location.Y - foo.Y) * Global._baseStretch.Y),
                 (int)(foo.Width * Global._baseStretch.X),
                 (int)(foo.Height * Global._baseStretch.Y));

                Debug.DrawRectangle(bar);
            }
        }
    }
}
