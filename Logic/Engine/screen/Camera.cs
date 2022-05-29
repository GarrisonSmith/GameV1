using System;
using Microsoft.Xna.Framework;
using Fantasy.Content.Logic.utility;

namespace Fantasy.Logic.Engine.screen
{
    /// <summary>
    /// Describes a scenes camera. Determines the placement and stretching of graphics when drawn by the spritebatch.
    /// </summary>
    class Camera
    {
        /// <summary>
        /// The scene for this camera.
        /// </summary>
        public Scene _scene;
        /// <summary>
        /// The center of whats on screen.
        /// </summary>
        public Point cameraCenter;
        /// <summary>
        /// The rectangle that describes what is in the cameras view.
        /// </summary>
        public Rectangle cameraPosition;
        /// <summary>
        /// The bounding collisionArea that the cameras center which can restricts the cameras movement.
        /// </summary>
        public Rectangle boundingBox;
        /// <summary>
        /// The max stretch for this camera.
        /// </summary>
        public Vector2 maxStretch = new Vector2(3f, 3f);
        /// <summary>
        /// The minimum stretch for this camera.
        /// </summary>
        public Vector2 minStretch = new Vector2(.5f, .5f);
        /// <summary>
        /// Determines how much the final drawing of the spritebatch is rotated around the origin.
        /// TODO Not implemented fully.
        /// </summary>
        public float rotation = 0f;
        /// <summary>
        /// Determines if vertical camera movement is restricted.
        /// </summary>
        public bool movementAllowedVertical = true;
        /// <summary>
        /// Determines if horizontal camera movement is restricted.
        /// </summary>
        public bool movementAllowedHorizontal = true;

