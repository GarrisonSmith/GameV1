using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.physics;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.screen.camera
{
    /// <summary>
    /// PanArgs objects are used by the CameraHandler to process the different stages of a Camera Pan task.
    /// </summary>
    public class PanArgs
    {
        /// <summary>
        /// Pan properties. Defines what order and how the panning operations will happen.
        /// Possible instructions:
        /// W: wait
        /// P: pan to current destination
        /// FP: force pan to current destination
        /// ZI: zoom in to original zoom
        /// ZO: zoom out to point
        /// R: pan to origin
        /// FR: force pan to origin
        /// END: ends the pan
        /// </summary>
        private string[] panInstructions;
        /// <summary>
        /// The instruction currently being done.
        /// </summary>
        private int currentInstruction = 0;
        /// <summary>
        /// The amount of time for the task to wait before continuing after panning to the destination.
        /// </summary>
        public double waitTime;
        /// <summary>
        /// The minimum zoom level allowed when zooming out.
        /// </summary>
        public byte panMinZoom;
        /// <summary>
        /// The original zoom of the camera during the creation of the task.
        /// </summary>
        public byte returnZoom;
        /// <summary>
        /// The original location of the camera during the creation of the task.
        /// </summary>
        public Point origin;
        /// <summary>
        /// The destinations of the pan.
        /// </summary>
        public Point[] destinations;
        /// <summary>
        /// The destination currently being panned to.
        /// </summary>
        private int currentDestination = 0;
        /// <summary>
        /// Determines how quickly the camera will move.
        /// </summary>
        public MoveSpeed speed;
        /// <summary>
        /// The time the pan begins waiting.
        /// </summary>
        public double startWaitTime;

        /// <summary>
        /// Creates a PanArgs object with the provided specifications.
        /// </summary>
        /// <param name="panInstructions">String describing in what order the panning operations will be done, operations should be seperated with a '.'.
        /// Possible instructions:
        /// W: wait
        /// P: pan to current destination
        /// FP: force pan to current destination
        /// ZI: zoom in to original zoom
        /// ZO: zoom out to point
        /// R: pan to origin
        /// FR: force pan to origin
        /// END: ends the pan
        /// Example string: "ZO.P.ZI.W.P.W.P.W.R"</param>
        /// <param name="waitTime">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="returnZoom">The zoom the camera will return to when zooming in.</param>
        /// <param name="speed">Describes the MoveSpeed of the camera.</param>
        /// <param name="origin">The original location of the camera during the creation of the task.</param>
        /// <param name="destination">The destination of the pan.</param>
        public PanArgs(string panInstructions, double waitTime, byte returnZoom, MoveSpeed speed, Point origin, Point destination)
        {
            this.panInstructions = panInstructions.Split('.');
            this.waitTime = waitTime;
            this.returnZoom = returnZoom;
            this.speed = speed;
            this.origin = origin;
            this.destinations = new Point[] { destination };
            startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
        }
        /// <summary>
        /// Creates a PanArgs object with the provided specifications.
        /// </summary>
        /// <param name="panInstructions">String describing in what order the panning operations will be done, operations should be seperated with a '.'.
        /// Possible instructions:
        /// W: wait
        /// P: pan to current destination
        /// FP: force pan to current destination
        /// ZI: zoom in to original zoom
        /// ZO: zoom out to point
        /// R: pan to origin
        /// FR: force pan to origin
        /// END: ends the pan
        /// Example string: "ZO.P.ZI.W.P.W.P.W.R"</param>
        /// <param name="waitTime">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="returnZoom">The zoom the camera will return to when zooming in.</param>
        /// <param name="speed">Describes the MoveSpeed of the camera.</param>
        /// <param name="origin">The original location of the camera during the creation of the task.</param>
        /// <param name="destinations">The destinations of the pan.</param>
        public PanArgs(string panInstructions, double waitTime, byte returnZoom, MoveSpeed speed, Point origin, Point[] destinations)
        {
            this.panInstructions = panInstructions.Split('.');
            this.waitTime = waitTime;
            this.returnZoom = returnZoom;
            this.speed = speed;
            this.origin = origin;
            this.destinations = destinations;
            startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
        }
        /// <summary>
        /// Finished the current instruction and moves the PanArgs onto the next one.
        /// </summary>
        public void FinishCurrentInstruction()
        {
            if (currentInstruction + 1 < panInstructions.Length  && currentInstruction != -1)
            {
                if (panInstructions[currentInstruction].Equals("P") || panInstructions[currentInstruction].Equals("FP"))
                {
                    FinishCurrentDestination();
                }
                if (panInstructions[currentInstruction].Equals("W"))
                {
                    speed.RefreshLastMovementTime();
                }
                currentInstruction++;
                if (panInstructions[currentInstruction].Equals("W"))
                {
                    startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            else
            {
                currentInstruction = -1;
            }
        }
        /// <summary>
        /// Determines the current instruction of the PanArgs
        /// </summary>
        /// <returns>String describing the current instruction to be done.</returns>
        public string GetCurrentInstruction()
        {
            if (currentInstruction == -1)
            {
                return "END";
            }
            else
            {
                return panInstructions[currentInstruction];
            }
        }
        /// <summary>
        /// Transitions the panning location from the current destination to the next destination.
        /// </summary>
        private void FinishCurrentDestination()
        {
            startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
            if (currentDestination + 1 < destinations.Length && currentInstruction != -1)
            {
                currentDestination++;
            }
            else
            {
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
        /// <summary>
        /// Determines if this PanArgs has finished its last wait instruction or not.
        /// </summary>
        /// <returns>True if the last wait instruction has been completed, False if not.</returns>
        public bool WaitFinished()
        {
            return (startWaitTime + waitTime <= Global._gameTime.TotalGameTime.TotalMilliseconds);
        }
    }
}