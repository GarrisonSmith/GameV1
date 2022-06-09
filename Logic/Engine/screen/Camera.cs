using System;
using Microsoft.Xna.Framework;
using Fantasy.Logic.Engine.utility;
using Fantasy.Logic.Controls;

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
        /// The current zoom level for this camera. Describes the pixel dimensions of a tile.
        /// </summary>
        public byte zoom = 64;
        /// <summary>
        /// The max zoom for this camera.
        /// </summary>
        public byte maxZoom = 192;
        /// <summary>
        /// The minimum zoom for this camera.
        /// </summary>
        public byte minZoom = 24;
        /// <summary>
        /// The stretching applied to this cameras transformation matrix.
        /// </summary>
        public float stretch = 1f;
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
        public Camera(Scene _scene, Point startingCoordinate, bool centerStartingCoordinate, bool allowCentering, float stretch) : this(_scene, startingCoordinate, centerStartingCoordinate, allowCentering)
        {
            Stretch(stretch, allowCentering);
        }
        
        public void DoAction(Actions action)
        {
            switch (action)
            {
                case Actions.up:
                    MoveVertical(true, 10);
                    break;
                case Actions.down:
                    MoveVertical(false, 10);
                    break;
                case Actions.left:
                    MoveHorizontal(false, 10);
                    break;
                case Actions.right:
                    MoveHorizontal(true, 10);
                    break;
                case Actions.zoomIn:
                    ZoomIn(true);
                    break;
                case Actions.zoomOut:
                    ZoomOut(true);
                    break;
            }
        }
        /// <summary>
        /// Sets this cameras zoom level to the default of 64.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void ZoomDefault(bool allowCentering)
        {
            Stretch(1f, allowCentering);
            zoom = 64;
        }
        /// <summary>
        /// Increases this cameras zoom level by one.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void ZoomIn(bool allowCentering)
        {
            if (zoom + 1 <= maxZoom)
            {
                Stretch(((zoom + 1f) / 64f), allowCentering);
                zoom = (byte)(zoom + 1);
            }
        }
        /// <summary>
        /// Decreases this cameras zoom level by one.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void ZoomOut(bool allowCentering)
        {
            if (zoom - 1 >= minZoom)
            {
                Stretch(((zoom - 1f) / 64f), allowCentering);
                zoom = (byte)(zoom - 1);
            }
        }
        /// <summary>
        /// Sets the Camera stretch to the provided amount.
        /// </summary>
        /// <param name="newStretch">The stretch the Camera is being set to.</param>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        private void Stretch(float newStretch, bool allowCentering)
        {
            cameraPosition.Width = (int)Math.Ceiling((Global._graphics.PreferredBackBufferWidth / newStretch));
            cameraPosition.Height = (int)Math.Ceiling((Global._graphics.PreferredBackBufferHeight / newStretch));

            cameraPosition.X = cameraCenter.X - cameraPosition.Width / 2;
            cameraPosition.Y = cameraCenter.Y + cameraPosition.Height / 2;

            stretch = newStretch;
            Global._currentStretch = newStretch;

            SetBoundingBox(allowCentering);
        }
        /// <summary>
        /// Repositions cameraCenter to be consistant with cameraPosition.
        /// </summary>
        public void Reposition()
        {
            cameraCenter.X = cameraPosition.X + (cameraPosition.Width / 2);
            cameraCenter.Y = cameraPosition.Y - (cameraPosition.Height / 2);
        }
        /// <summary>
        /// Sets this Cameras boundingBox to conform to the boundingBox of the Cameras Scenes TileMap.
        /// </summary>
        /// <param name="allowCentering">If true, allows Camera movement to be restricted with the Camera being centered on the TileMap if the TileMap boundingBox is smaller than Camera view.</param>
        public void SetBoundingBox(bool allowCentering)
        {
            Point mapCenter = _scene._tileMap.GetTileMapCenter();
            Rectangle mapBounding = _scene._tileMap.GetTileMapBounding();
            if (mapBounding.Width <= cameraPosition.Width && allowCentering)
            {
                movementAllowedHorizontal = false;
                cameraPosition.X = mapCenter.X - (cameraPosition.Width / 2);
            }
            else
            {
                movementAllowedHorizontal = true;
            }
            boundingBox.X = mapBounding.X;
            boundingBox.Width = mapBounding.Width;

            if (mapBounding.Height <= cameraPosition.Height && allowCentering)
            {
                movementAllowedVertical = false;
                cameraPosition.Y = mapCenter.Y + (cameraPosition.Height / 2);
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
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(stretch);
            return _transform;
        }
        /// <summary>
        /// Pans the camera to a point with the provided speed. 
        /// Follows camera movement constrictions.
        /// Causes Scene clears and redraws.
        /// </summary>
        /// <param name="destination">Point for the camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void Pan(Point destination, int speed, bool centerDestination)
        {
            if (centerDestination)
            {
                destination.X -= (cameraPosition.Width / 2);
                destination.Y += (cameraPosition.Height / 2);
            }

            if (PointInBoundingBox(destination))
            {
                while (cameraPosition.X != destination.X || cameraPosition.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        SetHorizontal(destination.X, false);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        MoveHorizontal(true, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        MoveHorizontal(false, speed);
                    }

                    if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                    {
                        SetVertical(destination.Y, false);
                    }
                    else if (cameraPosition.Y < destination.Y)
                    {
                        MoveVertical(true, speed);
                    }
                    else if (cameraPosition.Y > destination.Y)
                    {
                        MoveVertical(false, speed);
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
        /// <param name="destination">Point for the Camera to pan to.  By default this is the top right position of the Camera.</param>
        /// <param name="speed">Speed the Camera moves by when panning.</param>
        /// <param name="centerDestination">If true, the Camera pans to the destination as the center.</param>
        public void ForcePan(Point destination, int speed, bool centerDestination)
        {
            if (centerDestination)
            {
                destination.X -= (cameraPosition.Width / 2);
                destination.Y += (cameraPosition.Height / 2);
            }

            while (cameraPosition.X != destination.X || cameraPosition.Y != destination.Y)
            {
                if (Math.Abs(destination.X - cameraPosition.X) <= speed)
                {
                    ForceSetHorizontal(destination.X, false);
                }
                else if (cameraPosition.X < destination.X)
                {
                    ForceMoveHorizontal(true, speed);
                }
                else if (cameraPosition.X > destination.X)
                {
                    ForceMoveHorizontal(false, speed);
                }

                if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                {
                    ForceSetVertical(destination.Y, false);
                }
                else if (cameraPosition.Y < destination.Y)
                {
                    ForceMoveVertical(true, speed);
                }
                else if (cameraPosition.Y > destination.Y)
                {
                    ForceMoveVertical(false, speed);
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
                    byte original = zoom;

                    while (!Util.PointInsideRectangle(destination, cameraPosition))
                    {
                        if (zoom == maxZoom)
                        {
                            break;
                        }
                        ZoomOut(false);
                        _scene.ClearAndRedraw();
                    }

                    ForcePan(destination, speed, centerDestination);

                    while (original != zoom)
                    {
                        ZoomIn(false);
                        _scene.ClearAndRedraw();
                        ForcePan(destination, speed, centerDestination);
                    }

                    ForcePan(destination, speed, centerDestination);
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
            byte original = zoom;

            while (!Util.PointInsideRectangle(destination, cameraPosition))
            {
                if (zoom == maxZoom)
                {
                    break;
                }
                ZoomOut(false);
                _scene.ClearAndRedraw();
            }

            ForcePan(destination, speed, centerDestination);

            while (original != zoom)
            {
                ZoomIn(false);
                _scene.ClearAndRedraw();
                ForcePan(destination, speed, centerDestination);
            }

            ForcePan(destination, speed, centerDestination);
        }
        /// <summary>
        /// Moves the Camera vertically by the provided amount.
        /// Follows Camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the Camera moving up, False results in the Camera moving down.</param>
        /// <param name="amount">The amount the Camera will move in the provided direction.</param>
        public void MoveVertical(bool direction, int amount)
        {
            if (movementAllowedVertical)
            {
                if (direction)
                {
                    //moves up
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y + amount)))
                    {
                        cameraPosition.Y += amount;
                    }
                    else
                    {
                        cameraPosition.Y = boundingBox.Y + (cameraPosition.Height / 2);
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
                        cameraPosition.Y = boundingBox.Y - boundingBox.Height + (cameraPosition.Height / 2);
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
        public void SetVertical(int Y, bool centerDestination)
        {
            if (centerDestination)
            {
                Y += cameraPosition.Height / 2;
            }

            if (movementAllowedVertical)
            {
                if (PointInBoundingBox(new Point(cameraCenter.X, Y - (cameraPosition.Height / 2))))
                {
                    cameraPosition.Y = Y;
                }
                else if (Y >= boundingBox.Y)
                {
                    cameraPosition.Y = boundingBox.Y + (cameraPosition.Height / 2);
                }
                else
                {
                    cameraPosition.Y = boundingBox.Y - boundingBox.Height + (cameraPosition.Height / 2);
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
        public void MoveHorizontal(bool direction, int amount)
        {
            if (movementAllowedHorizontal)
            {
                if (direction)
                {
                    //moves right
                    if (PointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                    {
                        cameraPosition.X += amount;
                    }
                    else
                    {
                        cameraPosition.X = boundingBox.X + boundingBox.Width - (cameraPosition.Width / 2);
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
                        cameraPosition.X = boundingBox.X - (cameraPosition.Width / 2);
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
        public void SetHorizontal(int X, bool centerDestination)
        {
            if (centerDestination)
            {
                X -= cameraPosition.Width;
            }

            if (movementAllowedHorizontal)
            {
                if (PointInBoundingBox(new Point(X + (cameraPosition.Width / 2), cameraCenter.Y)))
                {
                    cameraPosition.X = X;
                }
                else if (X <= boundingBox.X)
                {
                    cameraPosition.X = boundingBox.X - (cameraPosition.Width / 2);
                }
                else
                {
                    cameraPosition.X = (boundingBox.X + boundingBox.Width) - (cameraPosition.Width / 2);
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera center to the cameraCenter. 
        /// Follows camerea movement constrictions.
        /// </summary>
        /// <param name="coordinate">New coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the coordinate is set as the Camera center.</param>
        public void SetCoordinate(Point coordinate, bool centerDestination)
        {
            SetHorizontal(coordinate.X, centerDestination);
            SetVertical(coordinate.Y, centerDestination);
        }
        /// <summary>
        /// Moves the camera vertically by the provided amount.
        /// Overrides camera movement constrictions.
        /// </summary>
        /// <param name="direction">True results in the camera moving up, False results in the camera moving down.</param>
        /// <param name="amount">The amount the camera will move in the provided direction.</param>
        public void ForceMoveVertical(bool direction, int amount)
        {
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
        public void ForceSetVertical(int Y, bool centerDestination)
        {
            if (centerDestination)
            {
                Y += cameraPosition.Height / 2;
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
        public void ForceMoveHorizontal(bool direction, int amount)
        {
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
        public void ForceSetHorizontal(int X, bool centerDestination)
        {
            if (centerDestination)
            {
                X -= cameraPosition.Width / 2;
            }

            cameraPosition.X = X;
            Reposition();
        }
        /// <summary>
        /// Sets the camera center to the cameraCenter. 
        /// Overrides camerea movement constrictions.
        /// </summary>
        /// <param name="coordinate">New coordinate for this Camera. By default this is the top right position of the Camera.</param>
        /// <param name="centerDestination">If true, the coordinate is set as the Camera center.</param> 
        public void ForceSetCoordinate(Point coordinate, bool centerDestination)
        {
            ForceSetHorizontal(coordinate.X, centerDestination);
            ForceSetVertical(coordinate.Y, centerDestination);
        }
    }
}