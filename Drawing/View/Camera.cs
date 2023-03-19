using Microsoft.Xna.Framework;
using Fantasy.Engine.Physics;
using System;
using Fantasy.Engine.Mapping.Tiling;

namespace Fantasy.Engine.Drawing.View
{
    public static class Camera
    {
        private static bool movementLockedVertical = true;
        private static bool movementLockedHorizontal = true;
        private static byte zoom;
        private static byte maxZoom;
        private static byte minZoom;
        private static float stretch;
        private static float rotation;
        private static BoundingBox2 cameraMovementBoundingBox;
        private static BoundingBox2 cameraViewBoundingBox;
        private static Game1 game;

        /// <summary>
        /// Determines if vertical camera movement is restricted.
        /// </summary>
        public static bool MovementLockedVertical
        {
            get => movementLockedVertical;
            set => movementLockedVertical = value;
        }
        /// <summary>
        /// Determines if horizontal camera movement is restricted.
        /// </summary>
        public static bool MovementLockedHorizontal
        {
            get => movementLockedHorizontal;
            set => movementLockedHorizontal = value;
        }
        /// <summary>
        /// The current zoom level of the camera. Describes the pixel dimensions of a tile.
        /// </summary>
        public static byte Zoom
        {
            get => zoom;
            set => zoom = value;
        }
        /// <summary>
        /// The max zoom of the camera.
        /// </summary>
        public static byte MaxZoom
        {
            get => maxZoom;
            set => maxZoom = value;
        }
        /// <summary>
        /// The minimum zoom of the camera.
        /// </summary>
        public static byte MinZoom
        {
            get => minZoom;
            set => minZoom = value;
        }
        /// <summary>
        /// The stretching applied to the cameras transformation matrix.
        /// </summary>
        private static float Stretch
        {
            get => stretch;
            set => stretch = value;
        }
        /// <summary>
        /// Determines how much the final drawing of the spritebatch is rotated around the origin of the frame.
        /// TODO Not implemented fully.
        /// </summary>
        public static float Rotation
        {
            get => rotation;
            set => rotation = value;
        }
        /// <summary>
        /// The bounding box the camera center cannot leave.
        /// </summary>
        public static BoundingBox2 CameraMovementBoundingBox
        {
            get => cameraMovementBoundingBox;
            set => cameraMovementBoundingBox = value;
        }
        /// <summary>
        /// The bounding box that describes what is inside the camera's view.
        /// </summary>
        public static BoundingBox2 CameraViewBoundingBox
        {
            get => cameraViewBoundingBox;
            set => cameraViewBoundingBox = value;
        }
        /// <summary>
        /// The game this Camera is viewing.
        /// </summary>
        private static Game1 Game
        {
            get => game; 
            set => game = value;
        }

        /// <summary>
        /// Sets the initial values of the camera.
        /// </summary>
        public static void Initialize(Game1 game) {
            MovementLockedVertical = true;
            MovementLockedHorizontal = true;
            Zoom = 64;
            MaxZoom = 192;
            MinZoom = 24;
            Stretch = 1f;
            Rotation = 0f;
            //CameraMovementBoundingBox;
            CameraViewBoundingBox = new BoundingBox2(new Vector2(192, 64), game._Graphics.PreferredBackBufferWidth, game._Graphics.PreferredBackBufferHeight);
            Game = game;
        }
        /// <summary>
        /// Creates the transformation matrix used to apply camera effects when drawing.
        /// </summary>
        /// <returns>Matrix used to apply camera effects (Camera movement, Camera rotation) when drawing in Scene.</returns>
        public static Matrix GetTransformationMatrix()
        {
            Matrix positionMatrix = new()
            {
                M11 = 1f,
                M12 = 0f,
                M13 = 0f,
                M14 = 0f,
                M21 = 0f,
                M22 = 1f,
                M23 = 0f,
                M24 = 0f,
                M31 = 0f,
                M32 = 0f,
                M33 = 1f,
                M34 = 0f,
                M41 = -CameraViewBoundingBox.TopLeft.X,
                M42 = -CameraViewBoundingBox.TopLeft.Y,
                M43 = 0f,
                M44 = 1f
            };

            return positionMatrix * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Stretch);
        }
        /// <summary>
        /// Centers the camera on the provided Vector2.
        /// </summary>
        /// <param name="foo">The Vector2 position for the camera to center on.</param>
        public static void CenterCamera(Vector2 foo)
        {
            if (CameraMovementBoundingBox != null && !CameraMovementBoundingBox.Contains(foo))
            {
                return;
            }

            CameraViewBoundingBox.Center = foo;
        }
        /// <summary>
        /// Zooms the camera in by one stage if the camera is not at max zoom already.
        /// </summary>
        public static void ZoomIn()
        {
            if (Zoom + 1 <= MaxZoom)
            {
                Zoom += 1;
                Stretch = Zoom / Tile.TILE_HEIGHT;
                CameraViewBoundingBox.Width = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferWidth / Stretch));
                CameraViewBoundingBox.Height = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferHeight / Stretch));
            }
        }
        /// <summary>
        /// Zooms the camera out by one stage if the camera is not at min zoom already.
        /// </summary>
        public static void ZoomOut()
        {
            if (Zoom - 1 <= MinZoom)
            { 
                Zoom -= 1;
                Stretch = Zoom / Tile.TILE_HEIGHT;
                CameraViewBoundingBox.Width = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferWidth / Stretch));
                CameraViewBoundingBox.Height = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferHeight / Stretch));
            }
        }
        /// <summary>
        /// Sets the zoom to be the provided amount if it is within the bounds of min and max zoom.
        /// </summary>
        /// <param name="zoom">The zoom for the camera to be set to.</param>
        public static void SetZoom(byte zoom)
        {
            if (zoom <= MinZoom && zoom >= MaxZoom)
            {
                Zoom = zoom;
                Stretch = Zoom / Tile.TILE_HEIGHT;
                CameraViewBoundingBox.Width = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferWidth / Stretch));
                CameraViewBoundingBox.Height = (int)Math.Ceiling((Game._Graphics.PreferredBackBufferHeight / Stretch));
            }
        }

    }
}
