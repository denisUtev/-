using Microsoft.Xna.Framework.Graphics;
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
using Strongest_Tank.States.maps;
using Microsoft.Xna.Framework.Media;

namespace Strongest_Tank.States.Menu
{
    public class ChooseCompanyLevelsState : BaseGameState
    {
        private const string ButtonStartTexture = "Buttons/ButtonStart";
        private const string TextFont = "Fonts/TextNameMap2";
        private const string CompanyLevel1MapTexture = "TextureMaps/CompanyLevel1Map";
        private const string CompanyLevel1DescriptionTexture = "TextureMaps/CompanyLevel1Description";
        private const string CompanyLevel2MapTexture = "TextureMaps/CompanyLevel2Map";
        private const string CompanyLevel2DescriptionTexture = "TextureMaps/CompanyLevel2Description";
        private const string CompanyLevel3MapTexture = "TextureMaps/CompanyLevel3Map";
        private const string CompanyLevel3DescriptionTexture = "TextureMaps/CompanyLevel3Description";
        private const string CompanyLevel4MapTexture = "TextureMaps/CompanyLevel4Map";
        private const string CompanyLevel4DescriptionTexture = "TextureMaps/CompanyLevel4Description";
        private const string CompanyLevel5MapTexture = "TextureMaps/CompanyLevel5Map";
        private const string CompanyLevel5DescriptionTexture = "TextureMaps/CompanyLevel5Description";

        private Texture2D _buttonStartTexture;
        private Texture2D _companyLevel1MapTexture;
        private Texture2D _companyLevel1DescriptionTexture;
        private Texture2D _companyLevel2MapTexture;
        private Texture2D _companyLevel2DescriptionTexture;
        private Texture2D _companyLevel3MapTexture;
        private Texture2D _companyLevel3DescriptionTexture;
        private Texture2D _companyLevel4MapTexture;
        private Texture2D _companyLevel4DescriptionTexture;
        private Texture2D _companyLevel5MapTexture;
        private Texture2D _companyLevel5DescriptionTexture;

        private int _choosedLevel = 1;

        private ImageObject BlackFieldEdging;
        private ImageObject BlackField;
        private ImageObject ImageMap;
        private ImageObject ImageMapDescription;
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

            _buttonStartTexture = LoadTexture(ButtonStartTexture);
            _companyLevel1MapTexture = LoadTexture(CompanyLevel1MapTexture);
            _companyLevel1DescriptionTexture = LoadTexture(CompanyLevel1DescriptionTexture);
            _companyLevel2MapTexture = LoadTexture(CompanyLevel2MapTexture);
            _companyLevel2DescriptionTexture = LoadTexture(CompanyLevel2DescriptionTexture);
            _companyLevel3MapTexture = LoadTexture(CompanyLevel3MapTexture);
            _companyLevel3DescriptionTexture = LoadTexture(CompanyLevel3DescriptionTexture);
            _companyLevel4MapTexture = LoadTexture(CompanyLevel4MapTexture);
            _companyLevel4DescriptionTexture = LoadTexture(CompanyLevel4DescriptionTexture);
            _companyLevel5MapTexture = LoadTexture(CompanyLevel5MapTexture);
            _companyLevel5DescriptionTexture = LoadTexture(CompanyLevel5DescriptionTexture);

            BlackFieldEdging = new ImageObject(LoadTexture("Images/Square"), new Color(0.1f, 0.1f, 0.1f, 0.75f), new Vector2(250 - 12, 118), new Vector2(_viewportWidth - 500 + 24, _viewportHeight - 260 + 24)) { zIndex = 0.3f };
            AddGameObject(BlackFieldEdging);
            BlackField = new ImageObject(LoadTexture("Images/Square"), new Color(0.2f, 0.2f, 0.2f, 0.75f), new Vector2(250, 130), new Vector2(_viewportWidth - 500, _viewportHeight - 260)) { zIndex = 0.3f };
            AddGameObject(BlackField);

