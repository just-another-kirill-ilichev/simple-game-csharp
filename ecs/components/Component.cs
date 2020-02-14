namespace SimpleGame.ECS.Components
{
    public abstract class Component
    {
        public int EntityId { get; set; }

        public Component()
        {
            EntityId = EntityManager.InvalidEntityId;
        }
    }
}