using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Strongest_Tank.States.Gameplay.GameplayEvents;

namespace Strongest_Tank.Objects
{
    internal class SpawnerObject : BaseGameObject
    {
        private int _count = 3;
        private float _reloadTimeSeconds = 10000;
        private DateTime _timeLastSpawn;
        private TimeSpan _timeBeforeSpawn;
        private string _tankName;

        public event EventHandler<SpawnTank> OnSpawnTank;
        public SpawnerObject(string tankName, Vector2 position) : base(null, position, Form.RECT, new Vector2(1, 1))
        {
            Body.Mass = 10;
            Name = "SpawnerObject";
            Body.IsStatic = true;
            Body.bodyForm = Form.NONE;
            _tankName = tankName;
        }

        public override void UpdateVars()
        {
            _timeBeforeSpawn = DateTime.Now - _timeLastSpawn;
            if (_count > 0 && _timeBeforeSpawn.TotalMilliseconds >= _reloadTimeSeconds)
            {
                OnSpawnTank?.Invoke(this, new SpawnTank(_tankName, Body.Position));
                _timeLastSpawn = DateTime.Now;
                _count--;
            }
            base.UpdateVars();
        }

        public override void Render(SpriteBatch spriteBatch)
        {

        }
    }
}
