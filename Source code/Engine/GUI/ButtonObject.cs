using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Input;

namespace Strongest_Tank.Engine.GUI
{
    public class ButtonObject : BaseGUIObject
    {
        public bool IsVisible { get; set; }
        public event EventHandler<BaseInputCommand> OnPress;
        public Color ColorPressedButton;
        public Color ColorText;
        public Color DefaultColorTint = Color.White;
        public SpriteFont Font;
        public string Text;

        public ButtonObject(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position, size)
        {
            Name = "ButtonObject";
            IsVisible = true;
            ColorPressedButton = new Color(255, 255, 0);
            ColorText = new Color(0, 0, 0);
        }

        public ButtonObject(Texture2D texture, SpriteFont font, Vector2 position, Vector2 size) : base(texture, position, size)
        {
            Name = "ButtonObject";
            Font = font;
            IsVisible = true;
            ColorPressedButton = new Color(255, 255, 0);
            ColorText = new Color(0, 0, 0);
        }

        public ButtonObject(Texture2D texture, Vector2 position, Vector2 size, bool isVisible) : base(texture, position, size)
        {
            Name = "ButtonObject";
            IsVisible = isVisible;
            Alpha = isVisible ? 1 : 0;
            ColorPressedButton = new Color(255, 255, 0);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            if (Font != null && Text != null)
            {
                spriteBatch.DrawString(Font,
                    Text,
                    Position,
                    ColorText * Alpha,
                    0,
                    -new Vector2(Size.X / 4, 0),
                    Vector2.One,
                    SpriteEffects.None,
                    zIndex - 0.01f);
            }
        }

        public override void UpdateVars()
        {
            if (Alpha < 1 && IsVisible)
                Alpha += 0.04f;
            if (Alpha > 0 && !IsVisible)
                Alpha -= 0.04f;
            base.UpdateVars();
        }

        public bool IsHover(Vector2 mousePos)
        {
            if (mousePos.X > Position.X && mousePos.X < Position.X + Size.X &&
                mousePos.Y > Position.Y && mousePos.Y < Position.Y + Size.Y)
            {
                ColorTint = ColorPressedButton * Alpha;
                return true;
            }
            ColorTint = DefaultColorTint * Alpha;
            return false;
        }

        public bool IsPressed(Vector2 mousePos)
        {
            if (IsHover(mousePos))
            {
                Pressing();
                return true;
            }
            return false;
        }

        public void Pressing()
        {
            OnPress?.Invoke(this, new BaseInputCommand());
        }
    }
}
