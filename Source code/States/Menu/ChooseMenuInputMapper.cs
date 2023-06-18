using Microsoft.Xna.Framework.Input;
using Strongest_Tank.Engine.Input;
using Strongest_Tank.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongest_Tank.States.Menu
{
    public class ChooseMenuInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<ChooseMenuInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new ChooseMenuInputCommand.ExitToSplash());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            var commands = new List<ChooseMenuInputCommand>();

            commands.Add(new ChooseMenuInputCommand.PlayerMouseHover() { Position = state.Position.ToVector2() });
            if (state.LeftButton == ButtonState.Pressed)
                commands.Add(new ChooseMenuInputCommand.PlayerPressed() { Position = state.Position.ToVector2() });
            else
                commands.Add(new ChooseMenuInputCommand.PlayerMouseUpped());

            return commands;
        }
    }
}
