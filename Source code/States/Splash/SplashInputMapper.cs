using Strongest_Tank.Engine.Input;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Strongest_Tank.Input
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.ShowButtons());
            }
            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new SplashInputCommand.GameExit());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            var commands = new List<SplashInputCommand>();

            commands.Add(new SplashInputCommand.PlayerMouseHover() { Position = state.Position.ToVector2() } );
            if (state.LeftButton == ButtonState.Pressed)
                commands.Add(new SplashInputCommand.PlayerPressed() { Position = state.Position.ToVector2() } );

            return commands;
        }
    }
}
