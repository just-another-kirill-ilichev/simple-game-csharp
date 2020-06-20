namespace SimpleGame.ECS.Components
{
    public sealed class NameComponent : Component
    {
        public string Name { get; set; }

        public override object Clone() =>
            new NameComponent()
            {
                Name = Name
            };

    }
}