using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.States.Gameplay;
using static Strongest_Tank.States.Gameplay.GameplayEvents;
using Strongest_Tank.Engine.GUI;
using Strongest_Tank.Engine.Particles;

namespace Strongest_Tank.Objects
{
    public class TankObject : BaseGameObject
    {
        protected string bulletTexture = "Images/ultraBullet";
        protected string bulletName = "BulletObject";
        protected float angleTurretRotation;
        public float Health { get; protected set; }
        public string CommandName = "null";

        protected Texture2D textureTurret;
        protected Vector2 textureSize;
        protected StatsBarObject HPStatsBar;
        protected TrackParticle trackParticle;
        protected Color MustColorTint;

        public float MaxHealth { get; protected set; }
        protected float bulletVelocity = 20;
        protected float bulletDamage = 22;
        protected float rotationCorpusVelocity = 0.03f;
        protected float rotationTurretVelocity = 0.15f;
        protected float moveForce = 5.5f;
        protected int armor;
        protected int bulletSize = 15;
        public float ReloadTimeSeconds { get; protected set; }
        public DateTime TimeLastShoot { get; protected set; }
        public TimeSpan timeBeforeShoot;

        public event EventHandler<ShootBullet> OnShootBullet;
        public event EventHandler<DestroyTank> OnDestroy;

        public TankObject(Texture2D texture, Texture2D textureTurret, Texture2D textureTrack, Vector2 position, Vector2 size) : base(texture, position, Form.RECT, size)
        {
            this.textureTurret = textureTurret;
            textureSize = new Vector2(95, 64.6f);
            ReloadTimeSeconds = 1500f;
            MaxHealth = 100;
            Name = "TankObject";
            Health = MaxHealth;
            ColorTint = new Color(255, 255, 255);
            MustColorTint = ColorTint;
            trackParticle = new TrackParticle(textureTrack, this, 20, new Color(40, 40 , 40), 10, 80);
            TimeLastShoot = DateTime.Now;
        }

        public virtual void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(- new Vector2(Body.Size.X / 2, Body.Size.Y));
        }

        public override void UpdateVars()
        {
            HPStatsBar.CurrentValue = Health;
            HPStatsBar.MaxValue = MaxHealth;
            timeBeforeShoot = DateTime.Now - TimeLastShoot;
            trackParticle.Update();
            if(Body.velocity.Length() > 1 && IsAlive)
                trackParticle.GenerateTrack();
            HPStatsBar.Update();

            if(IsAlive && ColorTint.R != MustColorTint.R)
            {
                ColorTint.R = (byte)(ColorTint.R + 5);
                ColorTint.G = (byte)(ColorTint.G + 5);
                ColorTint.B = (byte)(ColorTint.B + 5);
            }
            if (!IsAlive)
            {
                ColorTint = new Color(65, 65, 65);
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                ColorTint,
                Body.Angle + (float)Math.PI / 2,
                new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
                (textureSize.X) / _textureBody.Width,
                SpriteEffects.None,
                0.9f
                );
            spriteBatch.Draw(textureTurret,
                Body.Position,
                null,
                ColorTint,
                angleTurretRotation + (float)Math.PI / 2,
                new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.75f),
                (textureSize.X) / _textureBody.Width,
                SpriteEffects.None,
                0.8f
                );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
            trackParticle.Render(spriteBatch);
        }

        Func<double, double> normalizeVector = (vectorAngle) =>
        { return vectorAngle < 0 ? vectorAngle + Math.PI * 2 : vectorAngle - Math.PI * 2; };

        public void SetRotationTurret(float angle)
        {
            if(angleTurretRotation < 0)
                angleTurretRotation += (float)Math.PI*2;
            if (angleTurretRotation > Math.PI)
                angleTurretRotation -= (float)Math.PI * 2;
            float differenceAngle = (float)(angle% Math.PI) - angleTurretRotation;
            if(Math.Abs(differenceAngle) >= Math.PI)
                differenceAngle = (float)normalizeVector(differenceAngle);

            if (differenceAngle > Math.PI / 60.0)
                angleTurretRotation += Math.Min(differenceAngle, rotationTurretVelocity);
            if (differenceAngle < -Math.PI / 60.0)
                angleTurretRotation -= Math.Min(-differenceAngle, rotationTurretVelocity);
        }

        public void SetRotationCorpus(float angle)
        {
            var angleCorpusRotation = Body.Angle;
            if (angleCorpusRotation < 0)
                angleCorpusRotation += (float)Math.PI * 2;
            if (angleCorpusRotation > Math.PI)
                angleCorpusRotation -= (float)Math.PI * 2;
            float differenceAngle = (float)(angle % Math.PI) - angleCorpusRotation;
            if (Math.Abs(differenceAngle) >= Math.PI)
                differenceAngle = (float)normalizeVector(differenceAngle);

            if (differenceAngle > Math.PI / 60.0)
                Body.Angle += rotationCorpusVelocity;
            if (differenceAngle < -Math.PI / 60.0)
                Body.Angle -= rotationCorpusVelocity;
        }

        public void RotateCorpus(int direction)
        {
            Body.Angle += direction * rotationCorpusVelocity;
        }

        public void MoveUp()
        {
            Body.AddForce(new Vector2(moveForce * (float)Math.Cos(Body.Angle),
                moveForce * (float)Math.Sin(Body.Angle)));
        }

        public void MoveDown()
        {
            Body.AddForce(new Vector2(moveForce/2.0f * (float)Math.Cos(Body.Angle + Math.PI),
                moveForce/2.0f * (float)Math.Sin(Body.Angle + Math.PI)));
        }
        
        public virtual void Shooting()
        {
            if (timeBeforeShoot.TotalMilliseconds >= ReloadTimeSeconds)
            {
                var directionGun = (new Vector2(0.9f, 0)).Rotate(angleTurretRotation);
                OnShootBullet?.Invoke(this, new ShootBullet(bulletTexture, 
                    Body.Position + directionGun * Math.Min(Body.Size.X, Body.Size.Y),
                    directionGun * bulletVelocity + Body.velocity,
                    bulletDamage, bulletName, new Vector2(bulletSize, bulletSize)));
                TimeLastShoot = DateTime.Now;
            }
        }

        public virtual void GetDamave(BulletObject bullet)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            if (rand.Next(0, 10) >= armor)
            {
                Health -= bullet.Damage;
                if (Health <= 0 && IsAlive)
                {
                    IsAlive = false;
                    KillTank();
                }
            }
            else
            {
                AnimateProtection();
            }
        }

        protected void AnimateProtection()
        {
            ColorTint = new Color(200, 200, 200);
        }

        protected virtual void KillTank()
        {
            OnDestroy?.Invoke(this, new DestroyTank(Body.Position + Body.velocity, CommandName, Name));
            ColorTint = new Color(65, 65, 65);
        }
    }
}
