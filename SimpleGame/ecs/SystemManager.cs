using System.Collections.Generic;

using SimpleGame.ECS.Systems;

namespace SimpleGame.ECS
{
    public class SystemManager
    {
        private List<SystemBase> _systems;

        public SystemManager()
        {
            _systems = new List<SystemBase>();
        }

        public void Redraw() =>
            _systems.ForEach(sys => sys.Redraw());

        // TODO Hot remove of systems
        public void Update(uint deltaTicks) =>
            _systems.ForEach(sys => sys.Update(deltaTicks));

        public void Add(SystemBase system) =>
            _systems.Add(system);

        public void Remove(SystemBase system) =>
            _systems.Remove(system);

        public void Clear() =>
            _systems.Clear();
    }
}