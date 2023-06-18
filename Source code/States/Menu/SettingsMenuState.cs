using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.GUI;
using Strongest_Tank.Engine.Input;
using strongest_Tank;
using Strongest_Tank.Engine.States;
using Strongest_Tank.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Strongest_Tank.Engine;

namespace Strongest_Tank.States.Menu
{
    public class SettingsMenuState : BaseGameState
    {
        private const string MusicTextTexture = "Buttons/MusicText";
        private const string SoundsTextTexture = "Buttons/SoundsText";
        private const string TextFont = "Fonts/TextNameMap2";

        private Texture2D _MusicTextTexture;
        private Texture2D _SoundsTextTexture;

        private ImageObject BlackFieldEdging;
        private ImageObject BlackField;
        private List<ButtonObject> ButtonsList = new List<ButtonObject>();
        private ChangeableStatsBarObject SoundBar;
        private ChangeableStatsBarObject MusicBar;
        private ImageObject ImageMusicText;
        private ImageObject ImageSoundsText;

        public override void LoadContent()
        {
            _MusicTextTexture = LoadTexture(MusicTextTexture);
            _SoundsTextTexture = LoadTexture(SoundsTextTexture);

            AddGameObject(new SplashImage(LoadTexture("Images/SplashImage2")));
            Camera.SetPosition(new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2));

            BlackFieldEdging = new ImageObject(LoadTexture("Images/Square"), new Color(0.1f, 0.1f, 0.1f, 0.75f), new Vector2((_viewportWidth - 1140) / 2 - 12, 118), new Vector2(1140 + 24, 774), true) { zIndex = 0.3f };
            AddGameObject(BlackFieldEdging);
            BlackField = new ImageObject(LoadTexture("Images/Square"), new Color(0.2f, 0.2f, 0.2f, 0.75f), new Vector2((_viewportWidth - 1140) / 2, 130), new Vector2(1140, 750), true) { zIndex = 0.3f };
            AddGameObject(BlackField);

            ImageMusicText = new ImageObject(_MusicTextTexture, new Color(250, 250, 250), new Vector2((_viewportWidth - 1140) / 2 + 20 + 300 + 30, 258), new Vector2(140, 56), true) { zIndex = 0.3f };
            AddGameObject(ImageMusicText);
            ImageSoundsText = new ImageObject(_SoundsTextTexture, new Color(250, 250, 250), new Vector2((_viewportWidth - 1140) / 2 + 20 + 300 + 15, 205), new Vector2(140, 56), true) { zIndex = 0.3f };
            AddGameObject(ImageSoundsText);

            Save = LoadJson<Savings>("savings");
            MusicVolume = Save.SavedMusicVolume;
            SoundVolume = Save.SavedSoundVolume;

            SoundBar = new ChangeableStatsBarObject(LoadTexture("Images/Square"), new Vector2((_viewportWidth - 1140) / 2 + 20, 210), new Vector2(300,40), new Color(0, 80, 255), new Color(20, 20, 20), 0, 100);
            SoundBar.CurrentValue = SoundVolume * 100;
            AddGUIObject(SoundBar);
            MusicBar = new ChangeableStatsBarObject(LoadTexture("Images/Square"), new Vector2((_viewportWidth - 1140) / 2 + 20, 265), new Vector2(300, 40), new Color(30, 200, 80), new Color(20, 20, 20), 0, 100);
            MusicBar.CurrentValue = MusicVolume * 100;
            AddGUIObject(MusicBar);

            var track1 = LoadSong("Sounds/PHoneMusic1");
            MediaPlayer.Play(track1);
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
                if (cmd is ChooseMenuInputCommand.PlayerMouseUpped)
                {
                    SoundBar.MouseUpped();
                    MusicBar.MouseUpped();
                }
                if (cmd is ChooseMenuInputCommand.PlayerPressed)
                {
                    foreach (var button in ButtonsList)
                        button.IsPressed(((ChooseMenuInputCommand.PlayerPressed)cmd).Position);
                    SoundBar.AnalizeMousePos(((ChooseMenuInputCommand.PlayerPressed)cmd).Position);
                    MusicBar.AnalizeMousePos(((ChooseMenuInputCommand.PlayerPressed)cmd).Position);
                }
                if (cmd is ChooseMenuInputCommand.ExitToSplash)
                {
                    SwitchState(new SplashState(true));
                }
            });
        }

        public override void UpdateGameState(GameTime _)
        {
            foreach (var button in ButtonsList)
            {
                button.Update();
            }
            BlackField.Update();
            BlackFieldEdging.Update();
            SoundBar.Update();
            MusicBar.Update();
            ImageMusicText.Update();
            ImageSoundsText.Update();

            MusicVolume = MusicBar.CurrentValue / 100.0f;
            SoundVolume = SoundBar.CurrentValue / 100.0f;
            MediaPlayer.Volume = MusicVolume;
            Savings newSavings = new Savings();
            newSavings.SavedMusicVolume = MusicVolume;
            newSavings.SavedSoundVolume = SoundVolume;
            newSavings.CompanyLevel = Save.CompanyLevel;
            SaveJson<Savings>("savings", newSavings);
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new ChooseMenuInputMapper());
        }
    }
}
