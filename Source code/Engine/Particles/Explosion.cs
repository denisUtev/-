using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;

namespace Strongest_Tank.Engine.Particles
{
    public class Explosion
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color ColorTint;
        protected float maxRadius;
        protected float maxVelocity;
        protected float minSize;
        protected float maxSize;
        protected float resistance;
        public int MaxLifeTime;
        public int LifeTime = 0;
        protected List<Particle> particles;
        protected RandomNumberGenerator random;
        protected Texture2D _textureParticle;
        protected bool isAround = true;

        public Explosion(Texture2D texture, Vector2 pos, float maxRadius, float minSize, float maxSize, float resistance, Vector2 velocity, Color color, int count, int maxLifeTime) 
        { 
            _textureParticle = texture;
            Position = pos;
            Velocity = velocity;
            ColorTint = color;
            MaxLifeTime = maxLifeTime;
            this.maxRadius = maxRadius;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.resistance = resistance;
            isAround = false;
            CreateParticles(count);
        }

        public Explosion(Texture2D texture, Vector2 pos, float maxRadius, float minSize, float maxSize, float maxVelocity, Color color, int count, int maxLifeTime)
        {
            _textureParticle = texture;
            Position = pos;
            this.maxVelocity = maxVelocity;
            ColorTint = color;
            MaxLifeTime = maxLifeTime;
            this.maxRadius = maxRadius;
            this.minSize = minSize;
            this.maxSize = maxSize;
            isAround = true;
            resistance = 1;
            CreateParticles(count);
        }

        public Explosion(Texture2D texture, Vector2 pos, float maxRadius, float minSize, float maxSize, float maxVelocity, float resistance, Color color, int count, int maxLifeTime)
        {
            _textureParticle = texture;
            Position = pos;
            this.maxVelocity = maxVelocity;
            ColorTint = color;
            MaxLifeTime = maxLifeTime;
            this.maxRadius = maxRadius;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.resistance = resistance;
            isAround = true;
            CreateParticles(count);
        }

        protected virtual void CreateParticles(int count)
        {
            particles = new List<Particle>(count);
            random = new RandomNumberGenerator();

            Vector2 distance, velocity;
            Color newColor;
            float size;

            for (int i=0; i<count; i++)
            {
                distance = new Vector2(random.NextRandom(-maxRadius, maxRadius), random.NextRandom(-maxRadius, maxRadius));

                if (isAround)
                    velocity = new Vector2(random.NextRandom(-maxVelocity, maxVelocity), random.NextRandom(-maxVelocity, maxVelocity));
                else
                {
                    velocity = new Vector2(Velocity.X, Velocity.Y) * random.NextRandom(0.75f, 1.25f);
                    velocity = velocity.Rotate(random.NextRandom(-(float)Math.PI/4.5f, (float)Math.PI / 4.5f));
                }
                size = random.NextRandom(minSize, maxSize);
                newColor = new Color(random.NextRandom(ColorTint.R - 30, ColorTint.R + 30),
                    random.NextRandom(ColorTint.G - 30, ColorTint.G + 30),
                    random.NextRandom(ColorTint.B - 30, ColorTint.B + 30));
                particles.Add(new Particle(Position + distance, size, newColor, velocity, resistance, MaxLifeTime));
            }
        }

        public void Update()
        {
            foreach (var particle in particles)
            {
                particle.Update();
            }
            LifeTime++;
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            float sizeRatio = (float)_textureParticle.Width / _textureParticle.Height;
            foreach (var particle in particles)
            {
                spriteBatch.Draw(_textureParticle,
                    particle.Position,
                    null,
                    particle.ColorTint * (1 - particle.LifeTime / particle.MaxLifeTime),
                    (float)particle.Velocity.Angle(),
                    new Vector2(_textureParticle.Width / 2, _textureParticle.Height / 2),
                    new Vector2(particle.Size / _textureParticle.Width, sizeRatio * (particle.Size / _textureParticle.Height)),
                    SpriteEffects.None,
                    0.15f
                    );
            }
        }
    }

    public class Absorption : Explosion
    {

        public Absorption(Texture2D texture, Vector2 pos, float maxRadius, float minSize, float maxSize, float maxVelocity, Color color, int count, int maxLifeTime) : base(texture, pos, maxRadius, minSize, maxSize, maxVelocity, color, count, maxLifeTime)
        {
            
        }

        protected override void CreateParticles(int count)
        {
            particles = new List<Particle>(count);
            random = new RandomNumberGenerator();

            Vector2 distance, velocity;
            Color newColor;
            float size;

            for (int i = 0; i < count; i++)
            {
                distance = new Vector2(random.NextRandom(-maxRadius, maxRadius), random.NextRandom(-maxRadius, maxRadius));

                velocity = new Vector2(random.NextRandom(0.4f, maxVelocity), 0).Rotate(distance.Angle() + Math.PI);
                size = random.NextRandom(minSize, maxSize);
                newColor = new Color(random.NextRandom(ColorTint.R - 30, ColorTint.R + 30),
                    random.NextRandom(ColorTint.G - 30, ColorTint.G + 30),
                    random.NextRandom(ColorTint.B - 30, ColorTint.B + 30));
                particles.Add(new Particle(Position + distance, size, newColor, velocity, resistance, MaxLifeTime));
            }
        }
    }
}
