using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Controllers
{
    /// <summary>
    /// Static class used to proccess and track MouseStates.
    /// </summary>
    public static class MouseControlHandler
    {
        /// <summary>
        /// The current mousePosition on the window. X value describe the number of pixel to the left, The Y value describes the number of pixel down.
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
        /// <summary>
        /// Creates and returns a point describing the mouses position on the current scene.
        /// </summary>
        /// <returns>A point describing the mouses position on the current scene.</returns>
        public static Point MousePositionOnScene()
        { 
            return new Point(
                Global._currentScene._camera.cameraPosition.X + (int)(mousePosition.X * 1 / Global._currentScene._camera.stretch),
                Global._currentScene._camera.cameraPosition.Y - (int)(mousePosition.Y * 1 / Global._currentScene._camera.stretch));
        }
        public static void DrawMouse()
        {
            Global._spriteBatch.Draw(Global._content.Load<Texture2D>("tile-sets/particle"), mousePosition.ToVector2(),
                new Rectangle(0, 0, 2, 2),
                Color.White, 0f, new Vector2(0, 0), new Vector2(4, 4), new SpriteEffects(), 0);
        }
    }
}