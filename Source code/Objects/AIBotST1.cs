using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongest_Tank.Objects
{
    public class AIBotST1
    {
        public static string[,] Moves = new string[100, 100];
        public TankObject BotTank;
        public TankObject EnemyTank;
        public string CommandName;

        public AIBotST1(TankObject botTank, string commandName)
        {
            BotTank = botTank;
            BotTank.CommandName = commandName;
            CommandName = commandName;
        }

        public virtual void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            if (!BotTank.IsAlive)
                return;

            if (EnemyTank == null || !IsVisibleEnemy(buildingsList, EnemyTank) || !EnemyTank.IsAlive)
                FindEnemyTank(tanksList, buildingsList);

            if (EnemyTank != null)
            {
                var mustDirection = (float)(EnemyTank.Body.Position - BotTank.Body.Position).Angle();
                BotTank.SetRotationTurret(mustDirection);
                BotTank.SetRotationCorpus(mustDirection);
                BotTank.MoveUp();
                if (BotTank.timeBeforeShoot.TotalMilliseconds >= BotTank.ReloadTimeSeconds)
                    BotTank.Shooting();
            }
            else
            {
                BotMove();
            }
        }

        protected void BotMove()
        {
            string mustMove = Moves[(int)(BotTank.Body.Position.X / 100), (int)(BotTank.Body.Position.Y / 100)];

            if (mustMove.Equals("UP"))
            {
                BotTank.SetRotationTurret(-(float)Math.PI / 2);
                BotTank.SetRotationCorpus(-(float)Math.PI / 2);
                BotTank.MoveUp();
            }
            if (mustMove.Equals("RIGHT"))
            {
                BotTank.SetRotationTurret(0);
                BotTank.SetRotationCorpus(0);
                BotTank.MoveUp();
            }
            if (mustMove.Equals("DOWN"))
            {
                BotTank.SetRotationTurret((float)Math.PI / 2);
                BotTank.SetRotationCorpus((float)Math.PI / 2);
                BotTank.MoveUp();
            }
            if (mustMove.Equals("LEFT"))
            {
                BotTank.SetRotationTurret((float)Math.PI * 39 / 40.0f);
                BotTank.SetRotationCorpus((float)Math.PI * 39 / 40.0f);
                BotTank.MoveUp();
            }
        }

        protected void FindEnemyTank(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            double minDistance = double.MaxValue;
            EnemyTank = null;
            foreach (var otherTank in tanksList)
            {
                if(otherTank.BotTank.IsAlive && DistanceToObject(otherTank.BotTank) < 3000 && otherTank.CommandName != CommandName && IsVisibleEnemy(buildingsList, otherTank.BotTank) && DistanceToObject(otherTank.BotTank) < minDistance)
                {
                    EnemyTank = otherTank.BotTank;
                    minDistance = DistanceToObject(otherTank.BotTank);
                }
            }

        }

        public bool IsVisibleEnemy(List<BaseGameObject> buildingsList, TankObject otherTank)
        {
            double A = BotTank.Body.Position.Y - otherTank.Body.Position.Y;
            double B = otherTank.Body.Position.X - BotTank.Body.Position.X;
            double C = - B * otherTank.Body.Position.Y - otherTank.Body.Position.X * A;
            double denominator = Math.Sqrt(A * A + B * B);
            foreach (var building in buildingsList)
            {
                //building.Body.Size.X --> приведенный размер должен быть
                if (!(building is TreeObject) && building.Body.bodyForm != Form.NONE && Math.Abs(A * building.Body.Position.X + B * building.Body.Position.Y + C) / denominator <= building.Body.Size.X / 2.0f)
                {
                    var distBuildingToBot = (BotTank.Body.Position - building.Body.Position).Length();
                    var distOtherToBot = (BotTank.Body.Position - otherTank.Body.Position).Length();
                    var distBuildingToOther = (otherTank.Body.Position - building.Body.Position).Length();
                    if (distBuildingToBot <= distOtherToBot && distBuildingToOther <= distOtherToBot)
                        return false;
                }
            }
            return true;
        }

        protected double DistanceToObject(BaseGameObject obj)
        {
            return (BotTank.Body.Position - obj.Body.Position).Length();
        }
    }

    public class AIPlayer : AIBotST1
    {
        public AIPlayer(TankObject botTank, string commandName) : base(botTank, commandName)
        {

        }

        public override void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            
        }
    }

    public class AIPTTank : AIBotST1
    {
        protected float MaxDistanceShoot = 1300;
        public AIPTTank(TankObject botTank, string commandName) : base(botTank, commandName)
        {

        }

        public override void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            if (!BotTank.IsAlive)
                return;

            if (EnemyTank == null || !IsVisibleEnemy(buildingsList, EnemyTank) || !EnemyTank.IsAlive)
                FindEnemyTank(tanksList, buildingsList);

            if (EnemyTank != null)
            {
                var mustDirection = (float)(EnemyTank.Body.Position + EnemyTank.Body.velocity * 1.5f - BotTank.Body.Position).Angle();
                BotTank.SetRotationTurret(mustDirection);
                BotTank.SetRotationCorpus(mustDirection);
                if(DistanceToObject(EnemyTank) > MaxDistanceShoot)
                    BotTank.MoveUp();
                else
                    BotTank.MoveDown();
                if (BotTank.timeBeforeShoot.TotalMilliseconds >= BotTank.ReloadTimeSeconds)
                    BotTank.Shooting();
            }
            else
            {
                BotMove();
            }
        }
    }

    public class AIDotTank : AIPTTank
    {
        public AIDotTank(TankObject botTank, string commandName) : base(botTank, commandName)
        {
            MaxDistanceShoot = 10000;
        }
    }

    public class AIBotST2 : AIBotST1
    {
        public AIBotST2(TankObject botTank, string commandName) : base(botTank, commandName)
        {

        }

        public override void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            if (!BotTank.IsAlive)
                return;

            if (EnemyTank == null || !IsVisibleEnemy(buildingsList, EnemyTank) || !EnemyTank.IsAlive)
                FindEnemyTank(tanksList, buildingsList);

            if (EnemyTank != null)
            {
                var mustDirection = (float)(EnemyTank.Body.Position - BotTank.Body.Position).Angle();
                BotTank.SetRotationTurret(mustDirection);
                BotTank.SetRotationCorpus(mustDirection);
                BotTank.MoveUp();
                if (BotTank.timeBeforeShoot.TotalMilliseconds >= BotTank.ReloadTimeSeconds)
                    BotTank.Shooting();
            }
        }
        
    }

    public class AIBotST3 : AIBotST1
    {
        public AIBotST3(TankObject botTank, string commandName) : base(botTank, commandName)
        {

        }

        public override void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {
            if (!BotTank.IsAlive)
                return;

            if (EnemyTank == null || !IsVisibleEnemy(buildingsList, EnemyTank) || !EnemyTank.IsAlive)
                FindEnemyTank(tanksList, buildingsList);

            if (EnemyTank != null)
            {
                var mustDirection = (float)(EnemyTank.Body.Position - BotTank.Body.Position).Angle();
                BotTank.SetRotationTurret(mustDirection);
                BotTank.SetRotationCorpus(mustDirection);
                BotTank.MoveUp();
                if (DistanceToObject(EnemyTank) <= 600)
                    BotTank.RotateCorpus(-1);
                if (BotTank.timeBeforeShoot.TotalMilliseconds >= BotTank.ReloadTimeSeconds)
                    BotTank.Shooting();
            }
            else
            {
                BotMove();
            }
        }
    }

    public class AIBlock2 : AIBotST1
    {
        public AIBlock2(TankObject botTank, string commandName) : base(botTank, commandName)
        {

        }

        public override void Update(List<AIBotST1> tanksList, List<BaseGameObject> buildingsList)
        {

        }
    }
}
