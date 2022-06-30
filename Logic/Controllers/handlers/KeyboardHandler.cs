using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controllers
{
    /// <summary>
    /// Static class used to proccess KeyboardStates.
    /// </summary>
    static class KeyboardHandler
    {
        /// <summary>
        /// Processes the provided KeyboardState and adds active ActionControls to provided CurrentActionsList actives.
        /// </summary>
        /// <param name="keyboardState">The KeyboardState to be proccessed.</param>
        /// <param name="actives">The CurrentActionsList to containing active ActionControls detected by the KeyboardState.</param>
        public static void ProcessKeyboardState(KeyboardState keyboardState, CurrentActionsList actives)
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
        }
    }
}
