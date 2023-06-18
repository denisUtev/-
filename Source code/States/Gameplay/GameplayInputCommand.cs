using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Input;

namespace Strongest_Tank.Input
{
    public class GameplayInputCommand : BaseInputCommand 
    { 
        public class GamePause : GameplayInputCommand { }

        public class GoToMenu : GameplayInputCommand { }
        public class PlayerCorpusRotateLeft : GameplayInputCommand { }
        public class PlayerCorpusRotateRight : GameplayInputCommand { }
        public class PlayerMoveUp : GameplayInputCommand { }
        public class PlayerMoveDown : GameplayInputCommand { }

        public class PlayerRotateTurret : GameplayInputCommand 
        { 
            public Vector2 MousePos; 
        }
        public class PlayerShoots : GameplayInputCommand { }

        public class PlayerIncreaseAttackLevel : GameplayInputCommand { }
        public class PlayerDecreaseAttackLevel : GameplayInputCommand { }
        public class PlayerIncreaseArmorLevel : GameplayInputCommand { }
        public class PlayerDecreaseArmorLevel : GameplayInputCommand { }
        public class PlayerShootLazer : GameplayInputCommand { }
        public class PlayerActivateForceField : GameplayInputCommand { }
        public class PlayerRegen : GameplayInputCommand { }

        public class PlayerMouseHover : GameplayInputCommand
        {
            public Vector2 Position;
        }
        public class PlayerMousePressed : GameplayInputCommand
        {
            public Vector2 Position;
        }
    }
}
