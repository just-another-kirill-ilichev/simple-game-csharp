using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SimpleGame.ECS;

namespace SimpleGame.Core
{
    public class Application : GameWindow
    {
        public ResourceManager ResourceManager { get; }
        public EntityManager EntityManager { get; }
        public SystemManager SystemManager { get; }
        public SceneLoader SceneLoader { get; }

        public Application() : base(800, 600,
            GraphicsMode.Default,
            "loading...",
            GameWindowFlags.FixedWindow,
            DisplayDevice.Default,
            3, 3,
            GraphicsContextFlags.Default)
        {
            GL.GetString(StringName.Version); // TODO log gl version

            ResourceManager = new ResourceManager();
            SystemManager = new SystemManager();
            EntityManager = new EntityManager();
            SceneLoader = new SceneLoader(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            SceneLoader.LoadScene("../resources/test.json");         
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"OpenGL version: {GL.GetString(StringName.Version)}; FPS: {(int)(1 / e.Time)}";

            var background = new Color4(0, 0, 0, 0);
            GL.ClearColor(background);

            SystemManager.Redraw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            SystemManager.Update((uint)(e.Time * 1000));
        }
    }
}