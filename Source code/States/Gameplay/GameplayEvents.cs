using Strongest_Tank.Engine.Objects;
using Strongest_Tank.Engine.States;
using Microsoft.Xna.Framework;
using Strongest_Tank.Objects;

namespace Strongest_Tank.States.Gameplay
{
    public class GameplayEvents : BaseGameStateEvent
    {
        public class DestroyTank : GameplayEvents
        {
            public Vector2 Position;
            public string CommandName;
            public string TankName;
            public DestroyTank(Vector2 pos, string commandName, string tankName)
            {
                Position = pos;
                CommandName = commandName;
                TankName = tankName;
            }
        }

        public class TankShootMusic : GameplayEvents { }
        
        public class CyberTankShootMusic : GameplayEvents { }

        public class DestroyTankMusic : GameplayEvents { }

        public class RegenTankMusic : GameplayEvents { }

        public class ForceFieldMusic : GameplayEvents { }

        public class ShootBullet : GameplayEvents 
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Vector2 Size;
            public string BulletTexture;
            public string BulletName;
            public float Damage;
            public ShootBullet(string bulletTexture, Vector2 pos, Vector2 velocity, float damage, string name) 
            { 
                BulletTexture = bulletTexture;
                Position = pos;
                Velocity = velocity;
                Damage = damage;
                BulletName = name;
            }
            public ShootBullet(string bulletTexture, Vector2 pos, Vector2 velocity, float damage, string name, Vector2 size)
            {
                BulletTexture = bulletTexture;
                Position = pos;
                Size = size;
                Velocity = velocity;
                Damage = damage;
                BulletName = name;
            }
        }

        public class ShootRocket : GameplayEvents
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Vector2 Size;
            public string BulletTexture;
            public string BulletName;
            public float Damage;
            public ShootRocket(string bulletTexture, Vector2 pos, Vector2 size, Vector2 velocity, float damage, string name)
            {
                BulletTexture = bulletTexture;
                Position = pos;
                Size = size;
                Velocity = velocity;
                Damage = damage;
                BulletName = name;
            }
        }

        public class SpawnTank : GameplayEvents
        {
            public Vector2 Position;
            public string TankName;
            public SpawnTank(string tankName, Vector2 pos)
            {
                Position = pos;
                TankName = tankName;
            }
        }

        public class RegenCyberTank : GameplayEvents
        {
            public Vector2 Position;
            public RegenCyberTank(Vector2 pos)
            {
                Position = pos;
            }
        }

        public class BrokeTree : GameplayEvents
        {
            public Vector2 Position;
            public BrokeTree(Vector2 pos){ Position = pos; }
        }

        public class CollisionObjects : GameplayEvents
        {
            public BaseGameObject obj1;
            public BaseGameObject obj2;
            public CollisionObjects(BaseGameObject obj1, BaseGameObject obj2)
            {
                this.obj1 = obj1;
                this.obj2 = obj2;
            }
        }

        public class GoToMenu : GameplayEvents { }
    }
}
