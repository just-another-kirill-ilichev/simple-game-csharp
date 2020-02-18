using System;
using SDL2;

namespace SimpleGame.Core
{
    public class Font : Resource
    {
        private IntPtr _fontHandle;

        public Font(SDLApplication owner, string path, int ptsize) : base(owner)
        {

        }

        protected override void FreeUnmanaged()
        {
            SDL_ttf.TTF_CloseFont(_fontHandle);
        }
    }
}