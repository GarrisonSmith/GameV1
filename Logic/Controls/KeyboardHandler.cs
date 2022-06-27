using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controls
{
    class KeyboardHandler
    {
        public static void ProcessKeyboardState(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                //System.Diagnostics.Debug.WriteLine("here here");
            }
            else 
            {
                //System.Diagnostics.Debug.WriteLine("no no no");
            }

            List<ActionControl> actives = new List<ActionControl>();
            foreach (ActionControl actionControl in ActionControl.ControlActions)
            {
                if (actionControl.input <= (Inputs)254)
                {
                    bool found = false;
                    foreach (Keys key in keyboardState.GetPressedKeys())
                    {
                        if (actionControl.input == (Inputs)key)
                        {
                            found = true;
                            if (!actionControl.held)
                            {
                                actionControl.held = true;
                                actionControl.heldStartTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
                                actionControl.justTriggered = true;
                            }
                            else 
                            {
                                actionControl.justTriggered = false;
                            }
                            actives.Add(actionControl);
                            break;
                        }
                    }
                    if (!found)
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }
                }
            }
            Global._currentScene.ProcessInputs(actives);
        }
    }



}
