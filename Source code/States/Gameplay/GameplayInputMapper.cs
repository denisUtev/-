using Strongest_Tank.Engine.Input;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Strongest_Tank.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        private bool IsD1Pressed = false;
        private bool IsD2Pressed = false;
        private bool IsEscPressed = false;

        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();
            
            if (state.IsKeyDown(Keys.Escape) && !IsEscPressed)
            {
                commands.Add(new GameplayInputCommand.GamePause());
                IsEscPressed = true;
            }
            if (state.IsKeyDown(Keys.D))
            {
                commands.Add(new GameplayInputCommand.PlayerCorpusRotateRight());
            }
            if (state.IsKeyDown(Keys.A))
            {
                commands.Add(new GameplayInputCommand.PlayerCorpusRotateLeft());
            }
            if (state.IsKeyDown(Keys.W))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            if (state.IsKeyDown(Keys.S))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (state.IsKeyDown(Keys.D1) && !IsD1Pressed && !state.IsKeyDown(Keys.LeftShift))
            {
                commands.Add(new GameplayInputCommand.PlayerIncreaseAttackLevel());
                IsD1Pressed = true;
            }
            if (state.IsKeyDown(Keys.D1) && !IsD1Pressed && state.IsKeyDown(Keys.LeftShift))
            {
                commands.Add(new GameplayInputCommand.PlayerDecreaseAttackLevel());
                IsD1Pressed = true;
            }
            if (state.IsKeyDown(Keys.D2) && !IsD2Pressed && !state.IsKeyDown(Keys.LeftShift))
            {
                commands.Add(new GameplayInputCommand.PlayerIncreaseArmorLevel());
                IsD2Pressed = true;
            }
            if (state.IsKeyDown(Keys.D2) && !IsD2Pressed && state.IsKeyDown(Keys.LeftShift))
            {
                commands.Add(new GameplayInputCommand.PlayerDecreaseArmorLevel());
                IsD2Pressed = true;
            }
            if (state.IsKeyDown(Keys.E))
            {
                commands.Add(new GameplayInputCommand.PlayerShootLazer());
            }
            if (state.IsKeyDown(Keys.F))
            {
                commands.Add(new GameplayInputCommand.PlayerActivateForceField());
            }
            if (state.IsKeyDown(Keys.R))
            {
                commands.Add(new GameplayInputCommand.PlayerRegen());
            }

            if (state.IsKeyUp(Keys.D1))
                IsD1Pressed = false;
            if (state.IsKeyUp(Keys.D2))
                IsD2Pressed = false;
            if (state.IsKeyUp(Keys.Escape))
                IsEscPressed = false;

            return commands;
        }

        private bool IsMouseClicked = false;
        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            var commands = new List<GameplayInputCommand>();

            commands.Add(new GameplayInputCommand.PlayerRotateTurret() { MousePos = new Vector2(state.X, state.Y) });
            commands.Add(new GameplayInputCommand.PlayerMouseHover() { Position = new Vector2(state.X, state.Y) });
            if (state.LeftButton == ButtonState.Pressed)
            {
                commands.Add(new GameplayInputCommand.PlayerShoots());
                if (!IsMouseClicked)
                {
                    commands.Add(new GameplayInputCommand.PlayerMousePressed() { Position = new Vector2(state.X, state.Y) });
                    IsMouseClicked = true;
                }
            }
            else
                IsMouseClicked = false;

            return commands;
        }
    }
}
