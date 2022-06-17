using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.screen.camera
{
    public class PanArgs
    {   
        //pan properties

        public bool forced;

        public bool useZoom;

        public bool centerDestination;

        public bool panBack;

        public bool waitAfterPan;

        public byte originalZoom;

        public int speed = 10;

        public double startWaitTime;

        public double waitTime;

        public Point destination;

        public Point origin;

        //pan progress
        public bool panToDone = false;

        public bool panBackDone = false;

        public bool zoomInDone = false;

        public bool zoomOutDone = false;

        public bool waitDone = false;

        public PanArgs(bool forced, bool useZoom, bool panBack, bool centerDestination,bool waitAfterPan, byte originalZoom, Point destination, Point origin)
        {
            this.forced = forced;
            this.useZoom = useZoom;
            this.panBack = panBack;
            this.originalZoom = originalZoom;
            this.centerDestination = centerDestination;
            if (centerDestination && !useZoom)
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
