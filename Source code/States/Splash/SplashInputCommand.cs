using Strongest_Tank.Engine.Input;
using Microsoft.Xna.Framework;

namespace Strongest_Tank.Input
{
    public class SplashInputCommand : BaseInputCommand 
    {
        public class PlayerMouseHover : SplashInputCommand 
        {
            public Vector2 Position;
        }
        public class PlayerPressed : SplashInputCommand
        {
            public Vector2 Position;
        }
        public class GameSelect : SplashInputCommand { }

        public class ShowButtons : SplashInputCommand { }
        public class GameExit : SplashInputCommand { }
    }
}
