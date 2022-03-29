using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.Graphics
{
    class Camera
    {
        public Rectangle cameraPosition;
        public Point cameraCenter;
        public Vector2 zoom = new Vector2(1f, 1f);
        public Scene _scene;

        public Camera(Scene _scene, Rectangle cameraPosition)
        {
            this._scene = _scene;
            this.cameraPosition = cameraPosition;
            this.cameraCenter.X = (int)((cameraPosition.X - cameraPosition.Width) / 2);
            this.cameraCenter.Y = (int)((cameraPosition.Y - cameraPosition.Height) / 2);
        }
        public Camera(Scene _scene, Rectangle cameraPosition, Vector2 zoom) : this(_scene, cameraPosition)
        {
            this.zoom = zoom;
        }
        public void panCamera(Point destination, int speed)
        {
            while (cameraPosition.X != destination.X || cameraPosition.Y != destination.Y)
            {
                if (Math.Abs(destination.X - cameraPosition.X) < speed)
                {
                    cameraPosition.X = destination.X;
                    recenter();
                }
                else if (cameraPosition.X < destination.X)
                {
                    moveHorizontal(false, speed);
                }
                else if (cameraPosition.X > destination.X)
                {
                    moveHorizontal(true, speed);
                }

                if (Math.Abs(destination.Y - cameraPosition.Y) < speed)
                {
                    cameraPosition.Y = destination.Y;
                    recenter();
                }
                else if (cameraPosition.Y < destination.Y)
                {
                    moveVertical(true, speed);
                }
                else if (cameraPosition.Y > destination.Y) 
                {
                    moveVertical(false, speed);
                }
                _scene.clearAndRedraw();
            }
        }
        public void zoomCamra(Vector2 amount)
        {
            if (zoom.X != amount.X && amount.X >= .1f && amount.X <= 3f)
            {
                cameraCenter.X = (int)((cameraCenter.X * amount.X) / zoom.X);
                cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2 * zoom.X));
                cameraPosition.Width = (int)((cameraPosition.Width * amount.X) / zoom.X);
                zoom.X = amount.X;
            }
            if (zoom.Y != amount.Y && amount.Y >= .1f && amount.Y <= 3f)
            {
                cameraCenter.Y = (int)((cameraCenter.Y * amount.Y) / zoom.Y);
                cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2 * zoom.Y));
                cameraPosition.Height = (int)((cameraPosition.Height * amount.Y) / zoom.Y);
                zoom.Y = amount.Y;
            }
        }
        public void reposition()
        {
            cameraCenter.X = (cameraPosition.X - (int)(cameraPosition.Width / (2*zoom.X)));
            cameraCenter.Y = (cameraPosition.Y - (int)(cameraPosition.Height / (2*zoom.Y)));
        }
        public void recenter()
        {
            cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2*zoom.X));
            cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2*zoom.Y));
        }
        public void moveVertical(bool direction, int amount)
        {
            if (direction)
            {
                cameraPosition.Y += amount;
                cameraCenter.Y += amount;
            }
            else
            {
                cameraPosition.Y -= amount;
                cameraCenter.Y -= amount;
            }
        }
        public void moveHorizontal(bool direction, int amount) 
        {
            if (direction)
            {
                cameraPosition.X -= amount;
                cameraCenter.X -= amount;
            }
            else
            {
                cameraPosition.X += amount;
                cameraCenter.X += amount;
            }
        }
    }
}
