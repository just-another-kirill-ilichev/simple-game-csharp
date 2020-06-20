using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleGame.ECS;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;
using SimpleGame.Core.Utils.Extensions;
using SimpleGame.Core.Resources;

namespace SimpleGame.Core.Loaders
{
    public class ResourceData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object[] Args { get; set; }
    }

    public class EntityData
    {
        public int Id { get; set; } = EntityManager.InvalidEntityId;
        public string PrefabName { get; set; }
        public Component[] Components { get; set; }
    }

    public class SceneData
    {
        public string[] Prefabs { get; set; }
        public ResourceData[] Resources { get; set; }
        public string[] Systems { get; set; }
        public EntityData[] Entities { get; set; }
    }

    public class SceneLoader
    {
        private readonly Dictionary<string, Component[]> _prefabs;
        private readonly JsonSerializerSettings _serializationSetting;

        public Application OwnerApp { get; }

        public SceneLoader(Application owner)
        {
            OwnerApp = owner;

            _serializationSetting = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                NullValueHandling = NullValueHandling.Ignore
            };

            _prefabs = new Dictionary<string, Component[]>();
        }

        public void LoadScene(string path)
        {
            OwnerApp.EntityManager.Clear();
            OwnerApp.ResourceManager.Clear();
            OwnerApp.SystemManager.Clear();

            string content = File.ReadAllText(path); 
            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), _serializationSetting) as SceneData;

            // TODO use custom factories instead of using Activator.CreateInstance()
            LoadPrefabs(sceneData.Prefabs);
            LoadResources(sceneData.Resources);
            LoadEntities(sceneData.Entities);
            LoadSystems(sceneData.Systems);
        }

        private void LoadPrefabs(string[] paths)
        {
            foreach (var path in paths)
            {
                LoadPrefabFile(path);
            }
        }

        private void LoadPrefabFile(string path)
        {
            string content = File.ReadAllText(path);
            
            var prefabsData = JsonConvert.DeserializeObject(content, typeof(Dictionary<string, Component[]>), _serializationSetting) 
                as Dictionary<string, Component[]>;
            
            foreach (var prefab in prefabsData)
            {
                bool success = _prefabs.TryAdd(prefab.Key, prefab.Value);

                if (!success)
                    throw new GameException($"Many prefabs with the same name are not allowed (in file {path})");
            }
        }

        private void LoadDefaultResources()
        {
            // OwnerApp.ResourceManager.Add("meshPlane");
        }

        private void LoadResources(ResourceData[] resources)
        {
            LoadDefaultResources();

            foreach (var resourceData in resources)
            {
                LoadResource(resourceData);
            }
        }

        private void LoadResource(ResourceData data)
        {
            var type = Type.GetType(data.Type);

            // NewtonsoftJson parses all numbers as Int64
            // to prevent cast exceptions convert it to Int32
            var args = data.Args.Select(x => x is long ? Convert.ToInt32(x) : x).ToArray();
            var resource = Activator.CreateInstance(type, args) as Resource;

            OwnerApp.ResourceManager.Add(data.Name, resource);
        }

        private void LoadEntities(EntityData[] entities)
        {
            int id = 0; // TODO store id in json?

            foreach (var entity in entities)
            {
                var components = entity.Components;

                if (!String.IsNullOrEmpty(entity.PrefabName))
                {
                    if (!_prefabs.TryGetValue(entity.PrefabName, out var prefab))
                        throw new GameException($"Prefab definition not found, prefab name - {entity.PrefabName}");

                    prefab = prefab.Select(x => x.Clone() as Component).ToArray();
                    
                    components = components
                        .Concat(prefab)
                        .GroupBy(x => x.GetType()) // Remove duplicates
                        .Select(group => group.First())
                        .ToArray();
                }

                OwnerApp.EntityManager.CreateEntity(id++, components); // TODO id
            }
        }

        private void LoadSystems(string[] systems)
        {
            foreach (var systemName in systems)
            {
                var system = Activator.CreateInstance(Type.GetType(systemName), OwnerApp) as SystemBase;
                OwnerApp.SystemManager.Add(system);
            }
        }
    }
}