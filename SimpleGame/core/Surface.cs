using System;
using System.Drawing;


namespace SimpleGame.Core
{
    /*public class Surface : Resource
    {
        private IntPtr _surfaceHandle;

        public IntPtr Handle { get => _surfaceHandle; }

        public Surface(SDLApplication owner, IntPtr surface) : base(owner)
        {
            _surfaceHandle = surface;
        }

        public void Blit(Surface source)
        {
            SDL.SDL_BlitSurface(source._surfaceHandle, IntPtr.Zero, _surfaceHandle, IntPtr.Zero);
        }

        protected override void FreeUnmanaged()
        {
            SDL.SDL_FreeSurface(_surfaceHandle);
        }
    }*/
}