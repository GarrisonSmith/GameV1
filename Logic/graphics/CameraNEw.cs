using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Content.Logic.screen;

namespace Fantasy.Content.Logic.graphics
{
    class CameraNEw
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
        /// The zoom level of the camera. Higher values result in a closer zoom. Is used as a new Vector2(1, 1) on tiles when drawing.
        /// </summary>
        public Vector2 zoom = new Vector2(1f, 1f);
        /// <summary>
        /// The max zoom for this camera.
        /// </summary>
        public Vector2 maxZoom = new Vector2(3f, 3f);

        public float rotation;
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
        public CameraNEw(Scene _scene, Point startingCoordinate, bool forceCentering)
        {
            this._scene = _scene;
            this.cameraCenter = startingCoordinate;
            cameraPosition.Width = _scene._graphics.PreferredBackBufferWidth;
            cameraPosition.Height = _scene._graphics.PreferredBackBufferHeight;
            Reposition();
            SetBoundingBox(forceCentering);
        }
        /// <summary>
        /// Creates a Camera with the given properties. <c>startingCoordinate</c> becomes the cameraCenter.
        /// <c>forceCentering</c> determines that if the boundingBox is smaller than camera view, then the camera is centered on the tileMap and movement is disable without override.
        /// </summary>
        public CameraNEw(Scene _scene, Point startingCoordinate, Vector2 zoom, bool forceCentering) : this(_scene, startingCoordinate, forceCentering)
        {
            Zoom(zoom, true);
        }
        /// <summary>
        /// Sets the camera zoom by the provided <c>amount</c>. If <c>allowCentering</c> is true then the camera will lock on the scene tileMap center if the whole tileMap fits into view. 
        /// </summary>
        public void Zoom(Vector2 amount, bool allowCentering)
        {
            if (amount.X >= minZoom.X && amount.X <= maxZoom.X)
            {
                cameraPosition.X = (int)Math.Round(((cameraCenter.X * amount.X) / zoom.X), 0);
                cameraPosition.Width = (int)Math.Round(((cameraPosition.Width * (amount.X * amount.Y)) / (amount.X * amount.Y)), 0);
                zoom.X = amount.X;
            }
            if (amount.Y >= minZoom.Y && amount.Y <= maxZoom.Y)
            {
                cameraPosition.Y = (int)Math.Round(((cameraCenter.Y * amount.Y) / zoom.Y), 0);
                cameraPosition.Height = (int)Math.Round(((cameraPosition.Height * amount.Y) / zoom.Y), 0);
                zoom.Y = amount.Y;
            }


            SetBoundingBox(false);
        }
        /// <summary>
        /// Repositions <c>cameraPosition</c> to be consistant with <c>cameraCenter</c>.
        /// </summary>
        public void Reposition()
        {
            cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2 * zoom.X));
            cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2 * zoom.Y));
        }
        /// <summary>
        /// Sets boundingBox to conform to the scenes tileMap.
        /// <c>forceCentering</c> determines that if the boundingBox is smaller than camera view, then the camera is centered on the tileMap and movement is disable without override.
        /// </summary>
        public void SetBoundingBox(bool forceCentering)
        {
            Point _point = _scene._tileMap.GetTileMapCenter(zoom);
            Rectangle _rectangle = _scene._tileMap.GetTileMapBounding(zoom);
            if (_rectangle.Width <= (cameraPosition.Width / zoom.X) && forceCentering)
            {
                movementAllowedHorizontal = false;
                cameraCenter.X = _point.X;
            }
            else
            {
                movementAllowedHorizontal = true;
            }
            boundingBox.X = _rectangle.X;
            boundingBox.Width = _rectangle.Width;

            if (_rectangle.Height <= (cameraPosition.Height / zoom.Y) && forceCentering)
            {
                movementAllowedVertical = false;
                cameraCenter.Y = _point.Y;
            }
            else
            {
                movementAllowedVertical = true;
            }
            boundingBox.Y = _rectangle.Y;
            boundingBox.Height = _rectangle.Height;

            Reposition();
        }
        /// <summary>
        /// Determines if <c>point</c> is inside of the camera boundingBox
        /// </summary>
        public bool PointInBoundingBox(Point point)
        {
            if ((boundingBox.X >= point.X && boundingBox.X - boundingBox.Width <= point.X) && (boundingBox.Y >= point.Y && boundingBox.Y - boundingBox.Height <= point.Y))
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
                Matrix.CreateTranslation(new Vector3(cameraPosition.X, cameraPosition.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom.X, zoom.Y, 1));
            System.Diagnostics.Debug.WriteLine(_transform.ToString());
            return _transform;
        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c>. Follows camera movement constrictions.
        /// </summary>
        public void Pan(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

            if (PointInBoundingBox(destination))
            {
                while (cameraCenter.X != destination.X || cameraCenter.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        SetHorizontal(destination.X);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        MoveHorizontal(false, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        MoveHorizontal(true, speed);
                    }

                    if (Math.Abs(destination.Y - cameraCenter.Y) <= speed)
                    {
                        SetVertical(destination.Y);
                    }
                    else if (cameraCenter.Y < destination.Y)
                    {
                        MoveVertical(true, speed);
                    }
                    else if (cameraCenter.Y > destination.Y)
                    {
                        MoveVertical(false, speed);
                    }
                    _scene.DrawScene();
                }
            }
        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForcePan(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

            while (cameraCenter.X != destination.X || cameraCenter.Y != destination.Y)
            {
                if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                {
                    ForceSetHorizontal(destination.X);

                }
                else if (cameraCenter.X < destination.X)
                {
                    ForceMoveHorizontal(false, speed);
                }
                else if (cameraCenter.X > destination.X)
                {
                    ForceMoveHorizontal(true, speed);
                }

                if (Math.Abs(destination.Y - cameraCenter.Y) <= speed)
                {
                    ForceSetVertical(destination.Y);
                }
                else if (cameraCenter.Y < destination.Y)
                {
                    ForceMoveVertical(true, speed);
                }
                else if (cameraCenter.Y > destination.Y)
                {
                    ForceMoveVertical(false, speed);
                }
                _scene.ClearAndRedraw();
                //_scene.DrawScene();
            }
        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c> by first zooming out before panning then zooming back in after panning. Follows camera movement constrictions.
        /// </summary>
        public void PanWithZoom(Point destination, int speed)
        {
            if (movementAllowedVertical && movementAllowedHorizontal)
            {
                destination.X = (int)(destination.X * zoom.X);
                destination.Y = (int)(destination.Y * zoom.Y);
                if (PointInBoundingBox(destination))
                {
                    Vector2 original = zoom;
                    while (!(destination.X <= cameraPosition.X && destination.X >= cameraPosition.X - cameraPosition.Width) ||
                        !(destination.Y <= cameraPosition.Y && destination.Y >= cameraPosition.Y - cameraPosition.Height))
                    {
                        if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                        {
                            break;
                        }
                        Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                        _scene.ClearAndRedraw();
                    }
                    ForcePan(destination, speed);
                    while (original != zoom)
                    {
                        Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                        ForcePan(destination, speed);
                    }
                    Zoom(original, true);
                    ForcePan(destination, speed);
                }
            }

        }
        /// <summary>
        /// Pans the camera to <c>destination</c> with the provided <c>speed</c> by first zooming out before panning then zooming back in after panning. Overrides camera movement constrictions.
        /// </summary>
        public void ForcePanWithZoom(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);
            Vector2 original = zoom;
            while (!(destination.X <= cameraPosition.X && destination.X >= cameraPosition.X - cameraPosition.Width) ||
                !(destination.Y <= cameraPosition.Y && destination.Y >= cameraPosition.Y - cameraPosition.Height))
            {
                if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                {
                    break;
                }
                Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                _scene.ClearAndRedraw();
            }
            ForcePan(destination, speed);
            while (original != zoom)
            {
                Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                ForcePan(destination, speed);
            }
            Zoom(original, false);
            ForcePan(destination, speed);

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
                        cameraCenter.Y += amount;
                    }
                    else
                    {
                        cameraCenter.Y = boundingBox.Y;
                    }
                }
                else
                {
                    //moves down
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y - amount)))
                    {
                        cameraCenter.Y -= amount;
                    }
                    else
                    {
                        cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                    }
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera vetical coordinate to the provided <c>Y</c>. Follows camera movement constrictions.
        /// </summary>
        public void SetVertical(int Y)
        {
            if (movementAllowedVertical)
            {
                if (PointInBoundingBox(new Point(cameraCenter.X, -Y)))
                {
                    cameraCenter.Y = -Y;
                }
                else if (-Y <= boundingBox.Y - boundingBox.Height)
                {
                    cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                }
                else
                {
                    cameraCenter.Y = boundingBox.Y;
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
                    if (PointInBoundingBox(new Point(cameraCenter.X - amount, cameraCenter.Y)))
                    {
                        cameraCenter.X -= amount;
                    }
                    else
                    {
                        cameraCenter.X = boundingBox.X - boundingBox.Width;
                    }
                }
                else
                {
                    //moves left
                    if (PointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                    {
                        cameraCenter.X += amount;
                    }
                    else
                    {
                        cameraCenter.X = boundingBox.X;
                    }
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera horizontal coordinate to the provided <c>X</c>. Follows camera movement constrictions.
        /// </summary>
        public void SetHorizontal(int X)
        {
            if (movementAllowedHorizontal)
            {
                if (PointInBoundingBox(new Point(-X, cameraCenter.Y)))
                {
                    cameraCenter.X = -X;
                }
                else if (-X <= boundingBox.X - boundingBox.Width)
                {
                    cameraCenter.X = boundingBox.X - boundingBox.Width;
                }
                else
                {
                    cameraCenter.X = boundingBox.X;
                }
                Reposition();
            }
        }
        /// <summary>
        /// Sets the camera center to the <c>cameraCenter</c>. Follows camerea movement constrictions.
        /// </summary>
        public void SetCenter(Point cameraCenter)
        {
            SetVertical(cameraCenter.Y);
            SetHorizontal(cameraCenter.X);
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
                cameraCenter.Y += amount;
            }
            else
            {
                //moves down
                cameraCenter.Y -= amount;
            }
            Reposition();
        }
        /// <summary>
        /// Sets the camera vetical coordinate to the provided <c>Y</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForceSetVertical(int Y)
        {
            cameraCenter.Y = -Y;
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
                cameraCenter.X -= amount;
            }
            else
            {
                //moves left
                cameraCenter.X += amount;
            }
            Reposition();
        }
        /// <summary>
        /// Sets the camera horizontal coordinate to the provided <c>X</c>. Overrides camera movement constrictions.
        /// </summary>
        public void ForceSetHorizontal(int X)
        {
            cameraCenter.X = -X;
        }
        /// <summary>
        /// Sets the camera center to the <c>cameraCenter</c>. Overrides camerea movement constrictions.
        /// </summary>
        public void ForceSetCenter(Point cameraCenter)
        {
            ForceSetVertical(cameraCenter.Y);
            ForceSetHorizontal(cameraCenter.X);
        }
    }
}