using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine;

namespace Strongest_Tank.Objects
{
    public class BushObject : BaseGameObject
    {
        private Camera camera;

        public BushObject(Texture2D texture, Vector2 position, Vector2 size, Camera camera) : base(texture, position, Form.RECT, size)
        {
            Body.Mass = 10;
            Body.ConstantOfFriction = 1;
            Name = "BlockObject";
            zIndex = 0.2f;
            Body.IsStatic = true;
            Body.bodyForm = Form.NONE;
            this.camera = camera;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            var distanceToPlayer = Math.Max((camera.Position - Body.Position).Length(), _textureBody.Width * 0.6f);
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                Color.White * (distanceToPlayer / _textureBody.Width),
                0f,
                new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
                (Body.Size.X) / _textureBody.Width,
                SpriteEffects.None,
                zIndex
                );
        }
    }
}
