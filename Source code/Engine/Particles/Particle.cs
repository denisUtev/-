using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongest_Tank.Engine.Particles
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Size;
        public Color ColorTint;
        public float LifeTime = 0;
        public float MaxLifeTime;
        public float Resistance;

        public Particle(Vector2 pos, float size, Color colorTint, Vector2 velocity, float resistance, float lifeTime)
        {
            Position = pos;
            Size = size;
            ColorTint = colorTint;
            Velocity = velocity;
            MaxLifeTime = lifeTime;
            Resistance = resistance;
        }

        public Particle(Vector2 pos, float size, Color colorTint, Vector2 velocity, float lifeTime)
        {
            Position = pos;
            Size = size;
            ColorTint = colorTint;
            Velocity = velocity;
            MaxLifeTime = lifeTime;
            Resistance = 1;
        }

        public void Update()
        {
            Position += Velocity;
            Velocity /= Resistance;
            LifeTime++;
        }
    }
}
