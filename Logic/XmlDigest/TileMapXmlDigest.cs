using System.Xml;
using Fantasy.Logic.Engine.Hitboxes;
using Fantasy.Logic.Engine.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;
using Fantasy.Logic.Engine.Graphics.tilemap;
using Fantasy.Logic.Engine.Graphics;
using Fantasy.Logic.Engine.Graphics.Lighting;
using System;

namespace Fantasy.Logic.XmlDigest
{
    /// <summary>
    /// Static class containing method for creating objects from Xml used by TileMaps.
    /// </summary>
    public static class TileMapXmlDigest
    {
        /// <summary>
        /// Creates a Tile from the provided XmlElement.
        /// </summary>
        /// <param name="tileTag">Xml element describing a Tile.</param>
        /// <returns>A Tile with the perameters described in tileTag.</returns>
        public static Tile GetTile(XmlElement tileTag)
        {
            Texture2D tileSet = null;
            Point tileSetCoordinate = new Point();
            Point tileMapCoordinate = new Point();
            Rectangle positionBox = new Rectangle();
            Tilebox[] hitboxes = null;
            Lightbox[] lightboxes = null;

            foreach (XmlElement foo in tileTag)
            {
                if (foo.Name.Equals("tileSet"))
                {
                    ContentHandler.tileSets.TryGetValue(foo.InnerText, out tileSet);
                }
                else if (foo.Name.Equals("tileSetCoordinate"))
                {
                    tileSetCoordinate = Util.PointFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("tileMapCoordinate"))
                {
                    tileMapCoordinate = Util.PointFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("positionBox"))
                {
                    positionBox = Util.RectangleFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("hitboxes"))
                {
                    hitboxes = GetTileHitboxes(foo);
                }
                else if (foo.Name.Equals("lightboxes"))
                {
                    lightboxes = GetTileLightboxes(foo);
                }
            }
            return new Tile(tileSet, tileSetCoordinate, tileMapCoordinate, positionBox, hitboxes, lightboxes);
        }

        /// <summary>
        /// Creates a AnimatedTile from the provided XmlElement.
        /// </summary>
        /// <param name="animatedTag">Xml element describing a AnimatedTile.</param>
        /// <returns>A AnimatedTile with the perameters described in animatedTag.</returns>
        public static AnimatedTile GetAnimatedTile(XmlElement animatedTag)
        {
            Texture2D tileSet = null;
            Point tileSetCoordinate = new Point();
            Point tileMapCoordinate = new Point();
            Rectangle positionBox = new Rectangle();
            Tilebox[] hitboxes = null;
            Animation animation = null;

            foreach (XmlElement foo in animatedTag)
            {
                if (foo.Name.Equals("tileSet"))
                {
                    ContentHandler.tileSets.TryGetValue(foo.InnerText, out tileSet);
                }
                else if (foo.Name.Equals("tileSetCoordinate"))
                {
                    tileSetCoordinate = Util.PointFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("tileMapCoordinate"))
                {
                    tileMapCoordinate = Util.PointFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("positionBox"))
                {
                    positionBox = Util.RectangleFromString(foo.InnerText);
                }
                else if (foo.Name.Equals("hitboxes"))
                {
                    hitboxes = GetTileHitboxes(foo);
                }
                else if (foo.Name.Equals("animation"))
                {
                    animation = GetAnimation(foo);
                }
            }
            return new AnimatedTile(tileSet, tileSetCoordinate, tileMapCoordinate, positionBox, hitboxes, animation);
        }

        /// <summary>
        /// Creates a Tilebox array from the provided XmlElement.
        /// </summary>
        /// <param name="hitboxesTag">Xml element describing a Hitbox array.</param>
        /// <returns>A Tilebox array with the perameters described in hitboxesTag.</returns>
        public static Tilebox[] GetTileHitboxes(XmlElement hitboxesTag)
        {
            if (hitboxesTag.ChildNodes.Count == 0)
            {
                return null;
            }
            
            Tilebox[] tileboxes = new Tilebox[hitboxesTag.ChildNodes.Count];
            int hitboxIndex = 0;
            foreach (XmlElement hitbox in hitboxesTag)
            {
                MovementInclusions movementInclusions = MovementInclusions.inassessible;
                Point position = new Point();
                Rectangle[] boundings = null;

                foreach (XmlElement foo in hitbox)
                {
                    if (foo.Name.Equals("movementInclusion"))
                    {
                        movementInclusions = (MovementInclusions)Enum.Parse(typeof(MovementInclusions), foo.InnerText);
                    }
                    else if (foo.Name.Equals("position"))
                    {
                        position = Util.PointFromString(foo.InnerText);
                    }
                    else if (foo.Name.Equals("boundings"))
                    {
                        boundings = new Rectangle[foo.ChildNodes.Count];
                        int boundingIndex = 0;
                        foreach (XmlElement rectangle in foo)
                        {
                            boundings[boundingIndex] = Util.RectangleFromString(rectangle.InnerText);
                            boundingIndex++;
                        }
                    }
                }
                tileboxes[hitboxIndex] = new Tilebox(movementInclusions, position, boundings);
                hitboxIndex++;
            }
            return tileboxes;
        }

