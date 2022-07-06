using System;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.Physics
{
    /// <summary>
    /// Describes the speed something moves.
    /// </summary>
    public class MoveSpeed
    {
        /// <summary>
        /// The minimum number of pixels that can be moved per movement.
        /// </summary>
        private int pixelsPerMove;
        /// <summary>
        /// The minimum number of miliseconds that will result in a movement.
        /// </summary>
        private int milisecondsPerMove;
        /// <summary>
        /// The last time a movement was allowed.
        /// </summary>
        private double lastMovementTime;
        /// <summary>
        /// The number of pixel per milisecond this MoveSpeed describes.
        /// </summary>
        private double pixelsPerMilisecond;

        /// <summary>
        /// Creates a MoveSpeed descriptor with the provided specifications.
        /// </summary>
        /// <param name="pixelsPerUnit">The number of pixels per unit of time this movement describes.</param>
        /// <param name="unit">The unit of time the pixelsPerUnit uses.</param>
        public MoveSpeed(double pixelsPerUnit, TimeUnits unit)
        {
            if (Global._gameTime == null)
            {
                lastMovementTime = 0;
            }
            else
            {
                lastMovementTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
            }

            switch (unit)
            {
                case TimeUnits.miliseconds:
                    pixelsPerMilisecond = pixelsPerUnit;
                    break;
                case TimeUnits.seconds:
                    pixelsPerMilisecond = pixelsPerUnit * .001;
                    break;
                case TimeUnits.ticks:
                    pixelsPerMove = (int)Math.Truncate(pixelsPerUnit);
                    milisecondsPerMove = 0;
                    return;
            }

            if (pixelsPerMilisecond < 1)
            {
                pixelsPerMove = 1;
                milisecondsPerMove = (int)Math.Truncate(1f / (pixelsPerMilisecond));
            }
            else
            {
                pixelsPerMove = (int)Math.Truncate(pixelsPerMilisecond);
                milisecondsPerMove = 1;
            }
        }
        /// <summary>
        /// Determines the amount of pixels to be moved based off the current Gametime.
        /// </summary>
        /// <returns></returns>
        public int MovementAmount()
        {
            if (milisecondsPerMove == 0)
            {
                return pixelsPerMove;
            }

            double deltaMilisecond = Global._gameTime.TotalGameTime.TotalMilliseconds - lastMovementTime;

            if (deltaMilisecond >= 1 && milisecondsPerMove == 1)
            {
                RefreshLastMovementTime();
                return (int)Math.Round(deltaMilisecond * pixelsPerMove);
            }

            if (deltaMilisecond >= milisecondsPerMove)
            {
                RefreshLastMovementTime();
                return (int)Math.Round(pixelsPerMove * (deltaMilisecond / milisecondsPerMove));
            }
            return 0;
        }
        /// <summary>
        /// Refreshes the last internally tracked time instance that movement happened to be the current Gametime.
        /// </summary>
        public void RefreshLastMovementTime()
        {
            lastMovementTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
    /// <summary>
    /// Different TimeUnits used by MoveSpeed objects.
    /// </summary>
    public enum TimeUnits
    {
        miliseconds = 1,
        seconds = 2,
        ticks = 3
    }
}