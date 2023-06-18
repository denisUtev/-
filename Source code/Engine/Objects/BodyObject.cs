using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Strongest_Tank.States;

namespace Strongest_Tank.Engine.Objects
{
    public class BodyObject
    {
        public Vector2 Position { get; set; }
        public Vector2 velocity { get; set; }
        private Vector2 resultForce = new Vector2();
        public Vector2 lastVelocity = new Vector2();
        public Vector2 lastResultForce = new Vector2();
        public float ColliseForse = 8;
        public Vector2 Size { get; set; }
        public Form bodyForm { get; set; }
        public float Angle { get; set; }
        public int Mass { get; set; }
        public float ConstantOfFriction = 0.19f;
        public bool IsStatic = false;
        public bool IsCheckInBounds = true;

        public BodyObject(Vector2 pos, Form form, Vector2 size)
        {
            Position = pos;
            velocity = Vector2.Zero;
            Size = size;
            Mass = 10;
            bodyForm = form;
        }

        public virtual void Update()
        {
            if (!IsStatic)
            {
                AddFriction();
                velocity += CalculateAcceleration();
                if (velocity.Length() > 16)
                {
                    velocity /= velocity.Length();
                    velocity *= 16;
                }
                Position += velocity;
                if(IsCheckInBounds)
                    CheckInBounds();
                lastVelocity = velocity;
                lastResultForce = resultForce;
                resultForce = new Vector2();
            }
        }

        private void CheckInBounds()
        {
            if(Position.X - Size.X/2 < 0)
                Position = new Vector2(Size.X / 2, Position.Y);
            if (Position.Y - Size.Y / 2 < 0)
                Position = new Vector2(Position.X, Size.Y / 2);
            if (Position.X + Size.X / 2 > GameplayState.MapSizeX)
                Position = new Vector2(GameplayState.MapSizeX - Size.X / 2, Position.Y);
            if (Position.Y + Size.Y / 2 > GameplayState.MapSizeY)
                Position = new Vector2(Position.X, GameplayState.MapSizeY - Size.Y / 2);
        }

        public bool CheckCollision((BaseGameObject, Vector2[]) myObject, (BaseGameObject, Vector2[]) anotherObject)
        {
            if(isCollise(anotherObject.Item1.Body, myObject.Item2, anotherObject.Item2))
            {
                SetImpulse(lastVelocity, anotherObject.Item1.Body.lastVelocity,
                    Mass, anotherObject.Item1.Body.Mass,
                    Position, anotherObject.Item1.Body.Position);
                if (!anotherObject.Item1.Body.IsStatic)
                {
                    anotherObject.Item1.Body.SetImpulse(anotherObject.Item1.Body.lastVelocity, lastVelocity,
                        anotherObject.Item1.Body.Mass, Mass,
                        anotherObject.Item1.Body.Position, Position);
                    var newForce = anotherObject.Item1.Body.Position - Position;
                    newForce.Normalize();
                    if(!anotherObject.Item1.IsAlive || !myObject.Item1.IsAlive || Mass >= anotherObject.Item1.Body.Mass && (anotherObject.Item1.Body.velocity.Length() > 1 || IsStatic == true))
                        anotherObject.Item1.Body.AddForce(newForce * (Math.Max(ColliseForse, anotherObject.Item1.Body.ColliseForse) * Math.Min(1, (Mass / anotherObject.Item1.Body.Mass))));
                }
                else
                {
                    var newForce = anotherObject.Item1.Body.Position - Position;
                    newForce.Normalize();
                    AddForce(-newForce * Math.Max(ColliseForse, anotherObject.Item1.Body.ColliseForse));
                }
                return true;
            }
            return false;
        }

        public bool IsColliseRects(Vector2[] myAngles, Vector2[] objectAngles)
        {
            float[] Ax = myAngles.Select(v => v.X).OrderBy(v => v).ToArray();
            float[] Ay = myAngles.Select(v => v.Y).OrderBy(v => v).ToArray();
            float[] Bx = objectAngles.Select(v => v.X).OrderBy(v => v).ToArray();
            float[] By = objectAngles.Select(v => v.Y).OrderBy(v => v).ToArray();

            if (Ax.First() <= Bx.Last() && Bx.First() <= Ax.Last() &&
                Ay.First() <= By.Last() && By.First() <= Ay.Last())
                return true;
            else
                return false;
        }

