using Strongest_Tank.Engine.Input;
using Strongest_Tank.Engine.Objects;
using Strongest_Tank.Engine.States;
using Strongest_Tank.Input;
using Strongest_Tank.Objects;
using Strongest_Tank.States.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using strongest_Tank;
using Strongest_Tank.Engine.Phisics;
using Strongest_Tank.Engine.GUI;
using System.Text.Json;
using System.Text.Json.Serialization;
using Strongest_Tank.States.maps;
using Strongest_Tank.Engine.Particles;
using System.Linq;
using Strongest_Tank.States.Menu;
using static Strongest_Tank.States.Gameplay.GameplayEvents;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Strongest_Tank.Engine;

namespace Strongest_Tank.States
{
    public class GameplayState : BaseGameState
    {
        #region Texture names
        private const string PlayerTankCorpusTexture = "Images/cybertank_base";
        private const string PlayerTankTurretTexture = "Images/cybertank_turret";
        private const string UltraBulletTexture = "Images/ultraBullet";
        private const string BulletTexture = "Images/Bullet";
        private const string TankCorpusTexture = "Images/tank_base";
        private const string TankTurretTexture = "Images/tank_turret";
        private const string Tank2CorpusTexture = "Images/tank2_base";
        private const string Tank2TurretTexture = "Images/tank2_turret";
        private const string Tank3CorpusTexture = "Images/tank3_base";
        private const string Tank3TurretTexture = "Images/tank3_turret";
        private const string JeepCorpusTexture = "Images/jeep_base";
        private const string JeepTurretTexture = "Images/jeep_turret";
        private const string HeavyTankCorpusTexture = "Images/HeavyTankKorpus";
        private const string HeavyTankTurretTexture = "Images/HeavyTankTurret";
        private const string STTankCorpusTexture = "Images/STTankKorpus";
        private const string STTankTurretTexture = "Images/STTankTurret";
        private const string IS7TankCorpusTexture = "Images/IS7TankKorpus";
        private const string IS7TankTurretTexture = "Images/IS7TankTurret";
        private const string WhiteTigerCorpusTexture = "Images/whiteTiger_base";
        private const string WhiteTigerTurretTexture = "Images/whiteTigerTurret";
        private const string YagaTankTexture = "Images/Yaga";
        private const string DotTankTexture = "Images/Dot";
        private const string fieldTexture = "Images/Square";
        private const string blockTexture = "Images/block";
        private const string block2Texture = "Images/block2";
        private const string tree1Texture = "Images/tree1";
        private const string brokedTree1Texture = "Images/brokedTree1";
        private const string tree2Texture = "Images/tree2";
        private const string brokedTree2Texture = "Images/brokedTree2";
        private const string tree3Texture = "Images/tree3";
        private const string bush1Texture = "Images/bush1";
        private const string bush2Texture = "Images/bush2";
        private const string bush3Texture = "Images/bush3";
        private const string trunkTexture = "Images/trunk";
        private const string house1Texture = "Images/house1";
        private const string house2Texture = "Images/house2";
        private const string house3Texture = "Images/house3";
        private const string rock1Texture = "Images/rock1";
        private const string rock2Texture = "Images/rock2";
        private const string grass1Texture = "Images/grass1";
        private const string particleTexture = "Images/particle";
        private const string forceFieldTexture = "Images/forceField";
        private const string barrelTexture = "Images/Barrel";
        private const string barrel2Texture = "Images/Barrel2";
        private const string machineTexture = "Images/Machine";
        private const string factoryFloorTexture = "Images/FactoryFloor";
        private const string CityFloorTexture = "Images/CityFloor";
        private const string CityFloor2Texture = "Images/CityFloor2";
        private const string MonumentTexture = "Images/Monument";
        #endregion

        #region Textures
        private Texture2D _cybertankCorpusTexture;
        private Texture2D _cybertankTurretTexture;
        private Texture2D _ultraBulletTexture;
        private Texture2D _bulletTexture;
        private Texture2D _tankCorpusTexture;
        private Texture2D _tankTurretTexture;
        private Texture2D _tank2CorpusTexture;
        private Texture2D _tank2TurretTexture;
        private Texture2D _tank3CorpusTexture;
        private Texture2D _tank3TurretTexture;
        private Texture2D _jeepCorpusTexture;
        private Texture2D _jeepTurretTexture;
        private Texture2D _STTankCorpusTexture;
        private Texture2D _STTankTurretTexture;
        private Texture2D _IS7TankCorpusTexture;
        private Texture2D _IS7TankTurretTexture;
        private Texture2D _screenBoxTexture;
        private Texture2D _fieldTexture;
        private Texture2D _blockTexture;
        private Texture2D _block2Texture;
        private Texture2D _tree1Texture;
        private Texture2D _brokedTree1Texture;
        private Texture2D _tree2Texture;
        private Texture2D _brokedTree2Texture;
        private Texture2D _tree3Texture;
        private Texture2D _bush1Texture;
        private Texture2D _bush2Texture;
        private Texture2D _bush3Texture;
        private Texture2D _trunkTexture;
        private Texture2D _house1Texture;
        private Texture2D _house2Texture;
        private Texture2D _house3Texture;
        private Texture2D _rock1Texture;
        private Texture2D _rock2Texture;
        private Texture2D _grass1Texture;
        private Texture2D _particleTexture;
        private Texture2D _HeavyTankCorpusTexture;
        private Texture2D _HeavyTankTurretTexture;
        private Texture2D _YagaTankTexture;
        private Texture2D _DotTankTexture;
        private Texture2D _ForceFieldTexture;
        private Texture2D _BarrelTexture;
        private Texture2D _Barrel2Texture;
        private Texture2D _MachineTexture;
        private Texture2D _FactoryFloorTexture;
        private Texture2D _CityFloorTexture;
        private Texture2D _CityFloor2Texture;
        private Texture2D _whiteTigerCorpusTexture;
        private Texture2D _whiteTigerTurretTexture;
        private Texture2D _monumentTexture;
        #endregion

