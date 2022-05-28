using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Eventbox : Hitbox
    {
        Point location;
        SceneEvent sceneEvent;
        public Eventbox(Point location, SceneEvent sceneEvent)
        {
            this.location = location;
            this.sceneEvent = sceneEvent;
        }
        public Eventbox(Rectangle[] area, Point location, SceneEvent sceneEvent) : this(location, sceneEvent)
        {
            this.area = area;
        }
    }
}
