using System;
using Newtonsoft.Json;

namespace SimpleGame.ECS.Components
{
    public abstract class Component : ICloneable
    {
        [JsonIgnoreAttribute]
        public int EntityId { get; set; }

        public Component()
        {
            EntityId = EntityManager.InvalidEntityId;
        }

        public abstract object Clone();
    }
}