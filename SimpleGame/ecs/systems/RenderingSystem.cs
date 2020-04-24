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
        private Model _cube;

        public RenderingSystem(Application owner) : base(owner)
        {
            _cube = OwnerApp.ResourceManager.Get<Model>("modelCube");//new Sprite("../resources/dotted.png");//
            

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.DepthTest);
        }

        private Matrix4 CreateProjectionViewMatrix()
        {
            var camera = OwnerApp.EntityManager.WithComponent<ThirdPersonCameraComponent>().First();

            var characterTransform = OwnerApp.EntityManager.GetComponent<TransformComponent>(camera);
            var cameraSettings = OwnerApp.EntityManager.GetComponent<ThirdPersonCameraComponent>(camera);

            var characterPosition = new Vector3(characterTransform.X, characterTransform.Y, characterTransform.Z);
            var cameraOffset = new Vector3(cameraSettings.OffsetX, cameraSettings.OffsetY, cameraSettings.OffsetZ);

            var viewMatrix = Matrix4.LookAt(characterPosition + cameraOffset, characterPosition, Vector3.UnitY);

            var projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                cameraSettings.FieldOfView, 800 / 600, 0.1f, 4000f);

            var projectionView = viewMatrix * projectionMatrix; // TODO?: Weird multiplication order

            return projectionView;
        }

        public override void Redraw()
        {
            var shader = OwnerApp.ResourceManager.Get<Shader>("shaderModelTest");
            var entities = OwnerApp.EntityManager.WithComponent<SimpleCubeComponent>();
            
            var projectionView = CreateProjectionViewMatrix();

            foreach (var entity in entities)
            {
                var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);

                _cube.Render(shader, transform.Transform, projectionView);
            }
        }
    }
}