using System;

namespace SimpleGame.ECS.Components
{
    public class CameraComponent : Component
    {
        // TODO
        // public bool Perspective { get; set; } = true;
        public float FieldOfView { get; set; } = 60 * (MathF.PI / 180f);

        public override object Clone() =>
            new CameraComponent()
            {
                FieldOfView = FieldOfView
            };
    }
}