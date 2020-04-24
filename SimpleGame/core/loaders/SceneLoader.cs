using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;
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
        public int Id { get; set; }
        public string PrefabRef { get; set; }
        public Component[] Components { get; set; }
    }

    public class PrefabsData
    {
        public Dictionary<string, Component[]> Prefabs { get; set; }
    }

    public class SceneData
    {
        public string[] Prefabs { get; set; }
        public ResourceData[] Resources { get; set; }
        public string[] Systems { get; set; }
        public Component[][] Entities { get; set; }
    }

    public class SceneLoader
    {
        private readonly Dictionary<string, Component[]> _prefabs;

        public Application OwnerApp { get; }

        public SceneLoader(Application owner)
        {
            OwnerApp = owner;

            _prefabs = new Dictionary<string, Component[]>();
        }

        public void LoadScene(string path)
        {
            OwnerApp.EntityManager.Clear();
            OwnerApp.ResourceManager.Clear();
            OwnerApp.SystemManager.Clear();

            string content = File.ReadAllText(path);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                NullValueHandling = NullValueHandling.Ignore
            };

            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), settings) as SceneData;

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

            // TODO
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

        private void LoadEntities(Component[][] entities)
        {
            int id = 0; // TODO store id in json?

            foreach (var entity in entities)
            {
                OwnerApp.EntityManager.CreateEntity(id++, entity); // TODO id
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