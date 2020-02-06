using System;
using SDL2;

namespace SimpleGame.Core
{
    public class SDLApplication : IDisposable
    {
        private bool _isRunning;
        public Window Window { get; }
        private ResourceManager _resourceManager;

        public SDLApplication()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            _isRunning = true;
            _resourceManager = new ResourceManager(this);

            Window = new Window("test", 800, 600);

            _resourceManager.Load("tBrick", "SimpleGame.Core.Texture", "./resources/brick.jpg");
            
            Events.Close += (o, e) => _isRunning = false;
        }

        public void Run()
        {
            while (_isRunning)
            {
                Events.Process();

                // TODO Logic update

                Window.Clear();

                _resourceManager.Get<Texture>("tBrick").Draw(10, 10);
                // TODO Redraw
                Window.Refresh();
            }
        }

        public void Dispose()
        {
            Window.Dispose();

            SDL.SDL_Quit();
        }
    }
}