using System;
using System.Linq;
using System.Collections.Generic;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS
{
    public static class Extension
    {
        public static IEnumerable<int> WithComponent<T>(this IEnumerable<int> entities, EntityManager manager)
        {
            if (entities == manager.Entities)
                return manager.GetComponentsByType<T>().Keys;
            else
                return entities.Intersect(manager.GetComponentsByType<T>().Keys);
        }
    }


    public class EntityManager
    {
        public static int InvalidEntityId => -1;

        private Dictionary<Type, Dictionary<int, Component>> _componentsByType;
        private List<int> _ids;

        public IEnumerable<int> Entities { get => _ids; }

        public EntityManager()
        {
            _componentsByType = new Dictionary<Type, Dictionary<int, Component>>();
            _ids = new List<int>();
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

            _ids.Add(entityId);

            foreach (var component in entityComponents)
            {
                AddComponent(entityId, component);
            }
        }

        public void RemoveEntity(int entityId)
        {
            if (!Entities.Contains(entityId))
                throw new Exception(); // TODO
            
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
                throw new Exception(); // TODO

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

        public bool HasComponent<T>(int entityId) where T : Component
        {
            if (!Entities.Contains(entityId))
                throw new Exception(); // TODO
            
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
                throw new Exception(); // TODO
            }

            return _componentsByType[typeof(T)][entityId] as T;
        }

        public Dictionary<int, Component> GetComponentsByType<T>()
        {
            if (!_componentsByType.ContainsKey(typeof(T)))
                throw new Exception(); // TODO

            return _componentsByType[typeof(T)];
        }

        public void Clear()
        {
            _componentsByType.Clear();
            _ids.Clear();
        }
    }
}