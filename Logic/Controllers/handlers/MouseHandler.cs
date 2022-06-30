using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controllers
{
    /// <summary>
    /// Static class used to proccess and track MouseStates.
    /// </summary>
    static class MouseHandler
    {
        /// <summary>
        /// The current mousePosition on the window.
        /// </summary>
        public static Point mousePosition;
        /// <summary>
        /// Tracks the Mouse Vertical Scrolling.
        /// </summary>
        private static int currentVerticalScroll = 0;

        /// <summary>
        /// Processes the provided MouseState and adds active ActionControls to provided CurrentActionsList actives.
        /// </summary>
        /// <param name="mouseState">The MouseState to be proccessed.</param>
        /// <param name="actives">The CurrentActionsList to containing active ActionControls detected by the MouseState.</param>
        public static void ProcessMouseState(MouseState mouseState, CurrentActionsList actives)
        {
            mousePosition = mouseState.Position;
            
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
                        actives.Add(actionControl);
                    }
                    else
                    {
                        actionControl.held = false;
                        actionControl.heldStartTime = 0;
                    }

                    if (actionControl.input == Inputs.RightButton && ButtonState.Pressed == mouseState.RightButton)
                    {
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
            DetermineScroll(mouseState, actives);
        }
        /// <summary>
        /// Processes the provided MouseState to determine if a MouseScroll ActionControl has happened.
        /// </summary>
        /// <param name="mouseState">The MouseState to be proccessed.</param>
        /// <param name="actives">The CurrentActionsList to containing active ActionControls detected by the KeyboardState.</param>
        private static void DetermineScroll(MouseState mouseState, CurrentActionsList actives)
        {
            if (mouseState.ScrollWheelValue - currentVerticalScroll != 0)
            {
                if (mouseState.ScrollWheelValue < currentVerticalScroll) //scroll up logic
                {
                    actives.Add(ActionControl.ControlActions.Find(x => x.action == Actions.zoomOut));
                }
                else //scroll down logic
                {
                    actives.Add(ActionControl.ControlActions.Find(x => x.action == Actions.zoomIn));
                }
                currentVerticalScroll = mouseState.ScrollWheelValue;
            }
        }

        public static void Draw()
        {
            /*
            Global._spriteBatch.Draw(Global._content.Load<Texture2D>("tile-sets/particle"), new Vector2(mousePosition.X, mousePosition.Y),
                new Rectangle(0, 0, 2, 2),
                Color.White, 0f, new Vector2(0, 0), new Vector2(4, 4), new SpriteEffects(), 0);
            */
            SpriteFont foo = Global._content.Load<SpriteFont>("Fonts/ConsolaMono");
            Point pixelPosition = new Point(Global._currentScene._camera.cameraPosition.X + mousePosition.X, Global._currentScene._camera.cameraPosition.Y - mousePosition.Y);
            Global._spriteBatch.DrawString(foo, pixelPosition.ToString(), new Vector2(mousePosition.X, mousePosition.Y+24), Color.White);
        }
    }
}