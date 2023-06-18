using Strongest_Tank.Engine.GUI;
using Strongest_Tank.Engine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Objects;
using Strongest_Tank.Engine.Particles;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Strongest_Tank.Engine.Objects;
using Strongest_Tank.Input;
using Microsoft.Xna.Framework.Input;
using Strongest_Tank.Engine.Input;

namespace Strongest_Tank.States.Gameplay
{
    public class GameplayUserInterface
    {
        private const string squareTexture = "Images/Square";
        private const string statsBarSpriteFont = "Fonts/TextButton2";
        private const string smallSpriteFont = "Fonts/SmallText";
        private const string averageSpriteFont = "Fonts/AverageTextFont";
        private const string largeSpriteFont = "Fonts/LargeTextFont";
        private const string buttonMenuTexture = "Buttons/ButtonMenu";

        private const string EducationSetupTexture = "Education/EducationSetup";
        private const string EducationButtonOKTexture = "Education/EducationButtonOK";
        private const string EducationText1Texture = "Education/EducationText1";
        private const string EducationText2Texture = "Education/EducationText2";
        private const string EducationText3Texture = "Education/EducationText3";
        private const string EducationText4Texture = "Education/EducationText4";
        private const string EducationText5Texture = "Education/EducationText5";
        private const string EducationText6Texture = "Education/EducationText6";

        private Texture2D _SquareTexture;
        private SpriteFont _StartBarSpriteFont;
        private SpriteFont _SmallSpriteFont;
        private SpriteFont _AverageSpriteFont;
        private SpriteFont _LargeSpriteFont;
        private Texture2D _ButtonMenuTexture;

        private Texture2D _EducationSetupTexture;
        private Texture2D _EducationButtonOKTexture;
        private Texture2D _EducationText1Texture;
        private Texture2D _EducationText2Texture;
        private Texture2D _EducationText3Texture;
        private Texture2D _EducationText4Texture;
        private Texture2D _EducationText5Texture;
        private Texture2D _EducationText6Texture;

        private int _viewportWidth;
        private int _viewportheight;
        private DateTime TimeLastChangeParameters;
        private TimeSpan timeBeforeChangeParameters;
        private int CompanyLevel = 1;
        private bool IsGameEnd = false;
        private bool IsGamePause = false;

        private TankObject player;
        private Func<string, Texture2D> LoadTexture;
        private Action<BaseGUIObject> AddGUIObject;
        private Action<BaseGUIObject> RemoveGUIObject;

        protected StatsBarObject HPStatsBar;
        protected StatsBarObject ReloadStatsBar;
        protected StatsBarObject EnergyStatsBar;
        protected StatsBarObject AttackLevelStatsBar;
        protected StatsBarObject ArmorLevelStatsBar;
        protected TextObject TextCountAndKilledEnemies;
        protected BaseGUIObject BlackFone;
        protected TextObject TextGameCondition;
        private List<ButtonObject> buttonsList = new List<ButtonObject>();
        protected MapObject WorldMap;

        protected ButtonObject EducationButtonContinue;
        protected BaseGUIObject EducationWindow;

        public event EventHandler<BaseInputCommand> EventGoToMenu;

