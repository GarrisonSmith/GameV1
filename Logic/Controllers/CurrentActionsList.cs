using System;
using System.Collections.Generic;

namespace Fantasy.Logic.Controllers
{
    /// <summary>
    /// Data structure used for stroing the currently detected ActionControls to be processed in thier proper contexts.
    /// </summary>
    class CurrentActionsList
    {
        /// <summary>
        /// List containing the current camera ActionControls.
        /// </summary>
        public List<ActionControl> camera;
        /// <summary>
        /// List containing the current character ActionControls.
        /// </summary>
        public List<ActionControl> character;
        /// <summary>
        /// List containing the current menu ActionControls.
        /// </summary>
        public List<ActionControl> menu;

        /// <summary>
        /// Instantiates the CurrentActionsList.
        /// </summary>
        public CurrentActionsList()
        {
            camera = new List<ActionControl>();
            character = new List<ActionControl>();
            menu = new List<ActionControl>();
        }

        /// <summary>
        /// Adds the provided ActionControl foo to the correct context list if its active contexts is valid.
        /// </summary>
        /// <param name="foo">The ActionControl to be accessed and added.</param>
        public void Add(ActionControl foo)
        {
            if (!Array.Exists(foo.disableContexts, x => x == Controls.currentContext))
            {
                ControlContexts bar;
                if (Array.Exists(foo.activeContexts, x => x == Controls.currentContext))
                {
                    bar = Controls.currentContext;
                }
                else
                {
                    bar = foo.activeContexts[0];
                }

                if (bar == ControlContexts.current)
                {
                    bar = Controls.currentContext;
                }

                switch (bar)
                {
                    case ControlContexts.camera:
                        camera.Add(foo);
                        break;
                    case ControlContexts.character:
                        character.Add(foo);
                        break;
                    case ControlContexts.menu:
                        menu.Add(foo);
                        break;
                }
            }
        }
        /// <summary>
        /// Gets the ActionControl list for the provided ControlContexts foo.
        /// </summary>
        /// <param name="foo">The ControlContexts of the list to be returned.</param>
        /// <returns>A ActionControl list containg ActionControls active in the provided ControlContexts foo.</returns>
        public List<ActionControl> Get(ControlContexts foo)
        {
            if (foo == ControlContexts.current)
            {
                foo = Controls.currentContext;
            }

            switch (foo)
            {
                case ControlContexts.camera: return camera;
                case ControlContexts.character: return character;
                case ControlContexts.menu: return menu;
            }

            return camera;
        }
    }
}
