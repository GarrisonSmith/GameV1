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
                            }
                            Global._currentScene.ProcessInput(actionControl);
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
        }
    }



}
