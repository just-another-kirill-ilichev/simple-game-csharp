using Newtonsoft.Json;
using OpenTK;

namespace SimpleGame.ECS.Components
{
    public class TransformComponent : Component
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }

        public float ScaleX { get; set; } = 0.1f;
        public float ScaleY { get; set; } = 0.1f;
        public float ScaleZ { get; set; } = 0.1f;

        [JsonIgnore]
        public Matrix4 Transform => Matrix4.CreateScale(ScaleX, ScaleY, ScaleZ) *
                Matrix4.CreateRotationX(RotationX) *
                Matrix4.CreateRotationY(RotationY) *
                Matrix4.CreateRotationZ(RotationZ) *
                Matrix4.CreateTranslation(X, Y, Z);

    }
}