        /// <summary>
        /// Creates a Camera with the given properties.
        /// </summary>
        /// <param name="_scene">The Scene this Cameras view is being drawn to.</param>
        /// <param name="startingCoordinate">Describes the point the Camera begins at. By default this is the top right position of the Camera.</param>
        /// <param name="centerStartingCoordinate">If true, centers the Camera on the starting coordinate.</param>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public Camera(Scene _scene, Point startingCoordinate, bool centerStartingCoordinate, bool allowCentering)
        {
            this._scene = _scene;
            cameraPosition.Width = Global._graphics.PreferredBackBufferWidth;
            cameraPosition.Height = Global._graphics.PreferredBackBufferHeight;
            if (centerStartingCoordinate)
            {
                startingCoordinate = new Point(startingCoordinate.X - ((int)cameraPosition.Width / 2), startingCoordinate.Y + ((int)cameraPosition.Height / 2));
            }
            cameraPosition.X = startingCoordinate.X;
            cameraPosition.Y = startingCoordinate.Y;
            Reposition();
            SetBoundingBox(allowCentering);
        }
        /// <summary>
        /// Creates a Camera with the given properties.
        /// </summary>
        /// <param name="_scene">The Scene this Cameras view is being drawn to.</param>
        /// <param name="startingCoordinate">Describes the point the Camera begins at. By default this is the top right position of the Camera.</param>
        /// <param name="centerStartingCoordinate">If true, centers the Camera on the starting coordinate.</param>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        /// <param name="stretch">Stretch value this Camera will begin with.</param>
        public Camera(Scene _scene, Point startingCoordinate, bool centerStartingCoordinate, bool allowCentering, Vector2 stretch) : this(_scene, startingCoordinate, centerStartingCoordinate, allowCentering)
        {
            Stretch(stretch, allowCentering);
        }
        /// <summary>
        /// Sets the Camera stretch to the provided amount.
        /// </summary>
        /// <param name="newStretch">The stretch the Camera is being set to.</param>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void Stretch(Vector2 newStretch, bool allowCentering)
        {
            if (newStretch.X >= minStretch.X && newStretch.X <= maxStretch.X)
            {
                cameraPosition.X += ((int)((cameraCenter.X * newStretch.X) / Global._baseStretch.X) - cameraCenter.X);
                Global._baseStretch.X = newStretch.X;
            }
            if (newStretch.Y >= minStretch.Y && newStretch.Y <= maxStretch.Y)
            {
                cameraPosition.Y += ((int)((cameraCenter.Y * newStretch.Y) / Global._baseStretch.Y) - cameraCenter.Y);
                Global._baseStretch.Y = newStretch.Y;
            }
            Reposition();
            SetBoundingBox(allowCentering);
        }
        /// <summary>
        /// Repositions cameraCenter to be consistant with cameraPosition.
        /// </summary>
        public void Reposition()
        {
            cameraCenter = cameraPosition.Center;
        }
        /// <summary>
        /// Sets this Cameras boundingBox to conform to the boundingBox of the Cameras Scenes TileMap.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void SetBoundingBox(bool allowCentering)
        {
            Point mapCenter = _scene._tileMap.GetTileMapCenter();
            Rectangle mapBounding = _scene._tileMap.GetTileMapBounding();
            if (mapBounding.Width <= (cameraPosition.Width / Global._baseStretch.X) && allowCentering)
            {
                movementAllowedHorizontal = false;
                cameraPosition.X = mapCenter.X - (int)(cameraPosition.Width / 2);
            }
            else
            {
                movementAllowedHorizontal = true;
            }
            boundingBox.X = mapBounding.X;
            boundingBox.Width = mapBounding.Width;

            if (mapBounding.Height <= (cameraPosition.Height / Global._baseStretch.Y) && allowCentering)
            {
                movementAllowedVertical = false;
                cameraPosition.Y = mapCenter.Y + (int)(cameraPosition.Height / 2);
            }
            else
            {
                movementAllowedVertical = true;
            }
            boundingBox.Y = mapBounding.Y;
            boundingBox.Height = mapBounding.Height;

            Reposition();
        }
        /// <summary>
        /// Determines if a point is inside of the camera boundingBox.
        /// </summary>
        /// <param name="point">The point to be assessed</param>
        /// <returns>True if the point is inside or on the boundingBox, False if it not.</returns>
        public bool PointInBoundingBox(Point point)
        {
            if (Util.PointInsideRectangle(point, boundingBox))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        /// <summary>
        /// Creates the transfromation matrix used to apply camera effects when drawing.
        /// </summary>
        /// <returns>Matrix used to apply camera effects (Camera movement, Camera rotation) when drawing in Scene.</returns>
        public Matrix GetTransformation()
        {
            Matrix _transform =
                Matrix.CreateTranslation(new Vector3(-cameraPosition.X, cameraPosition.Y, 0)) *
                Matrix.CreateRotationZ(rotation);
            return _transform;
        }
        /// <summary>
        /// Pans the camera to a point with the provided speed. 
        /// Follows camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// TODO add stretching on camera pan speed.
        /// <param name="destination">Point for the camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void Pan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * Global._baseStretch.X);
            destination.Y = (int)(destination.Y * Global._baseStretch.Y);

            if (centerDestination)
            {
                destination.X -= (int)(cameraPosition.Width / 2);
                destination.Y += (int)(cameraPosition.Height / 2);
            }

            if (PointInBoundingBox(destination))
            {
                while (cameraPosition.X != destination.X || cameraPosition.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        SetHorizontal(destination.X, false, false);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        MoveHorizontal(true, speed, false);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        MoveHorizontal(false, speed, false);
                    }

                    if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                    {
                        SetVertical(destination.Y, false, false);
                    }
                    else if (cameraPosition.Y < destination.Y)
                    {
                        MoveVertical(true, speed, false);
                    }
                    else if (cameraPosition.Y > destination.Y)
                    {
                        MoveVertical(false, speed, false);
                    }
                    Reposition();
                    _scene.ClearAndRedraw();
                }
            }
        }
        /// <summary>
        /// Pans the Camera to a point with the provided speed. 
        /// Overrides Camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// TODO add stretching on camera pan speed.
        /// <param name="destination">Point for the Camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the Camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void ForcePan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * Global._baseStretch.X);
            destination.Y = (int)(destination.Y * Global._baseStretch.Y);

