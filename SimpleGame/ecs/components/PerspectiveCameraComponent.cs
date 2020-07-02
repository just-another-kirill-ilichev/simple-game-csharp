using System;

namespace SimpleGame.ECS.Components
{
    public class PerspectiveCameraComponent : Component
    {
        public float FieldOfView { get; set; } = 60 * (MathF.PI / 180f);
        public float AspectRatio { get; set; } = 4f / 3f;
        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 4000.0f;
        
        public override object Clone() =>
            new PerspectiveCameraComponent()
            {
                FieldOfView = FieldOfView
            };
    }
}