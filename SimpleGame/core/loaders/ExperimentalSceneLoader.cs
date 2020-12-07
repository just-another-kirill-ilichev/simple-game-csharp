using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleGame.Core.Resources;
using SimpleGame.ECS;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;

namespace SimpleGame.Core.Loaders.Experimental
{
    internal class ExperimentalSceneLoader
    {
        internal class EntityDataBase
        {
            public string[] Components { get; set; }
            public Dictionary<string, object> Properties { get; set; }
        }

        internal class EntityData : EntityDataBase
        {
            public string Prefab { get; set; }
            public Dictionary<string, EntityData> Children { get; set; }
        }

        internal class SceneData
        {
            public string[] Prefabs { get; set; }
            //public ResourceData[] Resources { get; set; }
            public string[] Systems { get; set; }
            public EntityData Scene { get; set; } // Root entity
        }

        private readonly JsonSerializerSettings _settings;
        private readonly Application _app;
        private readonly Dictionary<string, EntityDataBase> _prefabs;

        public ExperimentalSceneLoader(Application app)
        {
            _app = app;
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            _prefabs = new Dictionary<string, EntityDataBase>();
        }

        public void LoadScene(string path)
        {
            _app.EntityManager.Clear();
            _app.ResourceManager.Clear();
            _app.SystemManager.Clear();

            string content = File.ReadAllText(path);
            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), _settings) as SceneData;

            LoadPrefabs(sceneData.Prefabs);
            //LoadResources(sceneData.Resources);
            LoadEntities(sceneData.Scene);
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

            var prefabsData = JsonConvert.DeserializeObject(content, typeof(Dictionary<string, EntityDataBase>), _settings)
                as Dictionary<string, EntityDataBase>;

            foreach (var prefab in prefabsData)
            {
                bool success = _prefabs.TryAdd(prefab.Key, prefab.Value);

                if (!success)
                    throw new GameException($"Many prefabs with the same name are not allowed (in file {path})"); // TODO
            }
        }

        private void LoadEntities(EntityData scene)
        {
            int id = 0;
            var prefabs = new Dictionary<string, EntityDataBase>();

            CreateEntity("Scene", scene, ref id, EntityManager.InvalidEntityId);
        }

        private void CreateEntity(string name, EntityData data, ref int id, int parent)
        {
            var children = new List<int>();
            int selfId = id;

            if (data.Children != null)
            {
                foreach (var entity in data.Children)
                {
                    children.Add(++id);
                    CreateEntity(entity.Key, entity.Value, ref id, selfId);
                }
            }

            var componentsList = BuildComponents(data);
            componentsList.Add(new NameComponent { Name = name });
            componentsList.Add(new LogicalRelationComponent
            {
                Parent = parent,
                Children = children.ToArray()
            });

            _app.EntityManager.CreateEntity(selfId, componentsList);
        }

        private List<Component> BuildComponents(EntityData data)
        {
            var prefab = _prefabs[data.Prefab];
            var components = data.Components.Union(prefab.Components).ToArray();
            var properties = data.Properties;

            foreach (var property in prefab.Properties)
            {
                if (!properties.ContainsKey(property.Key))
                    properties.Add(property.Key, property.Value);
            }

            return BuildProperties(properties, components);
        }

        private List<Component> BuildProperties(Dictionary<string, object> properties, string[] components)
        {
            var componentsList = new List<Component>();
            var propertyNameToComponent = new Dictionary<string, Component>();

            foreach (var componentType in components)
            {
                var type = Type.GetType(componentType);
                var component = Activator.CreateInstance(type) as Component;

                componentsList.Add(component);

                var ComponentPropertiesTypes = type.GetProperties();

                foreach (var propertyType in ComponentPropertiesTypes)
                {
                    if (propertyNameToComponent.ContainsKey(propertyType.Name))
                        throw new GameException("Properties with the same name are not allowed."); // TODO

                    propertyNameToComponent.Add(propertyType.Name, component);
                }
            }

            foreach (var property in properties)
            {
                if (!propertyNameToComponent.ContainsKey(property.Key))
                    throw new GameException($"Definition of entity does not contain component with property named: {property.Key}"); // TODO

                var component = propertyNameToComponent[property.Key];

                var propertyInfo = component.GetType().GetProperty(property.Key);
                propertyInfo.SetValue(component, property.Value); // TODO
            }

            return null;
        }

        private void LoadDefaultResources()
        {
            // OwnerApp.ResourceManager.Add("meshPlane");
        }

       /* private void LoadResources(ResourceData[] resources)
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

            _app.ResourceManager.Add(data.Name, resource);
        }*/

        private void LoadSystems(string[] systems)
        {
            foreach (var systemName in systems)
            {
                var system = Activator.CreateInstance(Type.GetType(systemName), _app) as SystemBase;
                _app.SystemManager.Add(system);
            }
        }
    }
}