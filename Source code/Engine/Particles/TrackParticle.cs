using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Objects;

namespace Strongest_Tank.Engine.Particles
{
    public class TrackParticle
    {
        public Color ColorTint;
        private float size;
        public int MaxLifeTime;
        public float BodyScale = 0.7f;
        private RandomNumberGenerator random;
        private BaseGameObject gameObject;//от кого отсаются следы
        protected Texture2D _textureParticle;

        private float hipotenuze;
        private float angle;
        private List<Particle> activeParticles;

        public TrackParticle(Texture2D texture, BaseGameObject Object, float size, Color color, int count, int maxLifeTime)
        {
            _textureParticle = texture;
            ColorTint = color;
            this.size = size;
            MaxLifeTime = maxLifeTime;
            gameObject = Object;
            CreateParticles(count);
            hipotenuze = (float) Math.Sqrt(gameObject.Body.Size.X * gameObject.Body.Size.X / 4 + gameObject.Body.Size.Y * gameObject.Body.Size.Y / 4);
            angle = (float) Math.Asin((gameObject.Body.Size.X / 2) / hipotenuze);
        }

        private void CreateParticles(int count)
        {
            activeParticles = new List<Particle>(count);
            random = new RandomNumberGenerator();
        }

        public void GenerateTrack()
        {
            var newPos = gameObject.Body.Position - new Vector2(BodyScale, 0).Rotate(angle + gameObject.Body.Angle) * hipotenuze;
            activeParticles.Add(new Particle(newPos, size, ColorTint, Vector2.Zero, 1, MaxLifeTime));
            newPos = gameObject.Body.Position - new Vector2(BodyScale, 0).Rotate(-angle + gameObject.Body.Angle) * hipotenuze;
            activeParticles.Add(new Particle(newPos, size, ColorTint, Vector2.Zero, 1, MaxLifeTime));
        }

        public void Update()
        {
            foreach (var particle in activeParticles)
            {
                particle.Update();
            }


            for(int i = activeParticles.Count() - 1; i > -1; i--)
            {
                if (activeParticles[i].LifeTime >= activeParticles[i].MaxLifeTime)
                {
                    activeParticles.RemoveAt(i);
                }
            }
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            float sizeRatio = (float)_textureParticle.Width / _textureParticle.Height;
            foreach (var particle in activeParticles)
            {
                spriteBatch.Draw(_textureParticle,
                    particle.Position,
                    null,
                    particle.ColorTint * (1 - particle.LifeTime / particle.MaxLifeTime),
                    (float)particle.Velocity.Angle(),
                    new Vector2(_textureParticle.Width / 2, _textureParticle.Height / 2),
                    new Vector2(particle.Size / _textureParticle.Width, sizeRatio * (particle.Size / _textureParticle.Height)),
                    SpriteEffects.None,
                    0.91f
                    );
            }
        }
    }
}
