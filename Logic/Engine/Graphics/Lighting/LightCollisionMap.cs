using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Fantasy.Logic.Engine.Graphics.Lighting
{
    public class LightCollisionMap
    {
        Point center;
        
        public List<Tuple<Point, Point>> lineSegments;

        public LightCollisionMap(Point center)
        {
            this.center = center;
            lineSegments = new List<Tuple<Point, Point>>();
        }

        public void Add(Tuple<Point, Point> foo)
        {
            for (int i = 0; i < lineSegments.Count; i++)
            {
                if (foo.Item1.X == foo.Item2.X && lineSegments[i].Item1.X == lineSegments[i].Item2.X) //both lines are horizontal
                {
                    
                }
                else if (foo.Item1.Y == foo.Item2.Y && lineSegments[i].Item1.Y == lineSegments[i].Item2.Y) //both lines are veritcal
                {

                }
                else //one line is veritcal, the other is horzontal.
                { 
                    
                }
            }
        }
    }
}
