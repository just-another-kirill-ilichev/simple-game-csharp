namespace SimpleGame.ECS.Components
{
    public class AnimatedSpriteComponent : SpriteComponent
    {
        public int Frame { get; set; } = 0;
        public float FrameWidth { get; set; }
        public float FrameHeight { get; set; }

        public override object Clone() =>
            new AnimatedSpriteComponent
            {
                SpriteResourceRef = SpriteResourceRef,
                ShaderResourceRef = ShaderResourceRef,
                Frame = Frame,
                FrameWidth = FrameWidth,
                FrameHeight = FrameHeight
            };
    }
}