using System;
using System.Drawing;

namespace SimpleGame.Core
{
   /* public class Font : Resource
    {
        private IntPtr _fontHandle;

        public Font(SDLApplication owner, string path, int ptsize) : base(owner)
        {
            _fontHandle = SDL_ttf.TTF_OpenFont(path, ptsize);

            if (_fontHandle == IntPtr.Zero)
            {
                throw new GLException();
            }
        }

        public Surface Print(string text, Color textColor)
        {
            var color = new SDL.SDL_Color()
            {
                r = textColor.R,
                g = textColor.G,
                b = textColor.B,
                a = textColor.A
            };

            var surfaceHandle = SDL_ttf.TTF_RenderUNICODE_Blended(_fontHandle, text, color);
            var surface = new Surface(OwnerApp, surfaceHandle);

            return surface;
        }

        public Size TextSize(string text)
        {
            var code = SDL_ttf.TTF_SizeUNICODE(_fontHandle, text, out int w, out int h);

            if (code != 0)
                throw new GLException();

            return new Size(w, h);
        }

        protected override void FreeUnmanaged()
        {
            SDL_ttf.TTF_CloseFont(_fontHandle);
        }
    }*/
}