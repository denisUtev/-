using Strongest_Tank.Engine.States;
using Strongest_Tank.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.States.Menu;

namespace Strongest_Tank.Engine
{
    public class MainGame : Game
    {
        private BaseGameState _currentGameState;

        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        private int _DesignedResolutionWidth;
        private int _DesignedResolutionHeight;
        private float _designedResolutionAspectRatio;

        private BaseGameState _firstGameState;
        public Camera Camera;

        public MainGame(int width, int height, BaseGameState firstGameState)
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;

            _firstGameState = firstGameState;
            _DesignedResolutionWidth = width;
            _DesignedResolutionHeight = height;
            _designedResolutionAspectRatio = width / (float)height;

            Camera = new Camera();
            Camera.ViewportWidth = width;
            Camera.ViewportHeight = height;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _DesignedResolutionWidth;
            _graphics.PreferredBackBufferHeight = _DesignedResolutionHeight;
            //_graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ToggleFullScreen();
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, _DesignedResolutionWidth, _DesignedResolutionHeight, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= _designedResolutionAspectRatio)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / _designedResolutionAspectRatio + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * _designedResolutionAspectRatio + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SwitchGameState(_firstGameState);
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
                _currentGameState.UnloadContent();
            }

            _currentGameState = gameState;

            _currentGameState.Initialize(Content, Camera, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            _currentGameState.LoadContent();

            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
        {
            switch (e)
            {
                case BaseGameStateEvent.GameQuit _:
                    Exit();
                    break;
            }
        }

        protected override void UnloadContent()
        {
            _currentGameState?.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.HandleInput(gameTime);
            _currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                null, null, null, null, Camera.TranslationMatrix);

            _currentGameState.Render(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            _currentGameState.RenderGUI(spriteBatch);

            spriteBatch.End();


            _graphics.GraphicsDevice.SetRenderTarget(null);

            _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Savings
    {
        public int CompanyLevel { get; set; }
        public float SavedMusicVolume { get; set; }
        public float SavedSoundVolume { get; set; }
    }
}