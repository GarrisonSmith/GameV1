using System;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.utility;

namespace Fantasy.Logic.Engine.screen.camera
{
    static class CameraHandler
    {
        public static CameraTasks mainTask = CameraTasks.none;

        public static CameraTasks sideTask = CameraTasks.none;

        public static CameraPan panArgs;

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
                    if (camera.Pan(panArgs))
                    {
                        sideTask = CameraTasks.none;
                    }
                    break;
                case CameraTasks.forcedPanning:
                    if (camera.ForcePan(panArgs))
                    {
                        sideTask = CameraTasks.none;
                    }
                    break;
                case CameraTasks.zoomPanning:

                    break;
                case CameraTasks.forcedZoomPanning:
                    if (camera.ForcePanWithZoom(panArgs))
                    {
                        sideTask = CameraTasks.none;
                    }
                    break;
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

        public static void AssignPanningTask(Point point, bool forced, bool useZoom, bool panBack, bool centerDestination)
        {
            if (forced)
            {
                if (useZoom)
                {
                    sideTask = CameraTasks.forcedZoomPanning;
                }
                else
                {
                    sideTask = CameraTasks.forcedPanning;
                }
            }
            else
            {
                if (useZoom)
                {
                    sideTask = CameraTasks.zoomPanning;
                }
                else
                {
                    sideTask = CameraTasks.panning;
                }
            }
            panArgs = new CameraPan(forced, useZoom, panBack, centerDestination, Global._currentScene._camera.zoom, point, Util.GetTopLeft(Global._currentScene._camera.cameraPosition));
        }
    }
}
