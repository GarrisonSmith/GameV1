using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Controls;
using Fantasy.Logic.Engine.physics;

namespace Fantasy.Logic.Engine.screen.camera
{
    static class CameraHandler
    {
        public static CameraTasks mainTask = CameraTasks.free;

        public static CameraTasks sideTask = CameraTasks.none;

        public static MoveSpeed speed = new MoveSpeed(96, TimeUnits.seconds);

        public static PanArgs panArgs;

        public static Entity followEntity;

        public static void UpdateCamera()
        {
            if (sideTask == CameraTasks.none)
            {
                DoTask(mainTask);
            }
            else
            {
                DoTask(sideTask);
            }
        }

        /// <summary>
        /// Camera will do the provided action.
        /// </summary>
        /// <param name="actionControl">The action control for the camera to do.</param>
        public static void DoAction(ActionControl actionControl)
        {
            if (sideTask == CameraTasks.none)
            {
                System.Diagnostics.Debug.WriteLine(actionControl.held);
                if (!actionControl.held)
                {
                    speed.RefreshLastMovementTime();
                }

                switch (actionControl.action)
                {
                    case Actions.up:
                        MoveCamera(Orientation.up);
                        break;
                    case Actions.down:
                        MoveCamera(Orientation.down);
                        break;
                    case Actions.left:
                        MoveCamera(Orientation.left);
                        break;
                    case Actions.right:
                        MoveCamera(Orientation.right);
                        break;
                    case Actions.zoomIn:
                        Global._currentScene._camera.SmoothZoom(.10f, true, true);
                        break;
                    case Actions.zoomOut:
                        Global._currentScene._camera.SmoothZoom(.10f, true, false);
                        break;
                }
            }
        }

        public static void MoveCamera(Orientation direction)
        {
            if (mainTask == CameraTasks.free)
            {
                Global._currentScene._camera.Move(false, direction, speed.MovementAmount());
            }
            else if (mainTask == CameraTasks.forcedFree)
            {
                Global._currentScene._camera.Move(true, direction, speed.MovementAmount());
            }
        }
        /// <summary>
        /// Prompts the camera to begin/continue with the provided camera task.
        /// </summary>
        /// <param name="task">The task to be done.</param>
        private static void DoTask(CameraTasks task)
        {
            switch (task)
            {
                case CameraTasks.none:
                    //camera is static and wont move / accept any inputs.
                    break;
                case CameraTasks.free:
                    //camera will freely accept non-forced movements.
                    break;
                case CameraTasks.forcedFree:
                    //camera will freely accept forced movements.
                    break;
                case CameraTasks.following:
                    Global._currentScene._camera.SetCoordinate(false, followEntity.hitbox.characterArea.Center, false);
                    break;
                case CameraTasks.forcedFollowing:
                    Global._currentScene._camera.SetCoordinate(false, followEntity.hitbox.characterArea.Center, true);
                    break;
                case CameraTasks.panning:
                    DoPanningTask();
                    break;
            }
        }

        private static void DoPanningTask()
        {
            Camera camera = Global._currentScene._camera;

            switch (panArgs.GetCurrentInstruction())
            {
                case "W":
                    if (panArgs.WaitFinished())
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "P":
                    if (camera.Pan(panArgs.GetCurrentDestination(), panArgs.speed.MovementAmount(), false))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "FP":
                    if (camera.Pan(panArgs.GetCurrentDestination(), panArgs.speed.MovementAmount(), true))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "ZI":
                    if (camera.Pan_ZoomIn(panArgs.returnZoom))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "ZO":
                    if (camera.Pan_ZoomOut(panArgs.GetCurrentDestination(), panArgs.panMinZoom))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "R":
                    if (camera.Pan(panArgs.origin, panArgs.speed.MovementAmount(), false))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "FR":
                    if (camera.Pan(panArgs.origin, panArgs.speed.MovementAmount(), true))
                    {
                        panArgs.FinishCurrentInstruction();
                    }
                    break;
                case "END":
                    sideTask = CameraTasks.none;
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
        /// <summary>
        /// Creates and assignes a panning task for the current scenes camera.
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
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        /// <param name="speed">Describes the MoveSpeed of the camera.</param>
        /// <param name="destination">The point for the camera to pan to.</param>
        public static void AssignPanningTask(string panInstructions, double waitTime, MoveSpeed speed, Point destination)
        {
            Camera camera = Global._currentScene._camera;
            if (sideTask == CameraTasks.none)
            {
                sideTask = CameraTasks.panning;
                panArgs = new PanArgs(panInstructions, waitTime, camera.zoom, speed, camera.cameraCenter, destination);
            }
        }
        /// <summary>
        /// Creates and assignes a panning task for the current scenes camera.
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
        /// <param name="waitTime">The amount of time for the task to wait before continuing after panning to the destination.</param>
        /// <param name="speed">Describes the MoveSpeed of the camera.</param>
        /// <param name="destinations">The points for the camera to pan to.</param>
        public static void AssignPanningTask(string panInstructions, double waitTime, MoveSpeed speed, Point[] destinations)
        {
            Camera camera = Global._currentScene._camera;
            if (sideTask == CameraTasks.none)
            {
                sideTask = CameraTasks.panning;
                panArgs = new PanArgs(panInstructions, waitTime, camera.zoom, speed, camera.cameraCenter, destinations);
            }
        }
    }
}
