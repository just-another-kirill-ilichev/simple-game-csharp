using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleGame.ECS;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;
using SimpleGame.Core.Resources;

namespace SimpleGame.Core.Loaders
{
    public class EntityData
    {
        public int Id { get; set; } = EntityManager.InvalidEntityId;
        public string PrefabName { get; set; }
        public Component[] Components { get; set; }
    }

    public class SceneData
    {
        public string[] PrefabPaths { get; set; }
        public Dictionary<string, Resource> Resources { get; set; }
        public string[] Systems { get; set; }
        public EntityData[] Entities { get; set; }
    }

    public class SceneLoader
    {
        private readonly Dictionary<string, Component[]> _prefabs; // TODO clear old
        private string _file;
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

            _file = path;
            string content = File.ReadAllText(path); 
            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), _serializationSetting) as SceneData;

            LoadPrefabs(sceneData.PrefabPaths);
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
                    throw new LoaderException(_file, $"Many prefabs with the same name are not allowed (in file {path})");
            }
        }

        private void LoadDefaultResources()
        {
            // OwnerApp.ResourceManager.Add("meshPlane");
        }

        private void LoadResources(Dictionary<string, Resource> resources)
        {
            LoadDefaultResources();

            foreach (var resource in resources)
            {
                OwnerApp.ResourceManager.Add(resource.Key, resource.Value);
            }
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
                        throw new LoaderException(_file, $"Prefab definition not found, prefab name - {entity.PrefabName}");

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