        public string jsonMapString = MapJson.CompanyLevel1Buildings;
        public string jsonMovesString = MapJson.CompanyLevel1Moves;
        public const int MapSizeX = 10000;
        public const int MapSizeY = 10000;
        public int CountEnemies = 0;
        public int CountBarrels = -10000;
        public int CountKilledBarrels = 0;
        public int CountKilledEnemies = 0;
        public int CompanyLevel = -1;
        public GameCondition CurrentGameCondition = GameCondition.None;

        private List<AIBotST1> AItanksList = new List<AIBotST1>();
        private List<TankObject> tanksList = new List<TankObject>();
        private List<BulletObject> bulletsList = new List<BulletObject>();
        private List<BaseGameObject> buildingsList = new List<BaseGameObject>();
        private List<Explosion> ExplosionsList = new List<Explosion>();
        private List<SpawnerObject> SpawnersList = new List<SpawnerObject>();
        TankObject TankPlayer;
        FieldObject field;
        private Phisics fisica;
        GameplayUserInterface gameplayUserInterface;

        public override void LoadContent()
        {
            _cybertankCorpusTexture = LoadTexture(PlayerTankCorpusTexture);
            _cybertankTurretTexture = LoadTexture(PlayerTankTurretTexture);
            _ultraBulletTexture = LoadTexture(UltraBulletTexture);
            _bulletTexture = LoadTexture(BulletTexture);
            _tankCorpusTexture = LoadTexture(TankCorpusTexture);
            _tankTurretTexture = LoadTexture(TankTurretTexture);
            _tank2CorpusTexture = LoadTexture(Tank2CorpusTexture);
            _tank2TurretTexture = LoadTexture(Tank2TurretTexture);
            _tank3CorpusTexture = LoadTexture(Tank3CorpusTexture);
            _tank3TurretTexture = LoadTexture(Tank3TurretTexture);
            _jeepCorpusTexture = LoadTexture(JeepCorpusTexture);
            _jeepTurretTexture = LoadTexture(JeepTurretTexture);
            _fieldTexture = LoadTexture(fieldTexture);
            _blockTexture = LoadTexture(blockTexture);
            _block2Texture = LoadTexture(block2Texture);
            _tree1Texture = LoadTexture(tree1Texture);
            _brokedTree1Texture = LoadTexture(brokedTree1Texture);
            _tree2Texture = LoadTexture(tree2Texture);
            _brokedTree2Texture = LoadTexture(brokedTree2Texture);
            _tree3Texture = LoadTexture(tree3Texture);
            _bush1Texture = LoadTexture(bush1Texture);
            _bush2Texture = LoadTexture(bush2Texture);
            _bush3Texture = LoadTexture(bush3Texture);
            _trunkTexture = LoadTexture(trunkTexture);
            _house1Texture = LoadTexture(house1Texture);
            _house2Texture = LoadTexture(house2Texture);
            _house3Texture = LoadTexture(house3Texture);
            _rock1Texture = LoadTexture(rock1Texture);
            _rock2Texture = LoadTexture(rock2Texture);
            _grass1Texture = LoadTexture(grass1Texture);
            _screenBoxTexture = LoadTexture(fieldTexture);
            _particleTexture = LoadTexture(particleTexture);
            _HeavyTankCorpusTexture = LoadTexture(HeavyTankCorpusTexture);
            _HeavyTankTurretTexture = LoadTexture(HeavyTankTurretTexture);
            _YagaTankTexture = LoadTexture(YagaTankTexture);
            _STTankCorpusTexture = LoadTexture(STTankCorpusTexture);
            _STTankTurretTexture = LoadTexture(STTankTurretTexture);
            _ForceFieldTexture = LoadTexture(forceFieldTexture);
            _BarrelTexture = LoadTexture(barrelTexture);
            _Barrel2Texture = LoadTexture(barrel2Texture);
            _IS7TankCorpusTexture = LoadTexture(IS7TankCorpusTexture);
            _IS7TankTurretTexture = LoadTexture(IS7TankTurretTexture);
            _DotTankTexture = LoadTexture(DotTankTexture);
            _MachineTexture = LoadTexture(machineTexture);
            _FactoryFloorTexture = LoadTexture(factoryFloorTexture);
            _CityFloorTexture = LoadTexture(CityFloorTexture);
            _CityFloor2Texture = LoadTexture(CityFloor2Texture);
            _whiteTigerCorpusTexture = LoadTexture(WhiteTigerCorpusTexture);
            _whiteTigerTurretTexture = LoadTexture(WhiteTigerTurretTexture);
            _monumentTexture = LoadTexture(MonumentTexture);

            Save = LoadJson<Savings>("savings");
            MusicVolume = Save.SavedMusicVolume;
            SoundVolume = Save.SavedSoundVolume;

            var bulletSound = LoadSound("Sounds/TankShoot");
            _soundManager.RegisterSound(new GameplayEvents.TankShootMusic(), bulletSound, 0.3f * SoundVolume, 0.5f, 1);
            bulletSound = LoadSound("Sounds/CyberTankShoot");
            _soundManager.RegisterSound(new GameplayEvents.CyberTankShootMusic(), bulletSound, 0.9f * SoundVolume, 1, 0.5f);
            bulletSound = LoadSound("Sounds/DestroyTank");
            _soundManager.RegisterSound(new GameplayEvents.DestroyTankMusic(), bulletSound, 0.3f * SoundVolume, 1, 0.5f);
            bulletSound = LoadSound("Sounds/TankRegen");
            _soundManager.RegisterSound(new GameplayEvents.RegenTankMusic(), bulletSound, 0.2f * SoundVolume, 1, 0.5f);
            bulletSound = LoadSound("Sounds/ForceField");
            _soundManager.RegisterSound(new GameplayEvents.ForceFieldMusic(), bulletSound, 0.08f * SoundVolume, 1, 1);

            var track1 = LoadSong("Sounds/PhoneMusic2");
            MediaPlayer.Play(track1);
            MediaPlayer.Volume = MusicVolume;
            MediaPlayer.IsRepeating = true;
            
            gameplayUserInterface = new GameplayUserInterface(LoadTexture, LoadFont, AddGUIObject, RemoveGUIObject, _viewportWidth, _viewportHeight);
            Camera.MapSizeX = MapSizeX;
            Camera.MapSizeY = MapSizeY;
            Camera.IsCheckBounds = true;
            ResetGame();
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.GamePause)
                {
                    if (CurrentGameCondition == GameCondition.Pause)
                        CurrentGameCondition = GameCondition.None;
                    else if (CurrentGameCondition == GameCondition.None)
                        CurrentGameCondition = GameCondition.Pause;
                }
                if (cmd is GameplayInputCommand.PlayerMouseHover)
                {
                    if(CurrentGameCondition != GameCondition.None)
                        gameplayUserInterface.CheckButtonsHover(((GameplayInputCommand.PlayerMouseHover)cmd).Position);
                    if (CompanyLevel == 0)
                        gameplayUserInterface.EducationCheckButtonsHover(((GameplayInputCommand.PlayerMouseHover)cmd).Position);
                }
                if (cmd is GameplayInputCommand.PlayerMousePressed)
                {
                    if(CurrentGameCondition != GameCondition.None)
                        gameplayUserInterface.CheckButtonsPressed(((GameplayInputCommand.PlayerMousePressed)cmd).Position);
                    if(CompanyLevel == 0)
                        gameplayUserInterface.EducationCheckButtonsPressed(((GameplayInputCommand.PlayerMousePressed)cmd).Position);
                }
                if (CurrentGameCondition != GameCondition.None)
                    return;
                if (cmd is GameplayInputCommand.PlayerMoveUp)
                {
                   TankPlayer.MoveUp();
                }
                if (cmd is GameplayInputCommand.PlayerMoveDown)
                {
                    TankPlayer.MoveDown();
                }
                if (cmd is GameplayInputCommand.PlayerCorpusRotateLeft)
                {
                    TankPlayer.RotateCorpus(-1);
                }
                if (cmd is GameplayInputCommand.PlayerCorpusRotateRight)
                {
                    TankPlayer.RotateCorpus(1);
                }
                if (cmd is GameplayInputCommand.PlayerShoots)
                {
                    TankPlayer.Shooting();
                }
                if (cmd is GameplayInputCommand.PlayerRotateTurret)
                {
                    Vector2 mouse = ((GameplayInputCommand.PlayerRotateTurret) cmd).MousePos / Camera.Zoom
                        - TankPlayer.Body.Position
                        + Camera.Position
                        - new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2) / Camera.Zoom;
                    TankPlayer.SetRotationTurret((float)Math.Atan2(mouse.Y,
                        mouse.X));
                }
                if (cmd is GameplayInputCommand.PlayerIncreaseAttackLevel)
                {
                    ((CyberTank)TankPlayer).ChangeAttackLevel(1);
                    gameplayUserInterface.ChangePlayerParameters(TankPlayer.MaxHealth, TankPlayer.ReloadTimeSeconds, ((CyberTank)TankPlayer).AttackLevel, ((CyberTank)TankPlayer).ArmorLevel);
                }
                if (cmd is GameplayInputCommand.PlayerDecreaseAttackLevel)
                {
                    ((CyberTank)TankPlayer).ChangeAttackLevel(-1);
                    gameplayUserInterface.ChangePlayerParameters(TankPlayer.MaxHealth, TankPlayer.ReloadTimeSeconds, ((CyberTank)TankPlayer).AttackLevel, ((CyberTank)TankPlayer).ArmorLevel);
                }
                if (cmd is GameplayInputCommand.PlayerIncreaseArmorLevel)
                {
                    ((CyberTank)TankPlayer).ChangeArmorLevel(1);
                    gameplayUserInterface.ChangePlayerParameters(TankPlayer.MaxHealth, TankPlayer.ReloadTimeSeconds, ((CyberTank)TankPlayer).AttackLevel, ((CyberTank)TankPlayer).ArmorLevel);
                }
                if (cmd is GameplayInputCommand.PlayerDecreaseArmorLevel)
                {
                    ((CyberTank)TankPlayer).ChangeArmorLevel(-1);
                    gameplayUserInterface.ChangePlayerParameters(TankPlayer.MaxHealth, TankPlayer.ReloadTimeSeconds, ((CyberTank)TankPlayer).AttackLevel, ((CyberTank)TankPlayer).ArmorLevel);
                }
                if (cmd is GameplayInputCommand.PlayerShootLazer)
                {
                    ((CyberTank)TankPlayer).ShootLazer();
                }
                if (cmd is GameplayInputCommand.PlayerActivateForceField)
                {
                    ((CyberTank)TankPlayer).ActivateForceField();
                    NotifyEvent(new GameplayEvents.ForceFieldMusic());
                }
                if (cmd is GameplayInputCommand.PlayerRegen)
                {
                    ((CyberTank)TankPlayer).Regen();
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            Camera.SetPosition(TankPlayer.Body.Position);
            Camera.Zooming(Mouse.GetState().ScrollWheelValue / 2000.0f);
            gameplayUserInterface.Update();
            if(CompanyLevel == 1)
                gameplayUserInterface.ChangeWorldParameters((CountKilledEnemies, CountEnemies), CurrentGameCondition);
            if (CompanyLevel == 2)
                gameplayUserInterface.ChangeWorldParameters((CountKilledBarrels, CountBarrels), CurrentGameCondition);
            if (CompanyLevel == 3)
                gameplayUserInterface.ChangeWorldParameters((CountKilledEnemies, CountEnemies), CurrentGameCondition);
            if (CompanyLevel == 4)
                gameplayUserInterface.ChangeWorldParameters((CountKilledEnemies, CountEnemies), CurrentGameCondition);
            if (CompanyLevel == 5)
                gameplayUserInterface.ChangeWorldParameters((CountKilledEnemies, CountEnemies), CurrentGameCondition);
            if (CompanyLevel == 0)
                gameplayUserInterface.ChangeWorldParameters((CountKilledEnemies, CountEnemies), CurrentGameCondition);

            gameplayUserInterface.UpdateMap(tanksList);

            if (CurrentGameCondition == GameCondition.Pause)
                return;

            UpdatePhisics(gameTime);
            fisica.UpdatePhisics();

            CheckGameCondition();

            foreach (var tank in tanksList)
            {
                tank.Update();
            }
            foreach (var bullet in bulletsList)
            {
                bullet.Update();
            }
            foreach (var explosion in ExplosionsList)
            {
                explosion.Update();
            }
            if(CurrentGameCondition == GameCondition.None)
                foreach (var AITank in AItanksList)
                {
                    AITank.Update(AItanksList, buildingsList);
                }
            foreach (var spawner in SpawnersList)
            {
                spawner.Update();
            }


            CleanObjects();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            foreach (var explosion in ExplosionsList)
            {
                explosion.Render(spriteBatch);
            }
        }

