using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.screen;
using Fantasy.Content.Logic.utility;

namespace Fantasy.Content.Logic.graphics
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
        /// The center pixel of whats on screen.
        /// </summary>
        public Point cameraCenter;
        /// <summary>
        /// The rectangle that describes what is in the cameras view.
        /// </summary>
        public Rectangle cameraPosition;
        /// <summary>
        /// The bounding area that the cameras center which can restricts the cameras movement.
        /// </summary>
        public Rectangle boundingBox;
        /// <summary>
        /// The stretch level of the camera. Higher values result in a closer zoom. Is used as a stretch on tiles when drawing.
        /// </summary>
        public Vector2 _stretch = new Vector2(1f, 1f);
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
        /// Not implemented fully.
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
            cameraPosition.Width = _scene._graphics.PreferredBackBufferWidth;
            cameraPosition.Height = _scene._graphics.PreferredBackBufferHeight;
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
                cameraPosition.X += ((int)((cameraCenter.X * newStretch.X) / _stretch.X) - cameraCenter.X);
                _stretch.X = newStretch.X;
            }
            if (newStretch.Y >= minStretch.Y && newStretch.Y <= maxStretch.Y)
            {
                cameraPosition.Y += ((int)((cameraCenter.Y * newStretch.Y) / _stretch.Y) - cameraCenter.Y);
                _stretch.Y = newStretch.Y;
            }
            Reposition();
            SetBoundingBox(allowCentering);
        }
        /// <summary>
        /// Repositions cameraCenter to be consistant with cameraPosition.
        /// </summary>
        public void Reposition()
        {
            cameraCenter = util.GetRectangleCenter(cameraPosition);
        }
        /// <summary>
        /// Sets this Cameras boundingBox to conform to the boundingBox of the Cameras Scenes TileMap.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void SetBoundingBox(bool allowCentering)
        {
            Point mapCenter = _scene._tileMap.GetTileMapCenter(_stretch);
            Rectangle mapBounding = _scene._tileMap.GetTileMapBounding(_stretch);
            if (mapBounding.Width <= (cameraPosition.Width / _stretch.X) && allowCentering)
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

            if (mapBounding.Height <= (cameraPosition.Height / _stretch.Y) && allowCentering)
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
            if (util.PointInsideRectangle(point, boundingBox))
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
        /// Pans the camera to a point with the provided speed. Follows camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// <param name="destination">Point for the camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void Pan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * _stretch.X);
            destination.Y = (int)(destination.Y * _stretch.Y);

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
        /// Pans the camera to destination with the provided speed. Overrides camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// </summary>
        public void ForcePan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * _stretch.X);
            destination.Y = (int)(destination.Y * _stretch.Y);

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
        /// Pans the camera to destination with the provided speed by first stretching out before panning then stretching back in after panning. Follows camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// </summary>
        public void PanWithStretch(Point destination, int speed, bool centerDestination)
        {
            if (movementAllowedVertical && movementAllowedHorizontal)
            {
                if (PointInBoundingBox(destination))
                {
                    Vector2 original = _stretch;

                    while (!util.PointInsideRectangle(destination, cameraPosition))
                    {
                        if ((_stretch.X - .01f <= minStretch.X + .0f || _stretch.X <= original.X - 1f) || (_stretch.Y - .01f <= minStretch.Y + .0f || _stretch.Y <= original.Y - 1f))
                        {
                            break;
                        }
                        Stretch(new Vector2(_stretch.X - .01f, _stretch.Y - .01f), false);
                        _scene.ClearAndRedraw();
                    }

                    Pan(destination, speed, centerDestination);

                    while (original.X != _stretch.X)
                    {
                        Stretch(new Vector2(_stretch.X + .01f, _stretch.Y + .01f), false);
                        Pan(destination, speed, centerDestination);
                    }

                    Stretch(original, true);
                    Pan(destination, speed, centerDestination);
                }
            }

        }
        /// <summary>
        /// Pans the camera to destination with the provided speed by first stretching out before panning then stretching back in after panning. Overrides camera movement constrictions.
        /// </summary>
        public void ForcePanWithStretch(Point destination, int speed, bool centerDestination)
        {
            Vector2 original = _stretch;

            while (!util.PointInsideRectangle(destination, cameraPosition))
            {
                if ((_stretch.X - .01f <= minStretch.X + .0f || _stretch.X <= original.X - 1f) || (_stretch.Y - .01f <= minStretch.Y + .0f || _stretch.Y <= original.Y - 1f))
                {
                    break;
                }
                Stretch(new Vector2(_stretch.X - .01f, _stretch.Y - .01f), false);
                _scene.ClearAndRedraw();
            }

            ForcePan(destination, speed, centerDestination);

            while (original.X != _stretch.X)
            {
                Stretch(new Vector2(_stretch.X + .01f, _stretch.Y + .01f), false);
                ForcePan(destination, speed, centerDestination);
            }

            Stretch(original, false);
            ForcePan(destination, speed, centerDestination);
        }
        /// <summary>
        /// Moves the camera vertically by the provided amount. Follows camera movement constrictions.
        /// A true value for direction indicates upward movement and a false value indicates a downward movement.
        /// stretchAmount determines if the amount this camera is moved by is stretched by this cameras stretch.
        /// </summary>
        public void MoveVertical(bool direction, int amount, bool stretchAmount)
        {
            if (movementAllowedVertical)
            {
                if (stretchAmount)
                {
                    amount = (int)(amount * _stretch.Y);
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
        /// Sets the camera vetical coordinate to the provided Y. Follows camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// stretchY determines if this cameras stretch value is applied to the destination Y.
        /// </summary>
        public void SetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * _stretch.Y);
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
        /// Moves the camera horizontally by the provided amount. Follows camera movement constrictions.
        /// A true value for direction indicates rightward movement and a false value indicates a leftward movement.
        /// stretchAmount determines if the amount this camera is moved by is stretched by this cameras stretch.
        /// </summary>
        public void MoveHorizontal(bool direction, int amount, bool stretchAmount)
        {
            if (movementAllowedHorizontal)
            {
                if (stretchAmount)
                {
                    amount = (int)(amount * _stretch.X);
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
        /// Sets the camera horizontal coordinate to the provided X. Follows camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// stretchX determines if this cameras stretch value is applied to the destination X.
        /// </summary>
        public void SetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * _stretch.X);
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
        /// Sets the camera center to the cameraCenter. Follows camerea movement constrictions.
        /// stretchCoordinate determines if this cameras stretch value is applied to the destination coordinate.
        /// </summary>
        public void SetCenter(Point coordinate, bool stretchCoordinate)
        {
            SetHorizontal(coordinate.X, true, stretchCoordinate);
            SetVertical(coordinate.Y, true, stretchCoordinate);
        }
        /// <summary>
        /// Moves the camera vertically by the provided amount. Overrides camera movement constrictions.
        /// A true value for direction indicates upward movement and a false value indicates a downward movement.
        /// stretchAmount determines if the amount this camera is moved by is stretched by this cameras stretch.
        /// </summary>
        public void ForceMoveVertical(bool direction, int amount, bool stretchAmount)
        {
            if (stretchAmount)
            {
                amount = (int)(amount * _stretch.Y);
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
        /// Sets the camera vetical coordinate to the provided Y. Overrides camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// stretchY determines if this cameras stretch value is applied to the destination Y.
        /// </summary>
        public void ForceSetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * _stretch.Y);
            }

            if (centerDestination)
            {
                Y += (int)(cameraPosition.Height / 2);
            }

            cameraPosition.Y = Y;
            Reposition();
        }
        /// <summary>
        /// Moves the camera horizontally by the provided amount. Overrides camera movement constrictions.
        /// A true value for direction indicates rightward movement and a false value indicates a leftward movement.
        /// stretchAmount determines if the amount this camera is moved by is stretched by this cameras stretch.
        /// </summary>
        public void ForceMoveHorizontal(bool direction, int amount, bool stretchAmount)
        {
            if (stretchAmount)
            {
                amount = (int)(amount * _stretch.X);
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
        /// Sets the camera horizontal coordinate to the provided X. Overrides camera movement constrictions.
        /// centerDestination determines if the destination will be in the middle of the screen if true, top left if false.
        /// stretchX determines if this cameras stretch value is applied to the destination X.
        /// </summary>
        public void ForceSetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * _stretch.X);
            }

            if (centerDestination)
            {
                X -= (int)(cameraPosition.Width / 2);
            }

            cameraPosition.X = X;
            Reposition();
        }
        /// <summary>
        /// Sets the camera center to the cameraCenter. Overrides camerea movement constrictions.
        /// stretchCoordinate determines if this cameras stretch value is applied to the destination coordinate.
        /// </summary>
        public void ForceSetCenter(Point coordinate, bool stretchCoordinate)
        {
            ForceSetHorizontal(coordinate.X, true, stretchCoordinate);
            ForceSetVertical(coordinate.Y, true, stretchCoordinate);
        }
    }
}