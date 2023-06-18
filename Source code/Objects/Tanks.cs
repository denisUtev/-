using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Objects;
using Strongest_Tank.Engine.Particles;
using static Strongest_Tank.States.Gameplay.GameplayEvents;
using Strongest_Tank.Engine.GUI;

namespace Strongest_Tank.Objects
{
    public class CyberTank : TankObject
    {
        public int AttackLevel = 0;
        public int ArmorLevel = 0;
        public bool IsActivatedForceField = false;
        private float forceFieldAlpha = 0.0f;

        public int Energy = 1000;
        public event EventHandler<ShootBullet> OnShootLazer;
        public event EventHandler<RegenCyberTank> OnRegen;
        private RandomNumberGenerator random = new RandomNumberGenerator();
        private Texture2D _textureForceField;

        public CyberTank(Texture2D texture, Texture2D textureTurret, Texture2D textureForceField, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/ultraBullet";
            ReloadTimeSeconds = 300f;
            moveForce = 10f;
            bulletDamage = 15;
            MaxHealth = 300;
            Health = MaxHealth;
            bulletName = "UltraBulletObject";
            _textureForceField = textureForceField;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureForceField,
                Body.Position,
                null,
                Color.White * forceFieldAlpha,
                Body.Angle + (float)Math.PI / 2,
                new Vector2(_textureForceField.Width / 2, _textureForceField.Height / 2),
                (textureSize.X * 3) / _textureForceField.Width,
                SpriteEffects.None,
                0.01f
                );
            base.Render(spriteBatch);
        }

        public override void UpdateVars()
        {
            if(Energy < 1000)
                Energy++;

            if (IsActivatedForceField && forceFieldAlpha < 0.8f)
                forceFieldAlpha += 0.08f;
            else if (forceFieldAlpha > 0)
                forceFieldAlpha -= 0.08f;
            IsActivatedForceField = false;

            base.UpdateVars();
        }

        public override void GetDamave(BulletObject bullet)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            if (!IsActivatedForceField && rand.Next(0, 10) >= armor)
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

        public void ChangeAttackLevel(int direction)
        {
            //direction = -1 || 1
            if (AttackLevel < 7 && direction == 1 || AttackLevel > 0 && direction == -1)
            {
                bulletDamage += direction * 20;
                ReloadTimeSeconds += direction * 330;
                AttackLevel += direction;
            }
        }
        public void ChangeArmorLevel(int direction)
        {
            //direction = -1 || 1
            if (ArmorLevel < 7 && direction == 1 || ArmorLevel > 0 && direction == -1)
            {
                armor += direction;
                moveForce -= direction * 0.9f;
                ArmorLevel += direction;
            }
        }

        public void ShootLazer() 
        {
            if (Energy >= 50)
            {
                var directionGun = (new Vector2(1, 0)).Rotate(angleTurretRotation);
                OnShootLazer?.Invoke(this, new ShootBullet(bulletTexture,
                    Body.Position + directionGun * Math.Min(Body.Size.X, Body.Size.Y),
                    directionGun * bulletVelocity + Body.velocity,
                    45, bulletName, new Vector2(bulletSize, bulletSize)));
                Energy -= 50;
            }
        }

        public void ActivateForceField()
        {
            if(Energy >= 5)
            {
                IsActivatedForceField = true;
                Energy -= 5;
            }
        }

