using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Fantasy.Logic.Controls
{
    public class ActionControl
    {
        public static List<ActionControl> ControlActions = new List<ActionControl>();
        
        public Actions action;
        public Keys key;

        public ActionControl(Actions action)
        {
            this.action = action;
            ControlActions.Add(this);
        }
        public ActionControl(Actions action, Keys key) : this(action)
        {
            this.key = key;
        }
    }
    
    public enum Actions
    {
        up,
        down,
        left,
        right,
        zoomIn,
        zoomOut
    }
}
