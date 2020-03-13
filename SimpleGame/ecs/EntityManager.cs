using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS
{
    public class EntityManager
    {
        public static int InvalidEntityId => -1;

        private Dictionary<Type, Dictionary<int, Component>> _componentsByType;

        public List<int> Entities { get; }

        public EntityManager()
        {
            _componentsByType = new Dictionary<Type, Dictionary<int, Component>>();
            Entities = new List<int>();
        }

        public void AddComponent(int entityId, Component component)
        {
            if (!Entities.Contains(entityId))
                throw new ArgumentException($"Entity with id={entityId} does not exist.");

            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (component.EntityId != InvalidEntityId)
                throw new ArgumentException("This component already have owner entity.");

            if (!_componentsByType.ContainsKey(component.GetType()))
                _componentsByType.Add(component.GetType(), new Dictionary<int, Component>());

            _componentsByType[component.GetType()].Add(entityId, component);
        }

        public void CreateEntity(int entityId, IEnumerable<Component> entityComponents)
        {
            if (Entities.Contains(entityId))
                throw new ArgumentException($"Entity with id = {entityId} already exist.");

            if (entityId == InvalidEntityId)
                throw new ArgumentException("Cannot create entity with invalid id.");

            if (entityComponents is null)
                throw new ArgumentNullException(nameof(entityComponents));

            Entities.Add(entityId);

            foreach (var component in entityComponents)
            {
                AddComponent(entityId, component);
            }
        }

        public void RemoveEntity(int entityId)
        {
            if (!Entities.Contains(entityId))
                throw new ArgumentException($"There is no entity with id = {entityId}");

            foreach (var components in _componentsByType.Values)
            {
                if (components.ContainsKey(entityId))
                {
                    components.Remove(entityId);
                }
            }
        }

        public IEnumerable<Component> GetEntity(int entityId)
        {
            if (!Entities.Contains(entityId))
                throw new ArgumentException($"There is no entity with id = {entityId}");

            var temp = new List<Component>();

            foreach (var components in _componentsByType.Values)
            {
                if (components.ContainsKey(entityId))
                {
                    temp.Add(components[entityId]);
                }
            }

            return temp;
        }

        public IEnumerable<Component> GetEntity(string name)
        {
            var entity = GetComponentsByType<NameComponent>()
                         .First(x => (x.Value as NameComponent)?.Name == name).Key;

            return GetEntity(entity);
        }

        public bool HasComponent<T>(int entityId) where T : Component
        {
            if (!Entities.Contains(entityId))
                throw new ArgumentException($"There is no entity with id = {entityId}");

            if (_componentsByType.ContainsKey(typeof(T)))
            {
                return _componentsByType[typeof(T)].ContainsKey(entityId);
            }
            else
            {
                return false;
            }
        }

        public T GetComponent<T>(int entityId) where T : Component
        {
            if (!HasComponent<T>(entityId))
            {
                throw new ArgumentException($"Entity with id = {entityId} doesnt have component with type = {typeof(T)}");
            }

            return _componentsByType[typeof(T)][entityId] as T;
        }

        public IReadOnlyDictionary<int, Component> GetComponentsByType<T>()
        {
            if (!_componentsByType.ContainsKey(typeof(T)))
                return ImmutableDictionary.Create<int, Component>();

            return _componentsByType[typeof(T)];
        }

        public IEnumerable<int> WithComponent<T>()
        {
            return GetComponentsByType<T>().Keys;
        }

        public IEnumerable<int> WithComponent<T1, T2>()
        {
            return WithComponent<T1>().Intersect(WithComponent<T2>());
        }

        public IEnumerable<int> WithComponent<T1, T2, T3>()
        {
            return WithComponent<T1>()
                   .Intersect(WithComponent<T2>())
                   .Intersect(WithComponent<T3>());
        }


        public void Clear()
        {
            _componentsByType.Clear();
            Entities.Clear();
        }
    }
}