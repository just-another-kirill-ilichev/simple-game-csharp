using System.Collections.Generic;
using SimpleGame.Core.Resources;

namespace SimpleGame.Core
{
    public class ResourceManager
    {
        private Dictionary<string, Resource> _resources;

        public ResourceManager()
        {
            _resources = new Dictionary<string, Resource>();
        }

        public void Add(string name, Resource resource) =>
            _resources.Add(name, resource);

        public bool Contains(string name) =>
            _resources.ContainsKey(name);

        public T Get<T>(string name) where T : Resource =>
            _resources[name] as T;

        public void Clear()
        {
            foreach (var resource in _resources.Values)
            {
                resource.Dispose();
            }

            _resources.Clear();
        }
    }
}