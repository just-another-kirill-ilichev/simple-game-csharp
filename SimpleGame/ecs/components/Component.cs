using Newtonsoft.Json;

namespace SimpleGame.ECS.Components
{
    public abstract class Component
    {
        [JsonIgnoreAttribute]
        public int EntityId { get; set; }

        public Component()
        {
            EntityId = EntityManager.InvalidEntityId;
        }
    }
}