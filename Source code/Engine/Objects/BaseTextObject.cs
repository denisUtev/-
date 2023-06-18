using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strongest_Tank.Engine.Objects
{
    public class BaseTextObject : BaseGameObject
    {
        protected SpriteFont _font;
        public Color ColorText { get; set; }
        public float Alpha = 1;
        public string Text { get; set; }

        public BaseTextObject(SpriteFont font, string text, Vector2 pos, Color c) : base(pos, new Vector2(100, 100))
        {
            _font = font;
            Text = text;
            ColorText = c;
            zIndex = 0.02f;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, 
                Text, 
                Body.Position, 
                ColorText * Alpha,
                0,
                new Vector2(0, 0),
                Vector2.One,
                SpriteEffects.None,
                zIndex + 0.01f);
        }
    }
}