        public GameplayUserInterface(Func<string, Texture2D> LoadTexture, Func<string, SpriteFont> LoadSpriteFont, Action<BaseGUIObject> AddGUIObject, Action<BaseGUIObject> RemoveGUIObject, int viewportWidth, int viewportHeight) 
        {
            this.AddGUIObject = AddGUIObject;
            this.RemoveGUIObject = RemoveGUIObject;
            this.LoadTexture = LoadTexture;
            _viewportWidth = viewportWidth;
            _viewportheight = viewportHeight;
            _SquareTexture = LoadTexture(squareTexture);
            _StartBarSpriteFont = LoadSpriteFont(statsBarSpriteFont);
            _SmallSpriteFont = LoadSpriteFont(smallSpriteFont);
            _AverageSpriteFont = LoadSpriteFont(averageSpriteFont);
            _LargeSpriteFont = LoadSpriteFont(largeSpriteFont);
            _ButtonMenuTexture = LoadTexture(buttonMenuTexture);

            _EducationSetupTexture = LoadTexture(EducationSetupTexture);
            _EducationButtonOKTexture = LoadTexture(EducationButtonOKTexture);
            _EducationText1Texture = LoadTexture(EducationText1Texture);
            _EducationText2Texture = LoadTexture(EducationText2Texture);
            _EducationText3Texture = LoadTexture(EducationText3Texture);
            _EducationText4Texture = LoadTexture(EducationText4Texture);
            _EducationText5Texture = LoadTexture(EducationText5Texture);
            _EducationText6Texture = LoadTexture(EducationText6Texture);
        }

