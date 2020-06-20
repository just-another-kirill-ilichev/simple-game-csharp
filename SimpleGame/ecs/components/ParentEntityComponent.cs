namespace SimpleGame.ECS.Components
{
    public sealed class ParentEntityComponent : Component
    {
        public int ParentEntityId { get; set; }

        public ParentEntityComponent() : base()
        {
            ParentEntityId = EntityManager.InvalidEntityId;
        }

        public override object Clone() =>
            new ParentEntityComponent()
            {
                ParentEntityId = ParentEntityId
            };

    }
}