        public void Regen()
        {
            if(Energy >= 8 && Health < MaxHealth)
            {
                OnRegen?.Invoke(this, new RegenCyberTank(Body.Position));
                Health += 1;
                Energy -= 8;
                Health = Math.Min(Health, MaxHealth);
            }
        }
    }

    public class TankST1 : TankObject
    {
        public TankST1(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(120, 81.6f);
            trackParticle.BodyScale = 0.55f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)System.Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            (textureSize.X) / _textureBody.Width,
            SpriteEffects.None,
            0.9f
            );
            spriteBatch.Draw(textureTurret,
            Body.Position,
            null,
            ColorTint,
            angleTurretRotation + (float)System.Math.PI / 2,
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.71f),
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
    }

    public class TankST2 : TankObject
    {
        public TankST2(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(120, 81.6f);
            bulletDamage = 30;
            ReloadTimeSeconds = 1800;
            moveForce = 4.8f;
            MaxHealth = 110;
            Health = MaxHealth;
            trackParticle.BodyScale = 0.55f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)System.Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            (textureSize.X) / _textureBody.Width,
            SpriteEffects.None,
            0.9f
            );
            spriteBatch.Draw(textureTurret,
            Body.Position,
            null,
            ColorTint,
            angleTurretRotation + (float)System.Math.PI / 2,
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.71f),
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
    }

    public class TankST3 : TankObject
    {
        public TankST3(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(120, 81.6f);
            MaxHealth = 120;
            Health = MaxHealth;
            bulletDamage = 25;
            ReloadTimeSeconds = 1600;
            armor = 1;
            trackParticle.BodyScale = 0.55f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)System.Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            (textureSize.X) / _textureBody.Width,
            SpriteEffects.None,
            0.9f
            );
            spriteBatch.Draw(textureTurret,
            Body.Position,
            null,
            ColorTint,
            angleTurretRotation + (float)System.Math.PI / 2,
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.71f),
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
    }

    public class Jeep : TankObject
    {
        public Jeep(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(120, 120);
            moveForce = 10.5f; // 11.1f
            MaxHealth = 90;
            Health = MaxHealth;
            Body.Mass = 10;
            bulletDamage = 6;
            ReloadTimeSeconds = 600;
            Body.ConstantOfFriction = 0.055f;
            trackParticle.BodyScale = 0.48f;
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
            Body.Position - new Vector2(20, 0).Rotate(Body.Angle),
            null,
            ColorTint,
            angleTurretRotation + (float)Math.PI / 2,
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.8f),
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
    }
    
    public class HeavyTank : TankObject
    {
        public HeavyTank(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(126, 146.88f);

            Body.Mass = 50;
            armor = 4;
            MaxHealth = 220;
            Health = MaxHealth;
            bulletDamage = 30;
            ReloadTimeSeconds = 2000;
            moveForce = 4.5f;
            rotationCorpusVelocity = 0.01f;
            rotationTurretVelocity = 0.05f;
            trackParticle.BodyScale = 0.55f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)System.Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            (textureSize.X) / _textureBody.Width,
            SpriteEffects.None,
            0.9f
            );
            spriteBatch.Draw(textureTurret,
            Body.Position,
            null,
            ColorTint,
            angleTurretRotation + (float)System.Math.PI / 2,
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.8f),
            (textureSize.X + 20) / _textureBody.Width,
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
    }

    public class YagaTank : TankObject
    {
        public YagaTank(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(126, 146.88f);

            Body.Mass = 50;
            armor = 3;
            MaxHealth = 190;
            Health = MaxHealth;
            bulletDamage = 65;
            ReloadTimeSeconds = 1900;
            moveForce = 3f;
            rotationCorpusVelocity = 0.0075f;
            rotationTurretVelocity = 0;
            trackParticle.BodyScale = 0.55f;
        }

        public override void UpdateVars()
        {
            angleTurretRotation = Body.Angle;
            base.UpdateVars();
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

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
            trackParticle.Render(spriteBatch);
        }
    }

    public class TankST4 : TankObject
    {
        public TankST4(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(122f, 70);
            MaxHealth = 180;
            Health = MaxHealth;
            bulletDamage = 25;
            ReloadTimeSeconds = 1350;
            moveForce = 4.5f;
            armor = 1;
            trackParticle.BodyScale = 0.48f;
            Body.Angle = -(float)Math.PI / 2;
            angleTurretRotation = -(float)Math.PI / 2;
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(Body.Size.X / 3, Body.Size.Y));
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
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.71f),
            (textureSize.X * 0.78f) / _textureBody.Width,
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
    }

    public class TankIS7 : TankObject
    {
        public TankIS7(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            Name = "IS7";
            MaxHealth = 280;
            Health = MaxHealth;
            bulletDamage = 40;
            ReloadTimeSeconds = 1980;
            moveForce = 2.8f;
            armor = 5;
            Body.Angle = -(float)Math.PI / 2;
            angleTurretRotation = -(float)Math.PI / 2;
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
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.71f),
            (textureSize.X * 0.78f) / _textureBody.Width,
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
    }

    public class TankDot : TankObject
    {
        public event EventHandler<ShootRocket> OnShootRocket;

        public TankDot(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = size * 2;
            MaxHealth = 350;
            Health = MaxHealth;
            bulletDamage = 99;
            ReloadTimeSeconds = 4000;
            armor = 3;
            rotationTurretVelocity = 0;
            rotationCorpusVelocity = 0.05f;
            bulletVelocity = 30f;
            moveForce = 0f;
            Body.IsStatic = true;
            Body.bodyForm = Form.CIRCLE;
            Body.Mass = 100;
            Name = "Dot";
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(Body.Size.X / 3, Body.Size.Y));
        }

        public override void UpdateVars()
        {
            angleTurretRotation = Body.Angle;
            base.UpdateVars();
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

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }

        public override void Shooting()
        {
            if (timeBeforeShoot.TotalMilliseconds >= ReloadTimeSeconds)
            {
                var velocityGun = (new Vector2(1, 0)).Rotate(angleTurretRotation);
                var directionGun = (new Vector2(1.08f, 0)).Rotate(angleTurretRotation - Math.PI / 4.5f);
                OnShootRocket?.Invoke(this, new ShootRocket(bulletTexture,
                    Body.Position + directionGun * Math.Min(Body.Size.X, Body.Size.Y),
                    new Vector2(40, 40),
                    velocityGun * bulletVelocity + Body.velocity,
                    bulletDamage, bulletName));
                directionGun = (new Vector2(1.08f, 0)).Rotate(angleTurretRotation + Math.PI / 4.5f);
                OnShootRocket?.Invoke(this, new ShootRocket(bulletTexture,
                    Body.Position + directionGun * Math.Min(Body.Size.X, Body.Size.Y),
                    new Vector2(40, 40),
                    velocityGun * bulletVelocity + Body.velocity,
                    bulletDamage, bulletName));
                TimeLastShoot = DateTime.Now;
            }
        }
    }

    public class AliveBuilding : TankObject
    {

        public Action PlayerLose;
        public AliveBuilding(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = size;

            Body.IsStatic = true;
            Body.ColliseForse = 12;
            Body.Mass = 100;
            armor = 3;
            MaxHealth = 700;
            Health = MaxHealth;
            bulletDamage = 0;
            ReloadTimeSeconds = 19000;
            moveForce = 0f;
            rotationCorpusVelocity = 0.0f;
            rotationTurretVelocity = 0;
            trackParticle.BodyScale = 0.0f;
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(bar.Size.X / 2, Body.Size.Y));
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            new Vector2(textureSize.Y / _textureBody.Height, textureSize.X / _textureBody.Width),
            SpriteEffects.None,
            0.9f
            );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }

        protected override void KillTank()
        {
            PlayerLose();
            base.KillTank();
        }
    }

    public class Barrel : TankObject
    {

        public Barrel(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = size * 2;
            Name = "Barrel";

            Body.IsStatic = true;
            Body.ColliseForse = 12;
            Body.bodyForm = Form.CIRCLE;
            Body.Mass = 100;
            armor = 0;
            MaxHealth = 250;
            Health = MaxHealth;
            bulletDamage = 0;
            ReloadTimeSeconds = 19000;
            moveForce = 0f;
            rotationCorpusVelocity = 0.0f;
            rotationTurretVelocity = 0;
            trackParticle.BodyScale = 0.0f;
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(bar.Size.X / 2, Body.Size.Y));
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            new Vector2(textureSize.Y / _textureBody.Height, textureSize.X / _textureBody.Width),
            SpriteEffects.None,
            0.9f
            );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }
    }

    public class Barrel2 : Barrel
    {
        public Barrel2(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size, float angle) : base(texture, particleTexture, position, size)
        {
            textureSize = new Vector2(size.X - 25, size.Y + 100);
            MaxHealth = 300;
            Health = MaxHealth;
            Body.Angle = angle;
            Body.bodyForm = Form.RECT;
            Name = "Barrel2";
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            new Vector2(textureSize.Y / _textureBody.Height, textureSize.X / _textureBody.Width),
            SpriteEffects.None,
            0.9f
            );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }
    }

    public class Machine : TankObject
    {

        public Machine(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = size;
            Name = "Machine";

            Body.IsStatic = true;
            Body.ColliseForse = 12;
            Body.bodyForm = Form.RECT;
            Body.Mass = 100;
            armor = 0;
            MaxHealth = 250;
            Health = MaxHealth;
            bulletDamage = 0;
            ReloadTimeSeconds = 19000;
            moveForce = 0f;
            rotationCorpusVelocity = 0.0f;
            rotationTurretVelocity = 0;
            trackParticle.BodyScale = 0.0f;
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(bar.Size.X / 2, Body.Size.Y));
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            new Vector2(textureSize.Y / _textureBody.Height, textureSize.X / _textureBody.Width),
            SpriteEffects.None,
            0.9f
            );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }
    }

    public class TankKorpus : TankObject
    {

        public TankKorpus(Texture2D texture, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, null, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = size;
            Name = "TankKorpus";

            Body.IsStatic = true;
            Body.ColliseForse = 12;
            Body.bodyForm = Form.RECT;
            Body.Mass = 100;
            armor = 0;
            MaxHealth = 50;
            Health = MaxHealth;
            bulletDamage = 0;
            ReloadTimeSeconds = 19000;
            moveForce = 0f;
            rotationCorpusVelocity = 0.0f;
            rotationTurretVelocity = 0;
            trackParticle.BodyScale = 0.0f;
        }

        public override void CreateHPStatsBar(StatsBarObject bar)
        {
            HPStatsBar = bar;
            HPStatsBar.SetTranslation(-new Vector2(bar.Size.X / 2, Body.Size.Y));
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            ColorTint,
            Body.Angle + (float)Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            new Vector2(textureSize.Y / _textureBody.Height, textureSize.X / _textureBody.Width),
            SpriteEffects.None,
            0.9f
            );

            if (IsAlive)
            {
                HPStatsBar.SetPosition(Body.Position);
                HPStatsBar.Render(spriteBatch);
            }
        }
    }

    public class WhiteTigerTank : TankObject
    {
        public WhiteTigerTank(Texture2D texture, Texture2D textureTurret, Texture2D particleTexture, Vector2 position, Vector2 size) : base(texture, textureTurret, particleTexture, position, size)
        {
            bulletTexture = "Images/Bullet";
            textureSize = new Vector2(70, 64.6f) * 1.8f;

            Body.Mass = 15;
            armor = 8;
            MaxHealth = 1000;
            Health = MaxHealth;
            bulletDamage = 84;
            ReloadTimeSeconds = 1700;
            moveForce = 5.8f;
            rotationCorpusVelocity = 0.02f;
            rotationTurretVelocity = 0.07f;
            trackParticle.BodyScale = 0.55f;
            bulletSize = 25;
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
            new Vector2(textureTurret.Width / 2, textureTurret.Height * 0.7f),
            (textureSize.X + 20) / _textureBody.Width,
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
    }
}
