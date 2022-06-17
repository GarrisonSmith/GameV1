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
                    if (!panArgs.zoomOutDone) 
                    {
                        if (camera.Pan_ZoomOut(panArgs))
                        {
                            panArgs.zoomOutDone = true;
                        }
                    }
                    else if (!panArgs.panToDone)
                    {
                        byte tempZoom = camera.zoom;
                        camera.SetZoom(panArgs.originalZoom, false);
                        if (camera.Pan(panArgs))
                        {
                            panArgs.panToDone = true;
                            panArgs.destination = panArgs.origin;
                            panArgs.startWaitTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        camera.SetZoom(tempZoom, false);
                    }
                    else if (!panArgs.zoomInDone)
                    {
                        if (camera.Pan_ZoomIn(panArgs))
                        {
                            panArgs.zoomInDone = true;
                        }
                    }
                    else if (!panArgs.waitDone)
                    {
                        if (panArgs.startWaitTime + panArgs.waitTime <= Global._gameTime.TotalGameTime.TotalMilliseconds)
                        {
                            panArgs.waitDone = true;
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

        public static void AssignPanningTask(Point point, bool forced, bool useZoom, bool centerDestination, bool panBack, bool waitAfterPan, double waitTime)
        {
            if (sideTask == CameraTasks.none)
            {
                sideTask = CameraTasks.panning;
                panArgs = new PanArgs(forced, useZoom, centerDestination, panBack, waitAfterPan, waitTime, Global._currentScene._camera.zoom, Util.GetTopLeft(Global._currentScene._camera.cameraPosition), point);
            }
        }
    }
}
