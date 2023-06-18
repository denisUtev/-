using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace Strongest_Tank.Engine.GUI
{
    public class TextObject : BaseGUIObject
    {
        protected string Text;
        protected Color ColorText;
        protected SpriteFont _font;
        protected bool IsVisibleBorder = false;
        public bool IsVisible = true;
        public float Alpha = 1;
        public bool IsAnimatedShow = false;

        public TextObject(SpriteFont font, string text, Color color, Vector2 position, Vector2 size) : base(null, position, size)
        {
            Name = "TextObject";
            ColorText = color;
            Text = text;
            _font = font;
        }

        public TextObject(Texture2D texture, SpriteFont font, string text, Color color, Vector2 position, Vector2 size) : base(texture, position, size)
        {
            Name = "TextObject";
            ColorText = color;
            Text = text;
            _font = font;
            IsVisibleBorder = true;
        }

        public void UpdateText(string text)
        {
            Text = text;
        }

        public override void UpdateVars()
        {
            if (IsVisible && IsAnimatedShow && Alpha < 1)
                Alpha += 0.04f;
            if (!IsVisible && IsAnimatedShow && Alpha > 0)
                Alpha -= 0.04f;
            base.UpdateVars();
        }

        public void Hide()
        {
            IsVisible = false;
        }

        public void Show()
        {
            IsVisible = true;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (IsVisibleBorder)
                base.Render(spriteBatch);
            spriteBatch.DrawString(_font,
                Text,
                Position,
                ColorText * Alpha,
                0,
                Size / 2,
                Vector2.One,
                SpriteEffects.None,
                zIndex - 0.01f);
        }
    }
}
