using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.Graphics
{
    class Camera
    {
        public Rectangle cameraPosition;
        public Rectangle boundingBox;
        public bool centerBounding;
        public Point cameraCenter;
        public Vector2 zoom = new Vector2(1f, 1f);
        public Vector2 maxZoom = new Vector2(3f, 3f);
        public Vector2 minZoom = new Vector2(.5f, .5f);
        public Scene _scene;

        public Camera(Scene _scene, Rectangle cameraPosition)
        {
            this._scene = _scene;
            this.cameraPosition = cameraPosition;
            this.cameraCenter.X = (int)((cameraPosition.X - cameraPosition.Width) / 2);
            this.cameraCenter.Y = (int)((cameraPosition.Y - cameraPosition.Height) / 2);
            this.boundingBox = new Rectangle(0, 0, 832, 832);
            this.centerBounding = true;
        }
        public Camera(Scene _scene, Rectangle cameraPosition, Vector2 zoom) : this(_scene, cameraPosition)
        {
            this.zoom = zoom;
        }
        public void panCamera(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

            if (pointInBoundingBox(destination))
            {
                while (cameraCenter.X != destination.X || cameraCenter.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        setHorizontal(destination.X);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        moveHorizontal(false, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        moveHorizontal(true, speed);
                    }

                    if (Math.Abs(destination.Y - cameraCenter.Y) <= speed)
                    {
                        setVertical(destination.Y);
                    }
                    else if (cameraCenter.Y < destination.Y)
                    {
                        moveVertical(true, speed);
                    }
                    else if (cameraCenter.Y > destination.Y)
                    {
                        moveVertical(false, speed);
                    }
                    _scene.clearAndRedraw();
                }
            }
        }
        public void zoomCamera(Vector2 amount)
        {
            if (amount.X >= minZoom.X && amount.X <= maxZoom.X)
            {
                cameraCenter.X = (int)((cameraCenter.X * amount.X) / zoom.X);
                cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2 * zoom.X));
                cameraPosition.Width = (int)((cameraPosition.Width * amount.X) / zoom.X);
                boundingBox.Width = (int)((boundingBox.Width * amount.X) / zoom.X);
                zoom.X = amount.X;
            }
            if (amount.Y >= minZoom.Y && amount.Y <= maxZoom.Y)
            {
                cameraCenter.Y = (int)((cameraCenter.Y * amount.Y) / zoom.Y);
                cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2 * zoom.Y));
                cameraPosition.Height = (int)((cameraPosition.Height * amount.Y) / zoom.Y);
                boundingBox.Height = (int)((boundingBox.Height * amount.Y) / zoom.Y);
                zoom.Y = amount.Y;
            }
        }
        public void panWithZoom(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);
            if (pointInBoundingBox(destination))
            {
                Vector2 original = zoom;
                while (!(destination.X <= cameraPosition.X && destination.X >= cameraPosition.X - cameraPosition.Width) ||
                    !(destination.Y <= cameraPosition.Y && destination.Y >= cameraPosition.Y - cameraPosition.Height))
                {
                    if (zoom.X - .01f <= minZoom.X + .01f || zoom.Y - .01f <= minZoom.Y + .01f)
                    {
                        break;
                    }
                    zoomCamera(new Vector2(zoom.X - .01f, zoom.Y - .01f));
                    reposition();
                    _scene.clearAndRedraw();
                }
                panCamera(destination, speed);
                while (original != zoom)
                {
                    zoomCamera(new Vector2(zoom.X + .01f, zoom.Y + .01f));
                    panCamera(destination, speed);
                }
                zoomCamera(original);
                panCamera(destination, speed);
            }

        }
        public void reposition()
        {
            //sets camera top right position based off the camera center
            cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2 * zoom.X));
            cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2 * zoom.Y));
        }
        public void moveVertical(bool direction, int amount)
        {
            if (direction)
            {
                //moves up
                if (pointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y + amount)))
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
                if (pointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y - amount)))
                {
                    cameraCenter.Y -= amount;
                }
                else
                {
                    cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                }
            }
            reposition();
        }
        public void setVertical(int Y)
        {
            if (pointInBoundingBox(new Point(cameraCenter.X, Y)))
            {
                cameraCenter.Y = Y;
            }
            else if (Y <= boundingBox.Y - boundingBox.Height)
            {
                cameraCenter.Y = boundingBox.Y - boundingBox.Height;
            }
            else
            {
                cameraCenter.Y = boundingBox.Y;
            }
            reposition();
        }
        public void moveHorizontal(bool direction, int amount)
        {
            if (direction)
            {
                //moves right
                if (pointInBoundingBox(new Point(cameraCenter.X - amount, cameraCenter.Y)))
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
                if (pointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                {
                    cameraCenter.X += amount;
                }
                else
                {
                    cameraCenter.X = boundingBox.X;
                }
            }
            reposition();
        }
        public void setHorizontal(int X)
        {
            if (pointInBoundingBox(new Point(X, cameraCenter.Y)))
            {
                cameraCenter.X = X;
            }
            else if (X <= boundingBox.X - boundingBox.Width)
            {
                cameraCenter.X = boundingBox.X - boundingBox.Width;
            }
            else
            {
                cameraCenter.X = boundingBox.X;
            }
            reposition();
        }
        public bool pointInBoundingBox(Point point)
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
    }
}