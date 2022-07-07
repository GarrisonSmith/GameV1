using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Hitboxes
{
    /// <summary>
    /// Object class used to define a collection of rectangles with a common point offset.
    /// </summary>
    public class HitboxGeometry
    {
        /// <summary>
        /// Describes the top right position of the rectangles in boundings before any offset.
        /// </summary>
        public Point position;
        /// <summary>
        /// Describes the the visual area of the Hitbox this RectangleSet belong to.
        /// </summary>
        public Rectangle visualArea;
        /// <summary>
        /// The rectangle in this RectangleSet. Each rectangles X and Y values are used as offsets on positions corrasponding values.
        /// </summary>
        public Rectangle[] boundings;

        /// <summary>
        /// Creates a RectangleSet with the provided parameters.
        /// </summary>
        /// <param name="position">Describes the top right position of the rectangles in boundings before any offset.</param>
        /// <param name="visualArea"></param>
        /// <param name="boundings">The rectangles in this RectangleSet. Each rectangles X and Y values are used as offsets on positions corrasponding values.</param>
        public HitboxGeometry(Point position, Rectangle visualArea, Rectangle[] boundings)
        {
            this.position = position;
            this.visualArea = visualArea;
            this.boundings = boundings;
        }
        /// <summary>
        /// Determines if the provided point inside of the RectangleSets boundings.
        /// </summary>
        /// <param name="foo">The point to be investigated.</param>
        /// <returns>True if the point foo is inside of the RectangelSets boundings, False if not.</returns>
        public bool PointInsideRectangleSet(Point foo)
        {
            foreach (Rectangle bar in boundings)
            {
                if (Util.PointInsideRectangle(foo, bar))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Determines if this RectangleSet intersects with the provided RectangleSet.
        /// </summary>
        /// <param name="foo">The RectangleSet to be investigated.</param>
        /// <returns>True if the provided RectangleSet intersects with this RectangleSet, False if not.</returns>
        public bool Intersection(HitboxGeometry foo)
        {
            foreach (Rectangle thisRec in GetAbsoluteBoundings())
            {
                foreach (Rectangle fooRec in foo.GetAbsoluteBoundings())
                {
                    if (Util.RectanglesIntersect(thisRec, fooRec))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Determines if this RectangleSet intersects with the provided rectangle.
        /// </summary>
        /// <param name="foo">The rectangle to be investigated.</param>
        /// <returns>True if the provided rectangle intersects with this RectangleSet, False if not.</returns>
        public bool Intersection(Rectangle foo)
        {
            foreach (Rectangle thisRec in GetAbsoluteBoundings())
            {
                if (Util.RectanglesIntersect(thisRec, foo))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Determines if this RectangleSet intersects with any rectanlge in the provided rectangle array.
        /// </summary>
        /// <param name="foo">The rectangle array to be investigated.</param>
        /// <returns>True if the provided rectangle array intersects with this RectangleSet, False if not.</returns>
        public bool Intersection(Rectangle[] foo)
        {
            foreach (Rectangle bar in foo)
            {
                if (Intersection(bar))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Creates a new rectangle that has visualAreas perimeters with the RectangleSets position applied.
        /// </summary>
        /// <returns>A new rectnalge that has visualAreas perimeters with the RectangleSets position applied.</returns>
        public Rectangle GetAbsoluteVisualArea()
        {
            return new Rectangle(visualArea.X + position.X, visualArea.Y + position.Y, visualArea.Width, visualArea.Height);
        }
        /// <summary>
        /// Creates a new rectangle array that contains boundings rectangles with the RectangleSets position appplied.
        /// </summary>
        /// <returns>A new rectangle array that contains boundings rectangles with the RectangleSets position appplied.</returns>
        public Rectangle[] GetAbsoluteBoundings()
        {
            Rectangle[] foo = new Rectangle[boundings.Length];
            for (int i = 0; i < boundings.Length; i++)
            { 
                foo[i] = new Rectangle(boundings[i].X + position.X, boundings[i].Y + position.Y, boundings[i].Width, boundings[i].Height);
            }
            return foo;
        }
        /// <summary>
        /// Draws the rectangles in this RectangleSet.
        /// </summary>
        /// <param name="drawSegments">True results in overlapping perimeters being drawn, False results in only unique perimeter values being drawn.</param>
        public void Draw(bool drawSegments = false)
        {
            if (drawSegments)
            {
                foreach (Rectangle bounding in boundings)
                {
                    Debug.DrawRectangle(new Rectangle(bounding.X + position.X, bounding.Y + position.Y, bounding.Width, bounding.Height));
                }
            }
            else 
            {
                for (int i = 0; i < boundings.Length; i++)
                {
                    Point[] points = Util.RectanglePerimeterPoints(boundings[i]);
                    foreach (Point point in points)
                    {
                        bool drawPoint = true;
                        for (int j = 0; j < i; j++)
                        {
                            if (Util.PointInsideRectangle(point, boundings[j]))
                            {
                                drawPoint = false;
                                break;
                            }
                        }
                        if (drawPoint)
                        {
                            Debug.DrawPoint(new Point(point.X + position.X, point.Y + position.Y), false);
                        }
                    }
                }
            }
        }
    }
}