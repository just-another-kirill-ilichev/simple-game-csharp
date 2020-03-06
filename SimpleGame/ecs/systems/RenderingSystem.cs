using System.Linq;
using System.Drawing;
using SimpleGame.Core;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS.Systems
{
    public class RenderingSystem : SystemBase
    {
        public RenderingSystem(Application owner) : base(owner) {}

        public override void Redraw()
        {
            var entities = OwnerApp.EntityManager
                .WithComponent<TransformComponent, TextureComponent>()
                .ToArray();

            //Primitives.DrawIsometricCube(OwnerApp.Window, Color.Red, new Point(600, 50), 30);


            foreach (int entityId in entities)
            {
                var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entityId);
                var texture = OwnerApp.EntityManager.GetComponent<TextureComponent>(entityId);

                //OwnerApp.ResourceManager.Get<Texture>(texture.TextureResourceName).Draw((int)transform.X, (int)transform.Y, 100, 86, 45);
            }
        }
    }
}