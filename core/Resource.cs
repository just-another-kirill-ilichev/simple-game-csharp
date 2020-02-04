using System;

namespace SimpleGame.Core
{
    public abstract class Resource
    {
        public SDLApplication OwnerApp { get; }

        public Resource(SDLApplication owner)
        {
            OwnerApp = owner;
        }
    }
}