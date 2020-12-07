using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SimpleGame.Core;
using SimpleGame.ECS.Components;
using SimpleGame.Core.Resources;

namespace SimpleGame.ECS.Systems
{
    public class RenderingSystem : SystemBase
    {
        private Sprite _sprite;

        public RenderingSystem(Application owner) : base(owner)
        {
            _sprite = new Sprite("../resources/dotted.png"); // TODO

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.DepthTest);
        }

        private Matrix4 CreateProjectionViewMatrix()
        {
            var character = OwnerApp.EntityManager.WithComponent<ThirdPersonCameraComponent>().First();

            var characterTransform = OwnerApp.EntityManager.GetComponent<TransformComponent>(character);
            var camera = OwnerApp.EntityManager.GetComponent<ThirdPersonCameraComponent>(character);

            var characterPosition = new Vector3(characterTransform.X, characterTransform.Y, characterTransform.Z);
            var cameraOffset = new Vector3(camera.OffsetX, camera.OffsetY, camera.OffsetZ);

            var viewMatrix = Matrix4.LookAt(characterPosition + cameraOffset, characterPosition, Vector3.UnitY);

            var projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                camera.FieldOfView, camera.AspectRatio, camera.NearPlane, camera.FarPlane);

            var projectionView = viewMatrix * projectionMatrix; // TODO(?): Odd multiplication order

            return projectionView;
        }

        public override void Redraw()
        {
            var entities = OwnerApp.EntityManager.WithComponent<ModelComponent>();
            var projectionView = CreateProjectionViewMatrix();

            // DEBUG
            var _shader = OwnerApp.ResourceManager.Get<Shader>("shaderDefault");
            _sprite.Render(_shader, Matrix4.Identity, projectionView);
            // DEBUG

            foreach (var entity in entities)
            {
                var modelComponent = OwnerApp.EntityManager.GetComponent<ModelComponent>(entity);
                var transformComponent = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);

                var model = OwnerApp.ResourceManager.Get<BasicMaterialMesh>(modelComponent.ModelResourceRef);
                var shader = OwnerApp.ResourceManager.Get<Shader>(modelComponent.ShaderResourceRef);

                model.Render(shader, transformComponent.Transform, projectionView);
            }
        }
    }
}