            if (centerDestination)
            {
                destination.X -= (int)(cameraPosition.Width / 2);
                destination.Y += (int)(cameraPosition.Height / 2);
            }

            while (cameraPosition.X != destination.X || cameraPosition.Y != destination.Y)
            {
                if (Math.Abs(destination.X - cameraPosition.X) <= speed)
                {
                    ForceSetHorizontal(destination.X, false, false);
                }
                else if (cameraPosition.X < destination.X)
                {
                    ForceMoveHorizontal(true, speed, false);
                }
                else if (cameraPosition.X > destination.X)
                {
                    ForceMoveHorizontal(false, speed, false);
                }

                if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                {
                    ForceSetVertical(destination.Y, false, false);
                }
                else if (cameraPosition.Y < destination.Y)
                {
                    ForceMoveVertical(true, speed, false);
                }
                else if (cameraPosition.Y > destination.Y)
                {
                    ForceMoveVertical(false, speed, false);
                }
                Reposition();
                _scene.ClearAndRedraw();
            }
        }
        /// <summary>
        /// Pans the Camera to a point with the provided speed by first stretching textures until the point is in the cameres views (or until reaching Camera max stretch)
        /// then returning back to the starting stretching after panning. Provides a zoom out --> Pan to Point --> zoom back in effect.
        /// Follows Camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// TODO add stretching on camera pan speed.
        /// <param name="destination">Point for the Camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the Camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void PanWithStretch(Point destination, int speed, bool centerDestination)
        {
            if (movementAllowedVertical && movementAllowedHorizontal)
            {
                if (PointInBoundingBox(destination))
                {
                    Vector2 original = Global._baseStretch;

                    while (!Util.PointInsideRectangle(destination, cameraPosition))
                    {
                        if ((Global._baseStretch.X - .01f <= minStretch.X + .0f || Global._baseStretch.X <= original.X - 1f) || (Global._baseStretch.Y - .01f <= minStretch.Y + .0f || Global._baseStretch.Y <= original.Y - 1f))
                        {
                            break;
                        }
                        Stretch(new Vector2(Global._baseStretch.X - .01f, Global._baseStretch.Y - .01f), false);
                        _scene.ClearAndRedraw();
                    }

                    Pan(destination, speed, centerDestination);

                    while (original.X != Global._baseStretch.X)
                    {
                        Stretch(new Vector2(Global._baseStretch.X + .01f, Global._baseStretch.Y + .01f), false);
                        Pan(destination, speed, centerDestination);
                    }

                    Stretch(original, true);
                    Pan(destination, speed, centerDestination);
                }
            }

        }
        /// <summary>
        /// Pans the Camera to a point with the provided speed by first stretching textures until the point is in the cameres views (or until reaching Camera max stretch)
        /// then returning back to the starting stretching after panning. Provides a zoom out --> Pan to Point --> zoom back in effect.
        /// Overrides Camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// TODO add stretching on camera pan speed.
        /// <param name="destination">Point for the Camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the Camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void ForcePanWithStretch(Point destination, int speed, bool centerDestination)
        {
            Vector2 original = Global._baseStretch;

            while (!Util.PointInsideRectangle(destination, cameraPosition))
            {
                if ((Global._baseStretch.X - .01f <= minStretch.X + .0f || Global._baseStretch.X <= original.X - 1f) || (Global._baseStretch.Y - .01f <= minStretch.Y + .0f || Global._baseStretch.Y <= original.Y - 1f))
                {
                    break;
                }
                Stretch(new Vector2(Global._baseStretch.X - .01f, Global._baseStretch.Y - .01f), false);
                _scene.ClearAndRedraw();
            }

            ForcePan(destination, speed, centerDestination);

            while (original.X != Global._baseStretch.X)
            {
                Stretch(new Vector2(Global._baseStretch.X + .01f, Global._baseStretch.Y + .01f), false);
                ForcePan(destination, speed, centerDestination);
            }

            Stretch(original, false);
            ForcePan(destination, speed, centerDestination);
        }
        /// <summary>
        /// Moves the Camera vertically by the provided amount.
        /// Follows Camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the Camera moving up, False results in the Camera moving down.</param>
        /// <param name="amount">The amount the Camera will move in the provided direction.</param>
        /// <param name="stretchAmount">True results in the amount the Camera is moved being multiplied by the Camera stretching, False leaves the amount as is.</param>
        public void MoveVertical(bool direction, int amount, bool stretchAmount)
        {
            if (movementAllowedVertical)
            {
                if (stretchAmount)
                {
                    amount = (int)(amount * Global._baseStretch.Y);
                }

                if (direction)
                {
                    //moves up
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y + amount)))
                    {
                        cameraPosition.Y += amount;
                    }
                    else
                    {
                        cameraPosition.Y = boundingBox.Y + (int)(cameraPosition.Height / 2);
                    }
                }
                else
                {
                    //moves down
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y - amount)))
                    {
                        cameraPosition.Y -= amount;
                    }
                    else
                    {
                        cameraPosition.Y = (boundingBox.Y - boundingBox.Height) + (int)(cameraPosition.Height / 2);
                    }
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the Camera vetical coordinate to the provided Y.
        /// Follows Camera movement constrictions.
        /// </summary>
        /// <param name="Y">New Y coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the Camera set the Y as the center.</param>
        /// <param name="stretchY">True results in the Y being multiplied by the Camera stretching. False leaves the Y as is.</param>
        public void SetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * Global._baseStretch.Y);
            }

            if (centerDestination)
            {
                Y += (int)(cameraPosition.Height / 2);
            }

            if (movementAllowedVertical)
            {
                if (PointInBoundingBox(new Point(cameraCenter.X, Y - (int)(cameraPosition.Height / 2))))
                {
                    cameraPosition.Y = Y;
                }
                else if (Y >= boundingBox.Y)
                {
                    cameraPosition.Y = boundingBox.Y + (int)(cameraPosition.Height / 2);
                }
                else
                {
                    cameraPosition.Y = (boundingBox.Y - boundingBox.Height) + (int)(cameraPosition.Height / 2);
                }
                Reposition();
            }
        }
        /// <summary>
        /// Moves the camera horizontally by the provided amount.
        /// Follows camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the Camera moving right, False results in the Camera moving left.</param>
        /// <param name="amount">The amount the camera will move in the provided direction.</param>
        /// <param name="stretchAmount">True results in the amount the camera is moved being multiplied by the camera stretching, False leaves the amount as is.</param>
        public void MoveHorizontal(bool direction, int amount, bool stretchAmount)
        {
            if (movementAllowedHorizontal)
            {
                if (stretchAmount)
                {
                    amount = (int)(amount * Global._baseStretch.X);
                }

                if (direction)
                {
                    //moves right
                    if (PointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                    {
                        cameraPosition.X += amount;
                    }
                    else
                    {
                        cameraPosition.X = (boundingBox.X + boundingBox.Width) - (int)(cameraPosition.Width / 2);
                    }
                }
                else
                {
                    //moves left
                    if (PointInBoundingBox(new Point(cameraCenter.X - amount, cameraCenter.Y)))
                    {
                        cameraPosition.X -= amount;
                    }
                    else
                    {
                        cameraPosition.X = boundingBox.X - (int)(cameraPosition.Width / 2);
                    }
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera horizontal coordinate to the provided X. 
        /// Follows camera movement constrictions.
        /// </summary>
        /// <param name="X">New X coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the Camera set the X as the center.</param>
        /// <param name="stretchX">True results in the X being multiplied by the Camera stretching. False leaves the X as is.</param>
        public void SetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * Global._baseStretch.X);
            }

            if (centerDestination)
            {
                X -= (int)(cameraPosition.Width / 2);
            }

            if (movementAllowedHorizontal)
            {
                if (PointInBoundingBox(new Point(X + (int)(cameraPosition.Width / 2), cameraCenter.Y)))
                {
                    cameraPosition.X = X;
                }
                else if (X <= boundingBox.X)
                {
                    cameraPosition.X = boundingBox.X - (int)(cameraPosition.Width / 2);
                }
                else
                {
                    cameraPosition.X = (boundingBox.X + boundingBox.Width) - (int)(cameraPosition.Width / 2);
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera center to the cameraCenter. 
        /// Follows camerea movement constrictions.
        /// </summary>
        /// <param name="coordinate">New coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="stretchCoordinate">True results in the coordinate being multiplied by the camera stretching, False leaves the amount as is.</param>
        /// <param name="centerDestination">If true, the coordinate is set as the Camera center.</param>
        public void SetCoordinate(Point coordinate, bool centerDestination, bool stretchCoordinate)
        {
            SetHorizontal(coordinate.X, centerDestination, stretchCoordinate);
            SetVertical(coordinate.Y, centerDestination, stretchCoordinate);
        }
        /// <summary>
        /// Moves the camera vertically by the provided amount.
        /// Overrides camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the camera moving up, False results in the camera moving down.</param>
        /// <param name="amount">The amount the camera will move in the provided direction.</param>
        /// <param name="stretchAmount">True results in the amount the camera is moved being multiplied by the camera stretching, False leaves the amount as is.</param>
        public void ForceMoveVertical(bool direction, int amount, bool stretchAmount)
        {
            if (stretchAmount)
            {
                amount = (int)(amount * Global._baseStretch.Y);
            }

            if (direction)
            {
                //moves up
                cameraPosition.Y += amount;
            }
            else
            {
                //moves down
                cameraPosition.Y -= amount;
            }
            Reposition();
        }
        /// <summary>
        /// Sets the camera vetical coordinate to the provided Y. 
        /// Overrides camera movement constrictions.
        /// </summary>
        /// <param name="Y">New Y coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the Camera set the Y as the center.</param>
        /// <param name="stretchY">True results in the Y being multiplied by the Camera stretching. False leaves the Y as is.</param>
        public void ForceSetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * Global._baseStretch.Y);
            }

            if (centerDestination)
            {
                Y += (int)(cameraPosition.Height / 2);
            }

            cameraPosition.Y = Y;
            Reposition();
        }
        /// <summary>
        /// Moves the camera horizontally by the provided amount. 
        /// Overrides camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the Camera moving right, False results in the Camera moving left.</param>
        /// <param name="amount">The amount the camera will move in the provided direction.</param>
        /// <param name="stretchAmount">True results in the amount the camera is moved being multiplied by the camera stretching, False leaves the amount as is.</param>
        public void ForceMoveHorizontal(bool direction, int amount, bool stretchAmount)
        {
            if (stretchAmount)
            {
                amount = (int)(amount * Global._baseStretch.X);
            }

            if (direction)
            {
                //moves right
                cameraPosition.X += amount;
            }
            else
            {
                //moves left
                cameraPosition.X -= amount;
            }
            Reposition();
        }
        /// <summary>
        /// Sets the camera horizontal coordinate to the provided X. 
        /// Overrides camera movement constrictions.
        /// </summary>
        /// <param name="X">New X coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the Camera set the X as the center.</param>
        /// <param name="stretchX">True results in the X being multiplied by the Camera stretching. False leaves the X as is.</param>
        public void ForceSetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * Global._baseStretch.X);
            }

            if (centerDestination)
            {
                X -= (int)(cameraPosition.Width / 2);
            }

            cameraPosition.X = X;
            Reposition();
        }
        /// <summary>
        /// Sets the camera center to the cameraCenter. 
        /// Overrides camerea movement constrictions.
        /// </summary>
        /// <param name="coordinate">New coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="stretchCoordinate">True results in the coordinate being multiplied by the camera stretching, False leaves the amount as is.</param>
        /// <param name="centerDestination">If true, the coordinate is set as the Camera center.</param> 
        public void ForceSetCoordinate(Point coordinate, bool centerDestination, bool stretchCoordinate)
        {
            ForceSetHorizontal(coordinate.X, centerDestination, stretchCoordinate);
            ForceSetVertical(coordinate.Y, centerDestination, stretchCoordinate);
        }
    }
}