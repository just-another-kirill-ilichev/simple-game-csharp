using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SimpleGame.ECS;

namespace SimpleGame.Core
{
    public class Application : GameWindow
    {
        public ResourceManager ResourceManager { get; }
        public EntityManager EntityManager { get; }
        public SystemManager SystemManager { get; }
        public SceneLoader SceneLoader { get; }

        private Shader _shader;
        private int _vertexArray;
        private double _time;

        public Application() : base(800, 600,
            GraphicsMode.Default,
            "loading...",
            GameWindowFlags.FixedWindow,
            DisplayDevice.Default,
            3, 0,
            GraphicsContextFlags.ForwardCompatible)
        {

            GL.GetString(StringName.Version); // TODO log gl version

            ResourceManager = new ResourceManager();
            SystemManager = new SystemManager();
            EntityManager = new EntityManager();
            SceneLoader = new SceneLoader(this);

            Closing += (o, e) => { GL.DeleteVertexArrays(1, ref _vertexArray); };

            //SceneLoader.LoadScene("../resources/test.json");
        }

        protected override void OnLoad(EventArgs e)
        {
            _shader = new Shader(this, "../resources/default.frag", "../resources/default.vert");
            GL.GenVertexArrays(1, out _vertexArray);
            GL.BindVertexArray(_vertexArray);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _time += e.Time;
            Title = $"OpenGL version: {GL.GetString(StringName.Version)}; FPS: {(int)(1 / e.Time)}";

            var backgroud = new Color4(0, 0, 0, 0);
            GL.ClearColor(backgroud);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var position = new Vector4(MathF.Sin((float)_time) * 0.5f, MathF.Cos((float)_time) * 0.5f, 0.0f, 1.0f);
            GL.VertexAttrib4(1, position);

            _shader.Use();
            GL.VertexAttrib1(0, _time);

            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.PointSize(10);

            SystemManager.Redraw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            SystemManager.Update((uint)(e.Time * 1000));
        }
    }
}