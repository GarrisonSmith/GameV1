using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controls
{
    class MouseHandler
    {
        static int currentVerticalScroll = 0; 

        public static void ProcessMouseState(MouseState mouseState)
        {
            List<ActionControl> actives = new List<ActionControl>();
            foreach (ActionControl actionControl in ActionControl.ControlActions)
            {
                if (actionControl.input > (Inputs)254 && actionControl.input <= (Inputs)307)
                {
                    if (actionControl.input == Inputs.LeftButton && ButtonState.Pressed == mouseState.LeftButton)
                    {
                        System.Diagnostics.Debug.WriteLine("left");
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
            DetermineZoom(mouseState);
            Global._currentScene.ProcessInputs(actives);
        }

        private static void DetermineZoom(MouseState mouseState)
        {
            if (mouseState.ScrollWheelValue-currentVerticalScroll != 0)
            {
                if (mouseState.ScrollWheelValue < currentVerticalScroll) //scroll in logic
                {
                    //Global._currentScene._camera.SmoothZoomIn(.1f, false);
                }
                else //scroll out logic
                {
                    //Global._currentScene._camera.SmoothZoomOut(.1f, false);
                }
                currentVerticalScroll = mouseState.ScrollWheelValue;
            }
        }
    }
}
