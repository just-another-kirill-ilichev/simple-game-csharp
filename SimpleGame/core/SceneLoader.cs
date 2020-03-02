using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;

namespace SimpleGame.Core
{
    public class SceneData
    {
        //public Dictionary<string, string> Resources { get; set; }
        public List<string> Systems { get; set; }
        public List<List<Component>> Entities { get; set; }
    }

    public class SceneLoader
    {

        public SceneLoader()
        {

        }

        public void LoadScene(SDLApplication app, string path)
        {
            app.EntityManager.Clear();
            //app.ResourceManager.Clear();
            app.SystemManager.Clear();

            string content;

            using (var file = File.OpenText(path))
            {
                content = file.ReadToEnd();
            }

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            var sceneData = JsonConvert.DeserializeObject(content, typeof(SceneData), settings) as SceneData;

            foreach (var systemName in sceneData.Systems)
            {
                var system = Activator.CreateInstance(Type.GetType(systemName), app) as SystemBase;
                app.SystemManager.Add(system);
            }

            int id = 0; // TODO

            foreach (var entity in sceneData.Entities)
            {
                app.EntityManager.CreateEntity(id++, entity); // TODO id
            }
        }
    }
}