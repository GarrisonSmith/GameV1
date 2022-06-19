using System;

namespace Fantasy.Logic.Engine.physics
{
    public class MoveSpeed
    {    
        public int pixelsPerMove;

        private int milisecondsPerMove;

        private double pixelsPerMilisecond;

        private double lastMovementTime;

        public MoveSpeed(double pixelsPerUnit, TimeUnits unit)
        {
            lastMovementTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
            switch (unit)
            {
                case TimeUnits.miliseconds:
                    pixelsPerMilisecond = pixelsPerUnit;
                    break;
                case TimeUnits.seconds:
                    pixelsPerMilisecond = pixelsPerUnit * .001;
                    break;
                case TimeUnits.ticks:
                    //TODO: not implemented
                    break;
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

        public int MovementAmount()
        {
            double deltaMilisecond = Global._gameTime.TotalGameTime.TotalMilliseconds - lastMovementTime;

            if (deltaMilisecond >= 1 && milisecondsPerMove == 1)
            {
                return (int)(deltaMilisecond * pixelsPerMove);
            }

            if (deltaMilisecond >= milisecondsPerMove)
            {
                lastMovementTime = Global._gameTime.TotalGameTime.TotalMilliseconds;// + (deltaMilisecond - milisecondsPerMove);
                return pixelsPerMove;
            }

            return 0;
        }

    }

    public enum TimeUnits
    { 
        miliseconds = 1,
        seconds = 2,
        ticks = 3
    }
}