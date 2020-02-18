using System.Collections.Generic;
using System.Reflection;
using System;

namespace SimpleGame.Core
{
    public class ResourceManager
    {
        private Dictionary<string, Resource> _resources;
        private Dictionary<string, ConstructorInfo> _constructorCache;
        public SDLApplication OwnerApp { get; }

        public ResourceManager(SDLApplication owner)
        {
            OwnerApp = owner;
            _resources = new Dictionary<string, Resource>();
            _constructorCache = new Dictionary<string, ConstructorInfo>();
        }

        public void Add(string name, Resource resource) =>
            _resources.Add(name, resource);

        public bool Contains(string name) =>
            _resources.ContainsKey(name);

        public void Load(string name, string typeName, string path)
        {
            if (Contains(name))
            {
                // TODO log error
                return;
            }

            if (!_constructorCache.ContainsKey(typeName))
            {
                var type = Type.GetType(typeName) ?? throw new Exception(); // TODO

                if (!type.IsSubclassOf(typeof(Resource)))
                    throw new Exception(); // TODO

                var args = new Type[] { typeof(SDLApplication), typeof(string) };
                var ctor = type.GetConstructor(args) ?? throw new Exception(); // TODO

                _constructorCache.Add(typeName, ctor);
            }

            var obj = _constructorCache[typeName].Invoke(new object[] { OwnerApp, path }) as Resource;

            _resources.Add(name, obj);
        }

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