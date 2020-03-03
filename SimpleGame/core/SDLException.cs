using SDL2;

namespace SimpleGame.Core
{
    public class SDLException : GameException
    {
        public SDLException() : base(SDL.SDL_GetError())
        {

        }
    }
}