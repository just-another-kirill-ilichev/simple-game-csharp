using SimpleGame.Core;
using SimpleGame.ECS.Components.Actor;

namespace SimpleGame.ECS.Systems
{
    public class TerrainRenderingSystem : SystemBase
    {
        public TerrainRenderingSystem(Application owner) : base(owner)
        {

        }

        public override void Update(uint deltaTicks)
        {

        }

        public override void Redraw()
        {
            var entites = OwnerApp.EntityManager.WithComponent<StaticModel>();

            foreach (int entity in entites)
            {
                // TODO
            }
        }
    }
}