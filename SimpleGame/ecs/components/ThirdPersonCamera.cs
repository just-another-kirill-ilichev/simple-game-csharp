namespace SimpleGame.ECS.Components
{
    public class ThirdPersonCameraComponent : PerspectiveCameraComponent
    {
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float OffsetZ { get; set; }

        public override object Clone()
        {
            var clone = base.Clone() as ThirdPersonCameraComponent;
            
            clone.OffsetX = OffsetX;
            clone.OffsetY = OffsetY;
            clone.OffsetZ = OffsetZ;

            return clone;
        }
    }
}