using Strongest_Tank.Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Strongest_Tank.Engine.Objects
{
    public class BaseGameObject
    {
        protected Color ColorTint = Color.White;
        protected Texture2D _textureBody;
        public BodyObject Body;
        public string Name;

        public float zIndex = 1;
        public event EventHandler<BaseGameStateEvent> OnObjectChanged;
        public bool IsAlive = true;

        public BaseGameObject(Texture2D texture, Vector2 pos, Form form, Vector2 size)
        {
            _textureBody = texture;
            Body = new BodyObject(pos, form, size);
            Name = "BaseObject";
        }

        public BaseGameObject(Vector2 pos, Vector2 size)
        {
            _textureBody = null;
            Body = new BodyObject(pos, Form.NONE, size);
            Name = "BaseObject";
        }

        public virtual void Update()
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
                Body.Position,
                null,
                ColorTint,
                0f,
                new Vector2(0, 0),
                new Vector2(Body.Size.X / _textureBody.Width, Body.Size.Y / _textureBody.Height),
                SpriteEffects.None,
                zIndex
                );
        }
        
        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }
    }
}