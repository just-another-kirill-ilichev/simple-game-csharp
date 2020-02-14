using System;
using System.IO;
using Newtonsoft.Json;
using SDL2;
using SimpleGame.ECS;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Systems;

namespace SimpleGame.Core
{
    public class SDLApplication : IDisposable
    {
        private bool _isRunning;
        public Window Window { get; }
        public ResourceManager ResourceManager { get; }
        public EntityManager EntityManager { get; }
        public SystemManager SystemManager { get; }

        public SDLApplication()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            _isRunning = true;
            
            Window = new Window("test", 800, 600);
            Events.Close += (o, e) => _isRunning = false;
            
            ResourceManager = new ResourceManager(this);
            ResourceManager.Load("tBrick", "SimpleGame.Core.Texture", "./resources/brick.jpg");
            SystemManager = new SystemManager();
            SystemManager.Add(new RenderingSystem(this));
            EntityManager = new EntityManager();
            
            var entity = new Component[]
            {
                // Create a name for the new entity. It will be used for logging & debuging
                new NameComponent() { Name = "Test entity" }, 
                // TransformComponent holds information about position and rotation of entity
                // This information used by RenderingSystem and MovementSystem to provide
                // visualization of the entity and response to keyboard input  
                new TransformComponent(){ X = 59, Y = 30, Rotation = 0 }, 
                // TextureComponent contains name of texture resource stored into ResourceManager class
                // Used by any kind of visualization system
                new TextureComponent() { TextureResourceName = "tBrick" }
            };

            // using (FileStream fs = new FileStream("./resources/test.json", FileMode.Truncate))
            // {
                
            // }


            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            Console.WriteLine(JsonConvert.SerializeObject(entity, settings));

            EntityManager.AddEntity(0, entity);
        }

        public void Run()
        {
            while (_isRunning)
            {
                var time = DateTime.Now;

                Events.Process();

                // TODO Logic update

                Window.Clear();
                SystemManager.Redraw();
                Window.Refresh();

                var delta = DateTime.Now - time;
                Console.Write($"\rFrame time: {delta.TotalMilliseconds}");
            }

            Console.Clear();
        }

        public void Dispose()
        {
            Window.Dispose();

            SDL.SDL_Quit();
        }
    }
}