using Microsoft.Xna.Framework;

namespace Fantasy.Logic.Engine.screen.camera
{
    /// <summary>
    /// PanArgs objects are used by the CameraHandler to process the different stages of a Camera Pan task.
    /// </summary>
    public class PanArgs
    {
        /// <summary>
        /// Pan properties. Determines if this pan task will override camera movement restrictions.
        /// </summary>
        public bool forced;
        /// <summary>
        /// Pan properties. Determines if this pan task will utilize camera zooming.
        /// </summary>
        public bool useZoom;
        /// <summary>
        /// Pan properties. Determines if the destination will be centered in the cameras view, as opposed to in the top right.
        /// </summary>
        public bool centerDestination;
        /// <summary>
        /// Pan properties. Determines if the camera will pan back to the camera original location.
        /// </summary>
        public bool panBack;
        /// <summary>
        /// Pan properties. Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.
        /// </summary>
        public bool waitAfterPan;
        /// <summary>
        /// Pan properties. The amount of time for the task to wait before continuing after panning to the destination.
        /// </summary>
        public double waitTime;
        /// <summary>
        /// Pan properties. The original zoom of the camera during the creation of the task.
        /// </summary>
        public byte originalZoom;
        /// <summary>
        /// Pan properties. The original location of the camera during the creation of the task.
        /// </summary>
        public Point origin;
        /// <summary>
        /// Pan properties. The destinations of the pan.
        /// </summary>
        public Point[] destinations;
        /// <summary>
        /// Pan properties. The destination currently being panned to.
        /// </summary>
        public int currentDestination = 0;
        /// <summary>
        /// Pan properties.
        /// </summary>
        public int speed = 4;
        /// <summary>
        /// Pan properties.
        /// </summary>
        public double startWaitTime;
        /// <summary>
        /// Pan progress. Signals if the camera is done panning to the destinations.
        /// </summary>
        public bool panToDone = false;
        /// <summary>
        /// Pan progress. Signals if the camera is done zooming in.
        /// </summary>
        public bool zoomInDone = false;
        /// <summary>
        /// Pan progress. Signals if the camera is done zooming out.
        /// </summary>
        public bool zoomOutDone = false;
        /// <summary>
        /// Pan progress. Signals if the camera is done waiting at a destination.
        /// </summary>
        public bool waitDone = true;
        /// <summary>
        /// Pan progress. Signals if the camera is done panning back to origin.
        /// </summary>
        public bool panBackDone;

        /// <summary>
        /// Creates a PanArgs object with the provided specifications.
        /// </summary>
        /// <param name="forced">Determines if this pan task will override camera movement restrictions.</param>
        /// <param name="useZoom">Determines if this pan task will utilize camera zooming.</param>
        /// <param name="centerDestination">Determines if the destination will be centered in the cameras view, as opposed to in the top right.</param>
        /// <param name="panBack">Determines if the camera will pan back to the camera original location.</param>
        /// <param name="waitAfterPan">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        /// <param name="originalZoom">The original zoom of the camera during the creation of the task.</param>
        /// <param name="origin">The original location of the camera during the creation of the task.</param>
        /// <param name="destination">The destination of the pan.</param>
        public PanArgs(bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime, byte originalZoom, Point origin, Point destination)
        {
            this.forced = forced;
            this.useZoom = useZoom;
            this.panBack = panBack;
            this.originalZoom = originalZoom;
            this.centerDestination = centerDestination;
            this.waitTime = waitTime;
            this.origin = origin;
            this.destinations = new Point[] { destination };
            panBackDone = !panBack;
            waitDone = !waitAfterPan;
        }
        /// <summary>
        /// Creates a PanArgs object with the provided specifications.
        /// </summary>
        /// <param name="forced">Determines if this pan task will override camera movement restrictions.</param>
        /// <param name="useZoom">Determines if this pan task will utilize camera zooming.</param>
        /// <param name="centerDestination">Determines if the destination will be centered in the cameras view, as opposed to in the top right.</param>
        /// <param name="panBack">Determines if the camera will pan back to the camera original location.</param>
        /// <param name="waitAfterPan">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        /// <param name="originalZoom">The original zoom of the camera during the creation of the task.</param>
        /// <param name="origin">The original location of the camera during the creation of the task.</param>
        /// <param name="destinations">The destinations of the pan.</param>
        public PanArgs(bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime, byte originalZoom, Point origin, Point[] destinations)
        {
            this.forced = forced;
            this.useZoom = useZoom;
            this.panBack = panBack;
            this.originalZoom = originalZoom;
            this.centerDestination = centerDestination;
            this.waitTime = waitTime;
            this.origin = origin;
            this.destinations = destinations;
            panBackDone = !panBack;
            waitDone = !waitAfterPan;
        }
        /// <summary>
        /// Transitions the panning location from the current destination to the next destination.
        /// </summary>
        public void FinishCurrentDestination()
        {
            waitDone = false;
            startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
            if (currentDestination + 1 < destinations.Length)
            {
                currentDestination++;
            }
            else
            {
                panToDone = true;
                centerDestination = false;
                currentDestination = -1;
            }
        }
        /// <summary>
        /// Gets the destination the camera is currently panning to.
        /// </summary>
        /// <returns>The destination thec camera is currently panning to.</returns>
        public Point GetCurrentDestination()
        {
            if (currentDestination == -1)
            {
                return origin;
            }
            else
            {
                return destinations[currentDestination];
            }
        }
    }
}