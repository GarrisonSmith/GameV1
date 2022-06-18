using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.utility;

namespace Fantasy.Logic.Engine.screen.camera
{
    static class CameraHandler
    {
        public static CameraTasks mainTask = CameraTasks.none;

        public static CameraTasks sideTask = CameraTasks.none;

        public static PanArgs panArgs;

        public static Entity followEntity;

        public static void UpdateCamera(Camera camera)
        {
            if (sideTask == CameraTasks.none)
            {
                DoMainTask(camera);
            }
            else
            {
                DoSideTask(camera);
            }
        }

        private static void DoMainTask(Camera camera)
        { 

        }

        private static void DoSideTask(Camera camera)
        {
            switch (sideTask)
            {
                case CameraTasks.none:

                    break;
                case CameraTasks.free:

                    break;
                case CameraTasks.forcedFree:

                    break;
                case CameraTasks.following:

                    break;
                case CameraTasks.forcedFollowing:

                    break;
                case CameraTasks.panning:
                    DoPanningTask(camera);
                    break;
            }
        }

        private static void DoPanningTask(Camera camera)
        {
            if (!panArgs.waitDone)
            {
                if (panArgs.startWaitTime + panArgs.waitTime <= Global._gameTime.TotalGameTime.TotalMilliseconds)
                {
                    panArgs.waitDone = true;
                }
            }
            else if (!panArgs.zoomOutDone)
            {
                if (camera.Pan_ZoomOut(panArgs))
                {
                    panArgs.zoomOutDone = true;
                }
            }
            else if (!panArgs.panToDone)
            {
                if (camera.Pan(panArgs))
                {
                    panArgs.FinishCurrentDestination();
                }
            }
            else if (!panArgs.zoomInDone)
            {
                if (camera.Pan_ZoomIn(panArgs))
                {
                    panArgs.zoomInDone = true;
                }
            }
            else if (!panArgs.panBackDone)
            {
                if (camera.Pan(panArgs))
                {
                    panArgs.panBackDone = true;
                }
            }
            else
            {
                sideTask = CameraTasks.none;
            }
        }

        public static void AssignNoneTask()
        {
            mainTask = CameraTasks.none;
        }

        public static void AssignFreeTask(bool forced)
        {
            if (forced)
            {
                mainTask = CameraTasks.forcedFree;
            }
            else
            {
                mainTask = CameraTasks.free;
            }
        }

        public static void AssignFollowingTask(Entity entity, bool forced)
        {
            if (forced)
            {
                mainTask = CameraTasks.forcedFollowing;
            }
            else
            {
                mainTask = CameraTasks.following;
            }
            followEntity = entity;
        }
        /// <summary>
        /// Creates and assigned a panning task for a provided camera.
        /// </summary>
        /// <param name="destination">The point for the camera to pan to.</param>
        /// <param name="forced">Determines if this pan task will override camera movement restrictions.</param>
        /// <param name="useZoom">Determines if this pan task will utilize camera zooming.</param>
        /// <param name="centerDestination">Determines if the destination will be centered in the cameras view, as opposed to in the top right.</param>
        /// <param name="panBack">Determines if the camera will pan back to the camera original location.</param>
        /// <param name="waitAfterPan">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        public static void AssignPanningTask(Camera camera, Point destination, bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime)
        {
            if (sideTask == CameraTasks.none)
            {
                sideTask = CameraTasks.panning;
                panArgs = new PanArgs(forced, useZoom, centerDestination, panBack, waitAfterPan, waitTime, camera.zoom, Util.GetTopLeft(camera.cameraPosition), destination);
            }
        }
        /// <summary>
        /// Creates and assigned a panning task for a provided camera.
        /// </summary>
        /// <param name="destinations">The points for the camera to pan to.</param>
        /// <param name="forced">Determines if this pan task will override camera movement restrictions.</param>
        /// <param name="useZoom">Determines if this pan task will utilize camera zooming.</param>
        /// <param name="centerDestination">Determines if the destination will be centered in the cameras view, as opposed to in the top right.</param>
        /// <param name="panBack">Determines if the camera will pan back to the camera original location.</param>
        /// <param name="waitAfterPan">Determines if the camera will wait a specified amount of time after panning to the destination before either finishing the task or panning back.</param>
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        public static void AssignPanningTask(Camera camera, Point[] destinations, bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime)
        {
            if (sideTask == CameraTasks.none)
            {
                sideTask = CameraTasks.panning;
                panArgs = new PanArgs(forced, useZoom, centerDestination, panBack, waitAfterPan, waitTime, camera.zoom, Util.GetTopLeft(camera.cameraPosition), destinations);
            }
        }
    }
}
