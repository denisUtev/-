using Strongest_Tank.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Strongest_Tank.States.Gameplay.GameplayEvents;

namespace Strongest_Tank.Engine.Phisics
{
    public class Phisics
    {
        List<BaseGameObject> objectsList = new List<BaseGameObject>();

        Dictionary<BaseGameObject, Vector2[]> objectAnglesCoordinates = new Dictionary<BaseGameObject, Vector2[]>();

        public event EventHandler<CollisionObjects> OnColliseObjects;

        private BaseGameObject Player;//может быть нужен в будущем для оптимизиации

        public Phisics(){ }
        public void SetPlayerObject(BaseGameObject player) 
        {
            this.Player = player;
        }

        public void RemoveObject(BaseGameObject obj)
        {
            objectsList.Remove(obj);
        }

        public void AddObject(BaseGameObject obj)
        {
            objectsList.Add(obj);
        }

        public void UpdatePhisics()
        {
            foreach(BaseGameObject gameObject in objectsList)
            {
                gameObject.Body.Update();
                if(gameObject.Body.velocity.Length() > 0 || !objectAnglesCoordinates.ContainsKey(gameObject))
                    objectAnglesCoordinates[gameObject] = gameObject.Body.GetAngleCordinates(gameObject.Body);
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            foreach (BaseGameObject gameObject1 in objectsList)
            {
                foreach (BaseGameObject gameObject2 in objectsList)
                {
                    if (gameObject1 != gameObject2 && (gameObject1.Body.Position - gameObject2.Body.Position).Length() < 700)
                        if (gameObject1.Body.CheckCollision(
                            (gameObject1, objectAnglesCoordinates[gameObject1]),
                            (gameObject2, objectAnglesCoordinates[gameObject2])))
                        {
                            OnColliseObjects?.Invoke(this, new CollisionObjects(gameObject1, gameObject2));
                        }
                }
            }
        }

    }
}