        public void SetImpulse(Vector2 v1, Vector2 v2, float m1, float m2, Vector2 a, Vector2 b)
        {
            velocity = (m1*v1 + m2*v2)/(m1+m2);
            Vector2 distance = (a - b);
            distance.Normalize();
            if(!IsStatic)
                Position += distance * (m1 / (m1 + m2));
        }

        public void CheckCollision(List<BaseGameObject> Entities)
        {
            if (bodyForm == Form.NONE) return;
            foreach (BaseGameObject entity in Entities)
            {
                if (entity.Body.Equals(this) || entity.Body.bodyForm == Form.NONE) continue;
            }
        }
        
        bool isCollise(BodyObject r2, Vector2[] a, Vector2[] b)
        {
            if(bodyForm==Form.NONE || r2.bodyForm==Form.NONE)
                return false;

            if (r2.bodyForm == Form.CIRCLE && bodyForm == Form.CIRCLE)
                return Math.Pow(r2.Position.X - Position.X, 2) + Math.Pow(r2.Position.Y - Position.Y, 2) < Math.Pow(r2.Size.X + Size.X, 2);

            if (r2.bodyForm == Form.RECT)
            {
                for (int i = 0; i < 4; i++)
                {
                    double sumAngles = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        double sideA = Math.Pow(b[j].X - a[i].X, 2) + Math.Pow(b[j].Y - a[i].Y, 2);
                        double sideB = Math.Pow(b[(j + 1) % 4].X - a[i].X, 2) + Math.Pow(b[(j + 1) % 4].Y - a[i].Y, 2);
                        double sideC = Math.Pow(b[(j + 1) % 4].X - b[j].X, 2) + Math.Pow(b[(j + 1) % 4].Y - b[j].Y, 2);
                        sumAngles += Math.Acos((sideA + sideB - sideC) / (2 * Math.Sqrt(sideA * sideB)));
                    }
                    if (Math.Abs(sumAngles - 2 * Math.PI) < 1e-5)
                        return true;
                }
                return false;
            }
            else
            {
                foreach (var e in a)
                {
                    if (Math.Pow(e.X - r2.Position.X, 2) + Math.Pow(e.Y - r2.Position.Y, 2) < Math.Pow(r2.Size.X, 2))
                        return true;
                }
            }
            return false;
        }

        public Vector2[] GetAngleCordinates(BodyObject r)
        {
            Vector2[] a = new Vector2[4];
            a[0] = r.Position + new Vector2(-r.Size.X / 2, -r.Size.Y / 2).Rotate(Angle);
            a[1] = r.Position + new Vector2(r.Size.X / 2, -r.Size.Y / 2).Rotate(Angle);
            a[2] = r.Position + new Vector2(r.Size.X / 2, r.Size.Y / 2).Rotate(Angle);
            a[3] = r.Position + new Vector2(-r.Size.X / 2, r.Size.Y / 2).Rotate(Angle);
            return a;
        }

        public void AddForce(Vector2 force)
        {
            resultForce += force;
        }
        private void AddFriction()
        {
            float module = Math.Min(velocity.Length(), (ConstantOfFriction * Mass * 10));
            AddForce(new Vector2(module * (float)Math.Cos(velocity.Angle() + Math.PI),
                module * (float)Math.Sin(velocity.Angle() + Math.PI)));
        }
        private Vector2 CalculateAcceleration()
        {
            return resultForce / Mass;
        }
    }

    public enum Form
    {
        RECT,
        CIRCLE,
        NONE
    }

    static class Vector2Extensions
    {
        public static double Angle(this Vector2 p)
        {
            return Math.Atan2(p.Y, p.X);
        }

        public static Vector2 Rotate(this Vector2 p, double angle)
        {
            return new Vector2(p.Length() * (float)Math.Cos(angle + p.Angle()), p.Length() * (float)Math.Sin(angle + p.Angle()));
        }
    }
}
