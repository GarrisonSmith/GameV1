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
        /// The input that controls this ActionControl.
        /// </summary>
        public Inputs input;
        /// <summary>
        /// The GameTime that this ActionControl began being held.
        /// </summary>
        public double heldStartTime;
        /// <summary>
        /// True if this ActionControl just triggered, False if not.
        /// </summary>
        public bool held;

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
        public ActionControl(Actions action, Inputs input) : this(action)
        {
            this.input = input;
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
