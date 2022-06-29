using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controllers
{
    class MouseHandler
    {
        static int currentVerticalScroll = 0; 

        public static List<ActionControl> ProcessMouseState(MouseState mouseState)
        {
            List<ActionControl> actives = new List<ActionControl>();

            foreach (ActionControl actionControl in ActionControl.ControlActions)
            {
                if (actionControl.input > (Inputs)254 && actionControl.input <= (Inputs)307)
                {
                    if (actionControl.input == Inputs.LeftButton && ButtonState.Pressed == mouseState.LeftButton)
                    {
                        if (!actionControl.held)
                        {
                            actionControl.held = true;
                            actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                  
                        if (!Array.Exists(actionControl.disableContexts, x => x == Controls.currentContext))
                        {
                            actives.Add(actionControl);
                        }
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }

                    if (actionControl.input == Inputs.RightButton && ButtonState.Pressed == mouseState.RightButton)
                    {
                        System.Diagnostics.Debug.WriteLine("right");
                        if (!actionControl.held)
                        {
                            actionControl.held = true;
                            actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        actives.Add(actionControl);
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }

                    if (actionControl.input == Inputs.MiddleButton && ButtonState.Pressed == mouseState.MiddleButton)
                    {
                        System.Diagnostics.Debug.WriteLine("middle");
                        if (!actionControl.held)
                        {
                            actionControl.held = true;
                            actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        actives.Add(actionControl);
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }

                    if (actionControl.input == Inputs.XButton1 && ButtonState.Pressed == mouseState.XButton1)
                    {
                        System.Diagnostics.Debug.WriteLine("x1");
                        if (!actionControl.held)
                        {
                            actionControl.held = true;
                            actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        actives.Add(actionControl);
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }

                    if (actionControl.input == Inputs.XButton2 && ButtonState.Pressed == mouseState.XButton2)
                    {
                        System.Diagnostics.Debug.WriteLine("x2");
                        if (!actionControl.held)
                        {
                            actionControl.held = true;
                            actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        actives.Add(actionControl);
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }
                }
            }
            DetermineZoom(mouseState, actives);
            return actives;
        }

        private static void DetermineZoom(MouseState mouseState, List<ActionControl> actives)
        {
            if (mouseState.ScrollWheelValue-currentVerticalScroll != 0)
            {
                if (mouseState.ScrollWheelValue < currentVerticalScroll) //scroll up logic
                {
                    actives.Add(ActionControl.ControlActions.Find(x => x.action == Actions.zoomOut));
                    //Global._currentScene._camera.SmoothZoom(.1f, false, false);
                }
                else //scroll down logic
                {
                    actives.Add(ActionControl.ControlActions.Find(x => x.action == Actions.zoomIn));
                    //Global._currentScene._camera.SmoothZoom(.1f, false, true);
                }
                currentVerticalScroll = mouseState.ScrollWheelValue;
            }
        }
    }
}
