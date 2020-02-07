using System;
using System.Linq;
using SDL2;
using SimpleGame.ECS;
using SimpleGame.ECS.Components;

namespace SimpleGame.Core
{
    public class SDLApplication : IDisposable
    {
        private bool _isRunning;
        public Window Window { get; }
        private ResourceManager _resourceManager;
        private EntityManager _entityManager;

        public SDLApplication()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            _isRunning = true;
            _resourceManager = new ResourceManager(this);

            Window = new Window("test", 800, 600);

            _resourceManager.Load("tBrick", "SimpleGame.Core.Texture", "./resources/brick.jpg");
            _entityManager = new EntityManager();

            var entity = new Component[]
            {
                new TransformComponent(){ X = 59, Y = 30, Angle = 0 },
                new TextureComponent() { TextureResourceName = "tBrick" }
            };

            _entityManager.AddEntity(entity);

            Events.Close += (o, e) => _isRunning = false;
        }

        public void Run()
        {
            while (_isRunning)
            {
                Events.Process();

                // TODO Logic update

                Window.Clear();

                var entities = _entityManager.Entities
                    .WithComponent<TransformComponent>(_entityManager)
                    .WithComponent<TextureComponent>(_entityManager)
                    .ToArray();

                var transform = _entityManager.GetComponent<TransformComponent>(entities[0]);
                var texture = _entityManager.GetComponent<TextureComponent>(entities[0]);

                _resourceManager.Get<Texture>(texture.TextureResourceName).Draw((int)transform.X, (int)transform.Y);
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