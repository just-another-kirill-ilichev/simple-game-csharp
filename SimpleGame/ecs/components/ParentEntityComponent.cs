namespace SimpleGame.ECS.Components
{
    public class ParentEntityComponent : Component
    {
        public int ParentEntityId { get; set; }

        public ParentEntityComponent() : base()
        {
            ParentEntityId = EntityManager.InvalidEntityId;
        }
    }
}