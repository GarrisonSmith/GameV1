using Microsoft.Xna.Framework.Input;
using System;
using System.Xml;
using Fantasy.Logic.Engine;

namespace Fantasy.Logic.Controls
{
    static class Controls
    {
        //basic movement
        public static ActionControl up;
        public static ActionControl down;
        public static ActionControl right;
        public static ActionControl left;
        //camera
        public static ActionControl zoomIn;
        public static ActionControl zoomOut;

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
        public static void ProcessInput(KeyboardState keyboardState)
        {
            foreach (Keys foo in keyboardState.GetPressedKeys())
            {
                Global._currentScene.ProcessInput(GetAction(foo));
            }
        }
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
        public static Keys GetKey(Actions action)
        {
            return ActionControl.ControlActions.Find(x => x.action == action).key;
        }
    }
}
