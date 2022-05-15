using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.screen;
using Fantasy.Content.Logic.utility;

namespace Fantasy.Content.Logic.graphics
{
    class Camera
    {
        /// <summary>
        /// The scene for this camera.
        /// </summary>
        public Scene _scene;
        /// <summary>
        /// The center pixel of the scenes Viewport.
        /// </summary>
        public Point cameraCenter;
        /// <summary>
        /// The rectangle the describes what is in the cameras view.
        /// </summary>
        public Rectangle cameraPosition;
        /// <summary>
        /// The bounding area where the cameras center cannot move outside of.
        /// </summary>
        public Rectangle boundingBox;
        /// <summary>
        /// The zoom level of the camera. Higher values result in a closer zoom. Is used as a stretch on tiles when drawing.
        /// </summary>
        public Vector2 zoom = new Vector2(1f, 1f);
        /// <summary>
        /// The max zoom for this camera.
        /// </summary>
        public Vector2 maxZoom = new Vector2(3f, 3f);

        public float rotation = 0f;
        /// <summary>
        /// The minimum zoom for this camera.
        /// </summary>
        public Vector2 minZoom = new Vector2(.5f, .5f);
        /// <summary>
        /// Determines if camera movement is valid about the vertical axis.
        /// </summary>
        public bool movementAllowedVertical = true;
        /// <summary>
        /// Determines if camera movement is valid about the horizontal axis.
        /// </summary>
        public bool movementAllowedHorizontal = true;

