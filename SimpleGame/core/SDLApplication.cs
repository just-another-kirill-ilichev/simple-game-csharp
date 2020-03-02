using System;
using SDL2;
using SimpleGame.ECS;


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
            //SDL_image.IMG_Init();
            SDL_ttf.TTF_Init();

            _isRunning = true;

            Window = new Window("test", 800, 600);
            Events.Close += (o, e) => _isRunning = false;

            ResourceManager = new ResourceManager(this);
            ResourceManager.Load("tBrick", "SimpleGame.Core.Texture", "../resources/brick.jpg");
            ResourceManager.Add("fontTest24", new Font(this, "../resources/10423.ttf", 24));
            SystemManager = new SystemManager();
            EntityManager = new EntityManager();

            var sceneLoader = new SceneLoader();
            sceneLoader.LoadScene(this, "../resources/test.json");
        }

        public void Run()
        {
            while (_isRunning)
            {
                var time = DateTime.Now;

                Events.Process();

                SystemManager.Update(16); // TODO

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