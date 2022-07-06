using Microsoft.Xna.Framework;
using System.Xml;
using Fantasy.Logic.Engine.Utility;


namespace Fantasy.Logic.Engine.Screen
{
    /// <summary>
    /// Describes a scene event to be processed by a Scene.
    /// </summary>
    public class SceneEvent
    {
        /// <summary>
        /// Determines if this SceneEvent describes a scene transition. 
        /// </summary>
        public bool transitionScene = false;
        /// <summary>
        /// The name of the TileMap the scene will transition to if this SceneEvent causes a scene transition.
        /// </summary>
        public string transitionTileMapName;
        /// <summary>
        /// The starting location of the character if this SceneEvent causes a scene transition.
        /// </summary>
        public Point transitionStartLocation;

        /// <summary>
        /// Proccess and creates the SceneEvent described in the XmlElement sceneEventInfo.
        /// </summary>
        /// <param name="sceneEventInfo">Described the SceneEvents parameters.</param>
        public SceneEvent(XmlElement sceneEventInfo)
        {
            foreach (XmlElement foo in sceneEventInfo)
            {
                if (foo.Name == "transitionScene")
                {
                    CreateSceneTransition(foo);
                }
            }
        }
        /// <summary>
        /// Creates a scene transition for this SceneEvent.
        /// </summary>
        /// <param name="sceneTransitionInfo"></param>
        public void CreateSceneTransition(XmlElement sceneTransitionInfo)
        {
            transitionScene = true;
            transitionTileMapName = sceneTransitionInfo.GetAttribute("name");
            foreach (XmlElement foo in sceneTransitionInfo)
            {
                if (foo.Name == "startLocation")
                {
                    transitionStartLocation = Util.PointFromString(foo.InnerText);
                }
            }
        }

    }
}