            ImageMap = new ImageObject(_companyLevel1MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel1DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
            var buttonStart = CreateButton(new ButtonObject(_buttonStartTexture, new Vector2(1245 - 87, 130 + (_viewportHeight - 260) - 50 - 80), new Vector2(175, 70))
            {
                ColorPressedButton = new Color(250, 250, 0),
                DefaultColorTint = new Color(250, 250, 250),
                zIndex = 0.1f
            });
            buttonStart.OnPress += _Button_StartCompanyLevel;

            for (int i=1; i<=5; i++)
            {
                var pos = new Vector2(380 + (i-1) * 90, 130 + (_viewportHeight - 260) - 50 - 80);
                var newButton = CreateButton(new ButtonObject(LoadTexture("Images/Square"), LoadFont(TextFont), pos, new Vector2(80, 80))
                {
                    Text = Convert.ToString(i),
                    ColorText = new Color(0, 0, 0),
                    ColorPressedButton = new Color(160, 160, 160),
                    DefaultColorTint = Save.CompanyLevel >= i ? new Color(180, 180, 180) : new Color(120, 120, 120)
                });
                if (i == 1)
                    newButton.OnPress += _Button_OnLevel1ButtonPress;
                if (i == 2 && Save.CompanyLevel >= 2)
                    newButton.OnPress += _Button_OnLevel2ButtonPress;
                if (i == 3 && Save.CompanyLevel >= 3)
                    newButton.OnPress += _Button_OnLevel3ButtonPress;
                if (i == 4 && Save.CompanyLevel >= 4)
                    newButton.OnPress += _Button_OnLevel4ButtonPress;
                if (i == 5 && Save.CompanyLevel >= 5)
                    newButton.OnPress += _Button_OnLevel5ButtonPress;
            }
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

        public void _Button_OnLevel1ButtonPress(object sender, BaseInputCommand e)
        {
            _choosedLevel = 1;
            RemoveGameObject(ImageMap);
            RemoveGameObject(ImageMapDescription);
            ImageMap = new ImageObject(_companyLevel1MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel1DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
        }

        public void _Button_OnLevel2ButtonPress(object sender, BaseInputCommand e)
        {
            _choosedLevel = 2;
            RemoveGameObject(ImageMap);
            RemoveGameObject(ImageMapDescription);
            ImageMap = new ImageObject(_companyLevel2MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel2DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
        }

        public void _Button_OnLevel3ButtonPress(object sender, BaseInputCommand e)
        {
            _choosedLevel = 3;
            RemoveGameObject(ImageMap);
            RemoveGameObject(ImageMapDescription);
            ImageMap = new ImageObject(_companyLevel3MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel3DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
        }

        public void _Button_OnLevel4ButtonPress(object sender, BaseInputCommand e)
        {
            _choosedLevel = 4;
            RemoveGameObject(ImageMap);
            RemoveGameObject(ImageMapDescription);
            ImageMap = new ImageObject(_companyLevel4MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel4DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
        }

        public void _Button_OnLevel5ButtonPress(object sender, BaseInputCommand e)
        {
            _choosedLevel = 5;
            RemoveGameObject(ImageMap);
            RemoveGameObject(ImageMapDescription);
            ImageMap = new ImageObject(_companyLevel5MapTexture, new Color(255, 255, 255), new Vector2(300, 160), new Vector2(600, 600)) { zIndex = 0.2f };
            AddGameObject(ImageMap);
            ImageMapDescription = new ImageObject(_companyLevel5DescriptionTexture, new Color(255, 255, 255), new Vector2(960, 160), new Vector2(570, 660)) { zIndex = 0.2f };
            AddGameObject(ImageMapDescription);
        }

        private void _Button_StartCompanyLevel(object sender, BaseInputCommand e)
        {
            if(_choosedLevel == 1)
                SwitchState(new GameplayState() { CompanyLevel = 1 });
            if (_choosedLevel == 2)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 2,
                    CountBarrels = 0,
                    jsonMapString = MapJson.CompanyLevel2Buildings,
                    jsonMovesString = MapJson.CompanyLevel2Moves
                });
            if (_choosedLevel == 3)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 3,
                    jsonMapString = MapJson.CompanyLevel3Buildings,
                    jsonMovesString = MapJson.CompanyLevel3Moves
                });
            if (_choosedLevel == 4)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 4,
                    jsonMapString = MapJson.CompanyLevel4Buildings,
                    jsonMovesString = MapJson.CompanyLevel4Moves
                });
            if (_choosedLevel == 5)
                SwitchState(new GameplayState()
                {
                    CompanyLevel = 5,
                    jsonMapString = MapJson.CompanyLevel5Buildings,
                    jsonMovesString = MapJson.CompanyLevel5Moves
                });
        }

        public override void UpdateGameState(GameTime _)
        {
            ButtonsList[_choosedLevel].ColorTint = Color.White;

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
