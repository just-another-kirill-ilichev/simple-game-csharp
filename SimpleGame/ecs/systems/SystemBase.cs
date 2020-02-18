using SimpleGame.Core;

namespace SimpleGame.ECS.Systems
{
    public abstract class SystemBase
    {
        public SDLApplication OwnerApp;

        public SystemBase(SDLApplication owner)
        {
            OwnerApp = owner;
        }

        public virtual void Redraw() {}
        public virtual  void Update(uint deltaTicks) {}
    }
}