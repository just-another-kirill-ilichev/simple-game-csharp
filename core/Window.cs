using System;
using SDL2;

namespace SimpleGame.Core
{
    public class Window : IDisposable
    {
        private IntPtr _windowHandle;
        private IntPtr _rendererHandle;

        private bool _disposed;

        public string Title
        {
            get { CheckDisposed(); return SDL.SDL_GetWindowTitle(_windowHandle); }
            set { CheckDisposed(); SDL.SDL_SetWindowTitle(_windowHandle, value); }
        }

        public IntPtr Renderer
        {
            get { CheckDisposed(); return _rendererHandle; }
        }

        public Window(string title, int w, int h)
        {
            _disposed = true;

            _windowHandle = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED, w, h, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            _rendererHandle = SDL.SDL_CreateRenderer(_windowHandle, -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            SDL.SDL_SetRenderDrawBlendMode(_rendererHandle, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        }

        public void Clear()
        {
            SDL.SDL_RenderClear(_rendererHandle);
        }

        public void Refresh()
        {
            SDL.SDL_RenderPresent(_rendererHandle);
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(_windowHandle));
            }
        }

        private void CleanUp()
        {
            if (_disposed)
                return;

            SDL.SDL_DestroyRenderer(_rendererHandle);
            SDL.SDL_DestroyWindow(_windowHandle);
        }

        public void Dispose()
        {
            CleanUp();
        }

        ~Window()
        {
            CleanUp();
        }
    }
}