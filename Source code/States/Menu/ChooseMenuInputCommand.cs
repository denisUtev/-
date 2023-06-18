using Strongest_Tank.Engine.Input;
using Strongest_Tank.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Strongest_Tank.States.Menu
{
    public class ChooseMenuInputCommand : BaseInputCommand
    {
        public class PlayerMouseUpped : ChooseMenuInputCommand { }
        public class PlayerMouseHover : ChooseMenuInputCommand
        {
            public Vector2 Position;
        }
        public class PlayerPressed : ChooseMenuInputCommand
        {
            public Vector2 Position;
        }
        public class SelectLevel : ChooseMenuInputCommand 
        {
            public int ChoosedLevel;
        }
        public class ExitToSplash : ChooseMenuInputCommand { }
    }
}
