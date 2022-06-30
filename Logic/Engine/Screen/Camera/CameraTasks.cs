using System;
using System.Collections.Generic;
using System.Text;

namespace Fantasy.Logic.Engine.screen.camera
{
    /// <summary>
    /// Enum describing the different tasks a CameraHandler can be assigned.
    /// </summary>
    public enum CameraTasks
    {
        //
        // Summary:
        //     No specified task.
        none = 0,
        //
        // Summary:
        //     Camera is free to accept movement commands.
        free = 1,
        //
        // Summary:
        //     Camera is free to accept movement commands which will be forced.
        forcedFree = 2,
        //
        // Summary:
        //     Camera is following a entity.
        following = 3,
        //
        // Summary:
        //     Camera is following a entity with forced movement commands.
        forcedFollowing = 4,
        //
        // Summary:
        //     Camera is panning to a destination point.
        panning = 5,
    }
}
