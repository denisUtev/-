using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Objects;

namespace Strongest_Tank.Engine.GUI
{
    public class StatsBarObject : BaseGUIObject
    {
        public float MinValue;
        public float MaxValue;
        public float CurrentValue;
        public float Alpha = 0.55f;
        public float MaxAlpha = 0.55f;
        private float AnimatedCurrentValue;
        private float AnimatedChangedValue;
        protected Color FoneColor;
        protected Color ChangedValueColor;
        protected Vector2 Translation;
        public bool IsVisibleChangedValueBar;
        public bool IsVisibleCounter;
        public bool IsShowing = true;
        protected BaseTextObject textObjectCounter;

        public StatsBarObject(Texture2D texture, Vector2 pos, Vector2 size, Color ColorTint, Color foneColor, float minValue, float maxValue) : base(texture, pos, size) 
        { 
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = maxValue;
            this.ColorTint = ColorTint;
            FoneColor = new Color(0, 0, 0);
            FoneColor = foneColor;
            IsVisibleChangedValueBar = false;
            IsVisibleCounter = false;
            zIndex = 0.04f;
        }

        public StatsBarObject(Texture2D texture, SpriteFont font, Vector2 pos, Vector2 size, Color ColorTint, Color foneColor, float minValue, float maxValue) : base(texture, pos, size)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = maxValue;
            this.ColorTint = ColorTint;
            FoneColor = new Color(0, 0, 0);
            FoneColor = foneColor;
            IsVisibleChangedValueBar = false;
            IsVisibleCounter = true;
            textObjectCounter = new BaseTextObject(font, Convert.ToString(CurrentValue) + '/' + Convert.ToString(MaxValue), pos + new Vector2(10, 10), Color.White);
            zIndex = 0.04f;
        }

        public StatsBarObject(Texture2D texture, Vector2 pos, Vector2 size, Color ColorTint, Color ChangeValueColorTint, float minValue, float maxValue, bool IsVisibleChangedValueBar) : base(texture, pos, size)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = maxValue;
            this.ColorTint = ColorTint;
            FoneColor = new Color(0, 0, 0);
            ChangedValueColor = ChangeValueColorTint;
            this.IsVisibleChangedValueBar = IsVisibleChangedValueBar;
            IsVisibleCounter = false;
            zIndex = 0.04f;
        }

        public StatsBarObject(Texture2D texture, SpriteFont font, Vector2 pos, Vector2 size, Color ColorTint, Color ChangeValueColorTint, float minValue, float maxValue, bool IsVisibleChangedValueBar, bool IsVisibleTextCounter) : base(texture, pos, size)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = maxValue;
            this.ColorTint = ColorTint;
            FoneColor = new Color(0, 0, 0);
            ChangedValueColor = ChangeValueColorTint;
            this.IsVisibleChangedValueBar = IsVisibleChangedValueBar;
            IsVisibleCounter = IsVisibleTextCounter;
            textObjectCounter = new BaseTextObject(font, Convert.ToString(CurrentValue) + '/' + Convert.ToString(MaxValue), pos + new Vector2(Size.Y/6.5f, Size.Y / 6.5f), Color.White);
            zIndex = 0.04f;
        }

        public override void UpdateVars()
        {
            if (IsVisibleCounter)
            {
                textObjectCounter.Text = Convert.ToString(CurrentValue) + '/' + Convert.ToString(MaxValue);
                if (IsShowing)
                    textObjectCounter.Alpha = Alpha + (1 - MaxAlpha);
                else
                    textObjectCounter.Alpha = Alpha;
            }

            AnimatedCurrentValue += (CurrentValue - AnimatedCurrentValue) / 7.0f;
            AnimatedCurrentValue = Math.Min(MaxValue, AnimatedCurrentValue);
            if (IsVisibleChangedValueBar && Math.Abs(AnimatedCurrentValue - CurrentValue) < 2)
                AnimatedChangedValue += (CurrentValue - AnimatedChangedValue) / 7.0f;

            if (!IsShowing && Alpha >= 0)
                Alpha /= 1.2f;
            else if (Alpha < MaxAlpha)
                Alpha += (MaxAlpha - Alpha)/7.0f;
        }
        public void SetPosition(Vector2 pos)
        {
            Position = pos + Translation;
        }

        public void SetTranslation(Vector2 pos)
        {
            Translation = pos;
        }

        public void Hide()
        {
            IsShowing = false;
        }

        public void Show()
        {
            IsShowing = true;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
                Position - new Vector2(2, 2),
                null,
                FoneColor * Alpha,
                0f,
                new Vector2(0, 0),
                new Vector2((Size.X + 4) / _textureBody.Width, (Size.Y + 4) / _textureBody.Height),
                SpriteEffects.None,
                zIndex + 0.02f
                );
            if(IsVisibleChangedValueBar)
                spriteBatch.Draw(_textureBody,
                    Position,
                    null,
                    ChangedValueColor * Alpha,
                    0f,
                    new Vector2(0, 0),
                    new Vector2((Size.X / _textureBody.Width) * ((MinValue - AnimatedChangedValue) / (MinValue - MaxValue)), Size.Y / _textureBody.Height),
                    SpriteEffects.None,
                    zIndex + 0.01f
                    );
            spriteBatch.Draw(_textureBody,
                Position,
                null,
                ColorTint * Alpha,
                0f,
                new Vector2(0, 0),
                new Vector2((Size.X / _textureBody.Width) * ((MinValue - AnimatedCurrentValue) / (MinValue - MaxValue)), Size.Y / _textureBody.Height),
                SpriteEffects.None,
                zIndex
                );

            if (IsVisibleCounter)
                textObjectCounter.Render(spriteBatch);
        }
    }
}
