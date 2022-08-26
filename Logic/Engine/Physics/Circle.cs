using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.Utility;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Graphics;

namespace Fantasy.Logic.Engine.Physics
{
    /// <summary>
    /// Object class used to describe a circle.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// Static list that contains textures of circles of each radius and empty core.
        /// The first index of the key is the circle radius, the second index is the empty core length.
        /// </summary>
        private static Dictionary<Tuple<int, int>, Texture2D> _circleTextures = new Dictionary<Tuple<int, int>, Texture2D>();

        /// <summary>
        /// Gets or creates a circle texture with the provided radius. Already created circle textures are stored and returned later if called.
        /// </summary>
        /// <remarks>Can be very slow for larger circles that havent already been generated.</remarks>
        /// <param name="radius">The radius to be used.</param>
        /// <param name="emptyCoreRadius">The radius of the empty core the texture.</param>
        /// <returns>A texture of a circle with the provided radius.</returns>
        public static Texture2D GetCircleTexture(int radius, int emptyCoreRadius = 0)
        {
            Texture2D bar;
            if (_circleTextures.TryGetValue(new Tuple<int, int>(radius, emptyCoreRadius), out bar))
            {
                return bar;
            }

            Color[] foo = new Color[(radius * 2 + 1) * (radius * 2 + 1)];
            foo = foo.Select(i => Color.Transparent).ToArray(); //Fills the array with the transparent color.

            foreach (Point p in GetAreaPoints(radius, emptyCoreRadius))
            {
                int index = ((p.X + radius) + (((p.Y + radius) * (2 * radius + 1))));
                foo[index] = Color.White;
            }

            bar = new Texture2D(Global._graphics.GraphicsDevice, 2 * radius + 1, 2 * radius + 1); bar.SetData(foo);
            _circleTextures.Add(new Tuple<int, int>(radius, emptyCoreRadius), bar);
            return bar;
        }
        /// <summary>
        /// Creates a array containing all the points inside of a circle with the provided radius centered on (0, 0).
        /// </summary>
        /// <remarks>Can be very slow for larger circles.</remarks>
        /// <param name="radius">The radius to be used.</param>
        /// <returns>A array containing all the points inside of the circle with the provided radius centered on (0, 0)..</returns>
        private static Point[] GetAreaPoints(int radius, int emptyCoreRadius = 0)
        {
            List<Point> foo = new List<Point>();

            Point trCur = new Point(0, radius); //top right
            Point rtCur = new Point(radius, 0); //right top
            Point brCur = new Point(0, -radius); //bottom right
            Point rbCur = new Point(radius, 0); //right bottom
            while (trCur.Y >= 0 + Math.Floor(radius * Math.Cos(Math.PI / 4)))
            {
                if (Util.DistanceBetweenPoints(trCur, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(trCur); }
                int trMirrorX = -trCur.X; //adds current top right point and mirrors it across circle center, then fills everything between them.
                if (trCur.Y == radius)
                {
                    Point trMirror = new Point(trMirrorX, trCur.Y);
                    if (Util.DistanceBetweenPoints(trMirror, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(trMirror); }
                }
                else
                {
                    for (int i = trMirrorX; i < trCur.X; i++)
                    {
                        Point trI = new Point(i, trCur.Y);
                        if (Util.DistanceBetweenPoints(trI, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(trI); }
                    }
                }

                if (Util.DistanceBetweenPoints(rtCur, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(rtCur); } //adds current right top point and mirrors it across circle center, then fills everything between them.
                for (int i = -rtCur.X; i < rtCur.X; i++)
                {
                    Point rtI = new Point(i, rtCur.Y);
                    if (Util.DistanceBetweenPoints(rtI, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(rtI); }
                }

                if (Util.DistanceBetweenPoints(brCur, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(brCur); }
                int brMirrorX = -brCur.X; //adds current bottom right point and mirrors it across circle center, then fills everything between them.
                if (brCur.Y == -radius)
                {
                    if (Util.DistanceBetweenPoints(brCur, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(new Point(brMirrorX, brCur.Y)); }
                }
                else
                {
                    for (int i = brMirrorX; i < brCur.X; i++)
                    {
                        Point brI = new Point(i, brCur.Y);
                        if (Util.DistanceBetweenPoints(brI, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(brI); }
                    }
                }

                if (Util.DistanceBetweenPoints(rbCur, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(rbCur); }
                foo.Add(new Point(-rbCur.X, rbCur.Y)); //adds current right bottom point and mirrors it across circle center, then fills everything between them.
                for (int i = -rbCur.X; i < rbCur.X; i++)
                {
                    Point rbI = new Point(i, rbCur.Y);
                    if (Util.DistanceBetweenPoints(rbI, new Point(0, 0)) >= emptyCoreRadius) { foo.Add(rbI); }
                }


                if (Math.Abs(Util.DistanceBetweenPoints(new Point(0, 0), new Point(trCur.X + 1, trCur.Y)) - radius) <= Math.Abs(Util.DistanceBetweenPoints(new Point(0, 0), new Point(trCur.X + 1, trCur.Y - 1)) - radius))
                {
                    trCur = new Point(trCur.X + 1, trCur.Y);
                    rtCur = new Point(rtCur.X, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y);
                    rbCur = new Point(rbCur.X, rbCur.Y - 1);
                }
                else
                {
                    trCur = new Point(trCur.X + 1, trCur.Y - 1);
                    rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y + 1);
                    rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                }
            }

            return foo.ToArray();
        }

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public int radius;
        /// <summary>
        /// The radius of the empty core of the circle.
        /// </summary>
        public int emptyCoreRadius;
        /// <summary>
        /// The center of the circle.
        /// </summary>
        public Point center;
        /// <summary>
        /// The texture of the circle.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Creates a circle with the provided parameters.
        /// </summary>
        /// <param name="radius">The radius the circle will use.</param>
        /// <param name="center">The center the circle will use.</param>
        /// <param name="createTexture">Determines if the circles texture is generated immediately.</param>
        public Circle(int radius, Point center, bool createTexture = true)
        {
            this.radius = radius;
            emptyCoreRadius = 0;
            this.center = center;
            if (createTexture)
            {
                GetTexture();
            }
        }
        /// <summary>
        /// Creates a circle with the provided parameters.
        /// </summary>
        /// <param name="radius">The radius the circle will use.</param>
        /// <param name="emptyCoreRadius">the radius of the empty core the circle will use.</param>
        /// <param name="center">The center the circle will use.</param>
        /// <param name="createTexture">Determines if the circles texture is generated immediately.</param>
        public Circle(int radius, int emptyCoreRadius, Point center, bool createTexture = true)
        {
            this.radius = radius;
            this.emptyCoreRadius = emptyCoreRadius;
            this.center = center;
            if (createTexture)
            {
                GetTexture();
            }
        }

        /// <summary>
        /// Determines if the provided point is inside of this circle.
        /// </summary>
        /// <param name="point">The point to be investigated.</param>
        /// <returns>True if the point is inside of this circle, False if not.</returns>
        public bool PointInsideCircle(Point point)
        {
            return (Util.DistanceBetweenPoints(point, center) <= radius) && (Util.DistanceBetweenPoints(point, center) >= emptyCoreRadius);
        }
        /// <summary>
        /// Gets the top left position corrasponding to the circle center and radius.
        /// </summary>
        /// <param name="invertY">True will multiply the Y value of the returned point by -1, False will not.</param>
        /// <returns>A point describing the top left position of the circle.</returns>
        public Point GetTopLeftPointPosition(bool invertY = false)
        {
            if (invertY)
            {
                return new Point(center.X - radius, -(center.Y + radius));
            }
            else
            {
                return new Point(center.X - radius, center.Y + radius);
            }
        }
        /// <summary>
        /// Gets the top left position corrasponding to the circle center and radius.
        /// </summary>
        /// <param name="invertY">True will multiply the Y value of the returned Vector2 by -1, False will not.</param>
        /// <returns>A Vector2 describing the top left position of the circle.</returns>
        public Vector2 GetTopLeftVectorPosition(bool invertY = false)
        {
            if (invertY)
            {
                return new Vector2(center.X - radius, -(center.Y + radius));
            }
            else
            {
                return new Vector2(center.X - radius, center.Y + radius);
            }
        }
        /// <summary>
        /// Creates a array containing all points on the circumference of this circle.
        /// </summary>
        /// <param name="getEmptyCoreCircumference">True will returns the points on the empty cores circumference in addition to the outer circumference, 
        /// False will only return the outer circumference.</param>
        /// <returns>A array containing all the points on the circumference of this circle.</returns>
        public Point[] GetCircumferencePoints(bool getEmptyCoreCircumference = true)
        {
            List<Point> foo = new List<Point>();

            //gets out circumference.
            Point trCur = new Point(center.X, center.Y + radius); //top right
            Point rtCur = new Point(center.X + radius, center.Y); //right top
            Point brCur = new Point(center.X, center.Y - radius); //bottom right
            Point rbCur = new Point(center.X + radius, center.Y); //right bottom
            while (trCur.Y >= center.Y + Math.Floor(radius * Math.Cos(Math.PI / 4)))
            {
                foo.Add(trCur); foo.Add(new Point(2 * center.X - trCur.X, trCur.Y)); //adds current top right point and mirrors it across circle center.
                foo.Add(rtCur); foo.Add(new Point(2 * center.X - rtCur.X, rtCur.Y)); //adds current right top point and mirrors it across circle center.
                foo.Add(brCur); foo.Add(new Point(2 * center.X - brCur.X, brCur.Y)); //adds current bottom right point and mirrors it across circle center.
                foo.Add(rbCur); foo.Add(new Point(2 * center.X - rbCur.X, rbCur.Y)); //adds current right bottom point and mirrors it across circle center.

                if (Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y)) - radius) <= Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y - 1)) - radius))
                {
                    trCur = new Point(trCur.X + 1, trCur.Y);
                    rtCur = new Point(rtCur.X, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y);
                    rbCur = new Point(rbCur.X, rbCur.Y - 1);
                }
                else
                {
                    trCur = new Point(trCur.X + 1, trCur.Y - 1);
                    rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y + 1);
                    rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                }
            }

            //gets inner circumference.
            if (getEmptyCoreCircumference)
            {
                trCur = new Point(center.X, center.Y + emptyCoreRadius); //top right
                rtCur = new Point(center.X + emptyCoreRadius, center.Y); //right top
                brCur = new Point(center.X, center.Y - emptyCoreRadius); //bottom right
                rbCur = new Point(center.X + emptyCoreRadius, center.Y); //right bottom
                while (trCur.Y >= center.Y + Math.Floor(emptyCoreRadius * Math.Cos(Math.PI / 4)))
                {
                    foo.Add(trCur); foo.Add(new Point(2 * center.X - trCur.X, trCur.Y)); //adds current top right point and mirrors it across circle center.
                    foo.Add(rtCur); foo.Add(new Point(2 * center.X - rtCur.X, rtCur.Y)); //adds current right top point and mirrors it across circle center.
                    foo.Add(brCur); foo.Add(new Point(2 * center.X - brCur.X, brCur.Y)); //adds current bottom right point and mirrors it across circle center.
                    foo.Add(rbCur); foo.Add(new Point(2 * center.X - rbCur.X, rbCur.Y)); //adds current right bottom point and mirrors it across circle center.

                    if (Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y)) - emptyCoreRadius) <= Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y - 1)) - emptyCoreRadius))
                    {
                        trCur = new Point(trCur.X + 1, trCur.Y);
                        rtCur = new Point(rtCur.X, rtCur.Y + 1);
                        brCur = new Point(brCur.X + 1, brCur.Y);
                        rbCur = new Point(rbCur.X, rbCur.Y - 1);
                    }
                    else
                    {
                        trCur = new Point(trCur.X + 1, trCur.Y - 1);
                        rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                        brCur = new Point(brCur.X + 1, brCur.Y + 1);
                        rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                    }
                }
            }
            return foo.ToArray();
        }
        /// <summary>
        /// Creates a array containing all the points inside of this circle.
        /// </summary>
        /// <remarks>Can be very slow for larger circles.</remarks>
        /// <returns>A array containing all the points inside of the circle.</returns>
        public Point[] GetAreaPoints()
        {
            List<Point> foo = new List<Point>();

            Point trCur = new Point(center.X, center.Y + radius); //top right
            Point rtCur = new Point(center.X + radius, center.Y); //right top
            Point brCur = new Point(center.X, center.Y - radius); //bottom right
            Point rbCur = new Point(center.X + radius, center.Y); //right bottom
            while (trCur.Y >= center.Y + Math.Floor(radius * Math.Cos(Math.PI / 4)))
            {
                if (Util.DistanceBetweenPoints(trCur, center) >= emptyCoreRadius) { foo.Add(trCur); } //adds current top right point and mirrors it across circle center, then fills everything between them.
                int trMirrorX = 2 * center.X - trCur.X;
                if (trCur.Y == center.Y + radius)
                {
                    Point trMirror = new Point(trMirrorX, trCur.Y);
                    if (Util.DistanceBetweenPoints(trMirror, center) >= emptyCoreRadius) { foo.Add(trMirror); }
                }
                else
                {
                    for (int i = trMirrorX; i < trCur.X; i++)
                    {
                        Point trI = new Point(i, trCur.Y);
                        if (Util.DistanceBetweenPoints(trI, center) >= emptyCoreRadius) { foo.Add(trI); }
                    }
                }

                if (Util.DistanceBetweenPoints(rtCur, center) >= emptyCoreRadius) { foo.Add(rtCur); } //adds current right top point and mirrors it across circle center, then fills everything between them.
                for (int i = 2 * center.X - rtCur.X; i < rtCur.X; i++)
                {
                    Point rtI = new Point(i, rtCur.Y);
                    if (Util.DistanceBetweenPoints(rtI, center) >= emptyCoreRadius) { foo.Add(rtI); }
                }

                if (Util.DistanceBetweenPoints(brCur, center) >= emptyCoreRadius) { foo.Add(brCur); } //adds current bottom right point and mirrors it across circle center, then fills everything between them.
                int brMirrorX = 2 * center.X - brCur.X;
                if (brCur.Y == center.Y - radius)
                {
                    if (Util.DistanceBetweenPoints(brCur, center) >= emptyCoreRadius) { foo.Add(new Point(brMirrorX, brCur.Y)); }
                }
                else
                {
                    for (int i = brMirrorX; i < brCur.X; i++)
                    {
                        Point brI = new Point(i, brCur.Y);
                        if (Util.DistanceBetweenPoints(brI, center) >= emptyCoreRadius) { foo.Add(brI); }
                    }
                }

                if (Util.DistanceBetweenPoints(rbCur, center) >= emptyCoreRadius) { foo.Add(rbCur); } //adds current right bottom point and mirrors it across circle center, then fills everything between them.
                foo.Add(new Point(2 * center.X - rbCur.X, rbCur.Y));
                for (int i = 2 * center.X - rbCur.X; i < rbCur.X; i++)
                {
                    Point rbI = new Point(i, rbCur.Y);
                    if (Util.DistanceBetweenPoints(rbI, center) >= emptyCoreRadius) { foo.Add(rbI); }
                }


                if (Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y)) - radius) <= Math.Abs(Util.DistanceBetweenPoints(center, new Point(trCur.X + 1, trCur.Y - 1)) - radius))
                {
                    trCur = new Point(trCur.X + 1, trCur.Y);
                    rtCur = new Point(rtCur.X, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y);
                    rbCur = new Point(rbCur.X, rbCur.Y - 1);
                }
                else
                {
                    trCur = new Point(trCur.X + 1, trCur.Y - 1);
                    rtCur = new Point(rtCur.X - 1, rtCur.Y + 1);
                    brCur = new Point(brCur.X + 1, brCur.Y + 1);
                    rbCur = new Point(rbCur.X - 1, rbCur.Y - 1);
                }
            }

            return foo.ToArray();
        }
        /// <summary>
        /// Creates a Texture2D from this circle. The circle is white and its surroundings are transparent.
        /// If the circle texture already exists then it returns it.
        /// </summary>
        /// <remarks>Can be very slow for larger circles that havent already been generated.</remarks>
        /// <returns>A Texture2D of this circle.</returns>
        public Texture2D GetTexture()
        {
            if (texture != null)
            {
                return texture;
            }

            Texture2D bar;
            if (_circleTextures.TryGetValue(new Tuple<int, int>(radius, emptyCoreRadius), out bar))
            {
                texture = bar;
                return bar;
            }

            Color[] foo = new Color[(radius * 2 + 1) * (radius * 2 + 1)];
            foo = foo.Select(i => Color.Transparent).ToArray(); //Fills the array with the transparent color.

            foreach (Point p in GetAreaPoints())
            {
                int index = ((p.X - center.X + radius) + (((p.Y - center.Y + radius) * (2 * radius + 1))));
                foo[index] = Color.White;
            }

            bar = new Texture2D(Global._graphics.GraphicsDevice, 2 * radius + 1, 2 * radius + 1); bar.SetData(foo);
            _circleTextures.Add(new Tuple<int, int>(radius, emptyCoreRadius), bar);
            texture = bar;
            return bar;
        }
        /// <summary>
        /// Creates a Color array from this circles texture. 
        /// </summary>
        /// <returns>A Color array describing this circles texture.</returns>
        public Color[] GetTextureData()
        {
            Texture2D foo = GetTexture();
            Color[] textureArray = new Color[foo.Width * foo.Height];
            foo.GetData(textureArray);
            return textureArray;
        }
        /// <summary>
        /// Determines if this circle intersects with the provided rectangle.
        /// </summary>
        /// <param name="foo">The rectangle to be investigated.</param>
        /// <returns>True if the rectangle interesects this circle, False if not. 
        /// A rectangle completely inside a hollow inner circle is still inside the circle.</returns>
        public bool Intersection(Rectangle foo)
        {
            Point recCenter = Util.GetCenter(foo);
            
            int XDistance = Math.Abs(center.X - recCenter.X);
            int YDistance = Math.Abs(center.Y - recCenter.Y);

            if (XDistance > (foo.Width / 2 + radius)) { return false; }
            if (YDistance > (foo.Height / 2 + radius)) { return false; }

            if (XDistance <= (foo.Width / 2)) { return true; }
            if (YDistance <= (foo.Height / 2)) { return true; }

            double SQCornerDistance = (XDistance - foo.Width / 2) ^ 2 + (YDistance - foo.Height / 2) ^ 2;

            return (SQCornerDistance <= (radius ^ 2));
        }
        /// <summary>
        /// Sets this circles texture to the provided texture.
        /// </summary>
        /// <param name="foo">The texture to be made this circles texture.</param>
        public void SetTexture(Texture2D foo)
        {
            texture = foo;
        }

        /// <summary>
        /// Draws this circles texture.
        /// </summary>
        /// <param name="color">The color to be used when drawing.</param>
        public void Draw(Color color)
        {
            Global._spriteBatch.Draw(GetTexture(), GetTopLeftVectorPosition(true),
                new Rectangle(0, 0, 2 * radius + 1, 2 * radius + 1),
                color, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), 0);
            Debug.DrawPoint(center, Color.Red);
        }
    }
}
