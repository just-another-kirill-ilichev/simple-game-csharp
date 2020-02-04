using SDL2;
using System.Drawing; // TODO ?

namespace SimpleGame.Core
{
    public static class Mouse
    {
        public static bool ShowCursor
        {
            get => SDL.SDL_ShowCursor(SDL.SDL_QUERY) == SDL.SDL_ENABLE;
            set => SDL.SDL_ShowCursor(value ? SDL.SDL_ENABLE : SDL.SDL_DISABLE);
        }

        public static bool CaptureMouse
        {
            set => SDL.SDL_CaptureMouse((value ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE));
        }

        public static Point PositionInScreen
        {
            set => SDL.SDL_WarpMouseGlobal(value.X, value.Y);
        }


    }
}