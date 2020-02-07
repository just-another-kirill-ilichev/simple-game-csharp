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
        private Dictionary<Type, Dictionary<int, Component>> _componentsByType;
        private List<int> _ids;

        public IEnumerable<int> Entities { get => _ids; }

        public EntityManager()
        {
            _componentsByType = new Dictionary<Type, Dictionary<int, Component>>();
            _ids = new List<int>();
        }

        public void AddEntity(IEnumerable<Component> entityComponents)
        {
            int id = _ids.Count;

            _ids.Add(id);

            foreach (var component in entityComponents)
            {
                if (!_componentsByType.ContainsKey(component.GetType()))
                {
                    _componentsByType.Add(component.GetType(), new Dictionary<int, Component>());
                }

                _componentsByType[component.GetType()].Add(id, component);
            }

        }

        public void RemoveEntity(int entityId)
        {
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
    }
}