using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Fantasy.Logic.Controls
{
    /// <summary>
    /// Object used to describe what keys cause what actions.
    /// </summary>
    class ActionControl
    {
        /// <summary>
        /// Static list containing all instances of this object.
        /// </summary>
        public static List<ActionControl> ControlActions = new List<ActionControl>();
        
        /// <summary>
        /// The action this ActionControl describes.
        /// </summary>
        public Actions action;
        /// <summary>
        /// The key this ActionControl describes.
        /// </summary>
        public Keys key;
        /// <summary>
        /// True if this ActionControl just triggered, False if not.
        /// </summary>
        public bool justPressed;

        /// <summary>
        /// Creates a ActionConrol with the provided parameters.
        /// </summary>
        /// <param name="action">The action this ActionControl describes.</param>
        public ActionControl(Actions action)
        {
            this.action = action;
            ControlActions.Add(this);
        }
        /// <summary>
        /// Creates a ActionConrol with the provided parameters.
        /// </summary>
        /// <param name="action">The action this ActionControl describes.</param>
        /// <param name="key">The key this ActionControl describes.</param>
        public ActionControl(Actions action, Keys key) : this(action)
        {
            this.key = key;
        }
    }
    
    /// <summary>
    /// Describes the possible actions that a key press can cause to happen.
    /// </summary>
    public enum Actions
    {
        inaction,
        up,
        down,
        left,
        right,
        zoomIn,
        zoomOut
    }
}
