using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.GUI;
using Strongest_Tank.Engine.Input;
using strongest_Tank;
using Strongest_Tank.Engine.States;
using Strongest_Tank.Input;
using Strongest_Tank.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strongest_Tank.Engine.Objects;
using Microsoft.Xna.Framework.Media;
using Strongest_Tank.Engine;
using Strongest_Tank.States.maps;

namespace Strongest_Tank.States.Menu
{
    public class ChooseGameLevelsState : BaseGameState
    {
        private const string ButtonChallengeMenuTexture = "Buttons/ButtonEducation";
        private const string ButtonCompanyMenuTexture = "Buttons/ButtonCompany";
        private const string TextFont = "Fonts/TextNameMap2";

        private Texture2D _buttonChallengeMenuTexture;
        private Texture2D _buttonCompanyMenuTexture;

        private ImageObject BlackFieldEdging;
        private ImageObject BlackField;
        private List<ButtonObject> ButtonsList = new List<ButtonObject>();

        public override void LoadContent()
        {
            _buttonChallengeMenuTexture = LoadTexture(ButtonChallengeMenuTexture);
            _buttonCompanyMenuTexture = LoadTexture(ButtonCompanyMenuTexture);

            AddGameObject(new SplashImage(LoadTexture("Images/SplashImage2")));
            Camera.SetPosition(new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2));

            BlackFieldEdging = new ImageObject(LoadTexture("Images/Square"), new Color(0.1f, 0.1f, 0.1f, 0.75f), new Vector2((_viewportWidth - 1140) / 2 - 12, 118), new Vector2(1140 + 24, 774), true) { zIndex = 0.3f };
            AddGameObject(BlackFieldEdging);
            BlackField = new ImageObject(LoadTexture("Images/Square"), new Color(0.2f, 0.2f, 0.2f, 0.75f), new Vector2((_viewportWidth - 1140) / 2, 130), new Vector2(1140, 750), true) { zIndex = 0.3f };
            AddGameObject(BlackField);

            Save = LoadJson<Savings>("savings");
            MusicVolume = Save.SavedMusicVolume;
            SoundVolume = Save.SavedSoundVolume;

            if(Save.CompanyLevel== 0)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 0,
                    jsonMapString = MapJson.CompanyLevel0Buildings,
                    jsonMovesString = MapJson.CompanyLevel0Moves
                });

            var track1 = LoadSong("Sounds/PHoneMusic1");
            MediaPlayer.Play(track1);
            MediaPlayer.Volume = Save.SavedMusicVolume;

            //Button Challenge Menu
            var buttonStart = CreateButton(new ButtonObject(_buttonChallengeMenuTexture, new Vector2((_viewportWidth - 1140) / 2 + 15, 160), new Vector2((_viewportWidth - 800 - 60) / 2, 750 - 60), true) { ColorPressedButton = new Color(220, 220, 255) });
            buttonStart.OnPress += _Button_OnChooseEducationButtonPress;
             
            //Button Company Menu
            var buttonCompany = CreateButton(new ButtonObject(_buttonCompanyMenuTexture, new Vector2((_viewportWidth - 1140) / 2 + 1140 - 15 - 530, 160), new Vector2((_viewportWidth - 800 - 60) / 2, 750 - 60), true) { ColorPressedButton = new Color(220, 220, 255) }); // size = (530, 690);
            buttonCompany.OnPress += _Button_OnChooseCompanyButtonPress;
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

        public void _Button_OnChooseCompanyButtonPress(object sender, BaseInputCommand e)
        {
            if (Save.CompanyLevel == 0)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 0,
                    jsonMapString = MapJson.CompanyLevel0Buildings,
                    jsonMovesString = MapJson.CompanyLevel0Moves
                });
            else
                SwitchState(new ChooseCompanyLevelsState());
        }

        public void _Button_OnChooseEducationButtonPress(object sender, BaseInputCommand e)
        {
            SwitchState(new GameplayState()
            {
                CompanyLevel = 0,
                jsonMapString = MapJson.CompanyLevel0Buildings,
                jsonMovesString = MapJson.CompanyLevel0Moves
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
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new ChooseMenuInputMapper());
        }
    }
}
