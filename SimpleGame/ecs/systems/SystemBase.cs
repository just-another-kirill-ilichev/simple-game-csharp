using SimpleGame.Core;

namespace SimpleGame.ECS.Systems
{
    public abstract class SystemBase
    {
        public Application OwnerApp;

        public SystemBase(Application owner)
        {
            OwnerApp = owner;
        }

        public virtual void Redraw() {}
        public virtual  void Update(uint deltaMs) {}
    }
}