        public static Lightbox[] GetTileLightboxes(XmlElement lightboxesTag)
        {
            if (lightboxesTag.ChildNodes.Count == 0)
            {
                return null;
            }

            Lightbox[] lightboxes = new Lightbox[lightboxesTag.ChildNodes.Count];
            int lightboxIndex = 0;
            foreach (XmlElement hitbox in lightboxesTag)
            {
                Point position = new Point();
                bool[,] lightPhysics = null;
                Rectangle[] boundings = null;

                foreach (XmlElement foo in hitbox)
                {
                    if (foo.Name.Equals("position"))
                    {
                        position = Util.PointFromString(foo.InnerText);
                    }
                    else if (foo.Name.Equals("boundings"))
                    {
                        boundings = new Rectangle[foo.ChildNodes.Count];
                        int boundingIndex = 0;
                        foreach (XmlElement rectangle in foo)
                        {
                            boundings[boundingIndex] = Util.RectangleFromString(rectangle.InnerText);
                            boundingIndex++;
                        }
                    }
                    else if (foo.Name.Equals("lightphysics"))
                    {
                        lightPhysics = Util.BoolArrayFromString(foo.InnerText);
                    }
                }
                lightboxes[lightboxIndex] = new Lightbox(position, boundings, lightPhysics);
                lightboxIndex++;
            }
            return lightboxes;
        }
        /// <summary>
        /// Creates a Animation from the provided XmlElement.
        /// </summary>
        /// <param name="animationTag">Xml element describing a Animation.</param>
        /// <returns>A Animation with the perameters described in animationTag.</returns>
        public static Animation GetAnimation(XmlElement animationTag)
        {
            Texture2D graphic = null;
            int minFrameDuration = 0;
            int maxFrameDuration = 0;
            int startingFrame = 0;
            int maxFrame = 0;
            int columnReference = 0;
            int rowReference = 0;
            int sourceWidth = 0;
            int sourceHeight = 0;
            AnimationState animationState = AnimationState.cycling;

            foreach (XmlElement foo in animationTag)
            {
                if (foo.Name.Equals("graphic"))
                {
                    ContentHandler.TryGetTexture(foo.InnerText, out graphic);
                }
                else if (foo.Name.Equals("minFrameDuration"))
                {
                    minFrameDuration = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("maxFrameDuration"))
                {
                    maxFrameDuration = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("startingFrame"))
                {
                    startingFrame = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("maxFrame"))
                {
                    maxFrame = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("columnReference"))
                {
                    columnReference = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("rowReference"))
                {
                    rowReference = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("sourceWidth"))
                {
                    sourceWidth = int.Parse(foo.InnerText);
                }
                else if (foo.Name.Equals("sourceHeight"))
                {
                    sourceHeight = int.Parse(foo.InnerText);
                }
            }
            return new Animation(graphic, minFrameDuration, maxFrameDuration, startingFrame, maxFrame, rowReference, columnReference, sourceWidth, sourceHeight, animationState);
        }

        /// <summary>
        /// Creates a Eventbox from the provided XmlElement.
        /// </summary>
        /// <param name="eventTag">Xml element describing a Eventbox.</param>
        /// <returns>A Eventbox with the perameters described in eventboxTag.</returns>
        public static Eventbox GetEventbox(XmlElement eventTag)
        {
            string id = null;
            string description = null;
            SceneEvent sceneEvent = null;
            Point position = new Point();
            Rectangle[] bounding = null;

            foreach (XmlElement foo in eventTag)
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
                else if (foo.Name.Equals("boundings"))
                {
                    bounding = new Rectangle[foo.ChildNodes.Count];
                    for (int i = 0; i < foo.ChildNodes.Count; i++)
                    {
                        bounding[i] = Util.RectangleFromString(foo.ChildNodes[i].InnerText);
                    }
                }
            }
            return new Eventbox(id, description, sceneEvent, position, bounding);
        }
    }
}