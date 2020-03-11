using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;

namespace SimpleGame.Core
{
    public class ResourceData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object[] Args { get; set; }
    }

    public class SceneData
    {
        public List<ResourceData> Resources { get; set; }
        public List<string> Systems { get; set; }
        public List<List<Component>> Entities { get; set; }
    }

    public class SceneLoader
    {
        public Application OwnerApp { get; }

        public SceneLoader(Application owner)
        {
            OwnerApp = owner;
        }

        public void LoadScene(string path)
        {
            OwnerApp.EntityManager.Clear();
            OwnerApp.ResourceManager.Clear();
            OwnerApp.SystemManager.Clear();

            string content;

            using (var file = File.OpenText(path))
            {
                content = file.ReadToEnd();
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
            };

            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), settings) as SceneData;

            // TODO use custom factories instead of using Activator.CreateInstance()

            foreach (var resourceData in sceneData.Resources)
            {
                LoadResource(resourceData);
            }

            foreach (var systemName in sceneData.Systems)
            {
                var system = Activator.CreateInstance(Type.GetType(systemName), OwnerApp) as SystemBase;
                OwnerApp.SystemManager.Add(system);
            }

            int id = 0; // TODO store id in json?

            foreach (var entity in sceneData.Entities)
            {
                OwnerApp.EntityManager.CreateEntity(id++, entity); // TODO id
            }
        }

        private void LoadDefaultResources()
        {
            // OwnerApp.ResourceManager.Add("meshPlane");
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
    }
}