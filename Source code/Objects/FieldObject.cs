using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Strongest_Tank.Objects
{
    public class FieldObject : BaseGameObject
    {
        public FieldObject(Texture2D texture) : base(texture, new Vector2(0, 0), Form.NONE, new Vector2(10240, 10240))
        {
            zIndex = 1;
            Body.IsStatic = true;
            ColorTint = new Color(22, 150, 84);
        }
    }

    public class FloorObject : BaseGameObject
    {
        public FloorObject(Texture2D texture, Vector2 pos, Vector2 size) : base(texture, pos, Form.NONE, size)
        {
            zIndex = 0.98f;
            Body.IsStatic = true;
        }

        public FloorObject(Texture2D texture, Vector2 pos, Vector2 size, float angle) : base(texture, pos, Form.NONE, size)
        {
            zIndex = 0.98f;
            Body.IsStatic = true;
            Body.Angle = angle;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                ColorTint,
                Body.Angle,
                new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
                new Vector2(Body.Size.X / _textureBody.Width, Body.Size.Y / _textureBody.Height),
                SpriteEffects.None,
                zIndex
                );
        }
    }
}
