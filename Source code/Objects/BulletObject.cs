using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongest_Tank.Objects
{
    public class BulletObject : BaseGameObject
    {

        private float angle;
        public float Damage;
        public int TicksLive = 0;

        public BulletObject(Texture2D texture, Vector2 pos, Vector2 velocity, float damage, string name) : base(texture, pos, Form.RECT, new Vector2(15, 15))
        {
            Body.ConstantOfFriction = 0.0f;
            angle = (float)velocity.Angle();
            Body.AddForce(velocity);
            Body.IsCheckInBounds = false;
            Body.Mass = 1;
            Name = name;
            Damage = damage;
        }

        public BulletObject(Texture2D texture, Vector2 pos, Vector2 size, Vector2 velocity, float damage, string name) : base(texture, pos, Form.RECT, size)
        {
            Body.ConstantOfFriction = 0.0f;
            angle = (float)velocity.Angle();
            Body.AddForce(velocity);
            Body.IsCheckInBounds = false;
            Body.Mass = 1;
            Name = name;
            Damage = damage;
        }

        public override void UpdateVars()
        {
            TicksLive++;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureBody,
            Body.Position,
            null,
            Color.White,
            angle + (float)Math.PI / 2,
            new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
            Body.Size / _textureBody.Width,
            SpriteEffects.None,
            0.95f
            );
        }

    }
}
