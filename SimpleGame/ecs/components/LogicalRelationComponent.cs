namespace SimpleGame.ECS.Components
{
    public sealed class LogicalRelationComponent : Component
    {
        public int Parent { get; set; }
        public int[] Children { get; set; }

        public LogicalRelationComponent() : base()
        {
            Parent = EntityManager.InvalidEntityId;
            Children = null;
        }

        public override object Clone() =>
            new LogicalRelationComponent()
            {
                Parent = Parent,
                Children = Children
            };

    }
}