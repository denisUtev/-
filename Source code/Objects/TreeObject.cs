using Microsoft.Xna.Framework.Graphics;
using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.Engine;
using static Strongest_Tank.States.Gameplay.GameplayEvents;

namespace Strongest_Tank.Objects
{
    public class TreeObject : BaseGameObject
    {
        private Texture2D brokenTexture;
        private Texture2D trunkTexture;
        private Vector2 textureSize;
        private Camera camera;

        public event EventHandler<BrokeTree> OnBrokeTree;

        public TreeObject(Texture2D texture, Texture2D brokenTexture, Texture2D trunkTexture, Vector2 position, Vector2 size, Camera camera) : base(texture, position, Form.RECT, size/10)
        {
            this.brokenTexture = brokenTexture;
            this.trunkTexture = trunkTexture;
            textureSize = size;
            Name = "TreeObject";
            zIndex = 0.1f;
            Body.IsStatic = true;
            this.camera = camera;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            float distanceToPlayer = _textureBody.Width;
            if (Body.bodyForm != Form.NONE)
            {
                distanceToPlayer = Math.Max((camera.Position - Body.Position).Length(), _textureBody.Width * 0.6f);
                spriteBatch.Draw(trunkTexture,
                    Body.Position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(trunkTexture.Width / 2, trunkTexture.Height / 2),
                    (Body.Size.X) / _textureBody.Width,
                    SpriteEffects.None,
                    zIndex + 0.15f
                    );
            }
            spriteBatch.Draw(_textureBody,
                Body.Position,
                null,
                Color.White * (distanceToPlayer/ _textureBody.Width),
                0f,
                new Vector2(_textureBody.Width / 2, _textureBody.Height / 2),
                (textureSize.X) / _textureBody.Width,
                SpriteEffects.None,
                zIndex
                );
        }

        public void BrokeTree()
        {
            zIndex = 0.95f;
            _textureBody = brokenTexture;
            Body.bodyForm = Form.NONE;
            textureSize /= 2;
            OnBrokeTree?.Invoke(this, new BrokeTree(Body.Position));
        }
    }
}
