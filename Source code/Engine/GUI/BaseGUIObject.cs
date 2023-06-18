using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.States;
using System;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Input;

namespace Strongest_Tank.Engine.GUI
{
    public class BaseGUIObject
    {
        public Color ColorTint = Color.White;
        public float Alpha = 1;
        protected Texture2D _textureBody;
        public Vector2 Position;
        public Vector2 Size;
        public string Name;

        public float zIndex = 0.1f;
        public event EventHandler<BaseInputCommand> OnObjectChanged;
        
        public BaseGUIObject(Texture2D texture, Vector2 pos, Vector2 size)
        {
            _textureBody = texture;
            Position = pos;
            Size = size;
            Name = "BaseGUIObject";
        }

        public void Update()
        {
            UpdateVars();
        }

        public virtual void UpdateVars()
        {

        }

        public virtual void OnNotify(BaseGameStateEvent gameEvent) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
                Position,
                null,
                ColorTint * Alpha,
                0f,
                new Vector2(0, 0),
                new Vector2(Size.X / _textureBody.Width, Size.Y / _textureBody.Height),
                SpriteEffects.None,
                zIndex
                );
        }

        public void SendEvent(BaseInputCommand e)
        {
            OnObjectChanged?.Invoke(this, e);
        }
    }
}
