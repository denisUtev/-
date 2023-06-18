using Strongest_Tank.Engine.Input;
using Strongest_Tank.Engine.States;
using Strongest_Tank.Input;
using Strongest_Tank.Objects;
using Microsoft.Xna.Framework;
using strongest_Tank;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Strongest_Tank.States.Gameplay;
using Microsoft.Xna.Framework.Input;
using Strongest_Tank.Engine.GUI;
using Strongest_Tank.States.Menu;
using Microsoft.Xna.Framework.Media;
using Strongest_Tank.Engine;
using Strongest_Tank.States.maps;

namespace Strongest_Tank.States
{
    public class SplashState : BaseGameState
    {
        private const string ButtonStartTexture = "Buttons/ButtonStart";
        private const string ButtonExitTexture = "Buttons/ButtonExit";
        private const string ButtonSettingsTexture = "Buttons/ButtonSettings";
        private const string UTanksTexture = "Buttons/UTanks";

        private const string TextFont = "Fonts/TextButton2";

        private bool isVisibleButtonsInStart = false;

        private Texture2D _buttonStartTexture;
        private Texture2D _buttonExitTexture;
        private Texture2D _buttonSettingsTexture;
        private Texture2D _UTanksTexture;

        private ImageObject buttonsField;
        private ImageObject ImageUTanks;
        private List<ButtonObject> buttonsList = new List<ButtonObject>();

        public SplashState(){ }

        public SplashState(bool isVisibleButtons) 
        {
            isVisibleButtonsInStart = isVisibleButtons;
        }

        public override void LoadContent()
        {
            _buttonStartTexture = LoadTexture(ButtonStartTexture);
            _buttonExitTexture = LoadTexture(ButtonExitTexture);
            _buttonSettingsTexture = LoadTexture(ButtonSettingsTexture);
            _UTanksTexture = LoadTexture(UTanksTexture);

            AddGameObject(new SplashImage(LoadTexture("Images/SplashImage2")));
            buttonsField = new ImageObject(LoadTexture("Images/Square"), new Color(0.1f, 0.1f, 0.1f, 0.7f), new Vector2(0, _viewportHeight - 130), new Vector2(_viewportWidth, 130), isVisibleButtonsInStart) { zIndex = 0.3f };
            AddGameObject(buttonsField);
            ImageUTanks = new ImageObject(_UTanksTexture, new Color(50, 50, 50), new Vector2(_viewportWidth / 2 - 250, _viewportHeight / 2 - 100), new Vector2(500, 200), true) { zIndex = 0.3f };
            AddGameObject(ImageUTanks);
            Camera.IsCheckBounds = false;
            Camera.SetPosition(new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2));
            Camera.Zoom = 1.0f;

            Save = LoadJson<Savings>("savings");
            MusicVolume = Save.SavedMusicVolume;
            SoundVolume = Save.SavedSoundVolume;
            
            if (Save.CompanyLevel == 0)
            {
                Savings newSavings = new Savings();
                newSavings.SavedMusicVolume = 0.5f;
                newSavings.SavedSoundVolume = 0.5f;
                SaveJson<Savings>("savings", newSavings);
                MusicVolume = newSavings.SavedMusicVolume;
                SoundVolume = newSavings.SavedSoundVolume;
            }

            var track1 = LoadSong("Sounds/PHoneMusic1");
            MediaPlayer.Play(track1);
            MediaPlayer.Volume = Save.SavedMusicVolume;

            //Button Start
            var buttonStart = CreateButton(new ButtonObject(_buttonStartTexture, new Vector2(_viewportWidth/2 - 87, _viewportHeight - 110), new Vector2(175, 70), isVisibleButtonsInStart));
            buttonStart.OnPress += _Button_OnStartButtonPress;

            //Button End
            var buttonExit = CreateButton(new ButtonObject(_buttonExitTexture, new Vector2(_viewportWidth - 200, _viewportHeight - 110), new Vector2(175, 70), isVisibleButtonsInStart));
            buttonExit.OnPress += _Button_OnExitButtonPress;

            //Button Settings
            var buttonSettings = CreateButton(new ButtonObject(_buttonSettingsTexture, new Vector2(60, _viewportHeight - 130), new Vector2(280, 101.8f), isVisibleButtonsInStart));
            buttonSettings.OnPress += _Button_OnSettingsButtonPress;
        }

        private ButtonObject CreateButton(ButtonObject button)
        {
            AddGUIObject(button);
            buttonsList.Add(button);
            return button;
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.ShowButtons)
                {
                    foreach (var button in buttonsList)
                        button.IsVisible = true;
                    buttonsField.IsVisible = true;
                }
                if (cmd is SplashInputCommand.PlayerMouseHover)
                {
                    foreach (var button in buttonsList)
                        button.IsHover(((SplashInputCommand.PlayerMouseHover)cmd).Position);
                }
                if (cmd is SplashInputCommand.PlayerPressed)
                {
                    foreach (var button in buttonsList)
                        button.IsPressed(((SplashInputCommand.PlayerPressed)cmd).Position);
                }
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
            });
        }

        public void _Button_OnStartButtonPress(object sender, BaseInputCommand e)
        {
            SwitchState(new ChooseGameLevelsState());
        }

        public void _Button_OnSettingsButtonPress(object sender, BaseInputCommand e)
        {
            SwitchState(new SettingsMenuState());
        }

        public void _Button_OnExitButtonPress(object sender, BaseInputCommand e)
        {
            NotifyEvent(new BaseGameStateEvent.GameQuit());
        }

        public override void UpdateGameState(GameTime _) 
        {
            foreach(var button in buttonsList)
            {
                button.Update();
            }
            buttonsField.Update();
            ImageUTanks.Update();
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}