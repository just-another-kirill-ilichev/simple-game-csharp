using System.Linq;

using SimpleGame.Core;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS.Systems
{
    public class RenderingSystem : SystemBase
    {
        public RenderingSystem(SDLApplication owner) : base(owner) {}

        public override void Redraw()
        {
            var entities = OwnerApp.EntityManager.Entities
                .WithComponent<TransformComponent>(OwnerApp.EntityManager)
                .WithComponent<TextureComponent>(OwnerApp.EntityManager)
                .ToArray();

            foreach (int entityId in entities)
            {
                var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entityId);
                var texture = OwnerApp.EntityManager.GetComponent<TextureComponent>(entityId);

                OwnerApp.ResourceManager.Get<Texture>(texture.TextureResourceName).Draw((int)transform.X, (int)transform.Y);
            }
        }
    }
}