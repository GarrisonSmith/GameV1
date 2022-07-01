using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fantasy.Logic.Engine.Utility;

namespace Fantasy.Logic.Engine.graphics.particles
{
    class Particle
    {
        public static Texture2D white_particle = Global._content.Load<Texture2D>("tile-sets/particle");

        public Point position;
        public Point destination;
        public Color color;
        public int speed;
        public int duration;
        public double spawnTime;

        public Particle(Point position, Color color, int speed, int duration)
        {
            this.position = position;
            this.color = color;
            this.speed = speed;
            this.duration = duration;
            //spawnTime = Global._gameTime.TotalGameTime.TotalMilliseconds;
        }
        public Particle(Point position, Color color, int speed, int duration, Point destination) : this(position, color, speed, duration)
        {
            this.destination = destination;
        }
        public void Draw()
        {
            Global._spriteBatch.Draw(white_particle, new Vector2(position.X, position.Y),
                new Rectangle(0, 0, 2, 2),
                color, 0f, new Vector2(0, 0), new Vector2(2, 2), new SpriteEffects(), 0);
        }
        public void MovePosition()
        { 
            
        }
    }

    enum ParticleBehavior
    {
        radiateOut,
        radiateUp,
        radiateTowardPoint,
        moveToPoint
    }
}
