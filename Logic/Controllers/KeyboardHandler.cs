using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controllers
{
    class KeyboardHandler
    {
        public static List<ActionControl> ProcessKeyboardState(KeyboardState keyboardState)
        {
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

                            if (!Array.Exists(actionControl.disableContexts, x => x == Controls.currentContext))
                            {
                                actives.Add(actionControl);
                            }

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
            return actives;
        }
    }
}
