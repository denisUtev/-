using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Strongest_Tank.Objects
{
    internal class BuildingObject : BaseGameObject
    {
        private Vector2 textureSize;

        public BuildingObject(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position, Form.RECT, size)
        {
            Body.Mass = 10;
            Body.ConstantOfFriction = 1;
            Name = "BlockObject";
            zIndex = 0.2f;
            Body.IsStatic = true;
            textureSize = size;
        }
        public BuildingObject(Texture2D texture, Vector2 position, Vector2 size, Form form) : base(texture, position, form, size)
        {
            Body.Mass = 10;
            Body.ConstantOfFriction = 1;
            Name = "BlockObject";
            zIndex = 0.2f;
            Body.IsStatic = true;
            textureSize = size;
        }

        public BuildingObject(Texture2D texture, Vector2 position, Vector2 size, Vector2 textureSize) : base(texture, position, Form.RECT, size)
        {
            Body.Mass = 10;
            Body.ConstantOfFriction = 1;
            Name = "BlockObject";
            zIndex = 0.2f;
            Body.IsStatic = true;
            this.textureSize = textureSize;
        }

        public BuildingObject(Texture2D texture, Vector2 position, Vector2 size, Vector2 textureSize, Form form) : base(texture, position, form, size)
        {
            Body.Mass = 10;
            Body.ConstantOfFriction = 1;
            Name = "BlockObject";
            zIndex = 0.2f;
            Body.IsStatic = true;
            this.textureSize = textureSize;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                Color.White,
                0f,
                new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
                (textureSize.X) / _textureBody.Width,
                SpriteEffects.None,
                zIndex
                );
        }
    }

    /*
     * Размеры физического объекта относительно размеров текстур у некоторых спрайтов:
     * house1 - Normal
     * house2 - (400, 550) --> (450, 450)
     */
}
