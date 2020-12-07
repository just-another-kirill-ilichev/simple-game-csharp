namespace SimpleGame.ECS.Components
{
    public class SpriteComponent : Component
    {
        public string SpriteResourceRef { get; set; }
        public string ShaderResourceRef { get; set; }

        public override object Clone() =>
            new SpriteComponent
            {
                SpriteResourceRef = SpriteResourceRef,
                ShaderResourceRef = ShaderResourceRef
            };
    }
}