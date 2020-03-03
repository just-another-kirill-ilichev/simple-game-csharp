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
            Draw(x, y, Size.Width, Size.Height);
        }

        public void Draw(int x, int y, int w, int h)
        {
            CheckDisposed(nameof(_textureHandle));

            SDL.SDL_Rect rect = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };

            SDL.SDL_RenderCopy(OwnerApp.Window.Renderer, _textureHandle, IntPtr.Zero, ref rect);
        }

        public void Draw(Rectangle crop, Rectangle bounds)
        {
            CheckDisposed(nameof(_textureHandle));

            SDL.SDL_Rect crop_sdl = new SDL.SDL_Rect()
            {
                x = crop.X,
                y = crop.Y,
                w = crop.Width,
                h = crop.Height
            };

            SDL.SDL_Rect bounds_sdl = new SDL.SDL_Rect()
            {
                x = bounds.X,
                y = bounds.Y,
                w = bounds.Width,
                h = bounds.Height
            };

            SDL.SDL_RenderCopy(OwnerApp.Window.Renderer, _textureHandle, ref crop_sdl, ref bounds_sdl);
        }

        public void Draw(Rectangle crop, Rectangle bounds, double angle, Point center, SDL.SDL_RendererFlip flip)
        {
            CheckDisposed(nameof(_textureHandle));

            SDL.SDL_Rect crop_sdl = new SDL.SDL_Rect()
            {
                x = crop.X,
                y = crop.Y,
                w = crop.Width,
                h = crop.Height
            };

            SDL.SDL_Rect bounds_sdl = new SDL.SDL_Rect()
            {
                x = bounds.X,
                y = bounds.Y,
                w = bounds.Width,
                h = bounds.Height
            };

            SDL.SDL_Point center_sdl = new SDL.SDL_Point() { x = center.X, y = center.Y };

            SDL.SDL_RenderCopyEx(OwnerApp.Window.Renderer, _textureHandle, ref crop_sdl, ref bounds_sdl, angle, ref center_sdl, flip);
        }

        public void Draw(int x, int y, int w, int h, double angle)
        {
            CheckDisposed(nameof(_textureHandle));

            SDL.SDL_Rect bounds_sdl = new SDL.SDL_Rect()
            {
                x = x,
                y = y,
                w = w,
                h = h
            };  

            SDL.SDL_Point center = new SDL.SDL_Point() { x = w / 2, y = h / 2 };

            SDL.SDL_RenderCopyEx(OwnerApp.Window.Renderer, _textureHandle, IntPtr.Zero, ref bounds_sdl, angle, ref center, SDL.SDL_RendererFlip.SDL_FLIP_NONE);     
        }

        protected override void FreeUnmanaged()
        {
            SDL.SDL_DestroyTexture(_textureHandle);
        }
    }
}