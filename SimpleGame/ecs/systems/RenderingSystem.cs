using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SimpleGame.Core;
using SimpleGame.ECS.Components;
using SimpleGame.Core.Resources;

namespace SimpleGame.ECS.Systems
{
    public class RenderingSystem : SystemBase
    {
        private List<TexturedRenderObject> _renderObjects = new List<TexturedRenderObject>();

        public RenderingSystem(Application owner) : base(owner)
        {
            // TODO dispose
            _renderObjects.Add(new TexturedRenderObject(PrimitivesFactory.CreateTexturedCube(0.2f, 256, 256), "../resources/dotted.png"));

            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Redraw()
        {
            /*var entities = OwnerApp.EntityManager
                .WithComponent<TransformComponent, TextureComponent>()
                .ToArray();

            //Primitives.DrawIsometricCube(OwnerApp.Window, Color.Red, new Point(600, 50), 30);


            foreach (int entityId in entities)
            {
                var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entityId);
                var texture = OwnerApp.EntityManager.GetComponent<TextureComponent>(entityId);

                //OwnerApp.ResourceManager.Get<Texture>(texture.TextureResourceName).Draw((int)transform.X, (int)transform.Y, 100, 86, 45);
            }
            */
            var _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60*((float) Math.PI/180f), // field of view angle, in radians
                800 / 600,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);   

            
            var shader = OwnerApp.ResourceManager.Get<Shader>("shaderDefault");

            var transform = OwnerApp.EntityManager.GetComponentsByType<TransformComponent>()[0] as TransformComponent;          
           
            foreach (var obj in _renderObjects)
            {
                obj.Render(shader, transform.Transform, _projectionMatrix);
            }
        }
    }
}