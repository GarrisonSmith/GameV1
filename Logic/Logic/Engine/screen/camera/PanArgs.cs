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

        public int speed = 4;

        public double startWaitTime;

        public double waitTime;

        public Point origin;

        public Point destination;

        //pan progress
        public bool panToDone = false;

        public bool zoomInDone = false;

        public bool zoomOutDone = false;

        public bool panBackDone;

        public bool waitDone;

        public PanArgs(bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime, byte originalZoom, Point origin, Point destination)
        {
            this.forced = forced;
            this.useZoom = useZoom;
            this.panBack = panBack;
            this.originalZoom = originalZoom;
            this.centerDestination = centerDestination;
            this.waitTime = waitTime;
            this.origin = origin;
            if (centerDestination && !useZoom)
            {
                this.destination = Global._currentScene._camera.CenterPoint(destination);
            }
            else
            {
                this.destination = destination;
            }

            this.panBackDone = !panBack;
            this.waitDone = !waitAfterPan;
        }
    }
}
