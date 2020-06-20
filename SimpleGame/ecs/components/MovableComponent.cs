namespace SimpleGame.ECS.Components
{
    public class MovableComponent : Component
    {
        public float Speed { get; set; }
        public float JumpSpeed { get; set; }

        public override object Clone() =>
            new MovableComponent()
            {
                Speed = Speed, 
                JumpSpeed = JumpSpeed
            };
    }
}