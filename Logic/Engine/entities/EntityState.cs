namespace Fantasy.Logic.Engine.entities
{
    /// <summary>
    /// Defines different states a Entity can be in for moving.
    /// </summary>
    public enum EntityMovementState
    {
        idle,
        movingUp,
        movingDown,
        movingRight,
        movingRightUp,
        movingRightDown,
        movingLeft,
        movingLeftUp,
        movingLeftDown
    }
}