        public void CheckGameCondition()
        {
            if (!TankPlayer.IsAlive)
                CurrentGameCondition = GameCondition.Lose;
            if (CountKilledEnemies >= CountEnemies && CountBarrels < 0 || CountBarrels - CountKilledBarrels == 0)
            {
                CurrentGameCondition = GameCondition.Win;
                Savings newSavings = new Savings();
                newSavings.SavedMusicVolume = MusicVolume;
                newSavings.SavedSoundVolume = SoundVolume;
                newSavings.CompanyLevel = Math.Max(CompanyLevel + 1, Save.CompanyLevel);
                SaveJson<Savings>("savings", newSavings);
            }
        }

        public void PlayerLose()
        {
            CurrentGameCondition = GameCondition.Lose;
        }

        private void ResetGame()
        {
            CurrentGameCondition = GameCondition.None;
            AItanksList = new List<AIBotST1>();
            tanksList = new List<TankObject>();
            buildingsList = new List<BaseGameObject>();
            bulletsList = new List<BulletObject>();
            SpawnersList = new List<SpawnerObject>();

            fisica = new Phisics();
            fisica.OnColliseObjects += _fisica_OnColliseObjects;
            field = new FieldObject(_fieldTexture);
            AddGameObject(field);

            LoadMap();
            gameplayUserInterface.EventGoToMenu += GoToMenu;
            gameplayUserInterface.CreateUserInerface(TankPlayer, tanksList, CompanyLevel);
        }
        
