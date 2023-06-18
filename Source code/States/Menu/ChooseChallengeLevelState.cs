using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.GUI;
using Strongest_Tank.Engine.Input;
using Strongest_Tank.Engine.States;
using Strongest_Tank.Engine;
using Strongest_Tank.Objects;
using strongest_Tank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Strongest_Tank.States.Menu
{
    public class ChooseChallengeLevelState : BaseGameState
    {
        private const string TextFont = "Fonts/TextNameMap2";

        private ImageObject BlackFieldEdging;
        private ImageObject BlackField;
        private List<ButtonObject> ButtonsList = new List<ButtonObject>();

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture("Images/SplashImage2")));
            Camera.IsCheckBounds = false;
            Camera.SetPosition(new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2));
            Camera.Zoom = 1.0f;

            Save = LoadJson<Savings>("savings");
            MusicVolume = Save.SavedMusicVolume;
            SoundVolume = Save.SavedSoundVolume;

            var track1 = LoadSong("Sounds/PHoneMusic1");
            MediaPlayer.Play(track1);
            MediaPlayer.Volume = Save.SavedMusicVolume;

            BlackFieldEdging = new ImageObject(LoadTexture("Images/Square"), new Color(0.1f, 0.1f, 0.1f, 0.75f), new Vector2((_viewportWidth - 1140) / 2 - 12, 118), new Vector2(1140 + 24, 774)) { zIndex = 0.3f };
            AddGameObject(BlackFieldEdging);
            BlackField = new ImageObject(LoadTexture("Images/Square"), new Color(0.2f, 0.2f, 0.2f, 0.75f), new Vector2((_viewportWidth - 1140) / 2, 130), new Vector2(1140, 750)) { zIndex = 0.3f };
            AddGameObject(BlackField);
        }

        private ButtonObject CreateButton(ButtonObject button)
        {
            AddGUIObject(button);
            ButtonsList.Add(button);
            return button;
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is ChooseMenuInputCommand.PlayerMouseHover)
                {
                    foreach (var button in ButtonsList)
                        button.IsHover(((ChooseMenuInputCommand.PlayerMouseHover)cmd).Position);
                }
                if (cmd is ChooseMenuInputCommand.PlayerPressed)
                {
                    foreach (var button in ButtonsList)
                        button.IsPressed(((ChooseMenuInputCommand.PlayerPressed)cmd).Position);
                }
                if (cmd is ChooseMenuInputCommand.ExitToSplash)
                {
                    SwitchState(new SplashState(true));
                }
            });
        }

        public void _Button_OnStartButtonPress(object sender, BaseInputCommand e)
        {
            SwitchState(new GameplayState());
        }

        public void _Button_OnExitButtonPress(object sender, BaseInputCommand e)
        {
            NotifyEvent(new BaseGameStateEvent.GameQuit());
        }

        public override void UpdateGameState(GameTime _)
        {
            foreach (var button in ButtonsList)
            {
                button.Update();
            }
            BlackField.Update();
            BlackFieldEdging.Update();
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new ChooseMenuInputMapper());
        }
    }
}
