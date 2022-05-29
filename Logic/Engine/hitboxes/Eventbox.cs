using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.screen;

namespace Fantasy.Logic.Engine.hitboxes
{
    class Eventbox : Hitbox
    {
        SceneEvent sceneEvent;
        Point location;
        public Eventbox(SceneEvent sceneEvent, Point location)
        {
            this.sceneEvent = sceneEvent;
            this.location = location;
        }
        public Eventbox(SceneEvent sceneEvent, Point location, Rectangle[] collisionArea) : this(sceneEvent, location)
        {
            this.collisionArea = collisionArea;
        }
    }
}
