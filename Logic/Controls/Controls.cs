using System;
using System.Xml;
using Microsoft.Xna.Framework.Input;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controls
{
    /// <summary>
    /// Class used to contain methods and members that control user inputs.
    /// </summary>
    static class Controls
    {
        /// <summary>
        /// The ActionControl for the "up" action.
        /// Basic movement action.
        /// </summary>
        public static ActionControl up;
        /// <summary>
        /// The ActionControl for the "down" action.
        /// Basic movement action.
        /// </summary>
        public static ActionControl down;
        /// <summary>
        /// The ActionControl for the "right" action.
        /// Basic movement action.
        /// </summary>
        public static ActionControl right;
        /// <summary>
        /// The ActionControl for the "left" action.
        /// Basic movement action.
        /// </summary>
        public static ActionControl left;
        /// <summary>
        /// The ActionControl for the "zoomIn" action.
        /// Camera control action.
        /// </summary>
        public static ActionControl zoomIn;
        /// <summary>
        /// The ActionControl for the "zoomOut" action.
        /// Camera control action.
        /// </summary>
        public static ActionControl zoomOut;

        /// <summary>
        /// Instantiates all of the ActionControls used for user input.
        /// </summary>
        private static void Instantiate()
        {
            //basic movement keys
            up = new ActionControl(Actions.up);
            down = new ActionControl(Actions.down);
            right = new ActionControl(Actions.right);
            left = new ActionControl(Actions.left);

            //camera keys
            zoomIn = new ActionControl(Actions.zoomIn);
            zoomOut = new ActionControl(Actions.zoomOut);
        }
        /// <summary>
        /// Loads the keys assigned to each action from the "controls_config.xml" file.
        /// </summary>
        public static void LoadControls()
        {
            Instantiate();

            XmlDocument controlsConfig = new XmlDocument();
            controlsConfig.Load(@"Content\game-configs\controls_config.xml");

            foreach (XmlElement foo in controlsConfig.DocumentElement)
            {
                ActionControl temp = ActionControl.ControlActions.Find(x => x.action == (Actions)Enum.Parse(typeof(Actions), foo.Name));
                temp.key = (Keys)Enum.Parse(typeof(Keys), foo.GetAttribute("keys"));
            }
        }
        /// <summary>
        /// Processes the provided KeyboardState. 
        /// </summary>
        /// <param name="keyboardState">The KeyboardState to be processed.</param>
        public static void ProcessKeyboard(KeyboardState keyboardState)
        {
            foreach (Keys foo in keyboardState.GetPressedKeys())
            {
                Global._currentScene.ProcessInput(GetAction(foo));
            }
        }
        /// <summary>
        /// /Process the provided MouseState.
        /// </summary>
        /// <param name="mouseState">The MouseState to be processed.</param>
        public static void ProcessMouse(MouseState mouseState)
        {
            if (ButtonState.Pressed == mouseState.LeftButton)
            {
                System.Diagnostics.Debug.WriteLine("left");
            }
            if (ButtonState.Pressed == mouseState.RightButton)
            {
                System.Diagnostics.Debug.WriteLine("right");
            }
            if (ButtonState.Pressed == mouseState.MiddleButton)
            {
                System.Diagnostics.Debug.WriteLine("middle");
            }
            if (ButtonState.Pressed == mouseState.XButton1)
            {
                System.Diagnostics.Debug.WriteLine("x1");
            }
            if (ButtonState.Pressed == mouseState.XButton2)
            {
                System.Diagnostics.Debug.WriteLine("x2");
            }
        }
        /// <summary>
        /// Gets the corrasponding Action that the provided Key is bound to.
        /// </summary>
        /// <param name="key">The Key to find the matching Action to.</param>
        /// <returns></returns>
        public static Actions GetAction(Keys key)
        {
            ActionControl foo = ActionControl.ControlActions.Find(x => x.key.Equals(key));
            if (foo != null)
            {
                return foo.action;
            }
            else {
                return Actions.inaction;
            }
        }
        /// <summary>
        /// Gets the corrasponding Key that the provided Action is bound to.
        /// </summary>
        /// <param name="action">The Action to find the matching Key to.</param>
        /// <returns></returns>
        public static Keys GetKey(Actions action)
        {
            ActionControl foo = ActionControl.ControlActions.Find(x => x.action.Equals(action));
            if (foo != null)
            {
                return foo.key;
            }
            else
            {
                return Keys.None;
            }
        }
    }
}
