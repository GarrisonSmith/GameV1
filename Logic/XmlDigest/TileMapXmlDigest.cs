using System.Xml;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Screen;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.XmlDigest
{
    /// <summary>
    /// Static class containing method for creating objects from Xml used by TileMaps.
    /// </summary>
    public static class TileMapXmlDigest
    {
        /// <summary>
        /// Creates a Eventbox from the provided XmlElement.
        /// </summary>
        /// <param name="eventboxTag">Xml element descibing a Eventbox.</param>
        /// <returns>A Eventbox with the perameters described in eventboxTag.</returns>
        public static Eventbox GetEventbox(XmlElement eventboxTag)
        {
            string id = null;
            string description = null;
            SceneEvent sceneEvent = null;
            Point position = new Point();
            Rectangle visualArea = new Rectangle();
            Rectangle[] bounding = null;

            foreach (XmlElement foo in eventboxTag)
            {
                if (foo.Name.Equals("id"))
                {
                    id = foo.InnerText;
                }
                else if (foo.Name.Equals("discription"))
                {
                    description = foo.InnerText;
                }
                else if (foo.Name.Equals("sceneEvent"))
                {
                    sceneEvent = new SceneEvent(foo);
                }
                else if (foo.Name.Equals("position"))
                {
                    position = Util.PointFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("visualArea"))
                {
                    visualArea = Util.RectangleFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("boundings"))
                {
                    bounding = new Rectangle[foo.ChildNodes.Count];
                    for (int i = 0; i < foo.ChildNodes.Count; i++)
                    {
                        bounding[i] = Util.RectangleFromString(foo.ChildNodes[i].InnerText);
                    }
                }
            }

            return new Eventbox(id, description, sceneEvent, position, visualArea, bounding);
        }
    }
}
