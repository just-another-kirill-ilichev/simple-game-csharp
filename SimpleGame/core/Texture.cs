using SDL2;
using System;
using System.Drawing;

namespace SimpleGame.Core
{
    public class Texture : Resource
    {
        private IntPtr _textureHandle;

        public Size Size
        {
            get
            {
                CheckDisposed(nameof(_textureHandle));
                SDL.SDL_QueryTexture(_textureHandle, out _, out _, out int w, out int h);
                return new Size(w, h);
            }
        }

        public Texture(SDLApplication owner, string path) : base(owner)
        {
            _textureHandle = SDL_image.IMG_LoadTexture(owner.Window.Renderer, path);

            if (_textureHandle == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError()); // TODO
        }

        public Texture(SDLApplication owner, Surface surface) : base(owner)
        {
            _textureHandle = SDL.SDL_CreateTextureFromSurface(owner.Window.Renderer, surface.Handle);

            if (_textureHandle == IntPtr.Zero)
                throw new Exception(SDL.SDL_GetError()); // TODO
        }

        public void Draw(int x, int y)
        {
            CheckDisposed(nameof(_textureHandle));

            SDL.SDL_Rect rect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = Size.Width,
                h = Size.Height
            };

            SDL.SDL_RenderCopy(OwnerApp.Window.Renderer, _textureHandle, IntPtr.Zero, ref rect);
        }

        protected override void FreeUnmanaged()
        {
            SDL.SDL_DestroyTexture(_textureHandle);
        }
    }
}