        public void CreateUserInerface(TankObject player, List<TankObject> tanksList, int commandLevel)
        {
            this.player = player;
            CompanyLevel = commandLevel;

            HPStatsBar = new StatsBarObject(_SquareTexture, _StartBarSpriteFont, new Vector2(10, 10), new Vector2(400, 65), new Color(0, 255, 80), new Color(220, 0, 0), 0, player.MaxHealth, true, true);
            AddGUIObject(HPStatsBar);

            ReloadStatsBar = new StatsBarObject(_SquareTexture, new Vector2(10, 105), new Vector2(400, 10), new Color(255, 255, 255), new Color(0, 0, 0), 0, player.ReloadTimeSeconds);
            AddGUIObject(ReloadStatsBar);

            if(CompanyLevel == 1)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено противников :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };
            if (CompanyLevel == 2)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено Баррелей :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };
            if (CompanyLevel == 3)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено противников :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };
            if (CompanyLevel == 4)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено противников :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };
            if (CompanyLevel == 5)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено противников :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };
            if (CompanyLevel == 0)
                TextCountAndKilledEnemies = new TextObject(_AverageSpriteFont, "Уничтожено противников :", new Color(255, 255, 255), new Vector2(10, 150), new Vector2(0, 0)) { IsAnimatedShow = true };

            AddGUIObject(TextCountAndKilledEnemies);

            var ButtonExit = CreateButton(new ButtonObject(_ButtonMenuTexture, new Vector2(_viewportWidth / 2 - 125, _viewportheight / 2 + 200), new Vector2(230, 92), false));
            ButtonExit.OnPress += EventGoToMenu;
            ButtonExit.zIndex = 0;


            if(commandLevel == 0)
            {
                EducationButtonContinue = new ButtonObject(_EducationButtonOKTexture, new Vector2(_viewportWidth / 2 - 40, _viewportheight / 2 + 350 - 85), new Vector2(80, 80), true)
                {
                    DefaultColorTint= new Color(0, 170, 80),
                    ColorPressedButton = new Color(0, 140, 20)
                };
                EducationButtonContinue.OnPress += StartEducation;
                EducationButtonContinue.zIndex = 0.2f;
                AddGUIObject(EducationButtonContinue);

                EducationWindow = new BaseGUIObject(_EducationSetupTexture, new Vector2(_viewportWidth / 2 - 475, _viewportheight / 2 - 350), new Vector2(950, 700));
                EducationWindow.zIndex = 0.25f;
                AddGUIObject(EducationWindow);
            }


            if (player is CyberTank)
            {
                EnergyStatsBar = new StatsBarObject(_SquareTexture, _SmallSpriteFont, new Vector2(10, 75), new Vector2(400, 30), new Color(0, 100, 255), new Color(0, 0, 200), 0, 1000, false, true);
                AddGUIObject(EnergyStatsBar);

                AttackLevelStatsBar = new StatsBarObject(_SquareTexture, new Vector2(10, _viewportheight - 200), new Vector2(300, 25), new Color(255, 60, 60), new Color(100, 0, 0), 0, 7) { Alpha = 0.35f, CurrentValue = 0 };
                AddGUIObject(AttackLevelStatsBar);

                ArmorLevelStatsBar = new StatsBarObject(_SquareTexture, new Vector2(10, _viewportheight - 160), new Vector2(300, 25), new Color(230, 230, 230), new Color(70, 70, 70), 0, 7) { Alpha = 0.35f, CurrentValue = 0 };
                AddGUIObject(ArmorLevelStatsBar);
            }

            WorldMap = new MapObject(_SquareTexture, new Vector2(_viewportWidth - 424, _viewportheight - 424), new Vector2(400, 400))
            {
                Player = player,
                TanksList = tanksList
            };
            AddGUIObject(WorldMap);
        }

        private ButtonObject CreateButton(ButtonObject button)
        {
            AddGUIObject(button);
            buttonsList.Add(button);
            return button;
        }

        public void ChangePlayerParameters(float maxHealth, float maxReloadTime, int attackLevel, int armorLevel)
        {
            HPStatsBar.MaxValue = maxHealth;
            ReloadStatsBar.MaxValue = maxReloadTime;
            AttackLevelStatsBar.CurrentValue = attackLevel;
            ArmorLevelStatsBar.CurrentValue = armorLevel;
            TimeLastChangeParameters = DateTime.Now;
            AttackLevelStatsBar.Show();
            ArmorLevelStatsBar.Show();
        }

        public void ChangeWorldParameters((int, int) countAndKilledEnemies, GameCondition checkGameCondition)
        {
            StringBuilder newText = new StringBuilder();
            if (CompanyLevel == 1)
                newText.Append("Уничтожено противников :");
            if (CompanyLevel == 2)
                newText.Append("Уничтожено баррелей :");
            if (CompanyLevel == 3)
                newText.Append("Уничтожено противников :");
            if (CompanyLevel == 4)
                newText.Append("Уничтожено объектов :");
            if (CompanyLevel == 5)
                newText.Append("Уничтожено противников :");
            if (CompanyLevel == 0)
                newText.Append("Уничтожено противников :");
            newText.Append(countAndKilledEnemies.Item1.ToString());
            newText.Append('/');
            newText.Append(countAndKilledEnemies.Item2.ToString());
            TextCountAndKilledEnemies.UpdateText(newText.ToString());

            if (IsGameEnd)
                return;
            if (checkGameCondition == GameCondition.Pause && !IsGamePause)
            {
                BlackFone = new BaseGUIObject(_SquareTexture, new Vector2(0, 0), new Vector2(_viewportWidth, _viewportheight)) { ColorTint = new Color(0.1f, 0.1f, 0.1f) * 0.7f };
                AddGUIObject(BlackFone);

                TextGameCondition = new TextObject(_LargeSpriteFont, "Пауза", new Color(200, 200, 250), new Vector2(_viewportWidth / 2, _viewportheight / 2), new Vector2(240, 60)) { IsAnimatedShow = true, Alpha = 0 };
                AddGUIObject(TextGameCondition);

                foreach (var button in buttonsList)
                    button.IsVisible = true;
                IsGamePause = true;
            }
            if (checkGameCondition == GameCondition.None && IsGamePause)
            {
                RemoveGUIObject(BlackFone);
                RemoveGUIObject(TextGameCondition);

                foreach (var button in buttonsList)
                    button.IsVisible = false;

                IsGamePause = false;
            }
            if (checkGameCondition == GameCondition.Win)
            {
                BlackFone = new BaseGUIObject(_SquareTexture, new Vector2(0, 0), new Vector2(_viewportWidth, _viewportheight)) { ColorTint = new Color(0.1f, 0.1f, 0.1f) * 0.7f };
                AddGUIObject(BlackFone);

                TextGameCondition = new TextObject(_LargeSpriteFont, "Победа", new Color(40, 255, 50), new Vector2(_viewportWidth / 2, _viewportheight / 2), new Vector2(300, 60)) { IsAnimatedShow = true, Alpha = 0 };
                AddGUIObject(TextGameCondition);

                foreach (var button in buttonsList)
                    button.IsVisible = true;

                HPStatsBar.IsShowing = false;
                ReloadStatsBar.IsShowing = false;
                EnergyStatsBar.IsShowing = false;
                TextCountAndKilledEnemies.Hide();
                IsGameEnd = true;
            }

            if (checkGameCondition == GameCondition.Lose)
            {
                BlackFone = new BaseGUIObject(_SquareTexture, new Vector2(0, 0), new Vector2(_viewportWidth, _viewportheight)) { ColorTint = new Color(0.1f, 0.1f, 0.1f) * 0.7f };
                AddGUIObject(BlackFone);

                TextGameCondition = new TextObject(_LargeSpriteFont, "Поражение", new Color(255, 40, 50), new Vector2(_viewportWidth / 2, _viewportheight / 2), new Vector2(420, 60)) { IsAnimatedShow = true, Alpha = 0 };
                AddGUIObject(TextGameCondition);

                foreach (var button in buttonsList)
                    button.IsVisible = true;

                HPStatsBar.IsShowing = false;
                ReloadStatsBar.IsShowing = false;
                EnergyStatsBar.IsShowing = false;
                TextCountAndKilledEnemies.Hide();
                IsGameEnd = true;
            }
        }

        public void Update()
        {
            HPStatsBar.CurrentValue = player.Health;
            HPStatsBar.Update();

            ReloadStatsBar.CurrentValue = Math.Min((float)(DateTime.Now - player.TimeLastShoot).TotalMilliseconds, ReloadStatsBar.MaxValue);
            ReloadStatsBar.Update();

            TextCountAndKilledEnemies.Update();
            foreach (var button in buttonsList)
            {
                button.Update();
            }
            if (CompanyLevel == 0)
            {
                EducationWindow.Update();
                EducationButtonContinue.Update();
            }

            if (TextGameCondition != null)
            {
                BlackFone.Update();
                TextGameCondition.Update();
            }

            if (player is CyberTank)
            {
                EnergyStatsBar.CurrentValue = ((CyberTank)player).Energy;
                EnergyStatsBar.Update();

                AttackLevelStatsBar.Update();
                ArmorLevelStatsBar.Update();

                timeBeforeChangeParameters = DateTime.Now - TimeLastChangeParameters;
                if (timeBeforeChangeParameters.TotalMilliseconds >= 1500f)
                {
                    AttackLevelStatsBar.Hide();
                    ArmorLevelStatsBar.Hide();
                }
            }
        }

        public void CheckButtonsHover(Vector2 mousePos)
        {
            foreach (var button in buttonsList)
                button.IsHover(mousePos);
        }
        public void CheckButtonsPressed(Vector2 mousePos)
        {
            foreach (var button in buttonsList)
                button.IsPressed(mousePos);
        }

        public void EducationCheckButtonsHover(Vector2 mousePos)
        {
            EducationButtonContinue.IsHover(mousePos);
        }

        public void EducationCheckButtonsPressed(Vector2 mousePos)
        {
            EducationButtonContinue.IsPressed(mousePos);
        }

        private void StartEducation(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText1Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);

            EducationButtonContinue.Size = new Vector2(60, 60);
            EducationButtonContinue.Position = new Vector2(_viewportWidth - 15 - 475/2 - 30, 25 + 350 - 20 - 30);
            EducationButtonContinue.OnPress -= StartEducation;
            EducationButtonContinue.OnPress += EducationGoToText2;
        }

        private void EducationGoToText2(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText2Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);

            EducationButtonContinue.OnPress -= EducationGoToText2;
            EducationButtonContinue.OnPress += EducationGoToText3;
        }

        private void EducationGoToText3(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText3Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);
            EducationButtonContinue.OnPress -= EducationGoToText3;
            EducationButtonContinue.OnPress += EducationGoToText4;
        }

        private void EducationGoToText4(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText4Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);
            EducationButtonContinue.OnPress -= EducationGoToText4;
            EducationButtonContinue.OnPress += EducationGoToText5;
        }

        private void EducationGoToText5(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText5Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);
            EducationButtonContinue.OnPress -= EducationGoToText5;
            EducationButtonContinue.OnPress += EducationGoToText6;
        }

        private void EducationGoToText6(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            EducationWindow = new BaseGUIObject(_EducationText6Texture, new Vector2(_viewportWidth - 490, 25), new Vector2(475, 350))
            {
                ColorTint = new Color(255, 255, 255, 210)
            };
            EducationWindow.zIndex = 0.25f;
            AddGUIObject(EducationWindow);
            EducationButtonContinue.OnPress -= EducationGoToText6;
            EducationButtonContinue.OnPress += EducationEnd;
        }

        private void EducationEnd(object sender, BaseInputCommand e)
        {
            RemoveGUIObject(EducationWindow);
            RemoveGUIObject(EducationButtonContinue);
        }

        public void UpdateMap(List<TankObject> tanksList)
        {
            WorldMap.TanksList = tanksList;
        }
    }

    public class MapObject : BaseGUIObject
    {
        protected BaseGUIObject BorderFone;
        protected BaseGUIObject Fone;
        protected BaseGUIObject MapFriend;
        protected BaseGUIObject MapEnemy;
        protected BaseGUIObject MapPlayer;
        public Vector2 WorldSize = new Vector2(10000, 10000);
        public List<TankObject> TanksList;
        public TankObject Player;

        public MapObject(Texture2D squareTexture, Vector2 pos, Vector2 size) : base(squareTexture, pos, size)
        {
            Position = pos;
            BorderFone = new BaseGUIObject(squareTexture, pos + new Vector2(-12, -12), Size + new Vector2(24, 24)) { zIndex = 0.3f, ColorTint = new Color(30, 30, 30, 150) };
            Fone = new BaseGUIObject(squareTexture, pos, Size) { zIndex = 0.29f, ColorTint = new Color(150, 150, 150, 150) };
            MapFriend = new BaseGUIObject(squareTexture, pos, new Vector2(10, 10)) { zIndex = 0.28f, ColorTint = new Color(30, 220, 40, 220) };
            MapEnemy = new BaseGUIObject(squareTexture, pos, new Vector2(10, 10)) { zIndex = 0.28f, ColorTint = new Color(220, 30, 40, 220) };
            MapPlayer = new BaseGUIObject(squareTexture, pos, new Vector2(10, 10)) { zIndex = 0.28f, ColorTint = new Color(30, 40, 220, 220) };
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            BorderFone.Render(spriteBatch);
            Fone.Render(spriteBatch);

            foreach(TankObject obj in TanksList)
            {
                if (!obj.IsAlive)
                    continue;
                if(obj == Player)
                {
                    MapPlayer.Size = (obj.Body.Size / WorldSize) * Size;
                    MapPlayer.Position = Position + (obj.Body.Position / WorldSize) * Size - MapPlayer.Size / 2;
                    MapPlayer.Render(spriteBatch);
                }
                else if(obj.CommandName == Player.CommandName)
                {
                    MapFriend.Size = (obj.Body.Size / WorldSize) * Size;
                    MapFriend.Position = Position + (obj.Body.Position / WorldSize) * Size - MapFriend.Size / 2;
                    MapFriend.Render(spriteBatch);
                }
                else
                {
                    MapEnemy.Size = (obj.Body.Size / WorldSize) * Size;
                    MapEnemy.Position = Position + (obj.Body.Position / WorldSize) * Size - MapEnemy.Size / 2;
                    MapEnemy.Render(spriteBatch);
                }
            }
        }
    }
}
