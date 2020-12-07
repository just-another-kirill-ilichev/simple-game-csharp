namespace SimpleGame.ECS.Components
{
    public class ModelComponent : Component
    {
        public string ModelResourceRef { get; set; }
        public string ShaderResourceRef { get; set; }

        public override object Clone() =>
            new ModelComponent
            {
                ModelResourceRef = ModelResourceRef,
                ShaderResourceRef = ShaderResourceRef
            };
    }
}