using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleGame.ECS.Components;

namespace SimpleGame.Core
{
    public class SceneData
    {
        public Dictionary<string, string> Resources { get; set; }
        public List<string> Systems { get; set; }
        public List<List<Component>> Components { get; set; }
    }

    public class SceneLoader
    {

        public SceneLoader()
        {

        }

        public void LoadScene(SDLApplication app, string path)
        {
            app.EntityManager.Clear();
            app.ResourceManager.Clear();
            app.SystemManager.Clear();

            // TODO
        }
    }
}