using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.screen.camera
{
    public class CameraPan
    {   
        public bool forced;

        public bool useZoom;

        public int speed = 10;

        public byte originalZoom;

        public bool panBack;

        public Point destination;

        public Point origin;

        public Point lastPosition;

        public CameraPan(bool forced, bool useZoom, bool panBack, bool centerDestination, byte originalZoom, Point destination, Point origin)
        {
            this.forced = forced;
            this.useZoom = useZoom;
            this.panBack = panBack;
            this.originalZoom = originalZoom;
            if (centerDestination)
            {
                this.destination = Global._currentScene._camera.CenterPoint(destination);
            }
            else
            {
                this.destination = destination;
            }
            this.origin = origin;
        }
    }
}
