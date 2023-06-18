using Strongest_Tank.Engine.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using strongest_Tank;

namespace Strongest_Tank.Objects
{
    public class SplashImage : BaseGameObject
    {
        public SplashImage(Texture2D texture) : base(texture, Vector2.Zero, Form.RECT, new Vector2(Program.WIDTH, Program.HEIGHT))
        {
            Body.IsStatic = true;
            zIndex = 0.8f;
        }
    }

    public class ImageObject : BaseGameObject
    {
        public bool IsVisible = true;
        private float Alpha = 1;
        public ImageObject(Texture2D texture, Color c, Vector2 pos, Vector2 size) : base(texture, pos, Form.RECT, size)
        {
            Body.IsStatic = true;
            ColorTint = c;
        }

        public ImageObject(Texture2D texture, Color c, Vector2 pos, Vector2 size, bool isVisible) : base(texture, pos, Form.RECT, size)
        {
            Body.IsStatic = true;
            ColorTint = c;
            IsVisible = isVisible;
            Alpha = 0;
        }

        public override void UpdateVars()
        {
            if (Alpha < 1 && IsVisible)
                Alpha += 0.04f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                ColorTint * Alpha,
                0f,
                new Vector2(0, 0),
                new Vector2(Body.Size.X / _textureBody.Width, Body.Size.Y / _textureBody.Height),
                SpriteEffects.None,
                zIndex
                );
        }
    }
}