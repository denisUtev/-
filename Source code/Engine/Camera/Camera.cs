using Microsoft.Xna.Framework;
using System;

namespace Strongest_Tank.Engine
{
    public class Camera
    {
        public Camera()
        {
            Zoom = 1.0f;
        }

        public Vector2 Position { get; private set; }
        public float Zoom { get; set; }
        public float Rotation { get; private set; }

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public int MapSizeX { get; set; }
        public int MapSizeY { get; set; }

        public bool IsCheckBounds = false;

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }

        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X,
                   -(int)Position.Y, 0) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public void Translate(Vector2 newPos)
        {
            Position += newPos;
        }

        public void SetPosition(Vector2 newPos)
        {
            Position = newPos;
            if (IsCheckBounds)
            {
                if (Position.X < ViewportWidth * 0.5f / Zoom)
                    Position = new Vector2(ViewportWidth * 0.5f / Zoom, Position.Y);
                if (Position.Y < ViewportHeight * 0.5f / Zoom)
                    Position = new Vector2(Position.X, ViewportHeight * 0.5f / Zoom);
                if (Position.X > MapSizeX - ViewportWidth * 0.5f / Zoom)
                    Position = new Vector2(MapSizeX - ViewportWidth * 0.5f / Zoom, Position.Y);
                if (Position.Y > MapSizeY - ViewportHeight * 0.5f / Zoom)
                    Position = new Vector2(Position.X, MapSizeY - ViewportHeight * 0.5f / Zoom);
            }
        }

        private float lastValue = 0;
        public void Zooming(float value)
        {
            Zoom += (value - lastValue) / 10.0f;
            lastValue += (value - lastValue) / 10.0f;
            Zoom = Math.Max(Zoom, 0.5f);
            Zoom = Math.Min(Zoom, 3.5f);
        }
    }
}
