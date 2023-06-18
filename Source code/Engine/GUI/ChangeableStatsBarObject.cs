using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Strongest_Tank.Engine.GUI
{
    public class ChangeableStatsBarObject : StatsBarObject
    {

        private bool _isChoosed = false;
        public ChangeableStatsBarObject(Texture2D texture, Vector2 pos, Vector2 size, Color ColorTint, Color foneColor, float minValue, float maxValue) : base(texture, pos, size, ColorTint, foneColor, minValue,maxValue)
        {
            
        }

        public ChangeableStatsBarObject(Texture2D texture, SpriteFont font, Vector2 pos, Vector2 size, Color ColorTint, Color foneColor, float minValue, float maxValue) : base(texture, font, pos, size, ColorTint, foneColor, minValue, maxValue)
        {
            
        }

        public ChangeableStatsBarObject(Texture2D texture, Vector2 pos, Vector2 size, Color ColorTint, Color ChangeValueColorTint, float minValue, float maxValue, bool IsVisibleChangedValueBar) : base(texture, pos, size, ColorTint, ChangeValueColorTint, minValue, maxValue, IsVisibleChangedValueBar)
        {
            
        }

        public ChangeableStatsBarObject(Texture2D texture, SpriteFont font, Vector2 pos, Vector2 size, Color ColorTint, Color ChangeValueColorTint, float minValue, float maxValue, bool IsVisibleChangedValueBar, bool IsVisibleTextCounter) : base(texture, font, pos, size, ColorTint, ChangeValueColorTint, minValue, maxValue, IsVisibleChangedValueBar, IsVisibleTextCounter)
        {
            
        }
        
        public void AnalizeMousePos(Vector2 mousePos)
        {
            if(IsMouseHover(mousePos) || _isChoosed)
            {
                _isChoosed = true;
                CurrentValue = (MaxValue - MinValue) * ((mousePos.X - Position.X)/Size.X);
            }
        }

        public void MouseUpped()
        {
            _isChoosed = false;
        }

        private bool IsMouseHover(Vector2 mousePos)
        {
            return mousePos.X >= Position.X && mousePos.Y >= Position.Y && mousePos.X <= Position.X + Size.X && mousePos.Y <= Position.Y + Size.Y;
        }
    }
}
