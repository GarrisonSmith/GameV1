using Microsoft.Xna.Framework;
using System.Xml;
using Fantasy.Logic.Engine.utility;


namespace Fantasy.Logic.Engine.screen
{
    class SceneEvent
    {
        public bool transitionScene = false;
        public string transitionTileMapName;
        public Point transitionStartLocation;

        public SceneEvent(XmlElement sceneEventInfo)
        {
            foreach (XmlElement foo in sceneEventInfo)
            {
                if (foo.Name == "transitionScene")
                {
                    createSceneTransition(foo);
                }
            }
        }

        public void createSceneTransition(XmlElement sceneTransitionInfo)
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