        /// <summary>
        /// Creates a Camera with the given properties. <c>startingCoordinate</c> becomes the cameraCenter.
        /// <c>forceCentering</c> determines that if the boundingBox is smaller than camera view, then the camera is centered on the tileMap and movement is disable without override.
        /// </summary>
        public Camera(Scene _scene, Point startingCoordinate, bool forceCentering)
        {
            this._scene = _scene;
            this.cameraPosition.X = startingCoordinate.X;
            this.cameraPosition.Y = startingCoordinate.Y;
            cameraPosition.Width = _scene._graphics.PreferredBackBufferWidth;
            cameraPosition.Height = _scene._graphics.PreferredBackBufferHeight;
            Reposition();
            SetBoundingBox(forceCentering);
        }
        /// <summary>
        /// Creates a Camera with the given properties. <c>startingCoordinate</c> becomes the cameraCenter.
        /// <c>forceCentering</c> determines that if the boundingBox is smaller than camera view, then the camera is centered on the tileMap and movement is disable without override.
        /// </summary>
        public Camera(Scene _scene, Point startingCoordinate, Vector2 zoom, bool forceCentering) : this(_scene, startingCoordinate, forceCentering)
        {
            Zoom(zoom, forceCentering);
        }
        /// <summary>
        /// Sets the camera zoom by the provided <c>amount</c>. If <c>allowCentering</c> is true then the camera will lock on the scene tileMap center if the whole tileMap fits into view. 
        /// </summary>
        public void Zoom(Vector2 amount, bool allowCentering)
        {
            if (amount.X >= minZoom.X && amount.X <= maxZoom.X)
            {
                cameraPosition.X += ((int)((cameraCenter.X * amount.X) / zoom.X) - cameraCenter.X);
                zoom.X = amount.X;
            }
            if (amount.Y >= minZoom.Y && amount.Y <= maxZoom.Y)
            {
                cameraPosition.Y += ((int)((cameraCenter.Y * amount.Y) / zoom.Y) - cameraCenter.Y);
                zoom.Y = amount.Y;
            }
            Reposition();
            SetBoundingBox(allowCentering);
        }
        /// <summary>
        /// Repositions <c>cameraPosition</c> to be consistant with <c>cameraCenter</c>.
        /// </summary>
        public void Reposition()
        {
            cameraCenter.X = cameraPosition.X + (int)(cameraPosition.Width / 2);
            cameraCenter.Y = cameraPosition.Y - (int)(cameraPosition.Height / 2);
        }
        /// <summary>
        /// Sets boundingBox to conform to the scenes tileMap.
        /// <c>forceCentering</c> determines that if the boundingBox is smaller than camera view, then the camera is centered on the tileMap and movement is disable without override.
        /// </summary>
        public void SetBoundingBox(bool forceCentering)
        {
            Point mapCenter = _scene._tileMap.GetTileMapCenter(zoom);
            Rectangle mapBounding = _scene._tileMap.GetTileMapBounding(zoom);
            if (mapBounding.Width <= (cameraPosition.Width / zoom.X) && forceCentering)
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

            if (mapBounding.Height <= (cameraPosition.Height / zoom.Y) && forceCentering)
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
        /// Determines if <c>point</c> is inside of the camera boundingBox
        /// </summary>
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
        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Matrix _transform =
                Matrix.CreateTranslation(new Vector3(-cameraPosition.X, cameraPosition.Y, 0)) *
                Matrix.CreateRotationZ(rotation);
            return _transform;
        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c>. Follows camera movement constrictions.
        /// </summary>
        public void Pan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

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
                        MoveHorizontal(true, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        MoveHorizontal(false, speed);
                    }

                    if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                    {
                        SetVertical(destination.Y, false, false);
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
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForcePan(Point destination, int speed, bool centerDestination)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

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
                    ForceMoveHorizontal(true, speed);
                }
                else if (cameraPosition.X > destination.X)
                {
                    ForceMoveHorizontal(false, speed);
                }

                if (Math.Abs(destination.Y - cameraPosition.Y) <= speed)
                {
                    ForceSetVertical(destination.Y, false, false);
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
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c> by first zooming out before panning then zooming back in after panning. Follows camera movement constrictions.
        /// </summary>
        public void PanWithZoom(Point destination, int speed, bool centerDestination)
        {
            if (movementAllowedVertical && movementAllowedHorizontal)
            {
                if (PointInBoundingBox(destination))
                {
                    Vector2 original = zoom;

                    while (!util.PointInsideRectangle(destination, cameraPosition))
                    {
                        if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                        {
                            break;
                        }
                        Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                        _scene.ClearAndRedraw();
                    }

                    Pan(destination, speed, centerDestination);

                    while (original.X != zoom.X)
                    {
                        Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                        Pan(destination, speed, centerDestination);
                    }

                    Zoom(original, true);
                    Pan(destination, speed, centerDestination);
                }
            }

        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c> by first zooming out before panning then zooming back in after panning. Overrides camera movement constrictions.
        /// </summary>
        public void ForcePanWithZoom(Point destination, int speed, bool centerDestination)
        {
            Vector2 original = zoom;

            while (!util.PointInsideRectangle(destination, cameraPosition))
            {
                if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                {
                    break;
                }
                Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                _scene.ClearAndRedraw();
            }

            ForcePan(destination, speed, centerDestination);

            while (original.X != zoom.X)
            {
                Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                ForcePan(destination, speed, centerDestination);
            }

            Zoom(original, false);
            ForcePan(destination, speed, centerDestination);
        }
        /// <summary>
        /// Moves the camera vertically by the provided <c>amount</c>. Follows camera movement constrictions.
        /// A true value for <c>direction</c> indicates upward movement and a false value indicates a downward movement.
        /// </summary>
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
        /// Sets the camera vetical coordinate to the provided <c>Y</c>. Follows camera movement constrictions.
        /// </summary>
        public void SetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * zoom.Y);
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
        /// Moves the camera horizontally by the provided <c>amount</c>. Follows camera movement constrictions.
        /// A true value for <c>direction</c> indicates rightward movement and a false value indicates a leftward movement.
        /// </summary>
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
        /// Sets the camera horizontal coordinate to the provided <c>X</c>. Follows camera movement constrictions.
        /// </summary>
        public void SetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * zoom.X);
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
        /// Sets the camera center to the <c>cameraCenter</c>. Follows camerea movement constrictions.
        /// </summary>
        public void SetCenter(Point coordinate, bool stretchCoordinate)
        {
            SetHorizontal(coordinate.X, true, stretchCoordinate);
            SetVertical(coordinate.Y, true, stretchCoordinate);
        }
        /// <summary>
        /// Moves the camera vertically by the provided <c>amount</c>. Overrides camera movement constrictions.
        /// A true value for <c>direction</c> indicates upward movement and a false value indicates a downward movement.
        /// </summary>
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
        /// Sets the camera vetical coordinate to the provided <c>Y</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForceSetVertical(int Y, bool centerDestination, bool stretchY)
        {
            if (stretchY)
            {
                Y = (int)(Y * zoom.Y);
            }

            if (centerDestination)
            {
                Y += (int)(cameraPosition.Height / 2);
            }

            cameraPosition.Y = Y;
            Reposition();
        }
        /// <summary>
        /// Moves the camera horizontally by the provided <c>amount</c>. Overrides camera movement constrictions.
        /// A true value for <c>direction</c> indicates rightward movement and a false value indicates a leftward movement.
        /// </summary>
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
        /// Sets the camera horizontal coordinate to the provided <c>X</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForceSetHorizontal(int X, bool centerDestination, bool stretchX)
        {
            if (stretchX)
            {
                X = (int)(X * zoom.X);
            }

            if (centerDestination)
            {
                X -= (int)(cameraPosition.Width / 2);
            }

            cameraPosition.X = X;
            Reposition();
        }
        /// <summary>
        /// Sets the camera center to the <c>cameraCenter</c>. Overrides camerea movement constrictions.
        /// </summary>
        public void ForceSetCenter(Point coordinate, bool stretchCoordinate)
        {
            ForceSetHorizontal(coordinate.X, true, stretchCoordinate);
            ForceSetVertical(coordinate.Y, true, stretchCoordinate);
        }
    }
}