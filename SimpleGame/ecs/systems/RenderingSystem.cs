using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SimpleGame.Core;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS.Systems
{
    public class RenderingSystem : SystemBase
    {
        private string _shaderResourceRef;
        private Matrix4 _modelView;
        private List<Renderable> _renderObjects = new List<Renderable>();
        private double _time;

        public RenderingSystem(Application owner) : base(owner)
        {
            var shader = new Shader("../resources/default.frag", "../resources/default.vert");

            _shaderResourceRef = "shaderDefault";
            OwnerApp.ResourceManager.Add(_shaderResourceRef, shader);

            // TODO dispose
            _renderObjects.Add(new TexturedRenderObject(PrimitivesFactory.CreateTexturedCube(0.2f, 256, 256), shader, @"..\resources\dotted.png"));

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

              

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OwnerApp.ResourceManager.Get<Shader>(_shaderResourceRef).Use();

            GL.UniformMatrix4(20, false, ref _projectionMatrix);
            GL.UniformMatrix4(21, false, ref _modelView);
           
            foreach (var obj in _renderObjects)
            {
                obj.Render();
            }
        }

        public override void Update(uint deltaMs)
        {
            _time += deltaMs / 1000.0f;

            var k = (float)_time * 0.05f;
            var r1 = Matrix4.CreateRotationX(k * 13.0f);
            var r2 = Matrix4.CreateRotationY(k * 13.0f);
            var r3 = Matrix4.CreateRotationZ(k * 3.0f);

            var t1 = Matrix4.CreateTranslation(
                (float) (Math.Sin(k * 5f) * 0.5f),
                (float) (Math.Cos(k * 5f) * 0.5f),
                -1);

            _modelView = r1 * r2 * r3 * t1;
        }
    }
}