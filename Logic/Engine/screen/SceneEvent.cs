using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.graphics;
using Fantasy.Logic.Engine.graphics.tilemap;
using Fantasy.Logic.Engine.entities;
using Fantasy.Logic.Engine.graphics.particles;


namespace Fantasy.Logic.Engine.screen
{
    class SceneEvent
    {
        bool transitionScene;
        string newSceneTileMapName;
        Point transitionNewLocation;


        public SceneEvent() { }
    }
}