        private class MapList
        {
            public IList<MapObject> buildings { get; set; }
        }

        private class MapObject
        {
            public float sizeX { get; set; }
            public float sizeY { get; set; }
            public float x { get; set; }
            public float y { get; set; }
            public int id { get; set; }
            public float angle { get; set; }
            public string name { get; set; }
        }

        private class MovesList
        {
            public IList<moveObject> moves { get; set; }
        }

        private class moveObject
        {
            public string move { get; set; }
        }

        private void LoadMap()
        {
            var map = JsonSerializer.Deserialize<MapList>(jsonMapString);
            var moves = JsonSerializer.Deserialize<MovesList>(jsonMovesString);

            foreach (var building in map.buildings)
            {
                if (building.name == "tank3_base")
                    CreateTankST3(new Vector2(building.x, building.y), false);
                if (building.name == "tank2_base")
                    CreateTankST2(new Vector2(building.x, building.y), false);
                if (building.name == "tank_base")
                    CreateTankST1(new Vector2(building.x, building.y), false);
                if (building.name == "HeavyTankKorpus")
                    CreateHeavyTank(new Vector2(building.x, building.y), false);
                if (building.name == "Yaga")
                    CreateYagaTank(new Vector2(building.x, building.y), false);
                if (building.name == "jeep_base")
                    CreateJeep(new Vector2(building.x, building.y), false);
                if (building.name == "STTankKorpus")
                    CreateTankST4(new Vector2(building.x, building.y));
                if (building.name == "whiteTiger_base")
                    CreateWhiteTiger(new Vector2(building.x, building.y));
                if (building.name == "cybertank_base")
                    TankPlayer = CreateTankPlayer(new Vector2(building.x, building.y));
                if (building.name == "Dot")
                    TankPlayer = CreateTankDot(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY));
                if (building.name == "IS7TankKorpus")
                    TankPlayer = CreateTankIS7(new Vector2(building.x, building.y));
                if (building.name == "Block2")
                    CreateAliveBuilding(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY));
                if (building.name == "Barrel")
                    CreateBarrel(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY));
                if (building.name == "Barrel2")
                    CreateBarrel2(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), building.angle);
                if (building.name == "Machine")
                    CreateMachine(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY));
                if (building.name == "tankFactoryKorpus")
                    CreateTankKorpus(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY));

                if (building.name == "LTTankSpawner")
                    CreateLTTankSpawner(new Vector2(building.x, building.y));
                if (building.name == "STTankSpawner")
                    CreateSTTankSpawner(new Vector2(building.x, building.y));
                if (building.name == "TTTankSpawner")
                    CreateTTTankSpawner(new Vector2(building.x, building.y));

                if (building.name == "tree1")
                    buildingsList.Add(CreateTree1(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY)));
                if (building.name == "tree2")
                    buildingsList.Add(CreateTree2(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY)));
                if (building.name == "tree3")
                    buildingsList.Add(CreateTree3(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY)));
                if (building.name == "block")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_blockTexture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY))));
                if (building.name == "bush1")
                    buildingsList.Add(CreateBush1(new Vector2(building.x, building.y)));
                if (building.name == "bush2")
                    buildingsList.Add(CreateBush2(new Vector2(building.x, building.y)));
                if (building.name == "bush3")
                    buildingsList.Add(CreateBush3(new Vector2(building.x, building.y)));
                if (building.name == "rock1")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_rock1Texture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY) / 2.1f, new Vector2(building.sizeX, building.sizeY), Form.CIRCLE)));
                if (building.name == "rock2")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_rock2Texture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY) / 2.1f, new Vector2(building.sizeX, building.sizeY), Form.CIRCLE)));
                if (building.name == "house1")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_house1Texture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), new Vector2(building.sizeX, building.sizeY) * 1.2f)));
                if (building.name == "house2")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_house2Texture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), new Vector2(building.sizeX, building.sizeX) * 1.125f)));
                if (building.name == "house3")
                    buildingsList.Add(CreateBuildingObject(new BuildingObject(_house3Texture, new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), new Vector2(building.sizeX, building.sizeX) * 1.175f)));
                if (building.name == "grass1")
                    buildingsList.Add(CreateGrass1(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY)));
                if (building.name == "FactoryFloor")
                    buildingsList.Add(CreateFactoryFloor(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY)));
                if (building.name == "CityFloor")
                    buildingsList.Add(CreateCityFloor(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), building.angle));
                if (building.name == "CityFloor2")
                    buildingsList.Add(CreateCityFloor2(new Vector2(building.x, building.y), new Vector2(building.sizeX, building.sizeY), building.angle));

            }

            int i = 0, j = 0;
            foreach (var move in moves.moves)
            {
                AIBotST1.Moves[i, j] = move.move;
                i++;
                if (i == 100)
                {
                    j++;
                    i = 0;
                }
            }
            
            if(CompanyLevel == 3)
                buildingsList.Add(CreateBuildingObject(new BuildingObject(_monumentTexture, new Vector2(MapSizeX / 2, MapSizeY / 2), new Vector2(250, 250))));
        }

        private TankObject CreateTankPlayer(Vector2 pos)
        {
            var TankPlayer = new CyberTank(_cybertankCorpusTexture, _cybertankTurretTexture, _ForceFieldTexture, _particleTexture,
                pos, new Vector2(100, 100));
            TankPlayer.OnShootBullet += _Tank_OnTankShoots;
            TankPlayer.OnDestroy += _Tank_OnTankDestroy;
            TankPlayer.OnShootLazer += _Tank_OnTankShoots;
            TankPlayer.OnRegen += _CyberTank_OnTankRegen;
            AddGameObject(TankPlayer);
            fisica.AddObject(TankPlayer);
            fisica.SetPlayerObject(TankPlayer);
            tanksList.Add(TankPlayer);
            TankPlayer.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, TankPlayer.Body.Position, new Vector2(100, 24), 
                new Color(0, 110, 255), new Color(0, 0, 255), 0, TankPlayer.MaxHealth, true));

            AItanksList.Add(new AIPlayer(TankPlayer, "player"));
            return TankPlayer;
        }

        private TankObject CreateWhiteTiger(Vector2 pos)
        {
            var newTank = new WhiteTigerTank(_whiteTigerCorpusTexture, _whiteTigerTurretTexture, _particleTexture,
                pos, new Vector2(180, 180));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(150, 30),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST3(newTank, "bot"));
            CountEnemies++;
            return newTank;
        }
        private TankObject CreateTankST1(Vector2 pos, bool isSpawn)
        {
            var newTank = new TankST1(_tankCorpusTexture, _tankTurretTexture, _particleTexture,
                pos, new Vector2(140, 100));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST1(newTank, "bot"));
            if(!isSpawn)
                CountEnemies++;
            return newTank;
        }
        private TankObject CreateTankST2(Vector2 pos, bool isSpawn)
        {
            var newTank = new TankST2(_tank2CorpusTexture, _tank2TurretTexture, _particleTexture,
                pos, new Vector2(140, 100));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST1(newTank, "bot"));
            if (!isSpawn)
                CountEnemies++;
            return newTank;
        }
        private TankObject CreateTankST3(Vector2 pos, bool isSpawn)
        {
            var newTank = new TankST3(_tank3CorpusTexture, _tank3TurretTexture, _particleTexture,
                pos, new Vector2(140, 100));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST1(newTank, "bot"));
            if (!isSpawn)
                CountEnemies++;
            return newTank;
        }
        private TankObject CreateJeep(Vector2 pos, bool isSpawn)
        {
            var newJeep = new Jeep(_jeepCorpusTexture, _jeepTurretTexture, _particleTexture,
                pos, new Vector2(160, 90));
            newJeep.OnShootBullet += _Tank_OnTankShoots;
            newJeep.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newJeep);
            fisica.AddObject(newJeep);
            tanksList.Add(newJeep);
            newJeep.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newJeep.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newJeep.MaxHealth, true));

            AItanksList.Add(new AIBotST1(newJeep, "bot"));
            if (!isSpawn)
                CountEnemies++;
            return newJeep;
        }
        private TankObject CreateHeavyTank(Vector2 pos, bool isSpawn)
        {
            var newTank = new HeavyTank(_HeavyTankCorpusTexture, _HeavyTankTurretTexture, _particleTexture,
                pos, new Vector2(170, 120));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST1(newTank, "bot"));
            if (!isSpawn)
                CountEnemies++;
            return newTank;
        }

        private TankObject CreateYagaTank(Vector2 pos, bool isSpawn)
        {
            var newTank = new YagaTank(_YagaTankTexture, _particleTexture,
                pos, new Vector2(170, 120));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIPTTank(newTank, "bot"));
            if (!isSpawn)
                CountEnemies++;
            return newTank;
        }

        private TankObject CreateTankDot(Vector2 pos, Vector2 size)
        {
            var newDot = new TankDot(_DotTankTexture, _particleTexture,
                pos, size / 2);
            newDot.OnShootRocket += _Dot_OnDotShoots;
            newDot.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newDot);
            fisica.AddObject(newDot);
            tanksList.Add(newDot);
            newDot.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newDot.Body.Position, new Vector2(100, 24),
                new Color(255, 0, 0), new Color(180, 0, 0), 0, newDot.MaxHealth, true));

            AItanksList.Add(new AIDotTank(newDot, "bot"));
            CountEnemies++;
            return newDot;
        }

        private TankObject CreateTankST4(Vector2 pos)
        {
            var newTank = new TankST4(_STTankCorpusTexture, _STTankTurretTexture, _particleTexture,
                pos, new Vector2(130, 93));
            newTank.OnShootBullet += _Tank_OnTankShoots;
            newTank.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(100, 24),
                new Color(0, 210, 0), new Color(0, 100, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBotST2(newTank, "player"));
            return newTank;
        }

        private TankObject CreateTankIS7(Vector2 pos)
        {
            var newTankIS7 = new TankIS7(_IS7TankCorpusTexture, _IS7TankTurretTexture, _particleTexture, pos, new Vector2(100, 100));
            newTankIS7.OnShootBullet += _Tank_OnTankShoots;
            newTankIS7.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newTankIS7);
            fisica.AddObject(newTankIS7);
            tanksList.Add(newTankIS7);
            newTankIS7.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTankIS7.Body.Position, new Vector2(100, 24),
                new Color(0, 210, 0), new Color(0, 100, 0), 0, newTankIS7.MaxHealth, true));

            AItanksList.Add(new AIBotST2(newTankIS7, "player"));
            return newTankIS7;
        }

        private TankObject CreateAliveBuilding(Vector2 pos, Vector2 size)
        {
            //Это ворота на первом уровне в компании
            var newTank = new AliveBuilding(_block2Texture, _particleTexture,
                pos, size);
            newTank.PlayerLose += PlayerLose;
            AddGameObject(newTank);
            fisica.AddObject(newTank);
            tanksList.Add(newTank);
            newTank.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newTank.Body.Position, new Vector2(300, 48),
                new Color(0, 210, 0), new Color(0, 100, 0), 0, newTank.MaxHealth, true));

            AItanksList.Add(new AIBlock2(newTank, "player"));
            return newTank;
        }

        private TankObject CreateBarrel(Vector2 pos, Vector2 size)
        {
            var newBarrel = new Barrel(_BarrelTexture, _particleTexture,
                pos, size / 2);
            newBarrel.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newBarrel);
            fisica.AddObject(newBarrel);
            tanksList.Add(newBarrel);
            newBarrel.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newBarrel.Body.Position, new Vector2(300, 48),
                new Color(210, 0, 0), new Color(100, 0, 0), 0, newBarrel.MaxHealth, true));

            AItanksList.Add(new AIBlock2(newBarrel, "bot"));
            CountBarrels++;
            return newBarrel;
        }

        private TankObject CreateBarrel2(Vector2 pos, Vector2 size, float angle)
        {
            var newBarrel = new Barrel2(_Barrel2Texture, _particleTexture, pos, size, angle);
            newBarrel.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newBarrel);
            fisica.AddObject(newBarrel);
            tanksList.Add(newBarrel);
            newBarrel.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newBarrel.Body.Position, new Vector2(300, 48),
                new Color(210, 0, 0), new Color(100, 0, 0), 0, newBarrel.MaxHealth, true));

            AItanksList.Add(new AIBlock2(newBarrel, "bot"));
            CountBarrels++;
            return newBarrel;
        }

        private Machine CreateMachine(Vector2 pos, Vector2 size)
        {
            var newMachine = new Machine(_MachineTexture, _particleTexture,
                pos, size);
            newMachine.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newMachine);
            fisica.AddObject(newMachine);
            tanksList.Add(newMachine);
            newMachine.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newMachine.Body.Position, new Vector2(300, 48),
                new Color(210, 0, 0), new Color(100, 0, 0), 0, newMachine.MaxHealth, true));

            AItanksList.Add(new AIBlock2(newMachine, "bot"));
            CountEnemies++;
            return newMachine;
        }

        private TankKorpus CreateTankKorpus(Vector2 pos, Vector2 size)
        {
            var newMachine = new TankKorpus(_tankCorpusTexture, _particleTexture,
                pos, size);
            newMachine.OnDestroy += _Tank_OnTankDestroy;
            AddGameObject(newMachine);
            fisica.AddObject(newMachine);
            tanksList.Add(newMachine);
            newMachine.CreateHPStatsBar(new StatsBarObject(_screenBoxTexture, newMachine.Body.Position, new Vector2(100, 24),
                new Color(210, 0, 0), new Color(100, 0, 0), 0, newMachine.MaxHealth, true));

            AItanksList.Add(new AIBlock2(newMachine, "bot"));
            CountEnemies++;
            return newMachine;
        }

        private SpawnerObject CreateLTTankSpawner(Vector2 pos)
        {
            var newSpawner = new SpawnerObject("jeep_base", pos);
            newSpawner.OnSpawnTank += _Spawner_OnTankSpawn;
            AddGameObject(newSpawner);
            fisica.AddObject(newSpawner);
            SpawnersList.Add(newSpawner);
            CountEnemies += 3;
            return newSpawner;
        }

        private SpawnerObject CreateSTTankSpawner(Vector2 pos)
        {
            var newSpawner = new SpawnerObject("tank_base", pos);
            newSpawner.OnSpawnTank += _Spawner_OnTankSpawn;
            AddGameObject(newSpawner);
            fisica.AddObject(newSpawner);
            SpawnersList.Add(newSpawner);
            CountEnemies += 3;
            return newSpawner;
        }

        private SpawnerObject CreateTTTankSpawner(Vector2 pos)
        {
            var newSpawner = new SpawnerObject("HeavyTankKorpus", pos);
            newSpawner.OnSpawnTank += _Spawner_OnTankSpawn;
            AddGameObject(newSpawner);
            fisica.AddObject(newSpawner);
            SpawnersList.Add(newSpawner);
            CountEnemies += 3;
            return newSpawner;
        }

        private BuildingObject CreateBuildingObject(BuildingObject newBuilding)
        {
            AddGameObject(newBuilding);
            fisica.AddObject(newBuilding);
            return newBuilding;
        }

        private TreeObject CreateTree(Texture2D treeTexture, Texture2D brokedTreeTexure, Vector2 pos, Vector2 size)
        {
            var newTree = new TreeObject(treeTexture, brokedTreeTexure, _trunkTexture, pos, size, Camera);
            newTree.OnBrokeTree += _Tree_OnBrokeTree;
            AddGameObject(newTree);
            fisica.AddObject(newTree);
            return newTree;
        }
        private TreeObject CreateTree1(Vector2 pos, Vector2 size)
        {
            return CreateTree(_tree1Texture, _brokedTree1Texture, pos, size);
        }
        private TreeObject CreateTree2(Vector2 pos, Vector2 size)
        {
            return CreateTree(_tree2Texture, _brokedTree2Texture, pos, size);
        }
        private TreeObject CreateTree3(Vector2 pos, Vector2 size)
        {
            return CreateTree(_tree3Texture, _brokedTree2Texture, pos, size);
        }

        private BushObject CreateBush(Texture2D textureBush, Vector2 pos)
        {
            var newBush = new BushObject(textureBush, pos, new Vector2(150, 150), Camera);
            AddGameObject(newBush);
            fisica.AddObject(newBush);
            return newBush;
        }

        private FloorObject CreateFactoryFloor(Vector2 pos, Vector2 size)
        {
            var newFloor = new FloorObject(_FactoryFloorTexture, pos, size);
            AddGameObject(newFloor);
            return newFloor;
        }

        private FloorObject CreateCityFloor(Vector2 pos, Vector2 size, float angle)
        {
            var newFloor = new FloorObject(_CityFloorTexture, pos, size, angle);
            AddGameObject(newFloor);
            return newFloor;
        }

        private FloorObject CreateCityFloor2(Vector2 pos, Vector2 size, float angle)
        {
            var newFloor = new FloorObject(_CityFloor2Texture, pos, size, angle);
            AddGameObject(newFloor);
            return newFloor;
        }
        private BushObject CreateBush1(Vector2 pos)
        {
            return CreateBush(_bush1Texture, pos);
        }
        private BushObject CreateBush2(Vector2 pos)
        {
            return CreateBush(_bush2Texture, pos);
        }
        private BushObject CreateBush3(Vector2 pos)
        {
            return CreateBush(_bush3Texture, pos);
        }

        private ImageObject CreateGrass1(Vector2 pos, Vector2 size)
        {
            var newGrass = new ImageObject(_grass1Texture, new Color(26, 132, 77), pos - size/2, size);
            AddGameObject(newGrass);
            return newGrass;
        }


        private void CleanObjects()
        {
            var updatedBulletList = new List<BulletObject>(bulletsList.Count);
            foreach (var bullet in bulletsList)
            {
                if (!bullet.IsAlive || bullet.TicksLive > 300)
                {
                    RemoveGameObject(bullet);
                    fisica.RemoveObject(bullet);
                }
                else
                    updatedBulletList.Add(bullet);
            }
            bulletsList = updatedBulletList;

            var updatedExplosionList = new List<Explosion>(ExplosionsList.Count);
            foreach (var explosion in ExplosionsList)
            {
                if(explosion.LifeTime < explosion.MaxLifeTime)
                    updatedExplosionList.Add(explosion);
            }
            ExplosionsList = updatedExplosionList;

            var updatedTanksList = new List<TankObject>(tanksList.Count);
            foreach (var tank in tanksList)
            {
                if (tank.Health < -100)
                {
                    RemoveGameObject(tank);
                    fisica.RemoveObject(tank);
                    ExplosionsList.Add(new Explosion(_particleTexture, tank.Body.Position, 30, 40, 65, 3.5f, new Color(220, 50, 50), 30, 60));
                    ExplosionsList.Add(new Explosion(_particleTexture, tank.Body.Position, 30, 40, 65, 3f, new Color(220, 220, 50), 20, 60));
                }
                else
                    updatedTanksList.Add(tank);
            }
            tanksList = updatedTanksList;
        }

        private void _Tank_OnTankShoots(object sender, GameplayEvents.ShootBullet e)
        {
            var bullet = new BulletObject(LoadTexture(e.BulletTexture), e.Position, e.Size, e.Velocity, e.Damage, e.BulletName);
            AddGameObject(bullet);
            fisica.AddObject(bullet);
            bulletsList.Add(bullet);
            if ((Camera.Position - e.Position).Length() <= _viewportWidth)
            {
                if(e.BulletName == "UltraBulletObject")
                    NotifyEvent(new GameplayEvents.CyberTankShootMusic());
                else
                    NotifyEvent(new GameplayEvents.TankShootMusic());
            }
        }

        private void _Dot_OnDotShoots(object sender, GameplayEvents.ShootRocket e)
        {
            var bullet = new BulletObject(LoadTexture(e.BulletTexture), e.Position, e.Size, e.Velocity, e.Damage, e.BulletName);
            AddGameObject(bullet);
            fisica.AddObject(bullet);
            bulletsList.Add(bullet);
            if ((Camera.Position - e.Position).Length() <= _viewportWidth)
                NotifyEvent(new GameplayEvents.TankShootMusic());
        }

        private void _Spawner_OnTankSpawn(object sender, GameplayEvents.SpawnTank e)
        {
            ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 30, 60, 85, 3.5f, new Color(200, 200, 200), 30, 60));
            if (e.TankName == "tank3_base")
                CreateTankST3(e.Position, true);
            if (e.TankName == "tank2_base")
                CreateTankST2(e.Position, true);
            if (e.TankName == "tank_base")
                CreateTankST1(e.Position, true);
            if (e.TankName == "HeavyTankKorpus")
                CreateHeavyTank(e.Position, true);
            if (e.TankName == "Yaga")
                CreateYagaTank(e.Position, true);
            if (e.TankName == "jeep_base")
                CreateJeep(e.Position, true);
        }

        private void _Tank_OnTankDestroy(object sender, GameplayEvents.DestroyTank e)
        {
            if (e.TankName == "Barrel" || e.TankName == "Barrel2")
            {
                ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 100, 60, 95, 3.5f, new Color(220, 50, 50), 60, 80));
                ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 100, 60, 95, 3f, new Color(220, 220, 50), 40, 80));
                CountKilledBarrels++;
            }
            else
            {
                ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 30, 40, 65, 3.5f, new Color(220, 50, 50), 30, 60));
                ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 30, 40, 65, 3f, new Color(220, 220, 50), 20, 60));
                if (e.CommandName == "bot")
                    CountKilledEnemies++;
            }
            if ((Camera.Position - e.Position).Length() <= _viewportWidth)
                NotifyEvent(new GameplayEvents.DestroyTankMusic());
        }

        public void _fisica_OnColliseObjects(object sender, GameplayEvents.CollisionObjects e)
        {
            if (e.obj1 is TreeObject && !(e.obj2 is TankObject))
                ((TreeObject)e.obj1).BrokeTree();
            if (e.obj2 is TreeObject && !(e.obj1 is TankObject))
                ((TreeObject)e.obj2).BrokeTree();

            if(e.obj1 is BulletObject)
            {
                if(e.obj2 is TankObject)
                    ((TankObject)e.obj2).GetDamave((BulletObject)e.obj1);
                if (e.obj2 is BulletObject)
                {
                    if (e.obj1.Body.Size.X <= e.obj2.Body.Size.X)
                        e.obj1.IsAlive = false;
                }
                else
                    e.obj1.IsAlive = false;
                var direction = (e.obj1.Body.Position - e.obj2.Body.Position);
                direction.Normalize();
                if (((BulletObject)e.obj1).Name == "UltraBulletObject")
                    ExplosionsList.Add(new Explosion(_ultraBulletTexture, e.obj1.Body.Position, 5, 25, 45, 1.05f, direction * 5.0f, new Color(30, 100, 220), 12, 45));
                if (((BulletObject)e.obj1).Name == "BulletObject")
                    ExplosionsList.Add(new Explosion(_bulletTexture, e.obj1.Body.Position, 5, 25, 45, 1.05f, direction * 5.0f, new Color(220, 220, 220), 12, 45));
            }

            if (e.obj2 is BulletObject)
            {
                if (e.obj1 is TankObject)
                    ((TankObject)e.obj1).GetDamave((BulletObject)e.obj2);
                if (e.obj1 is BulletObject)
                {
                    if (e.obj2.Body.Size.X <= e.obj1.Body.Size.X)
                        e.obj2.IsAlive = false;
                }
                else
                    e.obj2.IsAlive = false;
                var direction = (e.obj2.Body.Position - e.obj1.Body.Position);
                direction.Normalize();
                if (((BulletObject)e.obj2).Name == "UltraBulletObject")
                    ExplosionsList.Add(new Explosion(_ultraBulletTexture, e.obj2.Body.Position, 5, 25, 45, 1.05f, direction * 5.0f, new Color(30, 100, 220), 12, 45));
                if (((BulletObject)e.obj2).Name == "BulletObject")
                    ExplosionsList.Add(new Explosion(_bulletTexture, e.obj2.Body.Position, 5, 25, 45, 1.05f, direction * 5.0f, new Color(220, 220, 220), 12, 45));
            }

        }

        private void _Tree_OnBrokeTree(object sender, GameplayEvents.BrokeTree e)
        {
            ExplosionsList.Add(new Explosion(_particleTexture, e.Position, 30, 40, 60, 3.5f, new Color(30, 160, 30), 40, 60));
        }

        private void _CyberTank_OnTankRegen(object sender, GameplayEvents.RegenCyberTank e)
        {
            ExplosionsList.Add(new Absorption(_particleTexture, e.Position, 200, 15, 30, 6f, new Color(0, 100, 230), 2, 30));
            NotifyEvent(new GameplayEvents.RegenTankMusic());
        }

        public void GoToMenu(object sender, BaseInputCommand e)
        {
            SwitchState(new ChooseCompanyLevelsState());
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }

    public enum GameCondition
    {
        None,
        Win,
        Pause,
        Lose
    }
}