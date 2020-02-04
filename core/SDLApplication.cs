using SDL2;

namespace SimpleGame.Core
{
    public class SDLApplication
    {
        private bool _isRunning;
        public Window Window { get; }

        public SDLApplication()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            _isRunning = true;

            Window = new Window("test", 800, 600);
            
            Events.Close += (o, e) => _isRunning = false;
        }

        public void Run()
        {
            while (_isRunning)
            {
                Events.Process();
            }
        }

        public void CleanUp()
        {
            Window.Dispose();

            SDL.SDL_Quit();
        }
    }
}