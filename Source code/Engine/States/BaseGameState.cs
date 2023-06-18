using System;
using System.Collections.Generic;
using System.Linq;
using Strongest_Tank.Engine.Input;
using Strongest_Tank.Engine.Objects;
using Strongest_Tank.Engine.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.IO;
using Strongest_Tank.Engine.GUI;
using Microsoft.Xna.Framework.Media;

namespace Strongest_Tank.Engine.States
{
    public abstract class BaseGameState
    {
        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected SoundManager _soundManager = new SoundManager();
        protected Camera Camera;
        protected float MusicVolume = 0.5f;
        protected float SoundVolume = 0.5f;
        protected Savings Save;

        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
        private readonly List<BaseGUIObject> _GUIObjects = new List<BaseGUIObject>();

        protected InputManager InputManager {get; set;}

        public void Initialize(ContentManager contentManager, Camera camera, int viewportWidth, int viewportHeight)
        {
            _contentManager = contentManager;
            _viewportHeight = viewportHeight;
            _viewportWidth = viewportWidth;
            Camera = camera;

            SetInputManager();
        }

        public abstract void LoadContent();
        public abstract void HandleInput(GameTime gameTime);
        public abstract void UpdateGameState(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;
        public event EventHandler<BaseGameStateEvent> OnEventNotification;
        protected abstract void SetInputManager();

        public void UnloadContent()
        {
            _contentManager.Unload();
        }

        public void Update(GameTime gameTime) 
        {
            UpdateGameState(gameTime);
            _soundManager.PlaySoundtrack();
        }

        public void UpdatePhisics(GameTime gameTime)
        {
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Body.Update();
            }
        }

        protected Texture2D LoadTexture(string textureName)
        {
            return _contentManager.Load<Texture2D>(textureName);
        }        

        protected static string GetPath(string name) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);

        protected static T LoadJson<T>(string name) where T : new()
        {
            T json;
            string jsonPath = GetPath(name);

            if (File.Exists(jsonPath))
            {
                json = JsonSerializer.Deserialize<T>(File.ReadAllText(jsonPath));
            }
            else
            {
                json = new T();
            }

            return json;
        }

        protected static void SaveJson<T>(string name, T json)
        {
            string jsonPath = GetPath(name);
            string jsonString = JsonSerializer.Serialize(json);
            File.WriteAllText(jsonPath, jsonString);
        }

        protected static T EnsureJson<T>(string name, JsonTypeInfo<T> typeInfo) where T : new()
        {
            T json;
            string jsonPath = GetPath(name);

            if (File.Exists(jsonPath))
            {
                json = JsonSerializer.Deserialize<T>(File.ReadAllText(jsonPath), typeInfo);
            }
            else
            {
                json = new T();
                string jsonString = JsonSerializer.Serialize(json, typeInfo);
                File.WriteAllText(jsonPath, jsonString);
            }

            return json;
        }

        protected SpriteFont LoadFont(string fontName)
        {
            return _contentManager.Load<SpriteFont>(fontName);
        }

        protected SoundEffect LoadSound(string soundName)
        {
            return _contentManager.Load<SoundEffect>(soundName);
        }

        protected Song LoadSong(string songName)
        {
            return _contentManager.Load<Song>(songName);
        }


            protected void NotifyEvent(BaseGameStateEvent gameEvent)
        {
            OnEventNotification?.Invoke(this, gameEvent);

            foreach (var gameObject in _gameObjects)
            {
                if (gameObject != null)
                    gameObject.OnNotify(gameEvent);
            }

            _soundManager.OnNotify(gameEvent);
        }

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected void AddGUIObject(BaseGUIObject gameObject)
        {
            _GUIObjects.Add(gameObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        protected void RemoveGUIObject(BaseGUIObject gameObject)
        {
            _GUIObjects.Remove(gameObject);
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Render(spriteBatch);
            }
        }

        public virtual void RenderGUI(SpriteBatch spriteBatch)
        {
            foreach (var GUIObject in _GUIObjects)
            {
                GUIObject.Render(spriteBatch);
            }
        }
    }
}